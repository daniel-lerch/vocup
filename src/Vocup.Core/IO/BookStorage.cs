using System;
using System.IO;
using System.Threading.Tasks;
using Vocup.Util;

namespace Vocup.IO;

public class BookStorage
{
    public async ValueTask<BookContext> LoadAsync(string path, string? vhrPath)
    {
        await default(HopToThreadPoolAwaitable);

        using FileStream stream = new(path, FileMode.Open, FileAccess.ReadWrite, FileShare.Read);

        Memory<byte> buffer = new byte[4];
        bool zipHeader = await stream.ReadAsync(buffer).ConfigureAwait(false) == 4
            && buffer.Span[0] == 0x50
            && buffer.Span[1] == 0x4B
            && buffer.Span[2] == 0x03
            && buffer.Span[3] == 0x04;
        stream.Seek(0, SeekOrigin.Begin);

        BookFileFormat serializer = zipHeader ? BookFileFormat.Vhf2 : BookFileFormat.Vhf1;

        BookContext bookContext = await serializer.ReadBookAsync(stream, vhrPath).ConfigureAwait(false);
        bookContext.FileStream = null; // Hacky solution in order not to leave file open
        return bookContext;
    }

    public async ValueTask<BookContext> OpenAsync(string path, string? vhrPath)
    {
        await default(HopToThreadPoolAwaitable);

        // Leave this stream open to block the file as being used by Vocup
        FileStream stream = new (path, FileMode.Open, FileAccess.ReadWrite, FileShare.Read);

        Memory<byte> buffer = new byte[4];
        bool zipHeader = await stream.ReadAsync(buffer).ConfigureAwait(false) == 4
            && buffer.Span[0] == 0x50
            && buffer.Span[1] == 0x4B
            && buffer.Span[2] == 0x03
            && buffer.Span[3] == 0x04;
        stream.Seek(0, SeekOrigin.Begin);

        BookFileFormat serializer = zipHeader ? BookFileFormat.Vhf2 : BookFileFormat.Vhf1;

        return await serializer.ReadBookAsync(stream, vhrPath).ConfigureAwait(false);
    }

    public ValueTask SaveAsync(BookContext bookContext, string? vhrPath)
    {
        if (bookContext.FileFormat == null) 
            throw new ArgumentNullException(nameof(bookContext) + "." + nameof(bookContext.FileFormat), "You have to select a file before saving");

        return bookContext.FileFormat.WriteBookAsync(bookContext, vhrPath);
    }

    public ValueTask SaveAsync(BookContext bookContext, string path, BookFileFormat fileFormat, string? vhrPath)
    {
        bookContext.FileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Read);
        bookContext.FileFormat = fileFormat;

        return bookContext.FileFormat.WriteBookAsync(bookContext, vhrPath);
    }
}
