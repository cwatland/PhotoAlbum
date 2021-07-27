using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;

namespace PhotoAlbum
{
    public interface IPhotoService
    {
        List<Photo> GetPhotosByAlbumId(int id);
    }

    public class PhotoService : IPhotoService
    {
        private static readonly HttpClient client = new HttpClient();

        public List<Photo> GetPhotosByAlbumId(int id)
        {
            Console.WriteLine("\n> photo-album " + id);

            UriBuilder builder = new UriBuilder("https://jsonplaceholder.typicode.com/photos")
            {
                Query = "albumId=" + id
            };

            var result = client.GetAsync(builder.Uri).Result;

            using StreamReader sr = new StreamReader(result.Content.ReadAsStreamAsync().Result);
            string response = sr.ReadToEnd();
            return JsonConvert.DeserializeObject<List<Photo>>(response);
        }
    }
}
