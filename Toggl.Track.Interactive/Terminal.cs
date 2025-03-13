namespace Toggl.Track.Interactive
{
    public interface ITerminal
    {
        void Clear();
        void Write(string text);
        void WriteLine(string text);
        void WriteLine();
        string? ReadLine();
        ConsoleKeyInfo ReadKey(bool intercept);
        void PrintKey(ConsoleKey key);
        void PrintKeys(params ConsoleKey[] keys);
        void PrintOption(ConsoleKey key, string description, bool highlight = false);
        T SelectOption<T>(string caption, IEnumerable<T> options, T current, Func<T, string?> labelOf);
        T SelectOptionWithPaging<T>(string caption, IReadOnlyCollection<T> options, T current, Func<T, string?> labelOf, int itemsPerPage);
        DateTimeOffset? SelectDate(string prompt);
    }

    public class Option
    {
        public ConsoleKey Key { get; set; }
        public string? Label { get; set; }
    }


    public class Terminal : ITerminal
    {
        public void Clear() => Console.Clear();
        public void Write(string text) => Console.WriteLine(text);
        public void WriteLine(string text) => Console.WriteLine(text);
        public void WriteLine() => Console.WriteLine();
        public string? ReadLine() => Console.ReadLine();
        public ConsoleKeyInfo ReadKey(bool intercept) => Console.ReadKey(intercept);
        public void PrintKey(ConsoleKey key) => Console.Write($"[{key}]");

        public void PrintKeys(params ConsoleKey[] keys)
        {
            Console.Write('[');
            var index = 0;
            foreach (ConsoleKey key in keys)
            {
                if (index++ > 0)
                    Console.Write('|');

                using (ForegroundColor.White)
                    Console.Write(key);
            }
            Console.Write(']');
        }

        public void PrintOption(ConsoleKey key, string? description, bool highlight = false)
        {
            using (highlight ? ForegroundColor.Yellow : null)
            {
                PrintKey(key);
                Console.WriteLine($@" {description}");
            }
        }

        public T SelectOptionWithPaging<T>(string caption, IReadOnlyCollection<T> options, T current, Func<T, string?> labelOf, int itemsPerPage)
        {
            int page = 0;
            if (itemsPerPage > 26)
                itemsPerPage = 26;

            while (true)
            {
                using (ForegroundColor.White)
                    WriteLine(caption);
                WriteLine();

                var option = ConsoleKey.A;
                var mapping = new Dictionary<ConsoleKey, T>();
                int count = 0;
                int first = page * itemsPerPage;
                foreach (T p in options.Skip(page * itemsPerPage).Take(itemsPerPage))
                {
                    mapping[option] = p;
                    PrintOption(option, labelOf(p), p.Equals(current));
                    option++;
                    count++;
                }

                if (options.Count > itemsPerPage)
                {
                    Write($"\n{first + 1} to {first + count} of {options.Count} ");
                    if (first > 0 && first + count < options.Count)
                        PrintKeys(ConsoleKey.PageUp, ConsoleKey.PageDown);
                    else if (first > 0)
                        PrintKeys(ConsoleKey.PageUp);
                    else if (first + count < options.Count)
                        PrintKeys(ConsoleKey.PageDown);
                    WriteLine();
                }

                bool exit = false;
                while (!exit)
                {
                    ConsoleKeyInfo key = ReadKey(true);
                    switch (key.Key)
                    {
                        case ConsoleKey.Escape:
                        case ConsoleKey.Enter:
                            if (current == null)
                                break;
                            else
                                return current;

                        case ConsoleKey.PageUp:
                            if (page > 0)
                            {
                                page--;
                                exit = true;
                                Clear();
                            }

                            break;

                        case ConsoleKey.PageDown:
                            if (first + count < options.Count)
                            {
                                page++;
                                exit = true;
                                Clear();
                            }

                            break;

                        default:
                            if (mapping.TryGetValue(key.Key, out T? result))
                                return result;
                            break;
                    }
                }
            }
        }

        public T SelectOption<T>(string caption, IEnumerable<T> options, T current, Func<T, string?> labelOf)
        {
            using (ForegroundColor.White)
                WriteLine(caption);
            WriteLine();

            var option = ConsoleKey.A;
            var mapping = new Dictionary<ConsoleKey, T>();
            foreach (T p in options.Take(26))
            {
                mapping[option] = p;
                PrintOption(option, labelOf(p), p?.Equals(current) == true);
                option++;
            }

            while (true)
            {
                ConsoleKeyInfo key = ReadKey(true);
                if (current != null && (key.Key == ConsoleKey.Escape || key.Key == ConsoleKey.Enter))
                    return current;

                if (mapping.TryGetValue(key.Key, out T? result))
                    return result;
            }
        }

        public ConsoleKey ReadOption(IReadOnlyCollection<Option> options, ConsoleKey current)
        {
            var keys = new HashSet<ConsoleKey>(options.Select(option => option.Key));
            foreach (Option p in options.Take(26))
                PrintOption(p.Key, p.Label, p.Key == current);

            while (true)
            {
                ConsoleKeyInfo key = ReadKey(true);
                if (key.Key == ConsoleKey.Escape)
                    return ConsoleKey.Escape;
                if (key.Key == ConsoleKey.Enter)
                    return current;
                if (keys.Contains(key.Key))
                    return key.Key;
            }
        }

        public DateTimeOffset? SelectDate(string prompt)
        {
            using (ForegroundColor.White)
                Console.Write(prompt);
            var input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
                return null;
            if (!DateTime.TryParse(input, out var result))
                return null;
            return result;
        }

        /// <summary>
        /// Change foreground color of the console.
        /// </summary>
        public class ForegroundColor : IDisposable
        {
            private readonly ConsoleColor _previous;
            private bool _disposed;

            private ForegroundColor(ConsoleColor color)
            {
                _previous = Console.ForegroundColor;
                Console.ForegroundColor = color;
            }

            /// <inheritdoc />
            public void Dispose()
            {
                if (_disposed)
                    return;
                _disposed = true;
                Console.ForegroundColor = _previous;
                GC.SuppressFinalize(this);
            }

            public static IDisposable Black => new ForegroundColor(ConsoleColor.Black);
            public static IDisposable White => new ForegroundColor(ConsoleColor.White);
            public static IDisposable Gray => new ForegroundColor(ConsoleColor.Gray);
            public static IDisposable Green => new ForegroundColor(ConsoleColor.Green);
            public static IDisposable Yellow => new ForegroundColor(ConsoleColor.Yellow);
            public static IDisposable Red => new ForegroundColor(ConsoleColor.Red);
            public static IDisposable Blue => new ForegroundColor(ConsoleColor.Blue);
        }

        /// <summary>
        /// Change background color of the console
        /// </summary>
        public class BackgroundColor : IDisposable
        {
            private readonly ConsoleColor _previous;
            private bool _disposed;

            private BackgroundColor(ConsoleColor color)
            {
                _previous = Console.BackgroundColor;
                Console.BackgroundColor = color;
            }

            /// <inheritdoc />
            public void Dispose()
            {
                if (_disposed)
                    return;
                _disposed = true;
                Console.BackgroundColor = _previous;
                GC.SuppressFinalize(this);
            }

            public static IDisposable Black => new BackgroundColor(ConsoleColor.Black);
            public static IDisposable White => new BackgroundColor(ConsoleColor.White);
            public static IDisposable Gray => new BackgroundColor(ConsoleColor.Gray);
            public static IDisposable Green => new BackgroundColor(ConsoleColor.Green);
            public static IDisposable Yellow => new BackgroundColor(ConsoleColor.Yellow);
            public static IDisposable Red => new BackgroundColor(ConsoleColor.Red);
            public static IDisposable Blue => new BackgroundColor(ConsoleColor.Blue);
        }
    }
}
