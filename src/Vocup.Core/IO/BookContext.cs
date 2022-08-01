using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.IO;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Vocup.Models;

namespace Vocup.IO;

public class BookContext : ReactiveObject, IAsyncDisposable
{
    public BookContext(Book book, BookFileFormat fileFormat) : this(book, fileFormat, null, null) { }

    internal BookContext(Book book, BookFileFormat fileFormat, FileStream? fileStream, string? vhrCode)
    {
        Book = book;
        FileFormat = fileFormat;
        FileStream = fileStream;
        VhrCode = vhrCode;

        this.WhenAnyValue(x => x.FileStream).Select(x => x?.Name).ToPropertyEx(this, x => x.FilePath);
        this.WhenAnyValue(x => x.FileStream).Select(x => Path.GetFileNameWithoutExtension(x?.Name)).ToPropertyEx(this, x => x.Name);
    }

    public Book Book { get; }
    [Reactive] public BookFileFormat FileFormat { get; internal set; }
    internal FileStream? FileStream { get; set; }
    [Reactive] public string? VhrCode { get; internal set; }
    [Reactive] public bool UnsavedChanges { get; set; } // TODO add change listener
    [ObservableAsProperty] public string? FilePath { get; }

    public ValueTask DisposeAsync()
    {
        if (FileStream != null)
            return FileStream.DisposeAsync();
        else
            return ValueTask.CompletedTask;
    }

    [Obsolete, ObservableAsProperty] public string? Name { get; }
}
