using System;
using System.Globalization;
using System.IO;
using log4net;

namespace Jarser.Logger
{
    public class Logger : ILogger
    {
        private static ILog log;

        static Logger()
        {
            var log4NetConfigDirectory = AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory;
            var log4NetConfigFilePath = Path.Combine(log4NetConfigDirectory, "log4net.config");
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(log4NetConfigFilePath));
            log = LogManager.GetLogger(typeof(Logger));
            GlobalContext.Properties["host"] = Environment.MachineName;
        }

        public Logger(Type logClass)
        {
            log = LogManager.GetLogger(logClass);
        }

        public void Exception(Exception exception)
        {
            if (log.IsErrorEnabled)
            {
                log.Error(string.Format(CultureInfo.InvariantCulture, "{0}", exception.Message), exception);
            }
        }

        public void Error(string message)
        {
            if (log.IsErrorEnabled)
            {
                log.Error(string.Format(CultureInfo.InvariantCulture, "{0}", message));
            }
        }

        public void Warning(string message)
        {
            if (log.IsWarnEnabled)
            {
                log.Warn(string.Format(CultureInfo.InvariantCulture, "{0}", message));
            }
        }

        public void Info(string message)
        {
            if (log.IsInfoEnabled)
            {
                log.Info(string.Format(CultureInfo.InvariantCulture, "{0}", message));
            }
        }
    }
}
