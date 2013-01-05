using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading;

namespace BasicLib.Util{
	public enum WorkType{
		Process,
		Task
	}

	public static class StreamHandler{
		private const string classname = "StreamHandler";

		private static StreamWriter GetStreamWriter(string filename){
			if (filename == null){
				return null;
			}
			try{
				return new StreamWriter(filename, true);
			} catch (Exception ex){
				Logger.Warn(classname, "Can not create StreamWriter for file " + filename + "\n" + ex);
				return null;
			}
		}

		public static void ErrorHandler(object sender, DataReceivedEventArgs args){
			if (!string.IsNullOrEmpty(args.Data)){
				Console.Error.WriteLine(args.Data.Trim());
			}
		}

		public static void OutputHandler(object sender, DataReceivedEventArgs args){
			if (!string.IsNullOrEmpty(args.Data)){
				Console.Out.WriteLine(args.Data.Trim());
			}
		}

		public static void StartLog(string infoFolder, string name, int id, string title, string description,
			WorkType workType, DateTime starttime){
			if (string.IsNullOrEmpty(infoFolder)){
				Logger.Warn(classname, "function parameter infoFolder is null or empty.");
				return;
			}
			if (workType == WorkType.Process){
				id = Process.GetCurrentProcess().Id;
				name += "_" + Environment.GetEnvironmentVariable("COMPUTERNAME").ToLower() + "_" + id;
			}
			string i = id.ToString(CultureInfo.InvariantCulture);
			if (workType == WorkType.Task){
				string line = ReadJobId(infoFolder);
				if (line != null){
					i = line + "." + id;
				}
			}
			string filename = GetStatusFile(name, infoFolder) + ".started.txt";
			TextWriter writer = GetStreamWriter(filename);
			try{
				if (writer != null){
					writer.WriteLine("type\t" + workType);
					writer.WriteLine("id\t" + i);
					writer.WriteLine("machine\t" + Environment.GetEnvironmentVariable("COMPUTERNAME").ToLower());
					writer.WriteLine("start\t" + starttime.ToString(FileUtils.dateFormat));
					writer.WriteLine("title\t" + title);
					writer.WriteLine("description\t" + description);
					writer.Flush();
					writer.Close();
					writer = null;
				}
			} catch (IOException){
				Thread.Sleep(1000);
				// try again because file is in use                
				// StartLog(title, title, description, workType, starttime);
			} catch (Exception ex){
				Exception e = new Exception("Can not write StatusLog to file " + filename, ex);
				Logger.Error("StreamHandler", e);
				throw e;
			} finally{
				if (writer != null){
					writer.Close();
				}
			}
		}

		private static string ReadJobId(string infoFolder){
			try{
				string jobfile = Path.Combine(infoFolder, "job");
				DateTime start = DateTime.Now;
				while (!File.Exists(jobfile) && (DateTime.Now - start).Minutes < 2){
					Thread.Sleep(5000);
				}
				StreamReader reader = new StreamReader(new FileStream(jobfile, FileMode.Open, FileAccess.Read, FileShare.Read));
				string line = reader.ReadLine().Trim();
				reader.Dispose();
				return line;
			} catch (Exception){}
			return null;
		}

		public static void EndLog(string infoFolder, string name, int id, string title, string description, WorkType workType,
			DateTime starttime, DateTime endtime){
			if (string.IsNullOrEmpty(infoFolder)){
				Logger.Warn(classname, "function parameter infoFolder is null or empty.");
				return;
			}
			if (workType == WorkType.Process){
				id = Process.GetCurrentProcess().Id;
				name += "_" + Environment.GetEnvironmentVariable("COMPUTERNAME").ToLower() + "_" + id;
			}
			string i = id.ToString(CultureInfo.InvariantCulture);
			if (workType == WorkType.Task){
				string job = ReadJobId(infoFolder);
				if (job != null){
					i = job + "." + id;
				}
				string line = GetLine(i, "Finished");
				Thread thread = new Thread(WriteAllocatedNodes);
				thread.Start(line);
			}
			string filename = GetStatusFile(name, infoFolder) + ".finished.txt";
			StreamWriter writer;
			try{
				writer = GetStreamWriter(filename);
			} catch (Exception){
				Thread.Sleep(5000);
				try{
					writer = GetStreamWriter(filename);
				} catch (Exception){
					return;
				}
			}
			try{
				if (writer != null){
					writer.WriteLine("type\t" + workType);
					writer.WriteLine("id\t" + i);
					writer.WriteLine("machine\t" + Environment.GetEnvironmentVariable("COMPUTERNAME").ToLower());
					writer.WriteLine("start\t" + starttime.ToString(FileUtils.dateFormat));
					writer.WriteLine("title\t" + title);
					writer.WriteLine("description\t" + description);
					writer.WriteLine("end\t" + endtime.ToString(FileUtils.dateFormat));
					writer.Flush();
					writer.Close();
					writer = null;
				}
			} catch (IOException){
				Thread.Sleep(1000);
				// try again because file is in use                
				// StartLog(title, title, description, workType, starttime);
			} catch (Exception ex){
				Exception e = new Exception("Can not write StatusLog to file " + filename, ex);
				Logger.Error("StreamHandler", e);
				throw e;
			} finally{
				if (writer != null){
					writer.Close();
				}
			}
			DeleteStartedFile(infoFolder, name);
		}

		public static void ErrorLog(string infoFolder, string name, int id, string title, string description,
			WorkType workType, DateTime starttime, DateTime endtime, string error){
			if (string.IsNullOrEmpty(infoFolder)){
				Logger.Warn(classname, "function parameter infoFolder is null or empty.");
				return;
			}
			if (workType == WorkType.Process){
				id = Process.GetCurrentProcess().Id;
				name += "_" + Environment.GetEnvironmentVariable("COMPUTERNAME").ToLower() + "_" + id;
			}
			string i = id.ToString(CultureInfo.InvariantCulture);
			if (workType == WorkType.Task){
				string job = ReadJobId(infoFolder);
				if (job != null){
					i = job + "." + id;
				}
				string line = GetLine(i, "Failed");
				Thread thread = new Thread(WriteAllocatedNodes);
				thread.Start(line);
			}
			string filename = GetStatusFile(name, infoFolder) + ".error.txt";
			StreamWriter writer;
			try{
				writer = GetStreamWriter(filename);
			} catch (Exception){
				Thread.Sleep(5000);
				try{
					writer = GetStreamWriter(filename);
				} catch (Exception){
					return;
				}
			}
			try{
				if (writer != null){
					writer.WriteLine("type\t" + workType);
					writer.WriteLine("id\t" + i);
					writer.WriteLine("machine\t" + Environment.GetEnvironmentVariable("COMPUTERNAME").ToLower());
					writer.WriteLine("start\t" + starttime.ToString(FileUtils.dateFormat));
					writer.WriteLine("title\t" + title);
					writer.WriteLine("description\t" + description);
					writer.WriteLine("error\t" + error);
					writer.WriteLine("end\t" + endtime.ToString(FileUtils.dateFormat));
					writer.Flush();
					writer.Close();
					writer = null;
				}
			} catch (Exception ex){
				Exception e = new Exception("Can not write ErrorLog to file " + filename, ex);
				Logger.Error("StreamHandler", e);
				throw e;
			} finally{
				if (writer != null){
					writer.Close();
				}
			}
			DeleteStartedFile(infoFolder, name);
		}

		private static void DeleteStartedFile(string infoFolder, string name){
			string started = GetStatusFile(name, infoFolder) + ".started.txt";
			try{
				FileUtils.DeleteFile(started);
			} catch (Exception){
				Thread.Sleep(5000);
				try{
					FileUtils.DeleteFile(started);
				} catch (Exception){}
			}
		}

		private static string GetLine(string id, string state){
			return id + "\t" + Process.GetCurrentProcess().ProcessName + "\t" + Environment.UserName + "\t" +
				DateTime.Now.ToString(FileUtils.dateFormat) + "\t" + DateTime.Now.ToString(FileUtils.dateFormat) + "\t" + state +
				"\n";
		}

		private static void WriteAllocatedNodes(object line){
			string path = null;
			try{
				path = Path.Combine("I:\\gpfs\\gpfs-mann\\cluster",
					Environment.GetEnvironmentVariable("COMPUTERNAME").ToLower() + ".txt");
				StreamWriter writer = new StreamWriter(path, true);
				writer.Write(line);
				writer.Flush();
				writer.Close();
			} catch (Exception){
				Logger.Warn(classname, "Could not write to " + path);
			}
		}

		private static string GetStatusFile(string name, string infoFolder){
			if (string.IsNullOrEmpty(infoFolder)){
				throw new Exception("Given string for proc folder is null or empty.");
			}
			if (!Directory.Exists(infoFolder)){
				throw new Exception("Given path for proc folder does not exist. Path=" + infoFolder);
			}
			name = StringUtils.Replace(name, new[]{"\\", "(", ")", "/"}, "");
			return Path.Combine(infoFolder, name);
		}
	}
}