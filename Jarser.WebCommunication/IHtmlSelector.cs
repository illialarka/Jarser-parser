using System;

namespace Jarser.WebCommunication
{
    public interface IHtmlSelector : IDisposable
    {
        Uri Uri { get; }

        string Run();
    }
}
