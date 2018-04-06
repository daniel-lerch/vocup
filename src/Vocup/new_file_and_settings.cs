using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Vocup
{
    public partial class new_file_and_settings : Form
    {
        string reset_fokus = "";

        public specialchars sonderzeichen_dialog = new specialchars();

        public new_file_and_settings()
        {
            InitializeComponent();
        }

        private void ok_button_Click(object sender, EventArgs e)
        {
            sonderzeichen_dialog.Close();

            Close();
        }

        // Falls sich der Text geändert hat

        private void muttersprache_text_TextChanged(object sender, EventArgs e)
        {
            //Überprüfen, ob Mutter- und Fremdsprache identisch sind

            if (muttersprache_text.Text != "" &&
                fremdsprache_text.Text != "" &&
                muttersprache_text.Text != fremdsprache_text.Text &&
                 muttersprache_text.Text.Contains("#") == false && muttersprache_text.Text.Contains("=") == false &&
               fremdsprache_text.Text.Contains("#") == false && fremdsprache_text.Text.Contains("=") == false &&
                muttersprache_text.Text.Contains(":") == false && fremdsprache_text.Text.Contains(":") == false &&
                muttersprache_text.Text.Contains("\\") == false && fremdsprache_text.Text.Contains("\\") == false &&
                muttersprache_text.Text.Contains("/") == false && fremdsprache_text.Text.Contains("/") == false &&
                muttersprache_text.Text.Contains("|") == false && fremdsprache_text.Text.Contains("|") == false &&
                muttersprache_text.Text.Contains("<") == false && fremdsprache_text.Text.Contains("<") == false &&
                muttersprache_text.Text.Contains(">") == false && fremdsprache_text.Text.Contains(">") == false &&
                muttersprache_text.Text.Contains("*") == false && fremdsprache_text.Text.Contains("*") == false &&
                muttersprache_text.Text.Contains("?") == false && fremdsprache_text.Text.Contains("?") == false &&
                muttersprache_text.Text.Contains("\"") == false && fremdsprache_text.Text.Contains("\"") == false)
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

            if (muttersprache_text.Text.Contains("#") == true || muttersprache_text.Text.Contains("=") == true ||
                muttersprache_text.Text.Contains(":") == true ||
                muttersprache_text.Text.Contains("\\") == true ||
                muttersprache_text.Text.Contains("/") == true ||
                muttersprache_text.Text.Contains("|") == true ||
                muttersprache_text.Text.Contains("<") == true ||
                muttersprache_text.Text.Contains(">") == true ||
                muttersprache_text.Text.Contains("*") == true ||
                muttersprache_text.Text.Contains("?") == true ||
                muttersprache_text.Text.Contains("\"") == true)
            {

                muttersprache_text.BackColor = Color.FromArgb(255, 192, 203);
            }
            else
            {
                muttersprache_text.BackColor = Color.White;
            }

        }

        private void fremdsprache_text_TextChanged(object sender, EventArgs e)
        {
            //Überprüfen, ob Mutter- und Fremdsprache identisch sind

            if (muttersprache_text.Text != "" &&
                fremdsprache_text.Text != "" &&
                muttersprache_text.Text != fremdsprache_text.Text &&
                 muttersprache_text.Text.Contains("#") == false && muttersprache_text.Text.Contains("=") == false &&
               fremdsprache_text.Text.Contains("#") == false && fremdsprache_text.Text.Contains("=") == false &&
                muttersprache_text.Text.Contains(":") == false && fremdsprache_text.Text.Contains(":") == false &&
                muttersprache_text.Text.Contains("\\") == false && fremdsprache_text.Text.Contains("\\") == false &&
                muttersprache_text.Text.Contains("/") == false && fremdsprache_text.Text.Contains("/") == false &&
                muttersprache_text.Text.Contains("|") == false && fremdsprache_text.Text.Contains("|") == false &&
                muttersprache_text.Text.Contains("<") == false && fremdsprache_text.Text.Contains("<") == false &&
                muttersprache_text.Text.Contains(">") == false && fremdsprache_text.Text.Contains(">") == false &&
                muttersprache_text.Text.Contains("*") == false && fremdsprache_text.Text.Contains("*") == false &&
                muttersprache_text.Text.Contains("?") == false && fremdsprache_text.Text.Contains("?") == false &&
                muttersprache_text.Text.Contains("\"") == false && fremdsprache_text.Text.Contains("\"") == false)
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

            if (fremdsprache_text.Text.Contains("#") == true || fremdsprache_text.Text.Contains("=") == true||
                fremdsprache_text.Text.Contains(":") == true ||
                fremdsprache_text.Text.Contains("\\") == true ||
                fremdsprache_text.Text.Contains("/") == true ||
                fremdsprache_text.Text.Contains("|") == true ||
                fremdsprache_text.Text.Contains("<") == true ||
                fremdsprache_text.Text.Contains(">") == true ||
                fremdsprache_text.Text.Contains("*") == true ||
                fremdsprache_text.Text.Contains("?") == true ||
                fremdsprache_text.Text.Contains("\"") == true)
            {

                fremdsprache_text.BackColor = Color.FromArgb(255, 192, 203);
            }
            else
            {
                fremdsprache_text.BackColor = Color.White;
            }
        }



        //-----

        //Enter

        private void muttersprache_text_Enter(object sender, EventArgs e)
        {
            reset_fokus = "muttersprache";
        }

        private void fremdsprache_text_Enter(object sender, EventArgs e)
        {
            reset_fokus = "fremdsprache";
        }



        //-----

        //Sonderzeichen

        private void sonderzeichen_button_Click(object sender, EventArgs e)
        {
            if (sonderzeichen_dialog.OwnedForms.Length == 0)
            {

                //Events definieren

                sonderzeichen_dialog.choose_char += new System.EventHandler(add_special_char);

                sonderzeichen_dialog.FormClosed += new System.Windows.Forms.FormClosedEventHandler(sonderzeichen_dialog_closed);



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
           
                sonderzeichen_button.Enabled = true;

                sonderzeichen_dialog = new specialchars(); 
        }

        private void add_special_char(object sender, EventArgs e)
        {
            System.Windows.Forms.Button aktuellerButton = (System.Windows.Forms.Button)sender;

            // Button-Text in die Zwischenablage und in das Textfeld kopieren kopieren

            Clipboard.SetText(aktuellerButton.Text);

            switch (reset_fokus)
            {
                case "muttersprache":

                    muttersprache_text.Focus();


                    muttersprache_text.Text = muttersprache_text.Text + Clipboard.GetText();
                    muttersprache_text.SelectionStart = muttersprache_text.TextLength;

                    break;

                case "fremdsprache":

                    fremdsprache_text.Focus();

                    fremdsprache_text.Text = fremdsprache_text.Text + Clipboard.GetText();
                    fremdsprache_text.SelectionStart = fremdsprache_text.TextLength;

                    break;
            }

        }

        private void new_file_and_settings_FormClosed(object sender, FormClosedEventArgs e)
        {
            sonderzeichen_dialog.Close();
        }

    }
}