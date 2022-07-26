using System.IO;
using System.Threading.Tasks;

namespace Vocup.IO;

public abstract class BookFileFormat
{
    internal abstract ValueTask<BookContext> ReadBookAsync(FileStream stream, string? vhrPath);
    internal abstract ValueTask WriteBookAsync(BookContext bookContext, string? vhrPath);
}
