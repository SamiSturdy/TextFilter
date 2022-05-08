using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextFilter.Filters
{
    public class MiddleVowelFilter : Filter
    {
        protected override bool ShouldCleanWordBeFiltered(string word)
        {
            var vowels = "aeiou";

            if (word.Length % 2 == 0)
            {
                var firstIndex = (word.Length / 2) - 1;
                return vowels.Contains(word[firstIndex], StringComparison.OrdinalIgnoreCase) || vowels.Contains(word[firstIndex + 1], StringComparison.OrdinalIgnoreCase);
            }
            else
            {
                var index = (word.Length - 1) / 2;
                return vowels.Contains(word[index], StringComparison.OrdinalIgnoreCase);
            }
        }
    }
}
