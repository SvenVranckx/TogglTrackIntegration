﻿using Toggl.Track.SDK;
using Toggl.Track.SDK.Queries;

namespace Toggl.Track.Interactive
{
    public class Session : IDisposable
    {
        private readonly ITerminal _terminal;
        private readonly ApiContext _context;
        private bool _disposed;

        public Session(string apiToken)
        {
            _terminal = new Terminal();
            _context = new ApiContext(apiToken);
        }

        public void Dispose()
        {
            if (_disposed)
                return;
            _disposed = true;
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Run()
        {
            while (true)
            {
                using (Terminal.ForegroundColor.White)
                    _terminal.WriteLine(@"Please select a command");
                _terminal.WriteLine();
                _terminal.PrintOption(ConsoleKey.T, @"Export time entries");
                _terminal.PrintOption(ConsoleKey.D, @"Show daily totals");
                _terminal.PrintOption(ConsoleKey.M, @"Show monthly totals");
                _terminal.PrintOption(ConsoleKey.X, @"Exit");

                var key = _terminal.ReadKey(true);
                _terminal.WriteLine();

                try
                {

                    switch (key.Key)
                    {
                        case ConsoleKey.Escape:
                        case ConsoleKey.X:
                            _terminal.Clear();
                            return;
                        case ConsoleKey.T:
                            await ExportTimeEntries();
                            break;
                        case ConsoleKey.D:
                            await DisplayDailyTotals();
                            break;
                        case ConsoleKey.M:
                            await DisplayMonthlyTotal();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    using (Terminal.ForegroundColor.Red)
                    {
                        _terminal.WriteLine(ex.Message);
                        _terminal.WriteLine(ex.StackTrace ?? string.Empty);
                    }
                }
            }
        }        

        private async Task<SDK.Models.Client?> SelectClient()
        {
            _terminal.WriteLine("Fetching clients...");
            var clients = await _context.Clients.Collect();
            if (clients.Length == 0)
                return null;

            var client = _terminal.SelectOptionWithPaging("Please select a client", clients, clients.First(), c => c.Name, 10);
            return client;
        }

        private enum Period { LastMonth, ThisMonth, LastWeek, ThisWeek, Custom };

        private Period? SelectPeriod()
        {
            var periods = new Period?[] { Period.LastMonth, Period.ThisMonth, Period.LastWeek, Period.ThisWeek, Period.Custom };
            var period = _terminal.SelectOption("Please select a period", periods, Period.LastMonth, p => p?.ToString()
                .Replace("Month", " month")
                .Replace("Week", " week"));
            return period;
        }

        private TimeEntryQuery? GetCustomPeriodQuery()
        {
            _terminal.WriteLine();
            var start = _terminal.SelectDate("Enter start date: ");
            if (start is null)
                return null;
            var end = _terminal.SelectDate("Enter end date: ");
            if (end is null)
                return null;
            return TimeEntryQuery.Between(start.Value, end.Value);
        }

        private TimeEntryQuery? GetTimeEntryQuery(Period period)
        {
            return period switch
            {
                Period.LastMonth => TimeEntryQuery.LastMonth,
                Period.ThisMonth => TimeEntryQuery.ThisMonth,
                Period.LastWeek => TimeEntryQuery.LastWeek,
                Period.ThisWeek => TimeEntryQuery.ThisWeek,
                Period.Custom => GetCustomPeriodQuery(),
                _ => throw new InvalidOperationException(),
            };
        }

        private async Task ExportTimeEntries()
        {
            var client = await SelectClient();
            if (client is null)
                return;

            _terminal.WriteLine();
            var period = SelectPeriod();
            if (period is null)
                return;
            TimeEntryQuery? query = GetTimeEntryQuery(period.Value);
            if (query is null)
                return;

            _terminal.WriteLine();
            var separators = new[] { ",", ";" };
            var separator = _terminal.SelectOption("Please select a separator", separators, ",", s => s);
            if (separator is null)
                return;

            _terminal.WriteLine();
            _terminal.WriteLine($"Please select an output file for {client} ({query})");
            var path = FileDialog.ShowSave();
            if (string.IsNullOrEmpty(path))
                return;

            _terminal.WriteLine();
            _terminal.WriteLine($"Fetching {client.Name} projects...");
            var projects = (await _context.Projects.Collect(ProjectQuery.ByClient(client))).ToDictionary(p => p.Id);
            _terminal.WriteLine($"Fetching {client.Name} time entries ({query})...");
            var entries = await _context.TimeEntries.Collect(query);
            var matching = entries
                .Where(e => e.Stop is not null)
                .Where(e => projects.ContainsKey(e.ProjectId ?? 0))
                .OrderBy(e => e.Start)
                .ToArray();

            using var output = new StreamWriter(path);
            var header = string.Join(separator, ["Date", "Project", "Description", "Type", "Duration (m)", "Duration (h)", "Start", "Stop"]);
            _terminal.WriteLine();
            _terminal.WriteLine(header);
            output.WriteLine(header);
            foreach (var entry in matching)
            {
                projects.TryGetValue(entry.ProjectId ?? 0, out var project);
                var duration = TimeSpan.FromSeconds(entry.Duration);
                var type = string.Join(" | ", entry.Tags ?? []);
                var row = string.Join(separator,
                    entry.Start?.Date.ToString("dd/MM/yyy"),
                    project?.Name,
                    entry.Description,
                    type,
                    duration.TotalMinutes,
                    Math.Round(duration.TotalHours, 4),
                    entry.Start?.TimeOfDay,
                    entry.Stop?.TimeOfDay);
                _terminal.WriteLine(row);
                output.WriteLine(row);
            }
            _terminal.WriteLine();
        }

        private async Task DisplayMonthlyTotal()
        {
            var client = await SelectClient();
            if (client is null)
                return;

            _terminal.WriteLine();
            var period = SelectPeriod();
            if (period is null)
                return;
            TimeEntryQuery? query = GetTimeEntryQuery(period.Value);
            if (query is null)
                return;

            _terminal.WriteLine();
            _terminal.WriteLine($"Fetching {client.Name} projects...");
            var projects = (await _context.Projects.Collect(ProjectQuery.ByClient(client))).Select(p => p.Id).ToHashSet();
            _terminal.WriteLine($"Fetching {client.Name} time entries... ({query})");
            var entries = await _context.TimeEntries.Collect(query);
            var totalSeconds = entries
                .Where(e => e.Stop is not null)
                .Where(e => projects.Contains(e.ProjectId ?? 0))
                .Select(e => e.Duration)
                .Sum();
            var total = TimeSpan.FromSeconds(totalSeconds);

            _terminal.WriteLine();
            _terminal.WriteLine($"Total time written: {total.TotalHours} hours | {total.TotalMinutes} minutes");
            _terminal.WriteLine();
        }

        private async Task DisplayDailyTotals()
        {
            var client = await SelectClient();
            if (client is null)
                return;

            _terminal.WriteLine();
            var period = SelectPeriod();
            if (period is null)
                return;
            TimeEntryQuery? query = GetTimeEntryQuery(period.Value);
            if (query is null)
                return;

            _terminal.WriteLine();
            _terminal.WriteLine($"Fetching {client.Name} projects...");
            var projects = (await _context.Projects.Collect(ProjectQuery.ByClient(client))).Select(p => p.Id).ToHashSet();
            _terminal.WriteLine($"Fetching {client.Name} time entries... ({query})");
            var entries = await _context.TimeEntries.Collect(query);
            var grouped = entries
                .Where(e => e.Stop is not null)
                .Where(e => projects.Contains(e.ProjectId ?? 0))
                .GroupBy(e => e.Start?.Date ?? DateTime.MinValue)
                .OrderBy(g => g.Key);            

            _terminal.WriteLine();
            int monthlySeconds = 0;
            foreach (var group in grouped)
            {
                var date = group.Key;
                var totalSeconds = group.Select(e => e.Duration).Sum();
                var total = TimeSpan.FromSeconds(totalSeconds);
                _terminal.WriteLine($"Time written on {date.ToShortDateString(),10}: {total.TotalHours} hours | {total.TotalMinutes} minutes ({8.0 - total.TotalHours} hours absence)");
                monthlySeconds += totalSeconds;
            }
            
            _terminal.WriteLine();
            var monthlyTotal = TimeSpan.FromSeconds(monthlySeconds);
            _terminal.WriteLine($"Total time written: {monthlyTotal.TotalHours} hours | {monthlyTotal.TotalMinutes} minutes");
            _terminal.WriteLine();
        }
    }
}
