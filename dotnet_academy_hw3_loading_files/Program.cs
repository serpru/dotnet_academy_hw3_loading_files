

using dotnet_academy_hw3_loading_files.input_data;
using static System.Reflection.Metadata.BlobBuilder;

namespace dotnet_academy_hw3_loading_files
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //  Change this boolean to switch between using async or not
            var runAsync = true;
            Console.WriteLine("Volvo .Net Academy Homework #3: Loading Files Asynchronously");
            var projectDir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent;
            var booksDir = String.Concat(projectDir.FullName, @"\input_data");
            var outputDir = String.Concat(projectDir.FullName, @"\output_data");
            var path = String.Concat(projectDir.FullName, @"\input_data\pg100.txt");

            var bookLoadTasks = new List<Task>();


            //  Get names of files in directory
            var files = Directory.GetFiles(booksDir);
            files = files.Where(f => f.EndsWith(".txt")).ToArray();
            if (files.Length > 0) 
            {
                if (runAsync)
                {
                    var watch = System.Diagnostics.Stopwatch.StartNew();
                    foreach (var file in files)
                    {
                        bookLoadTasks.Add(LoadFileAsync(file, outputDir));
                    }
                    await Task.WhenAll(bookLoadTasks);
                    watch.Stop();
                    Console.WriteLine($"All {files.Length} processed\nElapsed time(ms): {watch.ElapsedMilliseconds}");
                }
                else
                {
                    var watch = System.Diagnostics.Stopwatch.StartNew();
                    foreach (var file in files)
                    {
                        LoadFile(file, outputDir);
                    }
                    watch.Stop();
                    Console.WriteLine($"All {files.Length} processed\nElapsed time(ms): {watch.ElapsedMilliseconds}");
                }
            }
            else
            {
                Console.WriteLine("Directory empty");
            }
        }
        private static void LoadFile(string file, string outputDir)
        {
            var fileName = file.Substring(file.LastIndexOf(@"\") + 1);
            Console.WriteLine($"Loading file: {fileName}...");

            var book = BookLoader.LoadBook(file);

            if (book != null)
            {
                Console.WriteLine($"Saving book: \"{book.Title}\" from file {fileName}...");
                BookFileSaver.SaveBook(book, outputDir);
                Console.WriteLine($"##### FILE {fileName} PROCESSED #####");
            }
        }
        private static async Task<BookInfo?> LoadFileAsync(string file, string outputDir)
        {
            var fileName = file.Substring(file.LastIndexOf(@"\") + 1);
            Console.WriteLine($"Loading file: {fileName}...");

            var book = await BookLoader.LoadBookAsync(file);
            Console.WriteLine($"File {fileName} loaded");


            if (book != null)
            {
                Console.WriteLine($"Saving book: \"{book.Title}\" from file {fileName}...");
                await BookFileSaver.SaveBookAsync(book, outputDir);
                Console.WriteLine($"##### FILE {fileName} PROCESSED #####");
            }
            return await Task.FromResult(book);
        }
    }
}