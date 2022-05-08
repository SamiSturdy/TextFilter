using TextFilter;
using TextFilter.Filters;

var filters = new List<IFilter>()
{
    new MiddleVowelFilter(),
    new LengthLessThanThreeFilter(),
    new ContainsLetterTFilter()
};

var filePath = "Resources/Input.txt";

var filteredTextPrinter = new FilteredTextPrinter(filePath, filters);

filteredTextPrinter.PrintFilteredText();