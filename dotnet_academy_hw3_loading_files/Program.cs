

using dotnet_academy_hw3_loading_files.input_data;

namespace dotnet_academy_hw3_loading_files
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Volvo .Net Academy Homework #3: Loading Files Asynchronously");
            //  TODO change code to use async programming
            var projectDir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent;
            var booksDir = String.Concat(projectDir.FullName, @"\input_data");
            var outputDir = String.Concat(projectDir.FullName, @"\output_data");
            var path = String.Concat(projectDir.FullName, @"\input_data\pg100.txt");

            var bookLoader = new BookLoader();
            var bookSaver = new BookFileSaver();
            var books = new List<BookInfo>();

            //  Get names of files in directory
            var files = Directory.GetFiles(booksDir);
            files = files.Where(f => f.EndsWith(".txt")).ToArray();
            if (files.Length > 0) 
            {
                foreach (var file in files)
                {
                    file.LastIndexOf(@"\");
                    var fileName = file.Substring(file.LastIndexOf(@"\")+1);
                    Console.WriteLine($"Working with file: {fileName}...");

                    var book = bookLoader.LoadBook(file);
                    
                    if (book != null) 
                    { 
                        books.Add(book);
                        Console.WriteLine($"Saving book: \"{book.Title}\" from file {fileName}...");
                        bookSaver.SaveBook(book, outputDir);
                        Console.WriteLine($"##### FILE {fileName} PROCESSED #####");
                    }
                }
                Console.WriteLine($"All {books.Count} valid files processed");
            }
            else
            {
                Console.WriteLine("Directory empty");
            }
        }
    }
}