using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using Vocup.Forms;
using Vocup.Models;

namespace Vocup.UnitTests
{
    [TestClass]
    public class InitializeTest
    {
        [TestMethod]
        public void TestHelpFile()
        {
            Assert.IsTrue(File.Exists(Path.Combine("Resources", "help.chm")));
        }

        [TestMethod]
        public void TestFormConstructors()
        {
            Program.CreateVhfFolder();
            Program.CreateVhrFolder();
            VocabularyBook book = new VocabularyBook() { MotherTongue = "Deutsch", ForeignLang = "Englisch" };

            new MainForm().Dispose();
            new AboutBox().Dispose();
            new AddWordDialog(book).Dispose();
            new CreateBackup().Dispose();
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
