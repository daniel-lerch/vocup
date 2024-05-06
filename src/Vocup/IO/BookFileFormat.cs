using System;
using System.IO;
using System.Windows;
using Vocup.Models;
using Vocup.Properties;

namespace Vocup.IO;

public abstract class BookFileFormat
{
    public static Vhf1Format Vhf1 { get; } = Vhf1Format.Instance;
    public static Vhf2Format Vhf2 { get; } = Vhf2Format.Instance;

    public static bool TryDetectAndRead(string path, VocabularyBook book, string vhrPath)
    {
        try
        {
            if (!DetectAndRead(path, book, vhrPath))
                MessageBox.Show(Messages.VhfCompatMode, Messages.VhfCompatModeT, MessageBoxButton.OK, MessageBoxImage.Information);
            return true;
        }
        catch (VhfFormatException ex)
        {
            (string message, string title) = ex.ErrorCode switch
            {
                VhfError.InvalidVersion => (Messages.VhfInvalidVersion, Messages.VhfCorruptFileT),
                VhfError.InvalidVhrCode => (Messages.VhfInvalidVhrCode, Messages.VhfCorruptFileT),
                VhfError.InvalidLanguages => (Messages.VhfInvalidLanguages, Messages.VhfCorruptFileT),
                VhfError.InvalidRow => (Messages.VhfInvalidRow, Messages.VhfCorruptFileT),
                VhfError.UpdateRequired => (Messages.VhfMustUpdate, Messages.VhfMustUpdateT),
                _ => (Messages.VhfCorruptFile, Messages.VhfCorruptFileT),
            };
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Error);
            return false;
        }
        catch (Exception ex)
        {
            MessageBox.Show(string.Format(Messages.VocupFileReadError, ex.Message), Messages.VocupFileReadErrorT, MessageBoxButton.OK, MessageBoxImage.Error);
            return false;
        }
    }

    public static bool DetectAndRead(string path, VocabularyBook book, string vhrPath)
    {
        //await default(HopToThreadPoolAwaitable); // Perform IO operations on a separate thread

        using FileStream stream = new(path, FileMode.Open, FileAccess.Read, FileShare.Read);

        if (StartsWithZipHeader(stream))
        {
            return Vhf2Format.Instance.Read(stream, book);
        }
        else
        {
            Vhf1Format.Instance.Read(stream, book, vhrPath);
            return true;
        }
    }

    private static bool StartsWithZipHeader(Stream stream)
    {
        if (!stream.CanRead || !stream.CanSeek)
            throw new ArgumentException("Stream must be readable and seekable.", nameof(stream));

        Span<byte> buffer = stackalloc byte[4];
        bool zipHeader = stream.Read(buffer) == 4
            && buffer[0] == 0x50
            && buffer[1] == 0x4B
            && buffer[2] == 0x03
            && buffer[3] == 0x04;

        stream.Seek(0, SeekOrigin.Begin);
        return zipHeader;
    }

    public bool TryWrite(string path, VocabularyBook book, string vhrPath, bool includeResults = true)
    {
        try
        {
            //await default(HopToThreadPoolAwaitable); // Perform IO operations on a separate thread

            using FileStream stream = new(path, FileMode.Create, FileAccess.Write, FileShare.None);
            Write(stream, book, vhrPath, includeResults);
            return true;
        }
        catch (Exception ex)
        {
            MessageBox.Show(string.Format(Messages.VocupFileWriteError, ex.Message), Messages.VocupFileWriteErrorT, MessageBoxButton.OK, MessageBoxImage.Error);
            return false;
        }
    }

    public abstract void Write(FileStream stream, VocabularyBook book, string vhrPath, bool includeResults);

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
}
