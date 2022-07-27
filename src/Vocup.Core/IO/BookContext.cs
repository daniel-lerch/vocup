using System;
using System.IO;
using System.Threading.Tasks;
using Vocup.Models;

namespace Vocup.IO;

public class BookContext : IAsyncDisposable
{
    public BookContext(Book book)
    {
        Book = book;
    }

    public Book Book { get; }
    public BookFileFormat? FileFormat { get; internal set; }
    public FileStream? FileStream { get; internal set; }
    public string? VhrCode { get; internal set; }

    public ValueTask DisposeAsync()
    {
        if (FileStream != null)
            return FileStream.DisposeAsync();
        else
            return ValueTask.CompletedTask;
    }
}
