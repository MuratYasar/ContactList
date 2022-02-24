using Contracts;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Logging
{
    public class LoggerManager : ILoggerManager
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        public LoggerManager()
        {
        }

        public void LogTrace(string message, [CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0)
        {
            logger.Trace($"{message}|FilePath:{filePath}|MemberName:{memberName}|LineNumber:{lineNumber}");
        }

        public void LogDebug(string message, [CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0)
        {
            logger.Debug($"{message}|FilePath:{filePath}|MemberName:{memberName}|LineNumber:{lineNumber}");
        }

        public void LogInfo(string message, [CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0)
        {
            logger.Info($"{message}|FilePath:{filePath}|MemberName:{memberName}|LineNumber:{lineNumber}");
        }

        public void LogWarn(string message, [CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0)
        {
            logger.Warn($"{message}|FilePath:{filePath}|MemberName:{memberName}|LineNumber:{lineNumber}");
        }

        public void LogError(string message, [CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0)
        {
            logger.Error($"{message}|FilePath:{filePath}|MemberName:{memberName}|LineNumber:{lineNumber}");
        }

        public void LogFatal(string message, [CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0)
        {
            logger.Fatal($"{message}|FilePath:{filePath}|MemberName:{memberName}|LineNumber:{lineNumber}");
        }
    }
}
