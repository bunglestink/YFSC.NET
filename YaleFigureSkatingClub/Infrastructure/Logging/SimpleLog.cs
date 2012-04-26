using System;

namespace YaleFigureSkatingClub.Infrastructure.Logging
{
	public class SimpleLog : ILog
	{
		public LogLevel Level { get; set; }
		
		public SimpleLog ()
		{
			this.Level = LogLevel.Debug;
		}
		
		
		private void Log(LogLevel level, string message)
		{
			if (level <= Level) {
				Console.WriteLine(String.Format("{0}: {1}", level.ToString(), message));
			}
		}
		
		public void Fatal (string message)
		{
			Log(LogLevel.Fatal, message);
		}

		public void Error (string message)
		{
			Log(LogLevel.Error, message);
		}

		public void Warn (string message)
		{
			Log(LogLevel.Warn, message);
		}

		public void Info (string message)
		{
			Log(LogLevel.Info, message);
		}

		public void Debug (string message)
		{
			Log(LogLevel.Debug, message);
		}
		
		public void FatalFormat(string message, params string[] objects)
		{
			Fatal(String.Format (message, objects));
		}

		public void ErrorFormat(string message, params string[] objects)
		{
			Error(String.Format(message, objects));
		}

		public void WarnFormat(string message, params string[] objects)
		{
			Warn(String.Format(message, objects));
		}

		public void InfoFormat(string message, params string[] objects)
		{
			Info(String.Format(message, objects));
		}

		public void DebugFormat(string message, params string[] objects)
		{
			Debug(String.Format(message, objects));
		}
	}
}

