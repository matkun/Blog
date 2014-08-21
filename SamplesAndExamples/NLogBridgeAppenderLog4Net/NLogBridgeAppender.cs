using System;
using System.Collections.Generic;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using NLog;

namespace MyWeb.Core.Infrastructure.Logging
{
  public class NLogBridgeAppender : AppenderSkeleton
  {
    Dictionary<string, Logger> _loggers = new Dictionary<string, Logger>();
    readonly object _lockObject = new object();

    public static void Initialize()
    {
      BasicConfigurator.Configure(new NLogBridgeAppender());
    }

    protected override void Append(LoggingEvent loggingEvent)
    {
      var nLogEvent = ToNLog(loggingEvent);
      var nLogger = GetLogger(loggingEvent);
      nLogger.Log(nLogEvent);
    }

    Logger GetLogger(LoggingEvent loggingEvent)
    {
      Logger nLogger;
      if (_loggers.TryGetValue(loggingEvent.LoggerName, out nLogger))
      {
        return nLogger;
      }

      lock (_lockObject)
      {
        if (_loggers.TryGetValue(loggingEvent.LoggerName, out nLogger))
        {
          return nLogger;
        }
        nLogger = LogManager.GetLogger(loggingEvent.LoggerName);
        _loggers = new Dictionary<string, Logger>(_loggers)
          {
            { loggingEvent.LoggerName, nLogger }
          };
      }
      return nLogger;
    }

    private static LogLevel ToNLogLevel(Level level)
    {
      LogLevel nLevel;

      if (level == Level.Fatal) nLevel = LogLevel.Fatal;
      else if (level == Level.Error) nLevel = LogLevel.Error;
      else if (level == Level.Warn) nLevel = LogLevel.Warn;
      else if (level == Level.Debug) nLevel = LogLevel.Debug;
      else if (level == Level.Info) nLevel = LogLevel.Info;
      else if (level == Level.Trace) nLevel = LogLevel.Trace;
      else if (level == Level.Off) nLevel = LogLevel.Off;
      else
      {
        var message = string.Format("Unsupported log level: {0}.", level);
        throw new NotSupportedException(message);
      }

      return nLevel;
    }

    private static LogEventInfo ToNLog(LoggingEvent loggingEvent)
    {
      return new LogEventInfo
        {
          Exception = loggingEvent.ExceptionObject,
          LoggerName = loggingEvent.LoggerName,
          Message = Convert.ToString(loggingEvent.MessageObject),
          Level = ToNLogLevel(loggingEvent.Level),
          TimeStamp = loggingEvent.TimeStamp,
          FormatProvider = null
        };
    }
  }
}