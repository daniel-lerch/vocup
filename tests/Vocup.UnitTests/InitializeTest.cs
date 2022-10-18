using System.IO;
using System.Threading.Tasks;
using Vocup.Forms;
using Vocup.IO;
using Vocup.Models;
using Xunit;

namespace Vocup.UnitTests;

public class InitializeTest
{
    [Fact]
    public void TestResourceFiles()
    {
        Assert.True(File.Exists(Path.Combine("Resources", "easter_egg.vhf")));
        Assert.True(File.Exists(Path.Combine("Resources", "help.chm")));
    }

    [Fact]
    public async Task TestFormConstructors()
    {
        await using var serviceScope = Program.InitializeServices();
        Program.CreateVhfFolder();
        Program.CreateVhrFolder();
        Book book = new("Deutsch", "Englisch");
        BookContext bookContext = new(book, BookFileFormat.Vhf1, Program.Settings);

        new MainForm().Dispose();
        // new AboutBox().Dispose(); WPF does not work without [STAThread]
        new AddWordDialog(book).Dispose();
        // new EditWordDialog(book: null, word: null).Dispose();
        new EvaluationInfoDialog().Dispose();
        new MergeFiles().Dispose();
        new PracticeCountDialog(book).Dispose();
        // new PracticeDialog(book: null, practiceList: null).Dispose();
        // new PracticeResultList(book: null, practiceList: null).Dispose();
        new PrintWordSelection(bookContext).Dispose();
        //new SettingsDialog(settings: null).Dispose();
        new SpecialCharKeyboard().Dispose();
        new SpecialCharManage().Dispose();
        new SplashScreen().Dispose();
        new VocabularyBookSettings(book).Dispose();
        new VocabularyWordDialog(book).Dispose();
    }
}
