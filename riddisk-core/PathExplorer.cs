using System;
using System.Collections.Specialized;
using System.Collections.Generic;

namespace Riddisk.Core
{
    /// <summary>
    /// Class for object letting querying of all paths or all paths per directories,
    /// from directory tree to linear queries.
    /// </summary>
    public class PathExplorer
    {
        /// <summary>
        /// Gets or sets the root path for directory/file exploration.
        /// </summary>
        /// <value>The root path.</value>
        public string RootPath { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Riddisk.Core.PathExplorer"/> class.
        /// </summary>
        public PathExplorer ()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Riddisk.Core.PathExplorer"/> class. Sets RootPath.
        /// </summary>
        /// <param name="rootPath">Root path.</param>
        public PathExplorer(string rootPath)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets all possible file paths from initialized root path.
        /// </summary>
        /// <returns>All file paths.</returns>
        public StringCollection GetAllFilePaths()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Enumerates all the subdirectories.
        /// </summary>
        /// <returns>The subdirectories.</returns>
        public IEnumerable<string> EnumerateSubdirectories()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Enumerates all file paths.
        /// </summary>
        /// <returns>Enumerator for all file paths.</returns>
        public IEnumerable<string> EnumerateFilePaths()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Enumerates subdirectories by returning 1. Directory, 2. Filepaths from directory.
        /// </summary>
        /// <returns>The directories and files per directories.</returns>
        public IEnumerable<KeyValuePair<string, StringCollection>> EnumerateFilesPerDirectories()
        {
            throw new NotImplementedException();
        }
    }
}


