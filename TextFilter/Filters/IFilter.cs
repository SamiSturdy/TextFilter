using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextFilter.Filters
{
    public interface IFilter
    {
        bool ShouldWordBeFiltered(string word);
    }
}
