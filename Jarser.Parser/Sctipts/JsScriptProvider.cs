using System;
using System.IO;
using Jarser.Logger;

namespace Jarser.Parser.Sctipts
{
    /// <summary>
    /// This class provides text of script from file.
    /// </summary>
    public class JsScriptProvider : IScriptsProvider
    {
        private readonly ILogger _logger = new Logger.Logger(typeof(JsScriptProvider));

        public string SelectScript(Uri path)
        {
            string file;
            try
            {
                file = File.ReadAllText(path.LocalPath);
            }
            catch (Exception e)
            {
                _logger.Exception(e);
                throw;
            }

            return file;
        }
    }
}
