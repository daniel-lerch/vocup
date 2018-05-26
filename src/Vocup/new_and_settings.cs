using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Vocup.Forms;

namespace Vocup
{
    public partial class new_and_settings : Form
    {
        string reset_fokus = "";

        //Sonderzeichen-Dialog vorbereiten

        public bool show_specialchars_dialog;

        public SpecialCharKeyboard sonderzeichen_dialog = new SpecialCharKeyboard();

        

        public new_and_settings()
        {
            InitializeComponent();
        }

        private void new_and_settings_Load(object sender, EventArgs e)
        {
            if (show_specialchars_dialog == true)
            {
                //Sonderzeichen-Dialog anzeigen

                //Position festlegen
                
                sonderzeichen_dialog.Left = this.Left + (this.Width - sonderzeichen_dialog.Width) / 2;
                sonderzeichen_dialog.Top = this.Top + this.Height + 10;

                //Events festlegen

                sonderzeichen_dialog.choose_char += add_special_char;

                sonderzeichen_dialog.FormClosed += sonderzeichen_dialog_closed;

                //Setzt die mForm als Besitzer

                sonderzeichen_dialog.Owner = this.Owner;

                sonderzeichen_dialog.Show();

                sonderzeichen_button.Enabled = false;
               

            }
        }

        private void ok_button_Click(object sender, EventArgs e)
        {
            Close();
        }

        // Falls sich der Text geändert hat

        private void own_language_TextChanged(object sender, EventArgs e)
        {
            //Überprüfen, ob Mutter- und Fremdsprache identisch sind

            if (own_language.Text != "" &&
               foreign_language.Text != "" &&
               foreign_language.Text != foreign_language_2.Text &&
               own_language.Text.Contains("#") == false && own_language.Text.Contains("=") == false &&
               foreign_language.Text.Contains("#") == false && foreign_language.Text.Contains("=") == false &&
               foreign_language_2.Text.Contains("#") == false && foreign_language_2.Text.Contains("=") == false)
            {
                if (ok_button.Enabled == false)
                {
                    ok_button.Enabled = true;
                }
            }
            else
            {
                ok_button.Enabled = false;
            }

            // Überprüfen, ob nicht zugelassene Zeichen verwendet wurden

            if (own_language.Text.Contains("#") == true || own_language.Text.Contains("=") == true)
            {
                own_language.BackColor = Color.FromArgb(255, 192, 203);
            }
            else
            {
                own_language.BackColor = Color.White;
            }

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
        
        }

        private void foreign_language_TextChanged(object sender, EventArgs e)
        {
            //Überprüfen, ob Mutter- und Fremdsprache identisch sind


            if (own_language.Text != "" &&
               foreign_language.Text != "" &&
               foreign_language.Text != foreign_language_2.Text &&
               own_language.Text.Contains("#") == false && own_language.Text.Contains("=") == false &&
               foreign_language.Text.Contains("#") == false && foreign_language.Text.Contains("=") == false &&
               foreign_language_2.Text.Contains("#") == false && foreign_language_2.Text.Contains("=") == false)
            {
                if (ok_button.Enabled == false)
                {
                    ok_button.Enabled = true;
                }
            }
            else
            {
                ok_button.Enabled = false;
            }

            // Überprüfen, ob nicht zugelassene Zeichen verwendet wurden

            if (foreign_language.Text.Contains("#") == true || foreign_language.Text.Contains("=") == true)
            {
                foreign_language.BackColor = Color.FromArgb(255, 192, 203);
            }
            else
            {
                foreign_language.BackColor = Color.White;
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
        }

        private void foreign_language_2_TextChanged(object sender, EventArgs e)
        {
            //Überprüfen, ob Mutter- und Fremdsprache identisch sind

            if (own_language.Text != "" &&
               foreign_language.Text != "" &&
               foreign_language.Text != foreign_language_2.Text &&
               own_language.Text.Contains("#") == false && own_language.Text.Contains("=") == false &&
               foreign_language.Text.Contains("#") == false && foreign_language.Text.Contains("=") == false &&
               foreign_language_2.Text.Contains("#") == false && foreign_language_2.Text.Contains("=") == false)
            {
                if (ok_button.Enabled == false)
                {
                    ok_button.Enabled = true;
                }
            }
            else
            {
                ok_button.Enabled = false;
            }

            // Überprüfen, ob nicht zugelassene Zeichen verwendet wurden

            if (foreign_language_2.Text.Contains("#") == true || foreign_language_2.Text.Contains("=") == true)
            {
                foreign_language_2.BackColor = Color.FromArgb(255, 192, 203);
            }
            else
            {
                foreign_language_2.BackColor = Color.White;
            }

            //Falls Fertig, Abbrechen anzeigen
            if (foreign_language_2.Text != "" && cancel_button.Text == Properties.language.fertig)
            {
                cancel_button.Text = Properties.language.cancel;
                //ok_button.TabIndex = 0;
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

        //-----


        //Enter

        private void own_language_Enter(object sender, EventArgs e)
        {
            reset_fokus = "own_language";
        }

        private void foreign_language_Enter(object sender, EventArgs e)
        {
            reset_fokus = "foreign_language";
        }

        private void foreign_language_2_Enter(object sender, EventArgs e)
        {
            reset_fokus = "foreign_language_2";
        }

        //-----

        //Sonderzeichen

        public void sonderzeichen_button_Click(object sender, EventArgs e)
        {
            if (sonderzeichen_dialog.OwnedForms.Length == 0)
            {

                //Events definieren

                sonderzeichen_dialog.choose_char += add_special_char;
                      
                sonderzeichen_dialog.FormClosed += sonderzeichen_dialog_closed;


                show_specialchars_dialog = true;

                //Position des Fensters festlegen
                
                sonderzeichen_dialog.Left = this.Left + (this.Width - sonderzeichen_dialog.Width) / 2;
                sonderzeichen_dialog.Top = this.Top + this.Height + 10;

                //Setzt die mForm als Besitzer

                sonderzeichen_dialog.Owner = this.Owner;

                sonderzeichen_dialog.Show();

               
                //Sonderzeichen-Button deaktivieren

                sonderzeichen_button.Enabled = false;
            
            }
        }

        private void sonderzeichen_dialog_closed(object sender, EventArgs e)
        {
            if (this.Visible == true)
            {
                show_specialchars_dialog = false;

                sonderzeichen_button.Enabled = true;

                sonderzeichen_dialog = new SpecialCharKeyboard();
            }
        }

        private void add_special_char(object sender, EventArgs e)
        {
            Button aktuellerButton = (Button)sender;

            // Button-Text in die Zwischenablage und in das Textfeld kopieren kopieren

            Clipboard.SetText(aktuellerButton.Text);

            switch (reset_fokus)
            {
                case "own_language":

                    own_language.Focus();

                    own_language.Text = own_language.Text + Clipboard.GetText();
                    own_language.SelectionStart = own_language.TextLength;


                    break;

                case "foreign_language":

                    foreign_language.Focus();

                    foreign_language.Text = foreign_language.Text + Clipboard.GetText();
                    foreign_language.SelectionStart = foreign_language.TextLength;

                    break;

                case "foreign_language_2":

                    foreign_language_2.Focus();


                    foreign_language_2.Text = foreign_language_2.Text + Clipboard.GetText();
                    foreign_language_2.SelectionStart = foreign_language_2.TextLength;

                    break;
            }
        }
    }
}