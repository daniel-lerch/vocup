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
    public class Vhf2ReaderTests
    {
        [TestMethod]
        public async Task TestReadBookAsync()
        {
            Book book;
            using (var stream = new FileStream(Path.Join("Resources", "Year 12.vhf"), FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                book = await new Vhf2Reader().ReadBookAsync(stream).ConfigureAwait(false);
            }

            Assert.AreEqual(1, book.Words.Count);
            Assert.AreEqual(2, book.Words[0].MotherTongue.Count);
            Assert.AreEqual(2, book.Words[0].ForeignLanguage.Count);
        }
    }
}
