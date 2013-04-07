using System;
using System.IO;

namespace Riddisk
{
	public class RiddiskKonsole
	{
		/// <summary>
		/// List of logged files
		/// </summary>
		static System.Collections.Specialized.StringCollection log;
		
		/// <summary>
		/// Flag if restricted access files were already written to output, used no to do it twice
        /// like were it's finishing and user clicks Ctrl+C
		/// </summary>
		static bool filesAlreadyWritten;
		
		static bool followSymbolicLinks;
		
		/// <summary>
		/// Writes the out restriced files from log list.
		/// </summary>
		static void WriteOutRestricedFiles()
		{
			if (!filesAlreadyWritten) 
			{	
				filesAlreadyWritten = true;
				Console.WriteLine ("Files with restricted access:");
				foreach (string s in log) {
					Console.WriteLine (s);
				}				
			}
		}
		
		/// <summary>
		/// Determines if given path is a reparse point (link)
		/// </summary>
		/// <returns><c>true</c> if specified path is a reparse point; otherwise, <c>false</c>.</returns>
		/// <param name="path">Path to file/directory</param>
		static bool IsReparsePoint(string path)
		{
			FileAttributes attributes = File.GetAttributes(path);
			return ((attributes & FileAttributes.ReparsePoint) == FileAttributes.ReparsePoint);
		}
		
		/// <summary>
		/// The usage string.
		/// </summary>
		static string usageString = "usage: riddisk-konsole.exe [-l] path; " + 
			Environment.NewLine + Environment.NewLine +
			"-l: follow symbolic links";
		
		/// <summary>
		/// The entry point of the program, where the program control starts and ends.
		/// </summary>
		/// <param name="args">The command-line arguments.</param>
		static void Main(string[] args)
		{
			filesAlreadyWritten = false;
			followSymbolicLinks = false;
			string path = string.Empty;
			
			log = new System.Collections.Specialized.StringCollection();
			
			// Register Ctrl+C handling
			Console.CancelKeyPress += delegate {
				WriteOutRestricedFiles();
			};

			switch (args.Length) 
			{
			case 1:
				path = args[0];
				break;
			case 2:
				if (args[0] == "-l")
				{
					followSymbolicLinks = true;
					path = args[1];
					break;
				}
				goto default;
			default:
				Console.WriteLine (usageString);
				return;
			}

			System.IO.DirectoryInfo rootDir = new DirectoryInfo (path);
			WalkDirectoryTree(rootDir);

			// Write out all the files that could not be processed.
			WriteOutRestricedFiles ();
			/*
#if DEBUG			
			// Keep the console window open in debug mode.
			Console.WriteLine("Press any key");
			Console.ReadKey();
#endif		*/	
		}
		
		/// <summary>
		/// Walks the directory tree & reads files.
		/// </summary>
		/// <param name="root">Root folder.</param>
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
					// If not following symbolic links & finding symbolic link: omit!
					if (!followSymbolicLinks && IsReparsePoint(fi.FullName))
					{
						continue;
					}

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
//					Console.Write(String.Format ("Dir {0}: ", dirInfo.FullName));
					// If following symbolic links or this not & this is a link.
					//if (followSymbolicLinks || (!followSymbolicLinks && !IsReparsePoint(dirInfo.FullName)))
					bool isreparse = IsReparsePoint(dirInfo.FullName);
					bool follow = followSymbolicLinks;
					
						// Resursive call for each subdirectory.
					
					if (follow || (!follow && !isreparse))
						WalkDirectoryTree(dirInfo);
				}
			}  
		}
	}
}
