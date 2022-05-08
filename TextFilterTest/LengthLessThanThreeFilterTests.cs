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
    public class LengthLessThanThreeFilterTests
    {
        [TestCase("am")]
        [TestCase("so")]
        [TestCase("to")]
        [TestCase("a")]
        public void ShouldCleanWordBeFiltered_ShouldReturnTrueIfMiddleLettersOfWordAreVowels(string input)
        {
            // Setup
            var sut = new LengthLessThanThreeFilter();

            // Test
            var result = sut.ShouldWordBeFiltered(input);

            // Analysis
            Assert.That(result, Is.True);
        }

        [TestCase("the")]
        [TestCase("rather")]
        [TestCase("clean")]
        [TestCase("what")]
        public void ShouldCleanWordBeFiltered_ShouldReturnFalseIfMiddleLettersOfWordAreNotVowels(string input)
        {
            // Setup
            var sut = new LengthLessThanThreeFilter();

            // Test
            var result = sut.ShouldWordBeFiltered(input);

            // Analysis
            Assert.That(result, Is.False);
        }

        [TestCase("am...", true)]
        [TestCase("so!!!", true)]
        [TestCase("to\n\r", true)]
        [TestCase("a!?!?", true)]
        [TestCase("'the'", false)]
        [TestCase("rather...", false)]
        [TestCase("clean?", false)]
        [TestCase("what!", false)]
        public void ShouldCleanWordBeFiltered_ShouldIgnorePunctuationAndNewLineCharacters(string input, bool expectedResult)
        {
            // Setup
            var sut = new LengthLessThanThreeFilter();

            // Test
            var result = sut.ShouldWordBeFiltered(input);

            // Analysis
            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}
