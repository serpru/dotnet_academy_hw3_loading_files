using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace dotnet_academy_hw3_loading_files
{
    static internal class BookLoader
    {
        public static BookInfo? LoadBook(string path)
        {
            try
            {
                if (!File.Exists(path))
                {
                    return null;
                }
                var book = new BookInfo();
                using (StreamReader sr = new StreamReader(path))
                {
                    book.OriginalFileName = path.Substring(path.LastIndexOf(@"\") + 1);
                    book.OriginalFileName = book.OriginalFileName.Replace(".txt", "");
                    string line;
                    string titleIndicator = "Title: ";
                    string[] words;
                    string paragraph = string.Empty;
                    bool startFlag = false;
                    bool endFlag = false;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (endFlag)
                        {
                            break;
                        }
                        if (line.Contains("*** END OF THE PROJECT"))
                        {
                            endFlag = true;
                        }
                        if (!startFlag)
                        {
                            if (line.StartsWith(titleIndicator))
                            {
                                book.Title = line.Substring(titleIndicator.Length);
                            }
                            if (line.Contains("*** START OF THE PROJECT")) { startFlag = true; }
                            continue;
                        }
                        if (string.IsNullOrEmpty(line)) { continue; }
                        paragraph = string.Concat(paragraph, string.Concat(line, "\n"));
                        if (Equals(line.ElementAt(line.Length - 1), '.'))
                        {
                            //  Line ends with a dot indicating end of sentence/paragraph
                            book.LoadParagraph(paragraph);
                            paragraph = string.Empty;
                        }
                    }
                }
                return book;
            }
            catch (Exception e)
            {
                // File could not be read
                return null;
            }
        }
        public static async Task<BookInfo?> LoadBookAsync(string path)
        {
            try
            {
                if (!File.Exists(path))
                {
                    return null;
                }
                var book = new BookInfo();
                using (StreamReader sr = new StreamReader(path))
                {
                    book.OriginalFileName = path.Substring(path.LastIndexOf(@"\") + 1);
                    book.OriginalFileName = book.OriginalFileName.Replace(".txt", "");
                    string line;
                    string titleIndicator = "Title: ";
                    string[] words;
                    string paragraph = string.Empty;
                    bool startFlag = false;
                    bool endFlag = false;
                    while ((line = await sr.ReadLineAsync()) != null)
                    {
                        if (endFlag)
                        {
                            break;
                        }
                        if (line.Contains("*** END OF THE PROJECT"))
                        {
                            endFlag = true;
                        }
                        if (!startFlag)
                        {
                            if (line.StartsWith(titleIndicator))
                            {
                                book.Title = line.Substring(titleIndicator.Length);
                            }
                            if (line.Contains("*** START OF THE PROJECT")) { startFlag = true; }
                            continue;
                        }
                        if (string.IsNullOrEmpty(line)) { continue; }
                        paragraph = string.Concat(paragraph, string.Concat(line, "\n"));
                        if (Equals(line.ElementAt(line.Length - 1), '.'))
                        {
                            //  Line ends with a dot indicating end of sentence/paragraph
                            book.LoadParagraph(paragraph);
                            paragraph = string.Empty;
                        }
                    }
                }
                return await Task.FromResult(book);
            }
            catch (Exception e)
            {
                // File could not be read
                return null;
            }
        }
    }
}
