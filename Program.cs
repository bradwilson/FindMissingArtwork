using System;
using System.IO;

namespace FindMissingArtwork
{
    public class Program
    {
        static string basePath;

        public static void Main()
        {
            basePath = Directory.GetCurrentDirectory();
            SearchPath(basePath);
        }

        private static void SearchFiles(string path, string pattern)
        {
            foreach (var file in Directory.GetFiles(path, pattern))
            {
                try
                {
                    using (var tagFile = TagLib.File.Create(file))
                        if (tagFile.Tag.Pictures.Length == 0)
                            Console.WriteLine(file.Substring(basePath.Length + 1));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error processing {0}:", file);
                    Console.WriteLine(ex.ToString());
                }
            }

        }

        private static void SearchPath(string path)
        {
            SearchFiles(path, "*.mp3");
            SearchFiles(path, "*.m4a");

            foreach (var subPath in Directory.GetDirectories(path))
                SearchPath(subPath);
        }
    }
}