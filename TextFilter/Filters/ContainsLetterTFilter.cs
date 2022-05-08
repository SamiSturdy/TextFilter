using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextFilter.Filters
{
    public class ContainsLetterTFilter : Filter
    {
        protected override bool ShouldCleanWordBeFiltered(string word)
        {
            return word.Contains('t', StringComparison.OrdinalIgnoreCase);
        }
    }
}
