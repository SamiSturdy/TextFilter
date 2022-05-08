using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextFilter;
using TextFilter.Filters;
using System.IO;

namespace TextFilterTest
{
    [TestFixture]
    public class FilteredTextPrinterTests
    {
        private string _filePath;

        private Mock<ITextWorker> _mockTextWorker;

        [SetUp]
        public void SetUp()
        {
            _mockTextWorker = new Mock<ITextWorker>();

            _filePath = "someFile/Path.txt";
        }

        [Test]
        public void FilePathSetter_ShouldSetFilePathOnTextWorker()
        {
            //Setup
            var sut = new FilteredTextPrinter(_mockTextWorker.Object);

            // Test
            sut.FilePath = _filePath;

            // Analysis
            _mockTextWorker.VerifySet(x => x.FilePath = _filePath);
        }

        [Test]
        public void FilePathGetter_ShouldGetFilePathFromTextWorker()
        {
            // Setup
            var sut = new FilteredTextPrinter(_mockTextWorker.Object);
            _mockTextWorker.Setup(x => x.FilePath).Returns(_filePath);

            // Test & Analysis
            Assert.That(sut.FilePath, Is.EqualTo(_filePath));
        }

        [Test]
        public void AddFilter_ShouldCallAddFilterOnTextWorker()
        {
            // Setup
            var sut = new FilteredTextPrinter(_mockTextWorker.Object);
            sut.AddFilter(Mock.Of<IFilter>());

            // Test & Analysis
            _mockTextWorker.Verify(x => x.AddFilter(It.IsAny<IFilter>()), Times.Once);
        }

        [Test]
        public void ClearFilters_ShouldCallClearFiltersOnTextWorker()
        {
            // Setup
            var sut = new FilteredTextPrinter(_mockTextWorker.Object);
            sut.ClearFilters();

            // Test & Analysis
            _mockTextWorker.Verify(x => x.ClearFilters(), Times.Once);
        }

        [Test]
        public void PrintFilteredText_ShouldCallGetFilteredListOfWordsFromFileOnTextWorker()
        {
            //Data
            var wordList = new List<string> { "Some", "Example", "Words" };

            //Setup
            var sut = new FilteredTextPrinter(_mockTextWorker.Object);
            _mockTextWorker.Setup(x => x.GetFilteredListOfWordsFromFile()).Returns(wordList);

            // Test
            sut.PrintFilteredText();

            // Analysis
            _mockTextWorker.Verify(x => x.GetFilteredListOfWordsFromFile(), Times.Once);
        }

        [Test]
        public void PrintFilteredText_ShouldPrintFilteredWordsToConsole()
        {
            //Data
            var wordList = new List<string> { "Some", "Example", "Words" };
            var expectedOutput = "Some Example Words\r\n";
            var stringWriter = new StringWriter();

            //Setup
            var sut = new FilteredTextPrinter(_mockTextWorker.Object);
            _mockTextWorker.Setup(x => x.GetFilteredListOfWordsFromFile()).Returns(wordList);
            Console.SetOut(stringWriter);

            // Test
            sut.PrintFilteredText();

            // Analysis
            Assert.That(stringWriter.ToString(), Is.EqualTo(expectedOutput));
        }
    }
}
