using System;
using Microsoft.Extensions.Logging;

namespace LoggerConsole
{
    internal static class LoggerHostLoggerExtensions
	{
		private static readonly Func<EventId> _getApplicationInitializedEvent = () => new EventId(LogEvents.ApplicationInitialized, nameof(LogEvents.ApplicationInitialized));
		private static readonly Func<EventId> _getApplicationErrorEvent = () => new EventId(LogEvents.ApplicationError, nameof(LogEvents.ApplicationError));

		private readonly static Func<ILogger, string, IDisposable> _appendMethod = LoggerMessage.DefineScope<string>("{Method}");
		private readonly static Func<ILogger, int, IDisposable> _appendNumber = LoggerMessage.DefineScope<int>("{Number}");
		private readonly static Func<ILogger, string, IDisposable> _appendState = LoggerMessage.DefineScope<string>("{State}");

		private static readonly Action<ILogger, string, Exception> _debug =
			LoggerMessage.Define<string>(
				LogLevel.Debug,
				_getApplicationInitializedEvent(),
				"{Message}");

		private static readonly Action<ILogger, string, Exception> _error =
			LoggerMessage.Define<string>(
				LogLevel.Error,
				_getApplicationErrorEvent(),
				"{Message}");

		public static IDisposable AppendMethodScope(this ILogger logger, string method) => _appendMethod(logger, method);
		public static IDisposable AppendNumber(this ILogger logger, int number) => _appendNumber(logger, number);
		public static IDisposable AppendState(this ILogger logger, string state) => _appendState(logger, state);

		public static void HostIsRunning(this ILogger logger) => _debug(logger, "Host is running", default);
		public static void HostExited(this ILogger logger) => _debug(logger, "Host has finished running", default);
		public static void ReceivedRandomNumber(this ILogger logger) => _debug(logger, "Received random number", default!);
		public static void CalculatedState(this ILogger logger) => _debug(logger, "Calculated running state", default);
		public static void RunningStateCritical(this ILogger logger) => _error(logger, "Running state is critical", default);
	}
}
