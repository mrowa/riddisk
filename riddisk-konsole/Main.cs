using System;
using System.IO;

namespace riddiskkonsole
{
	public class RecursiveFileSearch
	{
		static System.Collections.Specialized.StringCollection log = new System.Collections.Specialized.StringCollection();
		
		static void Main(string[] args)
		{
			Console.CancelKeyPress += delegate {
				Console.WriteLine("Files with restricted access:");
				foreach (string s in log)
				{
					Console.WriteLine(s);
				}
			};


			string path = string.Empty;

			if (args.Length < 1) {
				Console.WriteLine ("usage: riddisk-konsole.exe path");
				return;
			} else {
				path = args[0];
			}

			System.IO.DirectoryInfo rootDir = new DirectoryInfo (path);
			WalkDirectoryTree(rootDir);

			// Write out all the files that could not be processed.
			Console.WriteLine("Files with restricted access:");
			foreach (string s in log)
			{
				Console.WriteLine(s);
			}
			// Keep the console window open in debug mode.
			Console.WriteLine("Press any key");
			Console.ReadKey();
		}
		
		static void WalkDirectoryTree(System.IO.DirectoryInfo root)
		{
			System.IO.FileInfo[] files = null;
			System.IO.DirectoryInfo[] subDirs = null;

			Console.WriteLine ("Reading files in dir: " + root.FullName);
			
			// First, process all the files directly under this folder 
			try
			{
				files = root.GetFiles("*");
			}

			// This is thrown if even one of the files requires permissions greater 
			// than the application provides. 
			catch (UnauthorizedAccessException e)
			{
				// This code just writes out the message and continues to recurse. 
				// You may decide to do something different here. For example, you 
				// can try to elevate your privileges and access the file again.
				log.Add(e.Message);
			}
			
			catch (System.IO.DirectoryNotFoundException e)
			{
				Console.WriteLine(e.Message);
			}

			if (files != null)
			{
				foreach (System.IO.FileInfo fi in files)
				{
					// In this example, we only access the existing FileInfo object. If we 
					// want to open, delete or modify the file, then 
					// a try-catch block is required here to handle the case 
					// where the file has been deleted since the call to TraverseTree().
//					Console.WriteLine(fi.FullName);

					try
					{
						Stream stream = new System.IO.FileStream(fi.FullName, FileMode.Open, FileAccess.Read);
						stream.CopyTo(Stream.Null);
					}
					catch (IOException ioe)
					{
//						Console.WriteLine (String.Format ("File {0} had IO Error: {1}.", fi.FullName, ioe.Message));
						log.Add (ioe.Message);
					}
					catch (UnauthorizedAccessException uae)
					{
//						Console.WriteLine (String.Format ("File {0} had unauthorized access Error: {1}.", fi.FullName, uae.Message));
						log.Add (uae.Message);
					}
				}
				
				// Now find all the subdirectories under this directory.
				subDirs = root.GetDirectories();
				
				foreach (System.IO.DirectoryInfo dirInfo in subDirs)
				{
					// Resursive call for each subdirectory.
					WalkDirectoryTree(dirInfo);
				}
			}  
		}
	}
}
