using System;
using System.IO;
using System.Threading.Tasks;
using Vocup.Models;

namespace Vocup.IO;

public class BookContext : IAsyncDisposable
{
    public BookContext(Book book, BookFileFormat fileFormat)
    {
        Book = book;
        FileFormat = fileFormat;
    }

    public BookContext(Book book, BookFileFormat fileFormat, FileStream? fileStream, string? vhrCode)
    {
        Book = book;
        FileFormat = fileFormat;
        FileStream = fileStream;
        VhrCode = vhrCode;
    }

    public Book Book { get; }
    public BookFileFormat FileFormat { get; internal set; }
    public FileStream? FileStream { get; internal set; }
    public string? VhrCode { get; internal set; }

    public ValueTask DisposeAsync()
    {
        if (FileStream != null)
            return FileStream.DisposeAsync();
        else
            return ValueTask.CompletedTask;
    }

    [Obsolete] public string? FilePath { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    [Obsolete] public string? Name => throw new NotImplementedException();
    [Obsolete] public bool UnsavedChanges { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
}
