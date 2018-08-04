//-----------------------------------------------------------------------
// <copyright file="HtmlSelector.cs" company="Jarser Enterprises">
//     Copyright (c) All rights reserved.
// </copyright>
// <author>Larka Ilya</author>
//-----------------------------------------------------------------------

using System;
using System.IO;
using System.Net;
using System.ServiceModel;
using Jarser.Logger;

namespace Jarser.WebCommunication
{
    public class HtmlSelector : IHtmlSelector
    {
        private readonly ILogger _logger = new Logger.Logger(typeof(HtmlSelector));

        private bool _invoked;

        private WebClient _webClient;

        private Stream _stream;

        private StreamReader _streamReader;

        private bool _disposed;

        public HtmlSelector(Uri uri)
        {
            try
            {
                if (uri == null)
                {
                    throw new ArgumentNullException(nameof(uri), "Url can not be null.");
                }

                Uri = uri;
                _webClient = new WebClient();

                _stream = _webClient.OpenRead(uri);

                _streamReader = new StreamReader(_stream);
            }
            catch (Exception exc)
            {
                // irgnore
                _logger.Exception(exc);
            }
        }

        public Uri Uri { get; }

        public string Run()
        {
            var htmlString = string.Empty;
            try
            {
                _logger.Info($"Run HtmlSelector with URL {Uri}");

                if (_disposed)
                {
                    _logger.Warning($"Run method of HtmlSelector({Uri}) throws exception (invoke on disposed method).");
                    throw new ObjectDisposedException(nameof(HtmlSelector));
                }

                if (!_invoked)
                {
                    try
                    {
                        htmlString = _streamReader.ReadToEnd();
                        _invoked = true;
                    }
                    catch (Exception e)
                    {
                        _logger.Exception(e);
                    }
                }
                else
                {
                    _logger.Warning($"Run methof of HtmlSelector({Uri}) throws exception (double invoked).");
                    throw new NotSupportedException("Run has already invoked.");
                }
            }
            catch (Exception e)
            {
                _logger.Exception(e);
            }

            return htmlString;
        }

        /// <summary>
        /// Dispose this instance. Close web client and inner streams.
        /// </summary>
        public virtual void Dispose()
        {
            _logger.Info($"The instance of HtmlSelector({Uri}) was disposed.");
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            _webClient?.Dispose();
            _webClient = null;

            _stream?.Close();
            _stream = null;

            _streamReader?.Close();
            _streamReader = null;

            _disposed = true;
        }
    }
}
