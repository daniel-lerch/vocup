using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Vocup.Properties;

namespace Vocup
{
    public partial class backup_add : Form
    {
        public backup_add()
        {
            InitializeComponent();
            Icon = Icon.FromHandle(Icons.backup_add.GetHicon());
        }

        public string pfad;

        private void backup_add_Load(object sender, EventArgs e)
        {
            //Sonderzeichentabellen einlesen

            string file_path = Path.Combine(Properties.Settings.Default.path_vhr, "specialchar");

            DirectoryInfo directory_info = new DirectoryInfo(file_path);

            if (directory_info.Exists == true)
            {
                FileInfo[] files = directory_info.GetFiles();

                //Listbox füllen

                for (int i = 0; i < files.Length; i++)
                {
                    try
                    {

                        int j = 0;

                        if (files[i].Extension == ".txt")
                        {
                            //Listbox-Item hinzufügen

                            listbox_special_chars.Items.Add(Path.GetFileNameWithoutExtension(files[i].FullName), true);
                            j++;
                        }
                        if (j == 0)
                        {
                            groupBox3.Enabled = false;
                            listbox_special_chars.Enabled = false;
                        }
                    }
                    catch
                    {

                    }
                }
            }
            else
            {
                groupBox3.Enabled = false;
                listbox_special_chars.Enabled = false;
            }

        }


        //Listbox und Buttons ein- oder ausschalten falls nötig
        private void folgende_vokabelhefte_CheckedChanged(object sender, EventArgs e)
        {
            if (folgende_vokabelhefte.Checked == true)
            {
                listbox_vokabelhefte.Enabled = true;
                add_vokabelheft_button.Enabled = true;
                
            }
            else
            {
                listbox_vokabelhefte.Enabled = false;
                add_vokabelheft_button.Enabled = false;
                delete_vokabelheft_button.Enabled = false;
            }

            coordinater();

            if (listbox_vokabelhefte.Items.Count == 0 && alle_vokabelhefte.Checked == false)
            {
                gewaehlte_ergebnisse.Enabled = false;

                if (gewaehlte_ergebnisse.Checked == true)
                {
                    alle_ergebnisse.Checked = true;
                }
            }
            else
            {
                gewaehlte_ergebnisse.Enabled = true;
            }

        }

        //Falls nötig den Speichern-Button ausschalten
        private void keine_ergebnisse_CheckedChanged(object sender, EventArgs e)
        {
            coordinater();
        }

        private void alle_vokabelhefte_CheckedChanged(object sender, EventArgs e)
        {
            coordinater();
            
            if (listbox_vokabelhefte.Items.Count == 0 && alle_vokabelhefte.Checked == false)
            {
                gewaehlte_ergebnisse.Enabled = false;

                if (gewaehlte_ergebnisse.Checked == true)
                {
                    alle_ergebnisse.Checked = true;
                }
            }
            else
            {
                gewaehlte_ergebnisse.Enabled = true;
            }

        }

        private void listbox_special_chars_SelectedValueChanged(object sender, EventArgs e)
        {
            coordinater();
        }

        private void create_backup_button_MouseEnter(object sender, EventArgs e)
        {
            coordinater();

        }

        private void coordinater()
        {
            bool choosed_vokabehlefte = false;
            if (listbox_vokabelhefte.Items.Count > 0 && folgende_vokabelhefte.Checked == true)
            {
                choosed_vokabehlefte = true;
            }
            if (keine_ergebnisse.Checked == true && alle_vokabelhefte.Checked == false && choosed_vokabehlefte == false && listbox_special_chars.CheckedItems.Count < 1)
            {
                create_backup_button.Enabled = false;
            }
            else
            {
                create_backup_button.Enabled = true;
            }
        }

        //Open-file Dialog starten
        private void add_vokabelheft_button_Click(object sender, EventArgs e)
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
                    if (listbox_vokabelhefte.Items.Contains(add_file.FileNames[i]) == false)
                    {
                        listbox_vokabelhefte.Items.Add(add_file.FileNames[i]);

                        //Falls möglich Buttons etc. aktivieren
                        if (create_backup_button.Enabled == false && listbox_vokabelhefte.Items.Count > 0)
                        {
                            create_backup_button.Enabled = true;
                        }

                        if (listbox_vokabelhefte.Items.Count == 0 && alle_vokabelhefte.Checked == false)
                        {
                            gewaehlte_ergebnisse.Enabled = false;

                            if (gewaehlte_ergebnisse.Checked == true)
                            {
                                alle_ergebnisse.Checked = true;
                            }
                        }
                        else
                        {
                            gewaehlte_ergebnisse.Enabled = true;
                        }
                    }
                }
            }
        }

        //Vokabelheft aus der Liste löschen
        private void delete_vokabelheft_button_Click(object sender, EventArgs e)
        {
            while (listbox_vokabelhefte.SelectedItems.Count > 0)
            {
                listbox_vokabelhefte.Items.Remove(listbox_vokabelhefte.SelectedItems[0]);
            }
            
            //Buttons etc. deaktivieren, falls keine Items mehr vorhanden sind
            if (listbox_vokabelhefte.SelectedItems.Count < 1)
            {
                delete_vokabelheft_button.Enabled = false;
            }
            if (listbox_vokabelhefte.Items.Count < 1)
            {
                if (keine_ergebnisse.Checked == true && alle_vokabelhefte.Checked == false && listbox_special_chars.CheckedItems.Count < 1)
                {
                    create_backup_button.Enabled = false;
                }
                if (listbox_vokabelhefte.Items.Count == 0 && alle_vokabelhefte.Checked == false)
                {
                    gewaehlte_ergebnisse.Enabled = false;

                    if (gewaehlte_ergebnisse.Checked == true)
                    {
                        alle_ergebnisse.Checked = true;
                    }
                }
                else
                {
                    gewaehlte_ergebnisse.Enabled = true;
                }
            }
        }

        private void listbox_vokabelhefte_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listbox_vokabelhefte.SelectedItems.Count < 1)
            {
                delete_vokabelheft_button.Enabled = false;
            }
            else
            {
                delete_vokabelheft_button.Enabled = true;
            }
        }

        //Save-Dialog starten

        private void create_backup_button_Click(object sender, EventArgs e)
        {
            //Falls auf den Button geklickt wurde, aber nichts ausgewählt ist

            bool choosed_vokabehlefte = false;
            if (listbox_vokabelhefte.Items.Count > 0 && folgende_vokabelhefte.Checked == true)
            {
                choosed_vokabehlefte = true;
            }
            if (keine_ergebnisse.Checked == true && alle_vokabelhefte.Checked == false && choosed_vokabehlefte == false && listbox_special_chars.CheckedItems.Count < 1)
            {
                create_backup_button.Enabled = false;
            }
            //Falls Daten zum wiederherstellen vorhanden sind
            else
            {
                //Speichern-Dialog anzeigen

                SaveFileDialog save = new SaveFileDialog();
                save.Title = Properties.language.save_title_backup;

                if (Properties.Settings.Default.backup_folder == "" || Properties.Settings.Default.backup_folder == null)
                {
                    save.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                }
                else
                {
                    DirectoryInfo info = new DirectoryInfo(Properties.Settings.Default.backup_folder);

                    if (info.Exists == true)
                    {
                        save.InitialDirectory = Properties.Settings.Default.backup_folder;
                    }
                }


                save.Filter = Properties.language.backup + " (*.vdp)|*.vdp";


                if (save.ShowDialog() == DialogResult.OK)
                {
                    pfad = save.FileName;

                    FileInfo get_folder_path = new FileInfo(save.FileName);

                    Properties.Settings.Default.backup_folder = get_folder_path.DirectoryName;

                    Properties.Settings.Default.Save();

                    DialogResult = DialogResult.OK;
                }
            }
        }
    }
}