namespace dotnet_academy_hw3_loading_files
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            var dir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent;
            var path = String.Concat(dir.FullName, @"\input_data");
            var bookInfo = new BookInfo();

            try
            {
                if (!Directory.Exists(path)) 
                {
                    throw new DirectoryNotFoundException();
                }
                var fullPath = String.Concat(path, @"\", @"pg100.txt");
                using (StreamReader sr = new StreamReader(fullPath))
                {
                    string line;
                    string[] words;
                    while ((line = sr.ReadLine()) != null)
                    {
                        var x = line.Select(x => x).Where(x => x.Equals(' ')).ToList();
                        Console.WriteLine(line);
                        words = line.Split(" ");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }
    }
}