using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Vocup.Models;

namespace Vocup.IO
{
    public class Vhf2Reader : BookReader
    {
        public override Task<Book> ReadBookAsync(Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}
