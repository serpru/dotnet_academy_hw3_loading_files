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
                    string paragraph = string.Empty;
                    bool startFlag = false;
                    bool endFlag = false;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (endFlag) { 
                            break; }
                        if (line.Contains("*** END OF THE PROJECT")) 
                        { 
                            endFlag = true; }
                        if (!startFlag)
                        {
                            if (line.Contains("*** START OF THE PROJECT")) { startFlag = true; }
                            continue;
                        }
                        if (string.IsNullOrEmpty(line)) { continue; }
                        paragraph = string.Concat(paragraph, string.Concat(line, "\n"));
                        if (char.Equals(line.ElementAt(line.Length - 1), '.'))
                        {
                            //  Line ends with a dot indicating end of sentence/paragraph
                            bookInfo.LoadParagraph(paragraph);
                            paragraph = string.Empty;
                        }
                    }
                }
                //
                var longestSentences = bookInfo.LongestSentencesByChars(10).ToList();
                var shortestSentences = bookInfo.ShortestSentencesByWords(10).ToList();
                var longestWords = bookInfo.LongestWords(10);
                var mostCommonLetters = bookInfo.MostCommonLetters(10);
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }
    }
}