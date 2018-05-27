using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Vocup.Forms;
using Vocup.Util;

namespace Vocup
{
    public partial class new_and_settings : Form
    {
        private const string InvalidChars = "#=";
        private readonly Color redBgColor = Color.FromArgb(255, 192, 203);
        private SpecialCharKeyboard specialCharDialog;

        //Sonderzeichen-Dialog vorbereiten
        public bool show_specialchars_dialog;

        public new_and_settings()
        {
            InitializeComponent();
            specialCharDialog = new SpecialCharKeyboard();
            specialCharDialog.Initialize(this);
            specialCharDialog.VisibleChanged += (a0, a1) =>
            {
                sonderzeichen_button.Enabled = !specialCharDialog.Visible;
                show_specialchars_dialog = specialCharDialog.Visible;
            };
        }

        private void new_and_settings_Load(object sender, EventArgs e)
        {
            if (show_specialchars_dialog)
                specialCharDialog.Show();
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            ok_button.Enabled =
                !string.IsNullOrWhiteSpace(own_language.Text) &&
                !string.IsNullOrWhiteSpace(foreign_language.Text) &&
                foreign_language.Text != foreign_language_2.Text &&
                !own_language.Text.ContainsAny(InvalidChars) &&
                !foreign_language.Text.ContainsAny(InvalidChars) &&
                !foreign_language_2.Text.ContainsAny(InvalidChars);

            // Überprüfen, ob nicht zugelassene Zeichen verwendet wurden
            own_language.BackColor = own_language.Text.ContainsAny(InvalidChars) ? redBgColor : Color.White;
            foreign_language.BackColor = foreign_language.Text.ContainsAny(InvalidChars) ? redBgColor : Color.White;
            foreign_language_2.BackColor = foreign_language_2.Text.ContainsAny(InvalidChars) ? redBgColor : Color.White;

            // TODO: Redesign this part in order to fix new bug
            //Falls Fertig, Abbrechen anzeigen
            if (own_language.Text != "" && cancel_button.Text == Properties.language.fertig)
            {
                cancel_button.Text = Properties.language.cancel;
                cancel_button.TabIndex = 5;
                ok_button.TabIndex = 0;
                AcceptButton = ok_button;
            }
            else if (own_language.Text == "" && cancel_button.Text != Properties.language.fertig && option_box.Enabled == false)
            {
                cancel_button.Text = Properties.language.fertig;
                cancel_button.TabIndex = 0;
                AcceptButton = cancel_button;
            }

            //Falls Fertig, Abbrechen anzeigen
            if (foreign_language.Text != "" && cancel_button.Text == Properties.language.fertig)
            {
                cancel_button.Text = Properties.language.cancel;
                cancel_button.TabIndex = 5;
                ok_button.TabIndex = 0;
                AcceptButton = ok_button;
            }
            else if (foreign_language.Text == "" && cancel_button.Text != Properties.language.fertig && option_box.Enabled == false)
            {
                cancel_button.Text = Properties.language.fertig;
                cancel_button.TabIndex = 0;
                AcceptButton = cancel_button;
            }

            //Falls Fertig, Abbrechen anzeigen
            if (foreign_language_2.Text != "" && cancel_button.Text == Properties.language.fertig)
            {
                cancel_button.Text = Properties.language.cancel;
                cancel_button.TabIndex = 5;
                AcceptButton = ok_button;
            }
            else if (foreign_language_2.Text == "" && cancel_button.Text != Properties.language.fertig && option_box.Enabled == false)
            {
                cancel_button.Text = Properties.language.fertig;
                cancel_button.TabIndex = 0;
                AcceptButton = cancel_button;
            }
        }

        private void TextBox_Enter(object sender, EventArgs e)
        {
            specialCharDialog.RegisterTextBox((TextBox)sender);
        }

        public void sonderzeichen_button_Click(object sender, EventArgs e)
        {
            specialCharDialog.Show();
        }
    }
}