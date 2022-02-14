using System.IO;
using Vocup.Forms;
using Vocup.Models;
using Xunit;

namespace Vocup.UnitTests
{
    public class InitializeTest
    {
        [Fact]
        public void TestResourceFiles()
        {
            Assert.True(File.Exists(Path.Combine("Resources", "easter_egg.vhf")));
            Assert.True(File.Exists(Path.Combine("Resources", "help.chm")));
        }

        [Fact]
        public void TestFormConstructors()
        {
            Program.CreateVhfFolder();
            Program.CreateVhrFolder();
            VocabularyBook book = new VocabularyBook() { MotherTongue = "Deutsch", ForeignLang = "Englisch" };

            new MainForm().Dispose();
            new AboutBox().Dispose();
            new AddWordDialog(book).Dispose();
            // new EditWordDialog(book: null, word: null).Dispose();
            new EvaluationInfoDialog().Dispose();
            new MergeFiles().Dispose();
            new PracticeCountDialog(book).Dispose();
            // new PracticeDialog(book: null, practiceList: null).Dispose();
            // new PracticeResultList(book: null, practiceList: null).Dispose();
            new PrintWordSelection(book).Dispose();
            new RestoreBackup(null).Dispose();
            new SettingsDialog().Dispose();
            new SpecialCharKeyboard().Dispose();
            new SpecialCharManage().Dispose();
            new SplashScreen().Dispose();
            new VocabularyBookSettings(book).Dispose();
            new VocabularyWordDialog(book).Dispose();
        }
    }
}
