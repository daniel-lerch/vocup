using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Vocup.IO;
using Vocup.Models;

namespace Vocup.Core.UnitTests.IO
{
    [TestClass]
    public class Vhf1ReaderTests
    {
        [TestMethod]
        public async Task TestReadBookAsync()
        {
            Book book;
            using (var stream = new FileStream(Path.Join("Resources", "Year 11.vhf"), FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                book = await new Vhf1Reader().ReadBookAsync(stream).ConfigureAwait(false);
            }

            Assert.AreEqual(113, book.Words.Count);
        }
    }
}
