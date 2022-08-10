using Vocup.Models;
using Vocup.ViewModels;
using Xunit;

namespace Vocup.Core.UnitTests.ViewModels;

public class BookSettingsViewModelTests
{
    [Theory]
    [InlineData("", true)]
    [InlineData("Deutsch", true)]
    [InlineData("Deutsch=German", false)]
    [InlineData("<WTF>#", false)]
    public void TestMotherTongueValid(string motherTongue, bool valid)
    {
        Book book = new("Initial", "initial");
        BookSettingsViewModel viewModel = new(book);
        Assert.True(viewModel.MotherTongueValid);

        bool motherTongueChanged = false;
        bool motherTongueValidChanged = false;
        viewModel.PropertyChanged += (sender, e) =>
        {
            if (e.PropertyName == nameof(viewModel.MotherTongue))
                motherTongueChanged = true;
            if (e.PropertyName == nameof(viewModel.MotherTongueValid))
                motherTongueValidChanged = true;
        };

        viewModel.MotherTongue = motherTongue;
        Assert.True(motherTongueChanged);
        Assert.Equal(!valid, motherTongueValidChanged);
        Assert.Equal(valid, viewModel.MotherTongueValid);
    }
}
