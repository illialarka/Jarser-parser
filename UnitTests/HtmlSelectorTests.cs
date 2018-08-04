using System;
using Jarser.WebCommunication;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class HtmlSelectorTests
    {
        private IHtmlSelector _htmlSelector;

        [Test]
        public void CreateHtmlSelectoWithNullUriString()
        {
            // Arrange & Action & Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                var htmlSelector = new HtmlSelector(null);
            });
        }

        [Test]
        public void CreateHtmlSelectorWithEmptyUriString()
        {
            // Arrange & Action & Assert
            Assert.Throws<UriFormatException>(() =>
            {
                var htmlSelector = new HtmlSelector(new Uri(""));
            });
        }

        [Test]
        public void CreateHtmlSelectorWithNullUri()
        {
            // Arrange & Action & Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                var htmlSelector = new HtmlSelector(null);
            });
        }

        [Test]

        public void DoubleInvokeRunOnWebSite()
        {
            // Arrange
            _htmlSelector = new HtmlSelector(new Uri("https://www.instagram.com/"));

            // Action
            _htmlSelector.Run();

            // Assert
            Assert.Throws<NotSupportedException>(() => _htmlSelector.Run());
        }

        [Test]
        public void DisposeInstanceHtmlSelector()
        {
            // Arrange
            _htmlSelector = new HtmlSelector(new Uri("https://www.instagram.com/"));

            // Action
            _htmlSelector.Dispose();

            // Assert
            Assert.Throws<ObjectDisposedException>(() => _htmlSelector.Run());
        }
    }
}
