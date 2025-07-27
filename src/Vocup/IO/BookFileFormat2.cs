using Avalonia.Platform.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Vocup.Models;

namespace Vocup.IO;

public abstract class BookFileFormat2
{
    public static Vhf1Format2 Vhf1 { get; } = Vhf1Format2.Instance;
    public static Vhf2Format2 Vhf2 { get; } = Vhf2Format2.Instance;

    public static async ValueTask DetectAndRead(IStorageFile file, Book book, string? vhrPath)
    {
        // Despite its name, IStorageFile.OpenReadAsync is often a blocking operation
        using Stream stream = await Task.Run(file.OpenReadAsync);

        if (await StartsWithZipHeader(stream).ConfigureAwait(false))
        {
            await Vhf2Format2.Instance.Read(stream, book).ConfigureAwait(false);
        }
        else
        {
            await Vhf1Format2.Instance.Read(stream, book, vhrPath).ConfigureAwait(false);
        }
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

    protected abstract ValueTask Write(Stream stream, Book book, string vhrPath, bool includeResults);

    protected static bool TryDeleteVhrFile(string? vhrCode, string vhrPath)
    {
        try
        {
            if (vhrCode != null)
            {
                string path = Path.Combine(vhrPath, vhrCode + ".vhr");

                if (File.Exists(path))
                {
                    File.Delete(path);
                    return true;
                }
            }
        }
        // Cleaning up practice results is just nice to have.
        catch { }

        return false;
    }

    protected static List<Practice> GeneratePracticeHistory(int practiceStateNumber, DateTime practiceDate, bool coveredByPracticeMode)
    {
        if (practiceStateNumber <= 0 || !coveredByPracticeMode)
        {
            return [];
        }
        else if (practiceStateNumber == 1)
        {
            return [new Practice(practiceDate, PracticeResult2.Wrong)];
        }
        else
        {
            List<Practice> practices = new(capacity: practiceStateNumber - 1);
            for (int i = 0; i < practiceStateNumber - 1; i++)
            {
                practices.Add(new Practice(practiceDate, PracticeResult2.Correct));
            }
            return practices;
        }
    }
}
