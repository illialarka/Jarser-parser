using System;
using Jarser.Parser;
using Jarser.Parser.User;
using Jarser.WebCommunication;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class ProfileParserTests
    {
        private ProfileParser _profileParser;

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void CreateWrongHtmlString(string value)
        {
            // Arrange
            _profileParser = new ProfileParser();

            // Action & Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                var result = _profileParser.ParseObject<User>(value);
            });
        }
    }
}
