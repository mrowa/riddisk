using System;
using NUnit.Framework;

namespace Riddisk.Test
{
    /// <summary>
    /// Test simple parts of Riddisk.Core.PathExplorer.
    /// </summary>
    [TestFixture]
    public class PathExplorerTests
    {
        /// <summary>
        /// Check if filenames is saved by constructor.
        /// </summary>
        [Test]
        public void FilenameIsSavedByConstructor ()
        {
            const string mockedRootPath = "fd4ff6dd-51b3-4ccc-8fdf-3f53485b03c1";
            Riddisk.Core.PathExplorer pathExplorer = new Riddisk.Core.PathExplorer (mockedRootPath);
            Assert.AreEqual (pathExplorer.RootPath, mockedRootPath);
        }

        /// <summary>
        /// Check if constructor does not let null string.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorDoesNotLetNullString()
        {
            new Riddisk.Core.PathExplorer (null);
        }

        /// <summary>
        /// Check if throws InvalidOperationException when run uninitialized.
        /// </summary>
        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CannotGetAllFilePathsBeforeInitialization ()
        {
            new Riddisk.Core.PathExplorer ().GetAllFilePaths ();
        }

        /// <summary>
        /// Check if throws InvalidOperationException when run uninitialized.
        /// </summary>
        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CannotEnumerateSubdirectoriesBeforeInitialization ()
        {
            new Riddisk.Core.PathExplorer ().EnumerateSubdirectories ();
        }

        /// <summary>
        /// Check if throws InvalidOperationException when run uninitialized.
        /// </summary>
        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CannotEnumerateFilePathsBeforeInitialization ()
        {
            new Riddisk.Core.PathExplorer ().EnumerateFilePaths ();
        }

        /// <summary>
        /// Check if throws InvalidOperationException when run uninitialized.
        /// </summary>
        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CannotEnumerateFilesPerDirectoriesBeforeInitialization ()
        {
            new Riddisk.Core.PathExplorer ().EnumerateFilesPerDirectories ();
        }
        
    }
}


