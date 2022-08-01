using System.IO;
using System.Threading.Tasks;
using Vocup.IO.Vhf1;
using Vocup.IO.Vhf2;
using Vocup.Models;

namespace Vocup.IO;

public abstract class BookFileFormat
{
    public static BookFileFormat Vhf1 { get; } = Vhf1Format.Instance;
    public static BookFileFormat Vhf2 { get; } = Vhf2Format.Instance;
    internal abstract ValueTask<(Book book, string? vhrCode)> ReadBookAsync(Stream stream, string? fileName, string? vhrPath);
    internal abstract ValueTask WriteBookAsync(Book book, Stream stream, string? fileName, string? vhrCode, string? vhrPath);
}
