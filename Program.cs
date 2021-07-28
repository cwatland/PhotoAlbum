using System;
using System.Collections.Generic;

namespace PhotoAlbum
{
    class Program
    {
        static void Main(string[] args)
        {
            IPhotoService photoService = new PhotoService();

            do
            {
                bool valid = false;

                while (!valid)
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
                            Console.WriteLine($"\nNo photos were found having an ID of '{id}'...");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"'{input}' is not a valid number...\n");
                    }
                }
            }
            while (SearchAgain());
            
        }

        static public bool SearchAgain()
        {
            Console.Write("\nDo you want to search again [Y/n]?");
            string answer = Console.ReadLine().ToUpper();

            return answer == "Y";
        }
    }
}
