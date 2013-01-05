using System;
using System.Diagnostics;
using System.Threading;

namespace BasicLib.Util{
	public class Logger{
		public static LogLevel loglevel = LogLevel.Info;
		private static string Prefix { get { return string.Format(" [P{0}-T{1}] ", Process.GetCurrentProcess().Id, Thread.CurrentThread.ManagedThreadId); } }

		public static void Debug(string classname, string message){
			if (loglevel >= LogLevel.Debug){
				Console.Out.WriteLine(DateTime.Now.ToUniversalTime() + Prefix + "(DEBUG) - " + classname + ": " + message);
			}
		}

		public static void Info(string classname, string message){
			if (loglevel >= LogLevel.Info){
				Console.Out.WriteLine(DateTime.Now.ToUniversalTime() + Prefix + "(INFO) - " + classname + ": " + message);
			}
		}

		public static void Error(string classname, string message){
			if (loglevel >= LogLevel.Error){
				Console.Error.WriteLine(DateTime.Now.ToUniversalTime() + Prefix + "(ERROR) - " + classname + ": " + message);
			}
		}

		public static void Error(string classname, Exception ex){
			if (loglevel >= LogLevel.Error){
				Console.Error.WriteLine(DateTime.Now.ToUniversalTime() + Prefix + "(ERROR) - " + classname + ": " + ex + "\n" +
					ex.StackTrace);
			}
		}

		public static void Warn(string classname, string message){
			if (loglevel >= LogLevel.Warn){
				Console.Out.WriteLine(DateTime.Now.ToUniversalTime() + Prefix + "(WARN) - " + classname + ": " + message);
			}
		}

		public static void Warn(string classname, Exception ex){
			if (loglevel >= LogLevel.Warn){
				Console.Out.WriteLine(DateTime.Now.ToUniversalTime() + Prefix + "(WARN) - " + classname + ": " + ex);
			}
		}
	}
}