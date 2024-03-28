using Toggle.Track.SDK;

namespace Toggle.Track
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Enter API token:");
            var token = Console.ReadLine();

            using var context = new Context(token!);

            var organizations = await context.Organizations.Get();
            Console.Write(organizations.Length);

            var workspaces = await context.Workspaces.Get();
            Console.Write(workspaces.Length);

            var entries = await context.TimeEntries.Get();
            Console.Write(entries.Length);
        }
    }
}
