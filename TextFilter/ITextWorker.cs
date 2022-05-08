using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextFilter.Filters;

namespace TextFilter
{
    public interface ITextWorker
    {
        string FilePath { get; set; }

        List<string> GetFilteredListOfWordsFromFile();
        void AddFilter(IFilter filter);
        void ClearFilters();
    }
}
