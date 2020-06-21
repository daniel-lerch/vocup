using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Vocup.Models;

namespace Vocup.IO
{
    public abstract class BookReader
    {
        public abstract Task<Book> ReadBookAsync(Stream stream);
    }
}
