using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet_academy_hw3_loading_files.input_data
{
    static internal class BookFileSaver
    {
        public static void SaveBook(BookInfo book, string directory)
        {
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(directory,String.Concat(ToFileName(book.Title), "_", book.OriginalFileName, ".txt"))))
            {
                if (!Directory.Exists(directory)) 
                { 
                    Directory.CreateDirectory(directory);
                }
                outputFile.WriteLine("Longest sentences by number of characters:");
                var longestSentences = book.LongestSentencesByChars(10).ToList();
                foreach (var sentence in longestSentences) 
                {
                    PrintMessage(book.OriginalFileName);
                    outputFile.WriteLine(String.Concat(sentence, "\n")); 
                }

                var shortestSentences = book.ShortestSentencesByWords(10).ToList();
                outputFile.WriteLine("\nShortest sentences by number of words:");
                foreach (var sentence in shortestSentences) 
                {
                    PrintMessage(book.OriginalFileName);
                    outputFile.WriteLine(sentence); 
                }

                var longestWords = book.LongestWords(10);
                outputFile.WriteLine("\nLongest words:");
                foreach (var word in longestWords) 
                {
                    PrintMessage(book.OriginalFileName);
                    outputFile.WriteLine(word); 
                }

                var mostCommonLetters = book.MostCommonLetters(10);
                outputFile.WriteLine("\nMost common letters:");
                foreach (var letter in mostCommonLetters) 
                {
                    PrintMessage(book.OriginalFileName);
                    outputFile.WriteLine($"{letter.Key} - {letter.Value}"); 
                }

                var mostCommonWords = book.WordsByUseDesc(10);
                outputFile.WriteLine("\nMost common words:");
                foreach (var word in mostCommonWords)
                {
                    PrintMessage(book.OriginalFileName);
                    outputFile.WriteLine($"{word.Key} - {word.Value}");
                }
            }
        }
        public static async Task SaveBookAsync(BookInfo book, string directory)
        {
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(directory, String.Concat(ToFileName(book.Title), "_", book.OriginalFileName, ".txt"))))
            {
                await outputFile.WriteLineAsync("Longest sentences by number of characters:");
                var longestSentences = book.LongestSentencesByChars(10).ToList();
                foreach (var sentence in longestSentences) 
                {
                    PrintMessage(book.OriginalFileName);
                    await outputFile.WriteLineAsync(String.Concat(sentence, "\n")); 
                }

                var shortestSentences = book.ShortestSentencesByWords(10).ToList();
                await outputFile.WriteLineAsync("\nShortest sentences by number of words:");
                foreach (var sentence in shortestSentences) 
                {
                    PrintMessage(book.OriginalFileName);
                    await outputFile.WriteLineAsync(sentence); 
                }

                var longestWords = book.LongestWords(10).ToList();
                await outputFile.WriteLineAsync("\nLongest words:");
                foreach (var word in longestWords) 
                {
                    PrintMessage(book.OriginalFileName);
                    await outputFile.WriteLineAsync(word); 
                }

                var mostCommonLetters = book.MostCommonLetters(10);
                await outputFile.WriteLineAsync("\nMost common letters:");
                foreach (var letter in mostCommonLetters) 
                {
                    PrintMessage(book.OriginalFileName);
                    await outputFile.WriteLineAsync($"{letter.Key} - {letter.Value}"); 
                }

                var mostCommonWords = book.WordsByUseDesc(10);
                await outputFile.WriteLineAsync("\nMost common words:");
                foreach (var word in mostCommonWords)
                {
                    PrintMessage(book.OriginalFileName);
                    await outputFile.WriteLineAsync($"{word.Key} - {word.Value}");
                }
            }
        }
        private static string ToFileName(string text)
        {
            string fileName = text;
            var illegalChars = Path.GetInvalidFileNameChars();
            foreach (var character in illegalChars)
            {
                fileName = fileName.Replace(character.ToString(), "");
            }
            return fileName;
        }
        private static void PrintMessage(string fileName)
        {
            Console.WriteLine($"Writing a piece of data to {fileName}...");
        }
    }
}
