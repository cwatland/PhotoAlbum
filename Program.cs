using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;

namespace PhotoAlbum
{
    public class Photo
    {
        public int AlbumId { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string ThumbnailUrl { get; set; }

    }

    class Program
    {
        //private static readonly HttpClient client = new HttpClient();

        static void Main(string[] args)
        {
            var photoService = new PhotoService();

            do
            {
                bool valid = false;

                while (!valid) //Will run until a valid int is entered.
                {
                    Console.Write("\nPlease enter an Album ID: ");

                    string input = Console.ReadLine();

                    if (int.TryParse(input, out int id))
                    {
                        valid = true;
                        var photos = photoService.GetPhotosByAlbumId(id);

                        if (photos.Count > 0)
                        {
                            foreach (var photo in photos)
                            {
                                Console.WriteLine($"[{photo.Id}] {photo.Title}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("\nNo photos were found having an ID of \"" + id + "\"...\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("\"" + input + "\" is not a valid number...\n");
                    }
                }
            }
            while (SearchAgain()); //Runs again if user types "Y"
            
        }

        //static void GetPhotosByAlbumId(int id)
        //{

        //    Console.WriteLine("\n> photo-album " + id);

        //    UriBuilder builder = new UriBuilder("https://jsonplaceholder.typicode.com/photos")
        //    {
        //        Query = "albumId=" + id
        //    };

        //    var result = client.GetAsync(builder.Uri).Result;

        //    using StreamReader sr = new StreamReader(result.Content.ReadAsStreamAsync().Result);
        //    string response = sr.ReadToEnd();
        //    List<Photo> photos = JsonConvert.DeserializeObject<List<Photo>>(response);

        //}

        static public bool SearchAgain()
        {
            Console.Write("\nDo you want to search again [Y/n]?");
            string answer = Console.ReadLine().ToUpper();

            return answer == "Y";
        }
    }
}
