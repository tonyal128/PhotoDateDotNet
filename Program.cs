using System.Net.Http;
using System;
using System.Linq;

namespace Photo_Album
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpClient client = new HttpClient();
            IPhotoService photoService = new PhotoService(client);

            Console.WriteLine("Which album would you like to retrieve? ");
            int answer;
            string response = Console.ReadLine();
            bool parseResult = int.TryParse(response, out answer);

            if (parseResult)
            {
                var results = photoService.GetAlbumByIdAsync(answer);
                if (results.Result.Any())
                {
                    foreach (var r in results.Result)
                    {
                        Console.WriteLine($"[{r.Id}] {r.Title}");
                    }
                }
                else
                {
                    Console.WriteLine($"No results found for album id {answer}");
                }
            }
            else
            {
                Console.WriteLine($"You entered: {response}. Please enter a number.");
            }
        }
    }
}
