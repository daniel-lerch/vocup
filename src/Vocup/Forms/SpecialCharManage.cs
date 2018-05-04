using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Vocup.Util;

namespace Vocup.Forms
{
    public partial class SpecialCharManage : Form
    {
        private const string InvalidChars = "#=:\\/|<>*?\"";
        private readonly Color redBgColor = Color.FromArgb(255, 192, 203);
        private string file_path = Properties.Settings.Default.path_vhr + "\\specialchar";

        public SpecialCharManage()
        {
            InitializeComponent();
            Icon = Icon.FromHandle(icons.character.GetHicon());
        }

        //Laden

        private void SpecialCharManage_Load(object sender, EventArgs e)
        {
            RefreshListbox();
        }

        private void BtnNew_Click(object sender, EventArgs e)
        {
            const string name = "Neue Sprache";

            DirectoryInfo dirInfo = new DirectoryInfo(file_path);
            if (!dirInfo.Exists)
                dirInfo.Create();

            FileInfo fileInfo = new FileInfo(Path.Combine(file_path, name + ".txt"));

            if (fileInfo.Exists)
            {
                MessageBox.Show("Diese Sprache existiert bereits.");
            }
            else
            {
                fileInfo.Create().Dispose(); // Create empty file
                listBox.Items.Add(name);
                listBox.SelectedIndex = listBox.Items.Count - 1;
                TbLanguage.Focus();
                TbLanguage.SelectAll();
            }
        }

        private void TbLanguage_TbChars_TextChanged(object sender, EventArgs e)
        {
            //Überprüfen, das Textfeld nicht erlaubte zeichen enthält
            bool charsValid = !TbLanguage.Text.ContainsAny(InvalidChars);

            if (charsValid)
                TbLanguage.BackColor = Color.White;
            else
                TbLanguage.BackColor = redBgColor;

            if (!string.IsNullOrWhiteSpace(TbLanguage.Text) &&
                !string.IsNullOrWhiteSpace(TbChars.Text) &&
                charsValid)
            {
                BtnSave.Enabled = true;
                AcceptButton = BtnSave;
            }
            else
            {
                BtnSave.Enabled = false;
                AcceptButton = BtnClose;
            }
        }

        //Sonderzeichen-Tabelle löschen
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string language = listBox.Items[listBox.SelectedIndex].ToString();

                //Datei mit den Sonderzeichen löschen
                FileInfo info = new FileInfo(Path.Combine(file_path, language + ".txt"));
                if (info.Exists)
                    info.Delete();

                //Sprache aus der Listbox löschen
                listBox.Items.RemoveAt(listBox.SelectedIndex);
            }
            catch
            {
                MessageBox.Show("Fehler beim Löschen");
                RefreshListbox();
            }
        }

        //Sonderzeichen-Tabelle speichern
        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(file_path);
                if (!dirInfo.Exists)
                    dirInfo.Create();

                using (StreamWriter writer = new StreamWriter(Path.Combine(file_path, TbLanguage.Text + ".txt")))
                {
                    TbChars.Text = TbChars.Text.Replace(" ", "");
                    writer.Write(string.Join(Environment.NewLine, TbChars.Text.ToCharArray()));
                }

                // Delete old file if language was changed
                if (TbLanguage.Text != listBox.Items[listBox.SelectedIndex].ToString())
                {
                    FileInfo info = new FileInfo(Path.Combine(file_path, listBox.Items[listBox.SelectedIndex].ToString() + ".txt"));
                    info.Delete();

                    listBox.Items[listBox.SelectedIndex] = TbLanguage.Text;
                }
            }
            catch
            {
                MessageBox.Show("Fehler beim Speichern");
            }

            TbLanguage.Text = "";
            TbChars.Text = "";
            listBox.SelectedIndex = -1;
        }

        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox.SelectedIndex == -1)
            {
                RefreshListbox();

                BtnDelete.Enabled = false;

                TbChars.Enabled = false;
                TbChars.Text = "";

                TbLanguage.Enabled = false;
                TbLanguage.Text = "";
            }
            else
            {
                BtnDelete.Enabled = true;

                TbChars.Enabled = true;
                TbLanguage.Enabled = true;
                TbChars.Text = "";
                TbLanguage.Text = "";

                LoadLanguage();
            }
        }

        private void RefreshListbox()
        {
            //Im char-Ordner nach Sonderzeichen-Verzeichnissen suchen
            listBox.Items.Clear();

            DirectoryInfo directory_info = new DirectoryInfo(file_path);
            if (!directory_info.Exists)
                directory_info.Create();

            //Listbox füllen
            FileInfo[] files = directory_info.GetFiles();
            listBox.Items.AddRange(files
                .Where(x => x.Extension == ".txt")
                .Select(x => Path.GetFileNameWithoutExtension(x.FullName))
                .ToArray());
        }

        private void LoadLanguage()
        {
            try
            {
                FileInfo info = new FileInfo(Path.Combine(file_path, listBox.Items[listBox.SelectedIndex].ToString() + ".txt"));

                if (info.Exists)
                {
                    using (StreamReader reader = new StreamReader(info.FullName, Encoding.UTF8))
                    {
                        StringBuilder builder = new StringBuilder();

                        while (!reader.EndOfStream)
                        {
                            builder.Append(reader.ReadLine().Trim().Substring(0, 1));
                        }

                        TbChars.Text = builder.ToString();
                    }

                    TbLanguage.Text = listBox.Items[listBox.SelectedIndex].ToString();
                }
                else
                {
                    RefreshListbox();
                }
            }
            catch
            {
                RefreshListbox();
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}