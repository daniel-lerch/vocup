using ReactiveUI;
using Vocup.IO;
using Vocup.Models;
using Vocup.Settings;

namespace Vocup.ViewModels;

/// <summary>
/// Represents an opened vocabulary book. Multiple instances can be used simultaneously to implement tabs for books.
/// </summary>
public class BookViewModel : ReactiveObject
{
	public BookViewModel(BookContext bookContext, IVocupSettings settings)
	{
		Book = bookContext.Book;
		BookContext = bookContext;
	}

	public Book Book { get; }
	public BookContext BookContext { get; }
}
