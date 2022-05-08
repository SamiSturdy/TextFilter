using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextFilter.Filters;

namespace TextFilter
{
    public class TextWorker : ITextWorker
    {
        private readonly ICollection<IFilter> _filters;
        private readonly IFileWrapper _fileWrapper;
        public TextWorker(string filePath, ICollection<IFilter> filters) : this(filePath, filters, new FileWrapper())
        {
        }

        public TextWorker(string filePath, ICollection<IFilter> filters, IFileWrapper fileWrapper)
        {
            FilePath = filePath;
            _filters = filters;
            _fileWrapper = fileWrapper;
        }

        public string FilePath { get; set; }

        public List<string> GetFilteredListOfWordsFromFile()
        {
            if (!_fileWrapper.DoesFileExist(FilePath))
            {
                throw new FileNotFoundException($"File at provided file path, {FilePath}, could not be found.");
            }
            
            var loadedText = _fileWrapper.GetTextFromFilePath(FilePath);

            var wordList = SplitTextIntoWords(loadedText);

            return GetFilteredWordList(wordList);
        }

        public void AddFilter(IFilter filter)
        {
            _filters.Add(filter);
        }

        public void ClearFilters()
        {
            _filters.Clear();
        }

        private List<string> GetFilteredWordList(List<string> words)
        {
            for (int i = 0; i < words.Count; i++)
            {
                var word = words[i];
                foreach (var filter in _filters)
                {
                    if (filter.ShouldWordBeFiltered(word))
                    {
                        words.RemoveAt(i);
                        break;
                    }
                }
            }

            return words;
        }

        private List<string> SplitTextIntoWords(string loadedText)
        {
            var splitText = loadedText.Split(' ').ToList();

            for (var i = 0; i < splitText.Count; i++)
            {
                var word = splitText[i];
                var hasPunctuationBeenFound = false;

                for (var j = 1; j < word.Length; j++)
                {
                    if (!hasPunctuationBeenFound && Char.IsPunctuation(word[j]))
                    {
                        hasPunctuationBeenFound = true;
                    }
                    else if (hasPunctuationBeenFound && !Char.IsPunctuation(word[j]))
                    {
                        splitText[i] = word.Substring(0, j);
                        splitText.Insert(i + 1, word.Substring(j));

                        break;
                    }
                }
            }

           return splitText;
        }        
    }
}
