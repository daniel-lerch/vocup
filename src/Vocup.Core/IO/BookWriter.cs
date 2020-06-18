using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Vocup.Models;

namespace Vocup.IO
{
    internal abstract class BookWriter
    {
        public abstract Task WriteBookAsync(Stream stream, Book book);
    }
}
