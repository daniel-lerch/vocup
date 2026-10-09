using ReactiveUI.Primitives.Concurrency;
using System.Linq;
using Vocup.Models;
using Vocup.ViewModels;
using Xunit;

namespace Vocup.UnitTests;

public class BookViewModelTests
{
    [Fact]
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

        VirtualClock clock = new();
        using BookViewModel viewModel = new(book, clock, clock);

        clock.AdvanceBy(BookViewModel.SearchThrottle);
        Assert.Equal(3, viewModel.Words.Count);

        viewModel.SearchText = "app";
        clock.AdvanceBy(BookViewModel.SearchThrottle);

        Assert.Single(viewModel.Words);
        Assert.Equal("Apfel", viewModel.Words.Single().MotherTongue.Single().Value);

        viewModel.SearchText = "RNE";
        clock.AdvanceBy(BookViewModel.SearchThrottle);

        Assert.Single(viewModel.Words);
        Assert.Equal("Birne", viewModel.Words.Single().MotherTongue.Single().Value);

        viewModel.SearchText = "xyz";
        clock.AdvanceBy(BookViewModel.SearchThrottle);

        Assert.Empty(viewModel.Words);
    }

    [Fact]
    public void ThrottlesSearchText()
    {
        Book book = new();
        book.Words.Add(new Word(["Apfel"], ["apple"]));
        book.Words.Add(new Word(["Birne"], ["pear"]));

        VirtualClock clock = new();
        using BookViewModel viewModel = new(book, clock, clock);

        // The initial filter is throttled as well
        Assert.Empty(viewModel.Words);
        clock.AdvanceBy(BookViewModel.SearchThrottle);
        Assert.Equal(2, viewModel.Words.Count);

        // "x" matches no word, so applying it would empty the list
        viewModel.SearchText = "x";
        clock.AdvanceBy(BookViewModel.SearchThrottle / 2);
        viewModel.SearchText = "ap";
        clock.AdvanceBy(BookViewModel.SearchThrottle / 2);

        // Typing restarts the throttle so neither "x" nor "ap" has been applied yet
        Assert.Equal(2, viewModel.Words.Count);

        clock.AdvanceBy(BookViewModel.SearchThrottle / 2);
        Assert.Single(viewModel.Words);
    }
}
