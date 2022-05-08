using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextFilter.Filters;

namespace TextFilterTest
{
    [TestFixture]
    public class MiddleVowelFilterTests
    {
        [TestCase("clean")]
        [TestCase("what")]
        [TestCase("currently")]
        public void ShouldCleanWordBeFiltered_ShouldReturnTrueIfMiddleLettersOfWordAreVowels(string input)
        {
            // Setup
            var sut = new MiddleVowelFilter();

            // Test
            var result = sut.ShouldWordBeFiltered(input);

            // Analysis
            Assert.That(result, Is.True);
        }

        [TestCase("the")]
        [TestCase("rather")]
        public void ShouldCleanWordBeFiltered_ShouldReturnFalseIfMiddleLettersOfWordAreNotVowels(string input)
        {
            // Setup
            var sut = new MiddleVowelFilter();

            // Test
            var result = sut.ShouldWordBeFiltered(input);

            // Analysis
            Assert.That(result, Is.False);
        }

        [TestCase("clean!!!!", true)]
        [TestCase("'what", true)]
        [TestCase("...currently", true)]
        [TestCase("the\n\r", false)]
        [TestCase("rather...!", false)]
        public void ShouldCleanWordBeFiltered_ShouldIgnorePunctuationAndNewLineCharacters(string input, bool expectedResult)
        {
            // Setup
            var sut = new MiddleVowelFilter();

            // Test
            var result = sut.ShouldWordBeFiltered(input);

            // Analysis
            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}
