using System.Drawing;
using System.Windows.Forms;
using Vocup.Models;
using Vocup.Models.Legacy;
using Vocup.Properties;

#nullable disable

namespace Vocup.Forms;

public class AddWordDialog : VocabularyWordDialog
{
    private bool firstInput = true;

    public AddWordDialog(VocabularyBook book) : base(book)
    {
        Icon = Icon.FromHandle(Icons.Plus.GetHicon());
        Text = Words.AddWord;
    }

    protected override void OnInputValidated(bool passed)
    {
        if (passed)
        {
            BtnCancel.Text = Words.Cancel;
            BtnCancel.TabIndex = 5;
            BtnContinue.TabIndex = 0;
            AcceptButton = BtnContinue;
        }
        else if (TbMotherTongue.Text != "" || TbForeignLang.Text != "" || TbForeignLangSynonym.Text != "")
        {
            // Prevent user from accidentially canceling after entering some input
            BtnCancel.Text = Words.Cancel;
            BtnCancel.TabIndex = 5;
            BtnContinue.TabIndex = 0;
            AcceptButton = null;
        }
        else if (!firstInput) // Allow fast exit after entering at least one vocabulary word
        {
            BtnCancel.Text = Words.Finish;
            BtnCancel.TabIndex = 0;
            AcceptButton = BtnCancel;
        }
        else // Prevent user from accidentially canceling before entering any input
        {
            BtnCancel.Text = Words.Cancel;
            BtnCancel.TabIndex = 5;
            BtnContinue.TabIndex = 0;
            AcceptButton = null;
        }
    }

    protected override bool OnCommit()
    {
        if (BookContainsInput(exclude: null))
        {
            DialogResult dialogResult =
                MessageBox.Show(Messages.EditAddDuplicate, Messages.EditDuplicateT, MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                ResetUI();
            }
        }
        else // No duplicates to handle
        {
            book.Words.Add(new IVocabularyWord(TbMotherTongue.Text, TbForeignLang.Text)
            {
                ForeignLangSynonym = string.IsNullOrWhiteSpace(TbForeignLangSynonym.Text) ? null : TbForeignLangSynonym.Text
            });

            firstInput = false;
            ResetUI();
        }

        return false; // Always leave the dialog open for the user to add the next word
    }
}
