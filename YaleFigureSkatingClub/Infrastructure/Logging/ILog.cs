using System;

namespace YaleFigureSkatingClub.Infrastructure.Logging
{
	public interface ILog
	{
		void Fatal(string message);
		void Error(string message);
		void Warn(string message);
		void Info(string message);
		void Debug(string message);
		
		void FatalFormat(string message, params string[] objects);
		void ErrorFormat(string message, params string[] objects);
		void WarnFormat(string message, params string[] objects);
		void InfoFormat(string message, params string[] objects);
		void DebugFormat(string message, params string[] objects);
	}
	
	public enum LogLevel {
		Fatal,
		Error,
		Warn,
		Info,
		Debug
	}
}