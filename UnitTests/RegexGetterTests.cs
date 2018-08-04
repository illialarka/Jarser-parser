using System.Linq;
using Jarser.RegexSettings;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class RegexGetterTests
    {
        private IRegexGetter _regexGetter;

        [Test]
        public void CorrectSelectRegexes()
        {
            // Arrange
            _regexGetter = new RegexGetter();

            // Action
            var result = _regexGetter.GetRegexesByName("phone_number");

            // Assert
            Assert.AreEqual(4, result.Count());
        }

        [Test]
        public void CorrectSelectRegexWithAnd()
        {
            // Arrange
            _regexGetter = new RegexGetter();

            // Action
            var result = _regexGetter.GetRegexWithAnd("phone_number");

            // Assert
            Assert.IsFalse(string.IsNullOrEmpty(result));
        }

        [Test]
        public void CorrecSelectRegexWithOr()
        {
            // Arrange
            _regexGetter = new RegexGetter();

            // Action
            var result = _regexGetter.GetRegexWithOr("phone_number");

            // Assert
            Assert.IsFalse(string.IsNullOrEmpty(result));
        }
    }
}
