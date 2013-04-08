using System;
using NUnit.Framework;

namespace Riddisk.Test
{
    /// <summary>
    /// Test simple parts of Riddisk.Core.FileReader.
    /// </summary>
    [TestFixture()]
    public class FileReaderTests
    {
        /// <summary>
        /// Check if filename is saved by the constructor.
        /// </summary>
        [Test()]
        public void FilenameIsSavedByConstructor ()
        {
            const string mockedFilename = "fd4ff6dd-51b3-4ccc-8fdf-3f53485b03c1";
            Riddisk.Core.FileReader fileReader = new Riddisk.Core.FileReader (mockedFilename);
            Assert.AreEqual (fileReader.Filename, mockedFilename);
        }

        /// <summary>
        /// Check if constructor does not let null string as filepath.
        /// </summary>
        [Test()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorDoesNotLetNullString()
        {
            new Riddisk.Core.FileReader (null);
        }

        /// <summary>
        /// Check if throws InvalidOperationException when run uninitialized.
        /// </summary>
        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CannotReadFileWithoutFilenameInitialization()
        {
            const string mockedFilename = "ec5bf806-c11d-4c4c-bfdf-1b0a7e249915";
            Riddisk.Core.FileReader fileReader = new Riddisk.Core.FileReader ();
            fileReader.ReadToNull ();
        }
    }
}


