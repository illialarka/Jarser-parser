using Jarser.ErrorsProcessing;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    internal class ErrorProviderTests
    {
        private IErrorsLoader _errorLoader;

        [Test]
        public void SelectErrorsXml()
        {
            _errorLoader = new ErrorsLoader();

            var res = _errorLoader.GetErrorById(1);

            Assert.AreEqual(res,1);
        }
    }
}
