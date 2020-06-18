using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Vocup.Models;

namespace Vocup.IO
{
    internal class Vhf2Writer : BookWriter
    {
        public override Task WriteBookAsync(Stream stream, Book book)
        {
            throw new NotImplementedException();
        }
    }
}
