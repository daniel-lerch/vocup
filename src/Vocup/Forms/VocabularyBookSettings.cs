using ReactiveUI;
using System;
using System.Drawing;
using System.Windows.Forms;
using Vocup.Models;
using Vocup.Properties;
using Vocup.ViewModels;

namespace Vocup.Forms;

public partial class VocabularyBookSettings : Form, IViewFor<BookSettingsViewModel>
{
    private readonly Color redBgColor = Color.FromArgb(255, 192, 203);
    private readonly SpecialCharKeyboard specialCharDialog;

    public VocabularyBookSettings(out Book book)
    {
        InitializeComponent();

        ViewModel = new BookSettingsViewModel(new Book(string.Empty, string.Empty));

        Text = Words.CreateVocabularyBook;
        Icon = Icon.FromHandle(Icons.File.GetHicon());
        GroupOptions.Enabled = false;

        InitializeDataBindings();

        specialCharDialog = new SpecialCharKeyboard();
        specialCharDialog.Initialize(this, BtnSpecialChar);
        specialCharDialog.RegisterTextBox(TbMotherTongue);

        book = ViewModel.Book;
    }

    public VocabularyBookSettings(Book book)
    {
        InitializeComponent();

        ViewModel = new BookSettingsViewModel(book);

        Text = Words.EditVocabularyBook;
        Icon = Icon.FromHandle(Icons.FileSettings.GetHicon());
        GroupOptions.Enabled = true;

        InitializeDataBindings();

        specialCharDialog = new SpecialCharKeyboard();
        specialCharDialog.Initialize(this, BtnSpecialChar);
        specialCharDialog.RegisterTextBox(TbMotherTongue);
    }

    public BookSettingsViewModel? ViewModel { get; set; }
    object? IViewFor.ViewModel { get => ViewModel; set => ViewModel = value as BookSettingsViewModel; }

    private void InitializeDataBindings()
    {
        this.Bind(ViewModel, vm => vm.MotherTongue, x => x.TbMotherTongue.Text);
        this.OneWayBind(ViewModel, vm => vm.MotherTongueValid, x => x.TbMotherTongue.BackColor, x => x ? Color.White : redBgColor);
        this.Bind(ViewModel, vm => vm.ForeignLanguage, x => x.TbForeignLang.Text);
        this.OneWayBind(ViewModel, vm => vm.ForeignLanguageValid, x => x.TbForeignLang.BackColor, x => x ? Color.White : redBgColor);
        this.Bind(ViewModel, vm => vm.ResetPracticeResults, x => x.CbResetResults.Checked);
        this.Bind(ViewModel, vm => vm.AskForForeignLanguage, x => x.RbModeAskForeignLang.Checked);
        this.BindCommand(ViewModel, vm => vm.SaveCommand, x => x.BtnOK);
    }

    private void TextBox_Enter(object sender, EventArgs e)
    {
        specialCharDialog.RegisterTextBox((TextBox)sender);
    }
}
