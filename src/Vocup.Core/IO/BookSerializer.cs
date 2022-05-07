using System.IO;
using System.Threading.Tasks;
using Vocup.Models;

namespace Vocup.IO
{
    public abstract class BookSerializer
    {
        public abstract BookFileFormat FileFormat { get; }
        internal abstract Task<Book> ReadBookAsync(FileStream stream, string? vhrPath);
        internal abstract Task WriteBookAsync(FileStream stream, Book book, string? vhrPath);
    }
}
