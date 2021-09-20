using System;
using System.IO;
using System.Threading.Tasks;
using Vocup.Models;

namespace Vocup.IO
{
    public class BookStorage
    {
        private readonly Vhf1Serializer vhf1Serializer;
        private readonly Vhf2Serializer vhf2Serializer;

        public BookStorage()
        {
            vhf1Serializer = new Vhf1Serializer();
            vhf2Serializer = new Vhf2Serializer();
        }

        public BookSerializer GetSerializer(BookFileFormat fileFormat)
        {
            return fileFormat switch
            {
                BookFileFormat.Vhf_1_0 => vhf1Serializer,
                BookFileFormat.Vhf_2_0 => vhf2Serializer,
                _ => throw new ArgumentException("Unsupported file format")
            };
        }

        public async Task<Book> ReadBookAsync(string path, string? vhrPath)
        {
            await default(HopToThreadPoolAwaitable);
            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                byte[] buffer = new byte[4];
                bool zipHeader = await stream.ReadAsync(buffer, 0, 4).ConfigureAwait(false) == 4
                    && buffer[0] == 0x50
                    && buffer[1] == 0x4B
                    && buffer[2] == 0x03
                    && buffer[3] == 0x04;
                stream.Seek(0, SeekOrigin.Begin);

                var serializer = zipHeader ? (BookSerializer)vhf2Serializer : vhf1Serializer;

                return await serializer.ReadBookAsync(stream, vhrPath).ConfigureAwait(false);
            }
        }

        public async Task WriteBookAsync(string path, Book book, string? vhrPath)
        {
            if (book == null) throw new ArgumentNullException(nameof(book));
            if (!book.PracticeMode.IsValid()) throw new ArgumentOutOfRangeException(nameof(book), "Invalid pratice PracticeMode");

            await default(HopToThreadPoolAwaitable);
            book.Serializer ??= vhf2Serializer;

            using var stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
            await book.Serializer.WriteBookAsync(stream, book, vhrPath).ConfigureAwait(false);
        }
    }
}
