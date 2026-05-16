using Avalonia.Headless.XUnit;
using System;
using System.Linq;
using System.Threading.Tasks;
using Vocup.Models;
using Vocup.ViewModels;
using Xunit;

namespace Vocup.UnitTests;

public class BookViewModelTests
{
    [AvaloniaFact]
    public async Task FiltersBySearchText()
    {
        Assert.SkipUnless(OperatingSystem.IsWindows(), "Avalonia headless tests fail on Linux: https://github.com/avaloniaui/avalonia/issues/21332");

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

        await Task.Delay(150);
        Assert.Equal(3, viewModel.Words.Count);

        viewModel.SearchText = "app";
        await Task.Delay(150);

        Assert.Single(viewModel.Words);
        Assert.Equal("Apfel", viewModel.Words.Single().MotherTongue.Single().Value);

        viewModel.SearchText = "RNE";
        await Task.Delay(150);

        Assert.Single(viewModel.Words);
        Assert.Equal("Birne", viewModel.Words.Single().MotherTongue.Single().Value);

        viewModel.SearchText = "xyz";
        await Task.Delay(150);

        Assert.Empty(viewModel.Words);
    }
}
