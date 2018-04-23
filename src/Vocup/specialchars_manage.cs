using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Vocup.Util;

namespace Vocup
{
    public partial class specialchars_manage : Form
    {
        public const string InvalidChars = "#=:\\/|<>*?\"";

        public specialchars_manage()
        {
            InitializeComponent();
            Icon = Icon.FromHandle(icons.character.GetHicon());
        }

        string file_path = Properties.Settings.Default.path_vhr + "\\specialchar";

        //Laden

        private void specialchar_manage_Load(object sender, EventArgs e)
        {
            refresh_listbox();
        }

        //Sonderzeichen-Tabelle hinzufügen

        private void add_button_Click(object sender, EventArgs e)
        {
            //Tabelle hinzufügen

            try
            {
                DirectoryInfo directory_info = new DirectoryInfo(file_path);

                if (directory_info.Exists == false)
                {
                    directory_info.Create();
                }

                //Tabelle Speichern

                StreamWriter writer = new StreamWriter(file_path + "\\" + textbox_language_new.Text + ".txt");

                textbox_char_new.Text = textbox_char_new.Text.Replace(" ", "");

                for (int i = 0; i < textbox_char_new.Text.Length; i++)
                {
                    writer.Write(textbox_char_new.Text.Substring(i, 1));

                    if (i + 1 < textbox_char_new.Text.Length)
                    {
                        writer.WriteLine();
                    }
                }

                writer.Close();
            }
            catch
            {
            }
            refresh_listbox();
            listBox.SelectedValue = 0;
            textbox_language_new.Text = "";
            textbox_char_new.Text = "";
        }

        //Text überprüfen

        private void textbox_language_new_TextChanged(object sender, EventArgs e)
        {
            //Überprüfen, das Textfeld nicht erlaubte zeichen enthält

            if (textbox_language_new.Text != "" && textbox_char_new.Text != "" &&
                !textbox_language_new.Text.ContainsAny(InvalidChars))
            {
                if (add_button.Enabled == false)
                {
                    add_button.Enabled = true;
                }
                AcceptButton = add_button;
            }
            else
            {
                add_button.Enabled = false;
                AcceptButton = close_button;
            }

            // Überprüfen, ob nicht zugelassene Zeichen verwendet wurden

            if (textbox_language_new.Text.ContainsAny(InvalidChars))
            {
                textbox_language_new.BackColor = Color.FromArgb(255, 192, 203);
            }
            else
            {
                textbox_language_new.BackColor = Color.White;
                AcceptButton = add_button;
            }
        }

        private void textbox_char_new_TextChanged(object sender, EventArgs e)
        {
            //Überprüfen, das Textfeld nicht erlaubte zeichen enthält

            if (textbox_language_new.Text != "" && textbox_char_new.Text != "" &&
                !textbox_language_new.Text.ContainsAny(InvalidChars))
            {
                if (add_button.Enabled == false)
                {
                    add_button.Enabled = true;
                }
                AcceptButton = add_button;
            }
            else
            {
                add_button.Enabled = false;
                AcceptButton = close_button;
            }

            // Überprüfen, ob nicht zugelassene Zeichen verwendet wurden

            if (textbox_language_new.Text.ContainsAny(InvalidChars))
            {
                textbox_language_new.BackColor = Color.FromArgb(255, 192, 203);
            }
            else
            {
                textbox_language_new.BackColor = Color.White;
                AcceptButton = add_button;
            }
        }

        private void textbox_language_edit_TextChanged(object sender, EventArgs e)
        {
            //Überprüfen, das Textfeld nicht erlaubte zeichen enthält

            if (textbox_language_edit.Text != "" && textbox_char_edit.Text != "" &&
                !textbox_language_edit.Text.ContainsAny(InvalidChars))
            {
                if (save_button.Enabled == false)
                {
                    save_button.Enabled = true;
                }
                AcceptButton = save_button;
            }
            else
            {
                save_button.Enabled = false;
                AcceptButton = close_button;
            }

            // Überprüfen, ob nicht zugelassene Zeichen verwendet wurden

            if (textbox_language_edit.Text.ContainsAny(InvalidChars))
            {
                textbox_language_edit.BackColor = Color.FromArgb(255, 192, 203);
            }
            else
            {
                textbox_language_edit.BackColor = Color.White;
                AcceptButton = save_button;
            }
        }

        private void textbox_char_edit_TextChanged(object sender, EventArgs e)
        {
            //Überprüfen, das Textfeld nicht erlaubte zeichen enthält

            if (textbox_language_edit.Text != "" && textbox_char_edit.Text != "" &&
                !textbox_language_edit.Text.ContainsAny(InvalidChars))
            {
                if (save_button.Enabled == false)
                {
                    save_button.Enabled = true;
                }
                AcceptButton = save_button;
            }
            else
            {
                save_button.Enabled = false;
                AcceptButton = close_button;
            }

            // Überprüfen, ob nicht zugelassene Zeichen verwendet wurden

            if (textbox_language_edit.Text.ContainsAny(InvalidChars))
            {
                textbox_language_edit.BackColor = Color.FromArgb(255, 192, 203);
            }
            else
            {
                textbox_language_edit.BackColor = Color.White;
                AcceptButton = save_button;
            }
        }

        //Sonderzeichen-Tabelle löschen

        private void delete_button_Click(object sender, EventArgs e)
        {
            try
            {
                //Sprache aus der Listbox löschen

                string language = listBox.Items[listBox.SelectedIndex].ToString();

                listBox.Items.RemoveAt(listBox.SelectedIndex);

                //Datei mit den Sonderzeichen löschen

                FileInfo info = new FileInfo(file_path + "\\" + language + ".txt");

                if (info.Exists == true)
                {
                    info.Delete();
                }
            }
            catch
            {
            }
            refresh_listbox();
        }

        //Sonderzeichen-Tabelle speichern

        private void save_button_Click(object sender, EventArgs e)
        {
            try
            {
                //Änderungen speichern

                DirectoryInfo directory_info = new DirectoryInfo(file_path);

                if (directory_info.Exists == false)
                {
                    directory_info.Create();
                }

                StreamWriter writer = new StreamWriter(file_path + "\\" + textbox_language_edit.Text + ".txt");

                textbox_char_edit.Text = textbox_char_edit.Text.Replace(" ", "");

                for (int i = 0; i < textbox_char_edit.Text.Length; i++)
                {

                    writer.Write(textbox_char_edit.Text.Substring(i, 1));

                    if (i + 1 < textbox_char_edit.Text.Length)
                    {
                        writer.WriteLine();
                    }
                }

                writer.Close();

                if (textbox_language_edit.Text != listBox.Items[listBox.SelectedIndex].ToString())
                {
                    FileInfo info = new FileInfo(file_path + "\\" + listBox.Items[listBox.SelectedIndex].ToString() + ".txt");
                    info.Delete();

                    listBox.Items[listBox.SelectedIndex] = textbox_language_edit.Text;
                }
            }
            catch
            {
            }
            refresh_listbox();
            listBox.SelectedValue = 0;
            textbox_language_edit.Text = "";
            textbox_char_edit.Text = "";
        }



        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox.SelectedIndices.Count == 0)
            {
                refresh_listbox();

                delete_button.Enabled = false;

                textbox_char_edit.Enabled = false;
                textbox_char_edit.Text = "";

                textbox_language_edit.Enabled = false;
                textbox_language_edit.Text = "";
            }
            else
            {
                delete_button.Enabled = true;

                textbox_char_edit.Enabled = true;
                textbox_language_edit.Enabled = true;
                textbox_char_edit.Text = "";
                textbox_language_edit.Text = "";

                //Text laden
                try
                {

                    FileInfo info = new FileInfo(file_path + "\\" + listBox.Items[listBox.SelectedIndex].ToString() + ".txt");

                    if (info.Exists == true)
                    {

                        StreamReader reader = new StreamReader(info.FullName, Encoding.UTF8);

                        string[] characters = reader.ReadToEnd().Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

                        reader.Close();

                        for (int i = 0; i < characters.Length; i++)
                        {
                            if (characters[i].Length > 1)
                            {
                                textbox_char_edit.Text = textbox_char_edit.Text + characters[i].Replace(" ", "").Remove(1);
                            }
                            else
                            {
                                textbox_char_edit.Text = textbox_char_edit.Text + characters[i].Replace(" ", "");
                            }
                        }
                        textbox_language_edit.Text = listBox.Items[listBox.SelectedIndex].ToString();
                    }
                    else
                    {
                        refresh_listbox();
                    }
                }
                catch
                {
                    refresh_listbox();
                }
            }
        }

        private void refresh_listbox()
        {
            //Im char-Ordner nach Sonderzeichen-Verzeichnissen suchen

            listBox.Items.Clear();

            DirectoryInfo directory_info = new DirectoryInfo(file_path);

            if (directory_info.Exists == false)
            {
                directory_info.Create();
            }

            FileInfo[] files = directory_info.GetFiles();

            //Listbox füllen

            for (int i = 0; i < files.Length; i++)
            {
                try
                {
                    if (files[i].Extension == ".txt")
                    {
                        //Listbox-Item hinzufügen

                        listBox.Items.Add(Path.GetFileNameWithoutExtension(files[i].FullName));
                    }
                }
                catch
                {
                }
            }
        }

        //Dialog schliessen

        private void close_button_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}