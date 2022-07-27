using System.IO;
using System.Threading.Tasks;
using Vocup.IO.Vhf1;
using Vocup.IO.Vhf2;

namespace Vocup.IO;

public abstract class BookFileFormat
{
    public static BookFileFormat Vhf1 { get; } = Vhf1Format.Instance;
    public static BookFileFormat Vhf2 { get; } = Vhf2Format.Instance;
    internal abstract ValueTask<BookContext> ReadBookAsync(FileStream stream, string? vhrPath);
    internal abstract ValueTask WriteBookAsync(BookContext bookContext, string? vhrPath);
}
