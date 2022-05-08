using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextFilter.Filters;

namespace TextFilter
{
    public interface IFilteredTextPrinter
    {
        public string FilePath { get; set; }
        public void PrintFilteredText();
        public void AddFilter(IFilter filter);
        public void ClearFilters();
    }
}
