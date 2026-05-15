using Avalonia.Headless.XUnit;
using System.Linq;
using Vocup.Models;
using Vocup.ViewModels;
using Xunit;

namespace Vocup.UnitTests;

public class BookViewModelTests
{
    [AvaloniaFact]
    public void FiltersBySearchText()
    {
        Book book = new()
        {
            MotherTongue = "German",
            ForeignLanguage = "English",
            PracticeMode = PracticeMode.AskForForeignLang
        };
        book.Words.Add(new Word(["Apfel"], ["apple"]));
        book.Words.Add(new Word(["Birne"], ["pear"]));
        book.Words.Add(new Word(["Kirsche"], ["cherry"]));

        using BookViewModel viewModel = new(book);

        Assert.Equal(3, viewModel.Words.Count);

        viewModel.SearchText = "app";

        Assert.Single(viewModel.Words);
        Assert.Equal("Apfel", viewModel.Words.Single().MotherTongue.Single().Value);

        viewModel.SearchText = "RNE";

        Assert.Single(viewModel.Words);
        Assert.Equal("Birne", viewModel.Words.Single().MotherTongue.Single().Value);

        viewModel.SearchText = "xyz";

        Assert.Empty(viewModel.Words);
    }
}
