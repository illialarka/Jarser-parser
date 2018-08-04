using System;

namespace Jarser.Logger
{
    public interface ILogger
    {
        void Exception(Exception exception);

        void Error(string message);

        void Warning(string message);

        void Info(string message);
    }
}
