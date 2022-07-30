using ReactiveUI;
using System;
using System.Drawing;
using System.Windows.Forms;
using Vocup.Models;
using Vocup.Properties;
using Vocup.ViewModels;

#nullable disable

namespace Vocup.Forms;

public partial class VocabularyBookSettings : Form, IViewFor<BookSettingsViewModel>
{
    private readonly Color redBgColor = Color.FromArgb(255, 192, 203);
    private readonly SpecialCharKeyboard specialCharDialog;
    private readonly VocabularyBook book;

    private VocabularyBookSettings()
    {
        InitializeComponent();
        ViewModel = new BookSettingsViewModel();

        this.Bind(ViewModel, x => x.MotherTongue, x => x.TbMotherTongue.Text);
        this.OneWayBind(ViewModel, x => x.MotherTongueValid, x => x.TbMotherTongue.BackColor, x => x ? Color.White : redBgColor);
        this.Bind(ViewModel, x => x.ForeignLanguage, x => x.TbForeignLang.Text);
        this.OneWayBind(ViewModel, x => x.ForeignLanguageValid, x => x.TbForeignLang.BackColor, x => x ? Color.White : redBgColor);
        this.BindCommand(ViewModel, x => x.SaveCommand, x => x.BtnOK);

        specialCharDialog = new SpecialCharKeyboard();
        specialCharDialog.Initialize(this, BtnSpecialChar);
        specialCharDialog.RegisterTextBox(TbMotherTongue);
    }

    public VocabularyBookSettings(out VocabularyBook book) : this()
    {
        book = new VocabularyBook();
        this.book = book;
        book.Notify();

        Text = Words.CreateVocabularyBook;
        Icon = Icon.FromHandle(Icons.File.GetHicon());
        GroupOptions.Enabled = false;
    }

    public VocabularyBookSettings(VocabularyBook book) : this()
    {
        this.book = book;

        Text = Words.EditVocabularyBook;
        Icon = Icon.FromHandle(Icons.FileSettings.GetHicon());
        TbMotherTongue.Text = book.MotherTongue;
        TbForeignLang.Text = book.ForeignLang;
        RbModeAskForeignLang.Checked = book.PracticeMode == PracticeMode.AskForForeignLang;
        RbModeAskMotherTongue.Checked = book.PracticeMode == PracticeMode.AskForMotherTongue;
        GroupOptions.Enabled = true;
    }

    public BookSettingsViewModel ViewModel { get; set; }
    object IViewFor.ViewModel { get => ViewModel; set => ViewModel = (BookSettingsViewModel)value; }

    private void TextBox_Enter(object sender, EventArgs e)
    {
        specialCharDialog.RegisterTextBox((TextBox)sender);
    }

    private void BtnOK_Click(object sender, EventArgs e)
    {
        book.MotherTongue = TbMotherTongue.Text;
        book.ForeignLang = TbForeignLang.Text;
        book.PracticeMode = RbModeAskForeignLang.Checked ? PracticeMode.AskForForeignLang : PracticeMode.AskForMotherTongue;

        if (CbResetResults.Checked)
        {
            foreach (VocabularyWord word in book.Words)
            {
                word.PracticeStateNumber = 0;
            }
        }
    }
}