using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Retry
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var htmlFromGoogle = await Retryer.RetryAsync(
                () => FetchHtmlAsync("http://www.google.com"),
                TimeSpan.FromSeconds(2),
                3);

            Console.WriteLine("Html fetched from google");
            Console.WriteLine(htmlFromGoogle);

            Console.WriteLine();
            Console.WriteLine("##########################");
            Console.WriteLine();

            var htmlFromIncorrectWebsite = await Retryer.RetryAsync(
                () => FetchHtmlAsync("http://www.bla23.com/"),
                TimeSpan.FromSeconds(2),
                3); // It must fails

            Console.WriteLine(htmlFromIncorrectWebsite);
        }

        public static async Task<string> FetchHtmlAsync(string address)
        {
            Console.WriteLine("Start fetching");

            await Task.Delay(1000); //simulating delay
            using var client = new HttpClient();
            var result = await client.GetAsync(new Uri(address));
            result.EnsureSuccessStatusCode();
            return await result.Content.ReadAsStringAsync();
        }
    }
}
