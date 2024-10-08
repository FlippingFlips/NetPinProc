﻿using NetPinProc.Domain;
using NetPinProc.Domain.PinProc;

namespace NetPinProc.Domain.Tests.Logging
{
    /// <summary>
    /// no assertions, just a class to test future logging
    /// </summary>
    public class LoggingTests
    {
        /// <summary>
        /// <see cref="ConsoleLogger"/>
        /// </summary>
        [Fact]
        public void LoggingLevel_SetWarning_ShouldSkipInfoLog_Tests()
        {
            var logger = new ConsoleLogger(LogLevel.Warning) as ILoggerPROC;
            logger.Log("info logging", LogLevel.Info);
        }
    }
}
