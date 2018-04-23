using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Vocup
{
    public partial class specialchars : Form
    {
        public specialchars()
        {
            InitializeComponent();
        }

        public event EventHandler choose_char;

        protected virtual void on_choose_char(object sender, EventArgs e)
        {
            choose_char?.Invoke(sender, e);
        }

        //TabPage laden

        private void sonderzeichen_Load(object sender, EventArgs e)
        {

            //Tabpages Name in gewünschter Sprache zuordnen

            tabPage.TabPages[0].Text = Properties.language.de;
            tabPage.TabPages[1].Text = Properties.language.fr;
            tabPage.TabPages[2].Text = Properties.language.it;
            tabPage.TabPages[3].Text = Properties.language.es;
            tabPage.TabPages[4].Text = Properties.language.ro;


            try
            {
                //Im char-Ordner nach Sonderzeichen-Verzeichnissen suchen

                string file_path = Properties.Settings.Default.path_vhr + "\\specialchar";

                DirectoryInfo directory_info = new DirectoryInfo(file_path);

                FileInfo[] files = directory_info.GetFiles();

                for (int i = 0; i < files.Length; i++)
                {
                    try
                    {


                        if (files[i].Extension == ".txt")
                        {
                            //Name definieren

                            StreamReader reader = new StreamReader(files[i].FullName, Encoding.UTF8);

                            string[] characters = reader.ReadToEnd().Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

                            reader.Close();

                            //Neue Tabpage erstellen

                            TabPage page = new TabPage();
                            page.Name = Path.GetFileNameWithoutExtension(files[i].FullName).ToLower();
                            page.Text = Path.GetFileNameWithoutExtension(files[i].FullName);

                            page.AutoScroll = true;
                            page.UseVisualStyleBackColor = true;

                            page.Font = new System.Drawing.Font("Arial", 9.75F);

                            //TabPage füllen

                            int row = 1;
                            int position = 1;

                            for (int y = 0; y < characters.Length; y++)
                            {

                                Button button = new Button();

                                if (characters[y].Replace(" ", "") != "" && characters[y].Replace(" ", "") != null)
                                {
                                    if (characters[y].Length > 1)
                                    {
                                        button.Text = characters[y].Replace(" ", "").Remove(1);
                                        button.Name = characters[y].Replace(" ", "").Remove(1).ToLower();
                                    }
                                    else
                                    {
                                        button.Text = characters[y].Replace(" ", "");
                                        button.Name = characters[y].Replace(" ", "");
                                    }

                                    button.UseVisualStyleBackColor = true;

                                    button.Size = new System.Drawing.Size(25, 25);

                                    //Position

                                    button.Location = new System.Drawing.Point(8 + (position - 1) * 31, 6 * row + 25 * (row - 1));

                                    button.Click += new System.EventHandler(button_Click);

                                    page.Controls.Add(button);

                                    if (position == 12)
                                    {
                                        row++;
                                        position = 1;
                                    }
                                    else
                                    {
                                        position++;
                                    }

                                }
                            }
                            tabPage.TabPages.Add(page);

                        }


                    }
                    catch
                    {

                    }

                }

            }
            catch
            {

            }

            //Versucht die zuletzt geöffnete Registerkarte auszuwählen
            try
            {
                tabPage.SelectTab(Properties.Settings.Default.sonderzeichen_registerkarte);
            }
            catch
            {

            }
        }

        //-----

        // Wird aufgerufen, sobald auf ein Button geklickt wird

        public void button_Click(object sender, EventArgs e)
        {
            ////Button herausfinden

            //System.Windows.Forms.Button aktuellerButton = (System.Windows.Forms.Button)sender;

            //// Button-Text in die Zwischenablage kopieren

            //Clipboard.SetText(aktuellerButton.Text);

            on_choose_char(sender, e);
        }


        private void sonderzeichen_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.sonderzeichen_registerkarte = tabPage.SelectedTab.Name;
            Properties.Settings.Default.Save();
        }
    }
}