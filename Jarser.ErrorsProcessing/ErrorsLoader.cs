using System;
using System.Linq;
using System.Xml.Linq;

namespace Jarser.ErrorsProcessing
{
    public class ErrorsLoader : IErrorsLoader
    {
        private readonly string _documentPath;

        public ErrorsLoader()
        {
            _documentPath = @".\settings\errors_set.xml";
        }

        public Error GetErrorById(int errorId)
        {
            var errorElemet = XElement.Load(_documentPath);

            var error = errorElemet.Elements("error")
                .Where(err => err.FirstAttribute.Value == errorId.ToString()
                              && err.FirstAttribute.Name == "id"
                              && err.LastAttribute.Name == "caption")
                .Select(err => new Error(err.LastAttribute.Value, err.Value))
                .FirstOrDefault();

            return error;
        }
    }
}
