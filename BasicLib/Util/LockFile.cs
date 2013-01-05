using System;
using System.IO;
using System.Threading;

namespace BasicLib.Util{
	/// <summary>
	/// Class for creation and tracking of a lock-file, which can be used by different
	/// processes to synchronyze their actions on a single directory. 
	/// 
	/// <code>
	/// LockFile lock = new LockFile("d:\\test\\");
	/// try {
	///		// wait indefinite for the lock
	///		lock.Lock(-1);
	/// 
	///		// run functionality here
	/// } catch (Exception e) {
	///		;
	/// }
	/// 
	/// // release our lock when it exists.
	/// lock.Release();
	/// </code>
	/// </summary>
	public class LockFile{
		private FileStream handle;
		private readonly Random random;
		private readonly string lockFilePath;
		// constructor(s)
		/// <summary>
		/// Constructs a new instance of a lock-file in the given path. After this the actual lock-file will not
		/// have been created, which needs to be done with a call to <see cref="Lock"/>. When the directory does
		/// not exist it is created.
		/// </summary>
		/// <param name="path">The path where the lock-file is to be written.</param>
		public LockFile(string path){
			handle = null;
			lockFilePath = path + "\\" + ".lock";
			random = new Random();
			Directory.CreateDirectory(path);
		}

		// implementation
		/// <summary>
		/// Creates the actual lock-file, securing exclusive usage of the required resources in a multi-process
		/// system. A maximum waiting time can be set to wait for gaining the lock on the file, which is set
		/// to infinity with the value -1 (ie the process waits indefinitely).
		/// </summary>
		/// <returns>True when the lock has succeeded, false otherwise.</returns>
		public void Lock(){
			Thread.Sleep(random.Next(1, 5000));
			do{
				try{
					handle = File.Create(lockFilePath, 1024, FileOptions.DeleteOnClose | FileOptions.Asynchronous);
				} catch (Exception){}
				Thread.Sleep(5000);
			} while (handle == null);
		}

		/// <summary>
		/// Releases the lock-file so other processes can grab the resources.
		/// </summary>
		public void Release(){
			handle.Close();
			handle = null;
		}
	}
}