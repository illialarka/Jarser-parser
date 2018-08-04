using System;
using Jarser.Parser.Sctipts;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class JsScriptProviderTests
    {
        private IScriptsProvider _provider;

        [Test]
        public void CreateInstanceWithNullTextReader()
        {
            // Arrange & Action & Assert
            Assert.Throws<ArgumentNullException>(() => { _provider = new JsScriptProvider(); });
        }
    }
}
