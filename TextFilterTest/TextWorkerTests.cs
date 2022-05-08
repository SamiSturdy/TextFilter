using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextFilter;
using TextFilter.Filters;

namespace TextFilterTest
{
    [TestFixture]
    public class TextWorkerTests
    {
        private string _filePath;

        private Mock<ICollection<IFilter>> _mockFilterCollection;
        private Mock<IFilter> _mockFilter;
        private Mock<IFileWrapper> _mockFileWrapper;

        [SetUp]
        public void SetUp()
        {
            _mockFilter = new Mock<IFilter>();
            _mockFilterCollection = new Mock<ICollection<IFilter>>();
            _mockFileWrapper = new Mock<IFileWrapper>();

            _filePath = "someFile/Path.txt";

            _mockFilterCollection.Setup(x => x.GetEnumerator()).Returns(new List<IFilter>() { _mockFilter.Object }.GetEnumerator());
        }

        [Test]
        public void AddFilter_ShouldAddFilterToCollection()
        {
            // Setup
            var sut = new TextWorker(_filePath, _mockFilterCollection.Object, _mockFileWrapper.Object);

            // Test
            sut.AddFilter(_mockFilter.Object);

            // Analysis
            _mockFilterCollection.Verify(x => x.Add(_mockFilter.Object));
        }

        [Test]
        public void ClearFilters_ShouldClearFiltersFromCollection()
        {
            // Setup
            var sut = new TextWorker(_filePath, _mockFilterCollection.Object, _mockFileWrapper.Object);

            // Test
            sut.ClearFilters();

            // Analysis
            _mockFilterCollection.Verify(x => x.Clear());
        }

        [Test]
        public void GetFilteredListOfWordsFromFile_ShouldThrowFileNotFoundExceptionIfFileDoesNotExist()
        {
            // Setup
            var sut = new TextWorker(_filePath, _mockFilterCollection.Object, _mockFileWrapper.Object);
            _mockFileWrapper.Setup(x => x.DoesFileExist(It.IsAny<string>())).Returns(false);

            // Test & Analysis
            Assert.Throws<FileNotFoundException>(() => sut.GetFilteredListOfWordsFromFile());
        }

        [Test]
        public void GetFilteredListOfWordsFromFile_ShouldCallGetTextFromFilePathOnFileWrapper()
        {
            // Data
            var fileText = "Text loaded from file";

            // Setup
            var sut = new TextWorker(_filePath, _mockFilterCollection.Object, _mockFileWrapper.Object);
            _mockFileWrapper.Setup(x => x.DoesFileExist(It.IsAny<string>())).Returns(true);
            _mockFileWrapper.Setup(x => x.GetTextFromFilePath(It.IsAny<string>())).Returns(fileText);

            // Test
            sut.GetFilteredListOfWordsFromFile();

            // Analysis
            _mockFileWrapper.Verify(x => x.GetTextFromFilePath(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void GetFilteredListOfWords_ShouldSplitWordsSeparatedByWhitespace()
        {
            // Data
            var fileText = "Text loaded from file";
            var expectedOutput = fileText.Split(' ');

            // Setup
            var sut = new TextWorker(_filePath, _mockFilterCollection.Object, _mockFileWrapper.Object);
            _mockFileWrapper.Setup(x => x.DoesFileExist(It.IsAny<string>())).Returns(true).Verifiable();
            _mockFileWrapper.Setup(x => x.GetTextFromFilePath(It.IsAny<string>())).Returns(fileText).Verifiable();

            // Test
            var result = sut.GetFilteredListOfWordsFromFile();

            // Analysis
            Assert.That(result, Is.EqualTo(expectedOutput));
            _mockFileWrapper.Verify();
        }

        [Test]
        public void GetFilteredListOfWords_ShouldSplitWordsSeparatedByPunctuation()
        {
            // Data
            var fileText = "These.Words'Should'Be.Separate!";
            var expectedOutput = new List<string>() { "These.", "Words'", "Should'", "Be.", "Separate!" };

            // Setup
            var sut = new TextWorker(_filePath, _mockFilterCollection.Object, _mockFileWrapper.Object);
            _mockFileWrapper.Setup(x => x.DoesFileExist(It.IsAny<string>())).Returns(true).Verifiable();
            _mockFileWrapper.Setup(x => x.GetTextFromFilePath(It.IsAny<string>())).Returns(fileText).Verifiable();

            // Test
            var result = sut.GetFilteredListOfWordsFromFile();

            // Analysis
            Assert.That(result, Is.EqualTo(expectedOutput));
            _mockFileWrapper.Verify();
        }

        [Test]
        public void GetFilteredListOfWords_ShouldFilterOutWordsIfFilterReturnsShouldWordBeFilteredValueOfTrue()
        {
            // Data
            var fileText = "This output is not correct";
            var expectedOutput = new List<string>() { "This", "output", "is", "correct" };
            var filters = new List<IFilter>() { _mockFilter.Object };

            // Setup
            var sut = new TextWorker(_filePath, filters, _mockFileWrapper.Object);
            _mockFileWrapper.Setup(x => x.DoesFileExist(It.IsAny<string>())).Returns(true).Verifiable();
            _mockFileWrapper.Setup(x => x.GetTextFromFilePath(It.IsAny<string>())).Returns(fileText).Verifiable();
            _mockFilter.Setup(x => x.ShouldWordBeFiltered("not")).Returns(true);

            // Test
            var result = sut.GetFilteredListOfWordsFromFile();

            // Analysis
            Assert.That(result, Is.EqualTo(expectedOutput));
            _mockFilter.Verify(x => x.ShouldWordBeFiltered("not"), Times.Once);
            _mockFileWrapper.Verify();
        }
    }
}
