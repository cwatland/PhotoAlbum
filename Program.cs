using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;

namespace PhotoAlbum
{
    public class Photo
    {
        public Int32 AlbumId { get; set; }
        public Int32 Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string ThumbnailUrl { get; set; }

    }

    class Program
    {
        private static readonly HttpClient client = new HttpClient();

        static void Main(string[] args)
        {
            var valid = false;

            do
            {
                while (!valid) //Will run until a valid int is entered.
                {
                    Console.Write(@"Please enter an Album ID: ");

                    string input = Console.ReadLine();
                    int id;

                    if (int.TryParse(input, out id))
                    {
                        valid = true;
                        GetPhotosByAlbumId(id);
                    }
                    else
                    {
                        Console.WriteLine("\"" + input + "\" is not a valid number...\n");
                    }
                }
            }
            while (SearchAgain()); //Runs again if user types "Y"
            
        }

        static void GetPhotosByAlbumId(int id)
        {
            Console.WriteLine("> photo-album " + id);

            UriBuilder builder = new UriBuilder("https://jsonplaceholder.typicode.com/photos");
            builder.Query = "albumId=" + id;

            var result = client.GetAsync(builder.Uri).Result;

            using StreamReader sr = new StreamReader(result.Content.ReadAsStreamAsync().Result);
            string response = sr.ReadToEnd();
            List<Photo> photos = JsonConvert.DeserializeObject<List<Photo>>(response);

            foreach (var photo in photos)
            {
                Console.WriteLine("[" + photo.Id + "] " + photo.Title);
            }
        }

        static public bool SearchAgain()
        {
            //If user inputs "Y" then search again, otherwise close application
            while (true)
            {
                Console.Write("\nDo you want to search again [Y/n]?");
                string answer = Console.ReadLine().ToUpper();

                return answer == "Y" ? true : false; //used ternary operator here because I think it looks cleaner.
            }
        }
    }
}
