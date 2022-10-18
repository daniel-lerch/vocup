using System;
using System.IO;
using System.Threading.Tasks;
using Vocup.IO.Vhf1;
using Vocup.Models;
using Vocup.Settings;
using Vocup.Util;

namespace Vocup.IO;

public class BookStorage
{
    public async ValueTask<BookContext> LoadAsync(string path, string? vhrPath, IVocupSettings settings)
    {
        await default(HopToThreadPoolAwaitable);

        using FileStream stream = new(path, FileMode.Open, FileAccess.Read, FileShare.Read);

        (Book book, BookFileFormat fileFormat, string? vhrCode) =
            await ReadBookAsync(stream, path, vhrPath).ConfigureAwait(false);

        return new BookContext(book, fileFormat, fileStream: null, vhrCode, settings);
    }

    public async ValueTask<BookContext> OpenAsync(string path, string? vhrPath, IVocupSettings settings)
    {
        await default(HopToThreadPoolAwaitable);

        // Leave this stream open to block the file as being used by Vocup
        FileStream stream = new (path, FileMode.Open, FileAccess.ReadWrite, FileShare.Read);

        (Book book, BookFileFormat fileFormat, string? vhrCode) =
            await ReadBookAsync(stream, path, vhrPath).ConfigureAwait(false);

        return new BookContext(book, fileFormat, stream, vhrCode, settings);
    }

    public ValueTask SaveAsync(BookContext bookContext, string? vhrPath)
    {
        if (bookContext.FileStream == null)
            throw new ArgumentNullException(nameof(bookContext) + "." + nameof(bookContext.FileStream), "You have to select a file before saving");

        return bookContext.FileFormat.WriteBookAsync(bookContext.Book, bookContext.FileStream, bookContext.FileStream.Name, bookContext.VhrCode, vhrPath);
    }

    public async ValueTask SaveAsync(BookContext bookContext, string path, string? vhrPath)
    {
        FileStream fileStream = new (path, FileMode.Create, FileAccess.Write, FileShare.Read);
        string? vhrCode = null;
        if (bookContext.FileFormat == BookFileFormat.Vhf1)
            vhrCode = Vhf1Format.Instance.GenerateVhrCode();

        await bookContext.FileFormat.WriteBookAsync(bookContext.Book, fileStream, path, vhrCode, vhrPath).ConfigureAwait(false);

        bookContext.VhrCode = vhrCode;

        if (bookContext.FileStream != null) 
            await bookContext.FileStream.DisposeAsync().ConfigureAwait(false);
        bookContext.FileStream = fileStream;
    }

    protected async ValueTask<(Book book, BookFileFormat fileFormat, string? vhrCode)> ReadBookAsync(Stream stream, string? fileName, string? vhrPath)
    {
        Memory<byte> buffer = new byte[4];
        bool zipHeader = await stream.ReadAsync(buffer).ConfigureAwait(false) == 4
            && buffer.Span[0] == 0x50
            && buffer.Span[1] == 0x4B
            && buffer.Span[2] == 0x03
            && buffer.Span[3] == 0x04;
        stream.Seek(0, SeekOrigin.Begin);

        BookFileFormat serializer = zipHeader ? BookFileFormat.Vhf2 : BookFileFormat.Vhf1;

        (Book book, string? vhrCode) = await serializer.ReadBookAsync(stream, fileName, vhrPath).ConfigureAwait(false);

        return (book, serializer, vhrCode);
    }
}
