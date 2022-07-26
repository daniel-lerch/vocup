using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using Vocup.IO.Vhf1;
using Vocup.IO.Vhf2;
using Vocup.Util;

namespace Vocup.IO;

public class BookStorage
{
    private readonly Vhf1Format vhf1Format;
    private readonly Vhf2Format vhf2Format;

    public BookStorage()
    {
        vhf1Format = new Vhf1Format();
        vhf2Format = new Vhf2Format();
        FileFormats = new ReadOnlyCollection<BookFileFormat>(new BookFileFormat[] { vhf1Format, vhf2Format });
    }

    IReadOnlyList<BookFileFormat> FileFormats { get; }

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

        var serializer = zipHeader ? (BookFileFormat)vhf2Format : vhf1Format;

        return await serializer.ReadBookAsync(stream, vhrPath).ConfigureAwait(false);
    }

    public ValueTask SaveAsync(BookContext bookContext, string? vhrPath)
    {
        if (bookContext.FileFormat == null) 
            throw new ArgumentNullException(nameof(bookContext) + "." + nameof(bookContext.FileFormat), "You have to select a file before saving");

        return bookContext.FileFormat.WriteBookAsync(bookContext, vhrPath);
    }
}
