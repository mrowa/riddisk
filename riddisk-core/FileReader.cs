using System;

namespace Riddisk.Core
{
    /// <summary>
    /// Result if file reading was successful or not and in what way.
    /// </summary>
    public enum ReadingResult { OK, IO_Error, Auth_Error };

    /// <summary>
    /// Reads file to Stream.Null.
    /// </summary>
    public class FileReader
    {
        /// <summary>
        /// Gets or sets the filename to be read.
        /// </summary>
        /// <value>The filename.</value>
        public string Filename { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Riddisk.Core.FileReader"/> class.
        /// </summary>
        public FileReader ()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Riddisk.Core.FileReader"/> class. Sets the filename to read.
        /// </summary>
        /// <param name="filename">Filename.</param>
        public FileReader(string filename)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Reads given file to Stream.Null.
        /// </summary>
        /// <returns>Result of reading.</returns>
        public ReadingResult ReadToNull()
        {
            throw new NotImplementedException();
        }
    }
}


