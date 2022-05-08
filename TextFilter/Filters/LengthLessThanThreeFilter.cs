using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextFilter.Filters
{
    public class LengthLessThanThreeFilter : Filter
    {
        protected override bool ShouldCleanWordBeFiltered(string word)
        {
            return word.Length < 3;
        }
    }
}
