using Toggl.Track.SDK;
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
        }

        public async Task Run()
        {
            while (true)
            {
                using (Terminal.ForegroundColor.White)
                    _terminal.WriteLine(@"Please select a command");
                _terminal.WriteLine();
                _terminal.PrintOption(ConsoleKey.T, @"Export time entries (last month)");
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

        private async Task ExportTimeEntries()
        {
            var clients = await _context.Clients.Collect();
            if (clients.Length == 0)
                return;

            var client = _terminal.SelectOptionWithPaging("Please select a client", clients, clients.First(), c => c.Name, 10);
            if (client is null)
                return;

            _terminal.WriteLine();
            var separators = new[] { ",", ";" };
            var separator = _terminal.SelectOption("Please select a separator", separators, ",", s => s);
            if (separator is null)
                return;

            var projects = (await _context.Projects.Collect(ProjectQuery.ByClient(client))).ToDictionary(p => p.Id);
            var entries = await _context.TimeEntries.Collect(TimeEntryQuery.LastMonth);
            var matching = entries
                .Where(e => projects.ContainsKey(e.ProjectId ?? 0))
                .OrderBy(e => e.Start)
                .ToArray();

            const string path = @"C:\Users\Sven\OneDrive\Desktop\TimeEntries.csv";

            using var output = new StreamWriter(path);
            var header = string.Join(separator, ["Date", "Project", "Description", "Type", "Duration", "Start", "Stop"]);
            _terminal.WriteLine();
            _terminal.WriteLine(header);
            output.WriteLine(header);
            foreach (var entry in matching)
            {
                projects.TryGetValue(entry.ProjectId ?? 0, out var project);
                var type = string.Join(" | ", entry.Tags ?? []);
                var row = string.Join(separator,
                    entry.Start?.Date.ToString("dd/MM/yyy"),
                    project?.Name,
                    entry.Description,
                    type,
                    TimeSpan.FromSeconds(entry.Duration).TotalMinutes,
                    entry.Start?.TimeOfDay,
                    entry.Stop?.TimeOfDay);
                _terminal.WriteLine(row);
                output.WriteLine(row);
            }
            _terminal.WriteLine();
        }
    }
}
