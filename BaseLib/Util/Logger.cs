using System;
using System.Diagnostics;
using System.Threading;

namespace BaseLib.Util{
	public class Logger{
		public static LogLevel loglevel = LogLevel.Info;
		private static string Prefix { get { return string.Format(" [P{0}-T{1}] ", Process.GetCurrentProcess().Id, Thread.CurrentThread.ManagedThreadId); } }

		public static void Debug(string className, string message){
			if (loglevel >= LogLevel.Debug){
				Console.Out.WriteLine(DateTime.Now.ToUniversalTime() + Prefix + "(DEBUG) - " + className + ": " + message);
			}
		}

		public static void Info(string className, string message){
			if (loglevel >= LogLevel.Info){
				Console.Out.WriteLine(DateTime.Now.ToUniversalTime() + Prefix + "(INFO) - " + className + ": " + message);
			}
		}

		public static void Error(string className, string message){
			if (loglevel >= LogLevel.Error){
				Console.Error.WriteLine(DateTime.Now.ToUniversalTime() + Prefix + "(ERROR) - " + className + ": " + message);
			}
		}

		public static void Error(string className, Exception ex){
			if (loglevel >= LogLevel.Error){
				Console.Error.WriteLine(DateTime.Now.ToUniversalTime() + Prefix + "(ERROR) - " + className + ": " + ex + "\n" +
					ex.StackTrace);
			}
		}

		public static void Warn(string className, string message){
			if (loglevel >= LogLevel.Warn){
				Console.Out.WriteLine(DateTime.Now.ToUniversalTime() + Prefix + "(WARN) - " + className + ": " + message);
			}
		}

		public static void Warn(string className, Exception ex){
			if (loglevel >= LogLevel.Warn){
				Console.Out.WriteLine(DateTime.Now.ToUniversalTime() + Prefix + "(WARN) - " + className + ": " + ex);
			}
		}
	}
}