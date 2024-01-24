using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet_academy_hw3_loading_files
{
    internal class BookInfo
    {
        public List<string> Sentences { get; set; }
        public List<string> Words { get; set; }
        public List<string> Punctuation { get; set; }
        public BookInfo() 
        {
            Sentences = new List<string>();
            Words = new List<string>();
            Punctuation = new List<string>();
        }

        public string GetLongestSentenceByChars()
        {
            throw new NotImplementedException();
        }
        public string GetShortestSentenceByWords()
        {
            throw new NotImplementedException();
        }
        public string GetLongestWord()
        {
            throw new NotImplementedException();
        }
        public string GetMostCommonLetter()
        {
            throw new NotImplementedException();
        }
        public ICollection<string> GetWordsByUseDesc()
        {
            throw new NotImplementedException();
        }
    }
}
