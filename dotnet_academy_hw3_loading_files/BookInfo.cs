using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace dotnet_academy_hw3_loading_files
{
    internal class BookInfo
    {
        public string Title { get; set; }
        public List<string> Sentences { get; set; }
        public List<string> Words { get; set; }
        public Dictionary<char, uint> Punctuation { get; set; }
        public string OriginalFileName { get; set; }
        public BookInfo() 
        {
            Title = "Untitled";
            Sentences = new List<string>();
            Words = new List<string>();
            Punctuation = new Dictionary<char, uint>();
        }
        private void LoadWords(string text)
        {
            string pattern = @"(?<=^| )(\w+-{1}\w+)|(\w+’{1}\w+)|(\w+)(?= |$|.)";
            Regex regex = new Regex(pattern);
            var words = regex.Matches(text);
            List<string> wordsList = new List<string>();
            foreach ( var word in words )
            {
                wordsList.Add(word.ToString());
            }
            Words.AddRange(wordsList);
        }
        private void LoadSentences(string text)
        {
            string pattern = @"[A-Z,][^.?!]*[.?!]{1}";
            Regex regex = new Regex(pattern);
            var sentences = regex.Matches(text);
            List<string> sentencesList = new List<string>();
            foreach (var sentence in sentences)
            {
                sentencesList.Add(sentence.ToString().Trim().Replace("\n", " "));
            }
            Sentences.AddRange(sentencesList);
        }
        private void LoadPunctuation(string text)
        {
            var punctuationString = new string(text.Where(c => char.IsPunctuation(c)).ToArray());
            Dictionary<char, uint> punctuationChars = new Dictionary<char, uint>();
            foreach (var character in punctuationString)
            {
                if (!Punctuation.TryAdd(character, 1))
                {
                    Punctuation[character] += 1;
                }
            }
        }
        public void LoadParagraph(string text)
        {
            LoadWords(text);
            LoadSentences(text);
            LoadPunctuation(text);
        }
        public IEnumerable<string> LongestSentencesByChars(int count)
        {
            var result = Sentences.OrderByDescending(x => x.Count()).Take(count);
            return result;
        }
        private IEnumerable<string> WordsFromSentence(string sentence)
        {
            string pattern = @"(?<=^| )(\w+-{1}\w+)|(\w+’{1}\w+)|(\w+)(?= |$|.)";
            Regex regex = new Regex(pattern);
            var matches = regex.Matches(sentence);
            return matches.Select(x => x.Value);
        }
        public IEnumerable<string> ShortestSentencesByWords(int count)
        {
            var result = Sentences.OrderBy<string, int>(x => WordsFromSentence(x).Count()).Take(count);
            return result;
        }
        public IEnumerable<string> LongestWords(int count)
        {
            var result = Words.OrderByDescending(x => x.Count()).Take(count);
            return result;
        }
        public IDictionary<char, uint> MostCommonLetters(int count)
        {
            Dictionary<char, uint> letterDict = new Dictionary<char, uint>();
            foreach (var word in Words)
            {
                foreach (var letter in word)
                {
                    if (char.IsPunctuation(letter)) { continue; }
                    if (!letterDict.TryAdd(letter, 1))
                    {
                        letterDict[letter] += 1;
                    }
                }
            }
            return letterDict.OrderByDescending(x => x.Value).Take(count).ToDictionary();
        }
        public IDictionary<string, uint> WordsByUseDesc(int count)
        {
            Dictionary<string, uint> wordsDict = new Dictionary<string, uint>();
            foreach (var word in Words)
            {
                if (!wordsDict.TryAdd(word, 1))
                {
                    wordsDict[word] += 1;
                }
            }
            return wordsDict.OrderByDescending(w => w.Value).Take(count).ToDictionary();
        }
    }
}
