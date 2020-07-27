using System.Net.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

public class PhotoService : IPhotoService
{
    public HttpClient Client { get; }

    public PhotoService(HttpClient client)
    {
        Client = client;
    }
    public async Task<List<IPhoto>> GetAlbumByIdAsync(int albumId)
    {
        List<IPhoto> photos = new List<IPhoto>();
        string requestUrl = $"https://jsonplaceholder.typicode.com/photos?albumId={albumId}";
        try
        {
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.GetAsync(requestUrl))
                {
                    using (HttpContent content = res.Content)
                    {
                        string data = await content.ReadAsStringAsync();
                        if (data != null)
                        {
                            var elements = JArray.Parse(data);

                            foreach (var element in elements)
                            {
                                IPhoto photo = new Photo(albumId: int.Parse($"{element["albumId"]}"),
                                                          id: int.Parse($"{element["id"]}"),
                                                          title: $"{element["title"]}",
                                                          url: $"{element["url"]}",
                                                          thumbnailUrl: $"{element["thumbnailUrl"]}");
                                photos.Add(photo);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Data is null!");
                        }
                    }
                }
            }
        }
        catch (HttpRequestException exception)
        {
            Console.WriteLine(exception);
        }
        catch (TaskCanceledException exception)
        {
            Console.WriteLine(exception);
        }
        return photos;
    }
}