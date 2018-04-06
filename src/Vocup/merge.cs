using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Vocup
{
    public partial class merge : Form
    {
        public merge()
        {
            InitializeComponent();
        }

        //Focus

        string reset_focus = "";
        
        //Pfad zum Speicherort

        public string pfad;

        //Vokabelheft hinzufügen
        private void add_button_Click(object sender, EventArgs e)
        {

            //Neuer Öffnen-Dialog

            OpenFileDialog add_file = new OpenFileDialog();
            add_file.Title = Properties.language.add_title;
            
            //add_file.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\" + Properties.language.personal_directory;

            add_file.InitialDirectory = Properties.Settings.Default.path_vhf;
            
            add_file.Filter = Properties.language.personal_directory + " (*.vhf)|*.vhf";
            add_file.Multiselect = true;
            
            //Falls auf öffnen geklickt wurde
            if (add_file.ShowDialog() == DialogResult.OK)
            {
               
                
                for (int i = 0; i < add_file.FileNames.Length; i++)
                {
                    //Fügt das Item zur Liste hinzu, falls es noch nicht existiert
                    if (listBox_files.Items.Contains(add_file.FileNames[i]) == false)
                    {
                        listBox_files.Items.Add(add_file.FileNames[i]);

                       //Falls möglich Buttons etc. aktivieren
                        if (save_button.Enabled == false && listBox_files.Items.Count > 1)
                        {


                            groupBox1.Enabled = true;
                            groupBox2.Enabled = true;
                            
                            take_results.Enabled = true;

                            sonderzeichen_button.Enabled = true;

                            if (own_language.Text != "" && foreign_language.Text != "")
                            {
                                save_button.Enabled = true;
                            }

                        } 
                    
                    }
                }

            }
           


        }
        //Item löschen
        private void delete_button_Click(object sender, EventArgs e)
        {
            while (listBox_files.SelectedItems.Count > 0)
            {
                listBox_files.Items.Remove(listBox_files.SelectedItems[0]);
            }

            //Buttons etc. deaktivieren, falls keine Items mehr vorhanden sind
            if (listBox_files.SelectedItems.Count < 2)
            {
                save_button.Enabled = false;
                groupBox1.Enabled = false;
                groupBox2.Enabled = false;
                
                take_results.Enabled = false;

                sonderzeichen_button.Enabled = false;

            }
        }

        //Falls Elemente ausgewählt/abgewählt wurden
        private void listBox_files_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox_files.SelectedItems.Count < 1)
            {
                delete_button.Enabled = false;
            }
            else
            {
                delete_button.Enabled = true;
            }
        }

        
        //Falls der Text geändert wurde
        private void own_language_TextChanged(object sender, EventArgs e)
        {
            //Überprüfen, ob Mutter- und Fremdsprache identisch sind

            if (own_language.Text != "" &&
                foreign_language.Text != "" &&
                own_language.Text != foreign_language.Text &&
                 own_language.Text.Contains("#") == false && own_language.Text.Contains("=") == false &&
              foreign_language.Text.Contains("#") == false && own_language.Text.Contains("=") == false&&
              own_language.Text.Contains(":") == false && foreign_language.Text.Contains(":") == false &&
                own_language.Text.Contains("\\") == false && foreign_language.Text.Contains("\\") == false &&
                own_language.Text.Contains("/") == false && foreign_language.Text.Contains("/") == false &&
                own_language.Text.Contains("|") == false && foreign_language.Text.Contains("|") == false &&
                own_language.Text.Contains("<") == false && foreign_language.Text.Contains("<") == false &&
                own_language.Text.Contains(">") == false && foreign_language.Text.Contains(">") == false &&
                own_language.Text.Contains("*") == false && foreign_language.Text.Contains("*") == false &&
                own_language.Text.Contains("?") == false && foreign_language.Text.Contains("?") == false &&
                own_language.Text.Contains("\"") == false && foreign_language.Text.Contains("\"") == false)
            {
                if (save_button.Enabled == false)
                {

                    save_button.Enabled = true;
                }
            }
            else
            {
                save_button.Enabled = false;
            }

            // Überprüfen, ob nicht zugelassene Zeichen verwendet wurden

            if (own_language.Text.Contains("#") == true || own_language.Text.Contains("=") == true||
               own_language.Text.Contains(":") == true ||
                own_language.Text.Contains("\\") == true ||
                own_language.Text.Contains("/") == true ||
                own_language.Text.Contains("|") == true ||
                own_language.Text.Contains("<") == true ||
                own_language.Text.Contains(">") == true ||
                own_language.Text.Contains("*") == true ||
                own_language.Text.Contains("?") == true ||
                own_language.Text.Contains("\"") == true)
            {

                own_language.BackColor = Color.FromArgb(255, 192, 203);
            }
            else
            {
                own_language.BackColor = Color.White;
            }
        }
        //Falls der Text geändert wurde
        private void foreign_language_TextChanged(object sender, EventArgs e)
        {
            //Überprüfen, ob Mutter- und Fremdsprache identisch sind

            if (own_language.Text != "" &&
                foreign_language.Text != "" &&
                own_language.Text != foreign_language.Text &&
                 own_language.Text.Contains("#") == false && own_language.Text.Contains("=") == false &&
              foreign_language.Text.Contains("#") == false && own_language.Text.Contains("=") == false &&
              own_language.Text.Contains(":") == false && foreign_language.Text.Contains(":") == false &&
                own_language.Text.Contains("\\") == false && foreign_language.Text.Contains("\\") == false &&
                own_language.Text.Contains("/") == false && foreign_language.Text.Contains("/") == false &&
                own_language.Text.Contains("|") == false && foreign_language.Text.Contains("|") == false &&
                own_language.Text.Contains("<") == false && foreign_language.Text.Contains("<") == false &&
                own_language.Text.Contains(">") == false && foreign_language.Text.Contains(">") == false &&
                own_language.Text.Contains("*") == false && foreign_language.Text.Contains("*") == false &&
                own_language.Text.Contains("?") == false && foreign_language.Text.Contains("?") == false &&
                own_language.Text.Contains("\"") == false && foreign_language.Text.Contains("\"") == false)
            {
                if (save_button.Enabled == false)
                {

                    save_button.Enabled = true;
                }
            }
            else
            {
                save_button.Enabled = false;
            }

            // Überprüfen, ob nicht zugelassene Zeichen verwendet wurden

            if (foreign_language.Text.Contains("#") == true || foreign_language.Text.Contains("=") == true||
                foreign_language.Text.Contains(":") == true ||
                foreign_language.Text.Contains("\\") == true ||
                foreign_language.Text.Contains("/") == true ||
                foreign_language.Text.Contains("|") == true ||
                foreign_language.Text.Contains("<") == true ||
                foreign_language.Text.Contains(">") == true ||
                foreign_language.Text.Contains("*") == true ||
                foreign_language.Text.Contains("?") == true ||
                foreign_language.Text.Contains("\"") == true)
            {

                foreign_language.BackColor = Color.FromArgb(255, 192, 203);
            }
            else
            {
                foreign_language.BackColor = Color.White;
            }
        }

        //Enter
        
        private void own_language_Enter(object sender, EventArgs e)
        {
            reset_focus = "own_language";
        }

        private void foreign_language_Enter(object sender, EventArgs e)
        {
            reset_focus = "foreign_language";
        }
       

        //Sonderzeichen
        
        private void sonderzeichen_button_Click(object sender, EventArgs e)
        {
            //Dialog öffnen

            specialchars sonderzeichen_dialog = new specialchars();
            DialogResult result = new DialogResult();
            result = sonderzeichen_dialog.ShowDialog();

            //Schauen, ob auf ein Button geklickt wurde

            if (DialogResult.OK == result)
            {

                switch (reset_focus)
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

                }

            }
            else
            {
                switch (reset_focus)
                {
                    case "own_language":

                        own_language.Focus();

                        break;

                    case "foreign_language":

                        foreign_language.Focus();

                        break;

                }
            }
        }


        //Falls auf Speichern geklickt wurde
        private void save_button_Click(object sender, EventArgs e)
        {
            //Speichern-Dialog anzeigen

            SaveFileDialog save = new SaveFileDialog();
            save.Title = Properties.language.save_title;
            save.FileName = own_language.Text + " - " + foreign_language.Text;
            
            //save.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\" + Properties.language.personal_directory;

            save.InitialDirectory = Properties.Settings.Default.path_vhf;

            save.Filter = Properties.language.personal_directory + " (*.vhf)|*.vhf";


            if (save.ShowDialog() == DialogResult.OK)
            {
                pfad = save.FileName;

                DialogResult = DialogResult.OK;
            }


        }
       
    }
}