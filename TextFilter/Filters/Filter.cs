using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextFilter.Filters
{
    public abstract class Filter : IFilter
    {
        public bool ShouldWordBeFiltered(string word)
        {
            word = CleanExtraCharactersFromWord(word);
            return ShouldCleanWordBeFiltered(word);
        }

        protected abstract bool ShouldCleanWordBeFiltered(string word);

        private string CleanExtraCharactersFromWord(string word)
        {
            return new string(word.Where(x => !Char.IsPunctuation(x) && x != '\n' && x != '\r').ToArray());
        }
    }
}
