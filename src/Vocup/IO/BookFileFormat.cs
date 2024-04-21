using System;
using System.IO;
using System.Threading.Tasks;
using Vocup.IO.Vhf1;
using Vocup.IO.Vhf2;
using Vocup.Models;
using Vocup.Util;

namespace Vocup.IO;

public abstract class BookFileFormat
{
    public static Vhf1Format Vhf1 { get; } = Vhf1Format.Instance;
    public static Vhf2Format Vhf2 { get; } = Vhf2Format.Instance;

    public static async ValueTask DetectAndRead(string path, VocabularyBook book, string vhrPath)
    {
        await default(HopToThreadPoolAwaitable); // Perform IO operations on a separate thread

        using FileStream stream = new(path, FileMode.Open, FileAccess.Read, FileShare.Read);

        if (await StartsWithZipHeader(stream).ConfigureAwait(false))
            await Vhf2Format.Instance.Read(stream, book).ConfigureAwait(false);
        else
            await Vhf1Format.Instance.Read(stream, book, vhrPath).ConfigureAwait(false);
    }

    private static async ValueTask<bool> StartsWithZipHeader(Stream stream)
    {
        if (!stream.CanRead || !stream.CanSeek)
            throw new ArgumentException("Stream must be readable and seekable.", nameof(stream));

        Memory<byte> buffer = new byte[4];
        bool zipHeader = await stream.ReadAsync(buffer).ConfigureAwait(false) == 4
            && buffer.Span[0] == 0x50
            && buffer.Span[1] == 0x4B
            && buffer.Span[2] == 0x03
            && buffer.Span[3] == 0x04;

        stream.Seek(0, SeekOrigin.Begin);
        return zipHeader;
    }
}
