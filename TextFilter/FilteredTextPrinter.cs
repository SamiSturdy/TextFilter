using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextFilter.Filters;

namespace TextFilter
{
    public class FilteredTextPrinter : IFilteredTextPrinter
    {
        private ITextWorker _textWorker;

        public FilteredTextPrinter(string filePath, ICollection<IFilter> filters) : this(new TextWorker(filePath, filters))
        {
        }

        public FilteredTextPrinter(ITextWorker textWorker)
        {
            _textWorker = textWorker;
        }

        public string FilePath
        {
            get 
            { 
                return _textWorker.FilePath; 
            }
            set 
            { 
                _textWorker.FilePath = value; 
            }
        }

        public void PrintFilteredText()
        {
            var wordList = _textWorker.GetFilteredListOfWordsFromFile();

            Console.WriteLine(string.Join(' ', wordList));
        }

        public void AddFilter(IFilter filter)
        {
            _textWorker.AddFilter(filter);
        }

        public void ClearFilters()
        {
            _textWorker.ClearFilters();
        }
    }
}
