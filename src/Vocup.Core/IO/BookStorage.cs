using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Vocup.Models;

namespace Vocup.IO
{
    public class BookStorage
    {
        public async Task<Book> ReadBookAsync(string path)
        {
            await default(HopToThreadPoolAwaitable);
            var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            byte[] buffer = new byte[4];
            bool zipHeader = await stream.ReadAsync(buffer, 0, 4).ConfigureAwait(false) == 4
                && buffer[0] == 0x50
                && buffer[1] == 0x4B
                && buffer[2] == 0x03
                && buffer[3] == 0x04;
            stream.Seek(0, SeekOrigin.Begin);

            if (!zipHeader)
                return await new Vhf1Serializer().ReadBookAsync(stream).ConfigureAwait(false);
            else
                return await new Vhf2Serializer().ReadBookAsync(stream).ConfigureAwait(false);
        }

        public async Task WriteBookAsync(string path, Book book)
        {
            if (book == null) throw new ArgumentNullException(nameof(book));
            await default(HopToThreadPoolAwaitable);
            var stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);

            if (book.FileVersion == new Version(1, 0))
                await new Vhf1Serializer().WriteBookAsync(stream, book).ConfigureAwait(false);
            else if (book.FileVersion == new Version(2, 0))
                await new Vhf2Serializer().WriteBookAsync(stream, book).ConfigureAwait(false);
            else
                throw new NotSupportedException($"Cannot write a vocabulary book {book.FileVersion}");
        }
    }
}
