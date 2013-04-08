using System;
using NUnit.Framework;

namespace Riddisk.Test
{
    [TestFixture()]
    public class IrrelevantTests
    {
        [Test()]
        public void ThisOneSucceeds ()
        {
            Assert.AreEqual (true, true);
            Assert.AreNotEqual (true, false);
        }

        [Test()]
        public void ThisOneFails()
        {
            Assert.AreEqual (true, false);
        }
    }
}


