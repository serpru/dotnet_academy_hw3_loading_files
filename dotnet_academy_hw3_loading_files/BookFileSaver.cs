using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet_academy_hw3_loading_files.input_data
{
    internal class BookFileSaver
    {
        public void SaveBook(BookInfo book, string directory)
        {
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(directory,String.Concat(ToFileName(book.Title), ".txt"))))
            {
                outputFile.WriteLine("Longest sentences by number of characters:");
                var longestSentences = book.LongestSentencesByChars(10).ToList();
                var shortestSentences = book.ShortestSentencesByWords(10).ToList();
                var longestWords = book.LongestWords(10);
                var mostCommonLetters = book.MostCommonLetters(10);
                foreach (var sentence in longestSentences) { outputFile.WriteLine(String.Concat(sentence, "\n")); }
                outputFile.WriteLine("\nShortest sentences by number of words:");
                foreach (var sentence in shortestSentences) { outputFile.WriteLine(sentence); }
                outputFile.WriteLine("\nLongest words:");
                foreach (var word in longestWords) { outputFile.WriteLine(word); }
                outputFile.WriteLine("\nMost common letters:");
                foreach (var letter in mostCommonLetters) { outputFile.WriteLine(letter); }
                // add sorted words by number of uses in descending order
            }
        }
        private string ToFileName(string text)
        {
            string fileName = text;
            var illegalChars = Path.GetInvalidFileNameChars();
            foreach (var character in illegalChars)
            {
                fileName = fileName.Replace(character.ToString(), "");
            }
            return fileName;
        }
    }
}
