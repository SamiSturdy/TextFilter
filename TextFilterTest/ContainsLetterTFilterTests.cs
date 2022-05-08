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
    public class ContainsLetterTFilterTests
    {
        [TestCase("cat")]
        [TestCase("target")]
        [TestCase("party")]
        public void ShouldCleanWordBeFiltered_ShouldReturnTrueIfMiddleLettersOfWordAreVowels(string input)
        {
            // Setup
            var sut = new ContainsLetterTFilter();

            // Test
            var result = sut.ShouldWordBeFiltered(input);

            // Analysis
            Assert.That(result, Is.True);
        }

        [TestCase("why")]
        [TestCase("clean")]
        [TestCase("happy")]
        public void ShouldCleanWordBeFiltered_ShouldReturnFalseIfMiddleLettersOfWordAreNotVowels(string input)
        {
            // Setup
            var sut = new ContainsLetterTFilter();

            // Test
            var result = sut.ShouldWordBeFiltered(input);

            // Analysis
            Assert.That(result, Is.False);
        }

        [TestCase("cat", true)]
        [TestCase("target", true)]
        [TestCase("party", true)]
        [TestCase("why", false)]
        [TestCase("clean", false)]
        [TestCase("happy", false)]
        public void ShouldCleanWordBeFiltered_ShouldIgnorePunctuationAndNewLineCharacters(string input, bool expectedResult)
        {
            // Setup
            var sut = new ContainsLetterTFilter();

            // Test
            var result = sut.ShouldWordBeFiltered(input);

            // Analysis
            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}
