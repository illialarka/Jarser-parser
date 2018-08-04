using System;
using mshtml;
using WebBrowser = System.Windows.Controls.WebBrowser;

namespace Jarser.Wpf.ScriptInserter
{
    internal class JsScriptInserter
    {
        private WebBrowser _webBrowser;

        public JsScriptInserter(WebBrowser webBrowser)
        {
            if (webBrowser == null)
            {
                throw new ArgumentNullException(nameof(webBrowser));
            }

            _webBrowser = webBrowser;
        }

        public void InsertScript(string textScript)
        {
            try
            {
                if (string.IsNullOrEmpty(textScript))
                {
                    throw new ArgumentNullException(nameof(textScript));
                }

                var htmlDocument = (HTMLDocument) _webBrowser.Document;

                var headElements = htmlDocument.getElementsByTagName("head");
                if (headElements.length == 0)
                {
                    throw new NullReferenceException(nameof(headElements));
                }

                var headElement = headElements.item(0);

                var script = (IHTMLScriptElement) htmlDocument.createElement("script");
                script.text = textScript;
                headElement.AppendChild(script);
            }
            catch (Exception)
            {
                // ignore
            }
        }
    }
}
