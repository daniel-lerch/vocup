using System.IO;
using Vocup.Models;

namespace Vocup.IO;

public class BookContext
{
    public BookContext(Book book)
    {
        Book = book;
    }

    public Book Book { get; }
    public BookFileFormat? FileFormat { get; internal set; }
    public FileStream? FileStream { get; internal set; }
    public string? VhrCode { get; internal set; }
}
