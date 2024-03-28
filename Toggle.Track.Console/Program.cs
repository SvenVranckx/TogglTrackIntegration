using Toggle.Track.SDK;

namespace Toggle.Track
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Enter API token:");
            var token = Console.ReadLine();

            using var client = new Client(token!);
            var response = await client.GetString("/api/v9/me/time_entries");

            Console.WriteLine(response);
        }
    }
}
