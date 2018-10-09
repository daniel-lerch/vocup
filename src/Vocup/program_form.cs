using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Vocup.Controls;
using Vocup.Forms;
using Vocup.IO;
using Vocup.Models;
using Vocup.Properties;
using Vocup.Util;

namespace Vocup
{
    public partial class program_form : Form, IMainForm
    {
        // Deprecated, only for compatibility
        ListView originalListView;

        //gibt an, ob die Datei geändert wurde
        [Obsolete]
        bool edit_vokabelheft = false;

        //Gibt den Pfad zum Vokabelheft an
        [Obsolete]
        string pfad_vokabelheft = "";

        //Gibt das Start-Argument an
        string args_pfad = "";

        //Liste der Vokabeln die ausgedruckt werden soll
        int[] vokabelliste;

        //von Mutter zu Fremdsprache oder umgekehrt drucken
        bool if_own_to_foreign;

        //Anzahl Vokabeln beim Drucken
        int anz_vok;

        //Anzahl zu druckende Seiten
        int anzahl_seiten;

        //Aktuelle zu druckende Seite
        int aktuelle_seite;

        //Vorder- oder Rückseite bei den Kärtchen
        bool if_foreside;

        //Papiereinzug
        bool is_front;

        public program_form(string[] args)
        {
            InitializeComponent();

            originalListView = listView_vokabeln;

            //Falls die exe mit einer Vokabelheft-Datei geöffnet wurde:
            if (args.Length != 0)
            {
                args_pfad = args[0];
            }
        }

        public VocabularyBook CurrentBook { get; private set; }
        public VocabularyBookController CurrentController { get; private set; }
        public StatisticsPanel StatisticsPanel => GroupStatistics;
        public bool UnsavedChanges => CurrentBook?.UnsavedChanges ?? false;

        public void VocabularyWordSelected(bool value)
        {
            BtnEditWord.Enabled = value;
            TsmiEditWord.Enabled = value;
            BtnDeleteWord.Enabled = value;
            TsmiDeleteWord.Enabled = value;
        }
        public void VocabularyBookLoaded(bool value)
        {
            GroupBook.Enabled = value;
            GroupWord.Enabled = value;
            GroupSearch.Enabled = value;
            TsmiAddWord.Enabled = value;
            BtnBookSettings.Enabled = value;
            TsmiBookOptions.Enabled = value;
            BtnPractice.Enabled = value;
            TsmiCloseBook.Enabled = value;
            TsmiSaveAs.Enabled = value;
            TsmiOpenInExplorer.Enabled = value;
        }
        public void VocabularyBookHasContent(bool value)
        {
            GroupSearch.Enabled = value;
            if (!value) TbSearchWord.Text = "";

            TsmiPrint.Enabled = value;
            TsbPrint.Enabled = value;

            TsmiExport.Enabled = value;
        }
        public void VocabularyBookPracticable(bool value)
        {
            BtnPractice.Enabled = value;
            TsmiPractice.Enabled = value;
        }
        public void VocabularyBookUnsavedChanges(bool value)
        {
            TsmiSave.Enabled = value;
            TsbSave.Enabled = value;
        }
        public void VocabularyBookName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                Text = Words.Vocup;
            else
                Text = $"{Words.Vocup} - {value}";
        }
        public void LoadBook(VocabularyBook book)
        {
            VocabularyBookController controller = new VocabularyBookController(book) { Parent = this };
            SplitContainer.Panel2.Controls.Remove(listView_vokabeln);
            SplitContainer.Panel2.Controls.Add(controller.ListView);
            controller.ListView.PerformLayout();

            CurrentBook = book;
            CurrentController = controller;
            listView_vokabeln = controller.ListView.Control;

            VocabularyBookLoaded(true);

            // TODO: Select file in tree view
        }
        public void UnloadBook()
        {
            VocabularyBookController controller = CurrentController;
            CurrentBook = null;
            CurrentController = null;
            listView_vokabeln = originalListView;
            SplitContainer.Panel2.Controls.Remove(controller.ListView);
            SplitContainer.Panel2.Controls.Add(originalListView);
            originalListView.PerformLayout();

            VocabularyWordSelected(false);
            VocabularyBookLoaded(false);
            VocabularyBookHasContent(false);
            VocabularyBookPracticable(false);
            VocabularyBookUnsavedChanges(false);

            // TODO: Unselect file in tree view
        }

        //Sobald die Form geladen wird
        private void Form_Load(object sender, EventArgs e)
        {
            FileTreeView.RootPath = Settings.Default.VhfPath;

            Update();
            Activate();

            //Schaut ob die zuletzt geöffnete Datei noch Existiert

            if (args_pfad != "")
            {
                FileInfo info = new FileInfo(args_pfad);
                if (info.Extension == ".vhf")
                {
                    //Öffnet die Vokabeldatei

                    readfile(args_pfad);
                    args_pfad = "";
                }
                else if (info.Extension == ".vdp")
                {
                    //Nicht löschen!!

                }
                else
                {
                    MessageBox.Show(Properties.language.messagebox_no_vhf,
                                             "Error",
                                             MessageBoxButtons.OK,
                                             MessageBoxIcon.Error);
                }
            }
            else if (File.Exists(Settings.Default.last_file) &&
                Settings.Default.StartScreen == (int)StartScreen.LastFile)
            {
                readfile(Settings.Default.last_file); //Start-Screen festlegen
            }
        }

        private void Form_Shown(object sender, EventArgs e)
        {
            //Falls nötig Datensicherung Wiederherstellen öffnen

            if (args_pfad != "")
            {
                FileInfo info_vdp = new FileInfo(args_pfad);
                if (info_vdp.Extension == ".vdp")
                {
                    //Öffnet die Datensicherung

                    restore_backup(args_pfad);
                    args_pfad = "";
                }
            }
            else if (Settings.Default.StartScreen == (int)StartScreen.AboutBox) //Willkommensbild anzeigen
            {
                new AboutBox().ShowDialog();

                Settings.Default.StartScreen = (int)StartScreen.LastFile;
                Settings.Default.Save();
            }

            //Eventuell Updater ausschalten

            if (File.Exists(Path.Combine(Application.StartupPath, "updateroff.txt")))
            {
                TsmiUpdate.Enabled = false;
            }
            else if (Properties.Settings.Default.DisableInternetServices) //Eventuell nach Updates suchen
            {
                if ((Properties.Settings.Default.LastInternetConnection - DateTime.Now).TotalDays >= 30.0) // New binary format instead of "{Year}|{DayOfYear}"
                {
                    try
                    {
                        search_update();
                        Properties.Settings.Default.LastInternetConnection = DateTime.Now;
                    }
                    catch
                    {
                    }
                }
                else
                {
                    Properties.Settings.Default.LastInternetConnection = DateTime.MinValue;
                    Properties.Settings.Default.Save();
                }
            }

            Properties.Settings.Default.startup_counter++;
            Properties.Settings.Default.Save();
        }

        //-----


        //Dialoge

        //Hife
        private void TsmiHelp_Click(object sender, EventArgs e)
        {
            string path = Path.Combine(Application.StartupPath, "help.chm");
            Help.ShowHelp(this, new Uri(new Uri("file://"), path).ToString());
        }

        //AboutBox
        private void TsmiAbout_Click(object sender, EventArgs e)
        {
            new AboutBox().ShowDialog();
        }

        //Infos
        private void TsbtnEvalutionInfo_Click(object sender, EventArgs e)
        {
            new EvaluationInfoDialog().ShowDialog();
        }

        private void TsmiEvaluationInfo_Click(object sender, EventArgs e)
        {
            new EvaluationInfoDialog().ShowDialog();
        }

        //Optionen
        private void TsmiSettings_Click(object sender, EventArgs e)
        {
            string oldVhfPath = Settings.Default.VhfPath;

            SettingsDialog optionen = new SettingsDialog();

            if (optionen.ShowDialog() == DialogResult.OK)
            {
                // Renew practice state for Settings.MaxPracticeCount changes
                CurrentBook?.Words.ForEach(x => x.RenewPracticeState());

                if (CurrentController != null)
                    CurrentController.ListView.GridLines = Settings.Default.GridLines;

                // Eventually refresh tree view root path
                if (oldVhfPath != Settings.Default.VhfPath)
                    FileTreeView.RootPath = Settings.Default.VhfPath;

                //Autosave
                if (Settings.Default.AutoSave && (CurrentBook?.UnsavedChanges ?? false))
                {
                    savefile(false);
                }
            }
        }

        //Sonderzeichen verwalten

        private void TsmiSpecialChar_Click(object sender, EventArgs e)
        {
            new SpecialCharManage().ShowDialog();
        }

        //Updates
        private void TsmiUpdate_Click(object sender, EventArgs e)
        {
            search_update();
        }

        private void FileTreeView_FileSelected(object sender, FileSelectedEventArgs e)
        {
            if (UnsavedChanges && !vokabelheft_ask_to_save())
            {
                return;
            }

            readfile(e.FullName);
        }

        //Neues Vokabelheft erstellen

        private void TsbCreateBook_Click(object sender, EventArgs e)
        {
            create_new_vokabelheft();
        }

        private void TsmiCreateBook_Click(object sender, EventArgs e)
        {
            create_new_vokabelheft();
        }

        //-----

        //Vokabelheft bearbeiten

        private void BtnBookSettings_Click(object sender, EventArgs e)
        {
            edit_vokabelheft_dialog();
        }

        private void TsmiBookOptions_Click(object sender, EventArgs e)
        {
            edit_vokabelheft_dialog();
        }

        //-----


        // Öffnen

        private void TsbOpenBook_Click(object sender, EventArgs e)
        {
            open_file();
        }

        private void TsmiOpenBook_Click(object sender, EventArgs e)
        {
            open_file();
        }

        //-----


        // Vokabel hinzufügen

        private void BtnAddWord_Click(object sender, EventArgs e)
        {
            add_vokabel(false, false);
        }

        private void TsmiAddWord_Click(object sender, EventArgs e)
        {
            add_vokabel(false, false);
        }
        //-----


        //Vokabel bearbeiten

        private void BtnEditWord_Click(object sender, EventArgs e)
        {
            edit_vokabel_dialog();
        }

        private void TsmiEditWord_Click(object sender, EventArgs e)
        {
            edit_vokabel_dialog();
        }


        //Vokabeln löschen

        private void BtnDeleteWord_Click(object sender, EventArgs e)
        {
            vokabel_delete(); //Vokabel löschen (Toolbar)
        }

        private void TsmiDeleteWord_Click(object sender, EventArgs e)
        {
            vokabel_delete(); //Vokabel löschen (Menü)
        }

        //-----

        //Vokabelheft speichern

        private void TsmiSave_Click(object sender, EventArgs e)
        {
            try
            {
                savefile(false);
            }
            catch
            {
                pfad_vokabelheft = "";
                savefile(false);
            }
        }

        private void TsmiSaveAs_Click(object sender, EventArgs e)
        {
            if (pfad_vokabelheft != "")
            {
                savefile(true);
            }
            else
            {
                savefile(false);
            }
        }

        private void TsbSave_Click(object sender, EventArgs e)
        {
            try
            {
                savefile(false);
            }
            catch
            {
                pfad_vokabelheft = "";
                savefile(false);
            }
        }

        //-----

        //Vokabeln Üben

        private void BtnPractice_Click(object sender, EventArgs e)
        {
            vokabeln_üben();
        }

        private void TsmiPractice_Click(object sender, EventArgs e)
        {
            vokabeln_üben();
        }


        //-----

        //Vokabelheft drucken

        private void TsbPrint_Click(object sender, EventArgs e)
        {
            print_file();
        }

        private void TsmiPrint_Click(object sender, EventArgs e)
        {
            print_file();
        }

        //-----

        //Nach Vokabel suchen

        private void BtnSearchWord_Click(object sender, EventArgs e)
        {
            search_vokabel(TbSearchWord.Text);
        }

        private void TbSearchWord_TextChanged(object sender, EventArgs e)
        {
            //Falls kein Suchtext eingegeben wurde, Button deaktivieren

            if (TbSearchWord.Text != "")
            {
                BtnSearchWord.Enabled = true;
                AcceptButton = BtnSearchWord;
            }
            else
            {
                BtnSearchWord.Enabled = false;
                AcceptButton = BtnAddWord;
            }
        }

        //-----

        //Vokabelheft schliessen

        private void TsmiCloseBook_Click(object sender, EventArgs e)
        {
            if (UnsavedChanges && vokabelheft_ask_to_save())
            {
                close_vokabelheft();
            }
        }
        //-----

        //ListView

        private void listView_vokabeln_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            edit_vokabel_dialog();
        }

        //-----



        //***Methoden***


        // Datei einlesen

        private void readfile(string path)
        {
            VocabularyBook book = new VocabularyBook();
            // TODO: Check result codes
            VocabularyFile.ReadVhfFile(path, book);
            VocabularyFile.ReadVhrFile(book);
            book.Notify();

            LoadBook(book);
        }

        /*
        private void readfile(string file_path)
        {
            //Cursor auf warten setzen
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                //Falls eine neue Ergebnis-Datei gespeichert werden soll
                if (save_new == true)
                {
                    savefile(false);
                }

                listView_vokabeln.Enabled = false;

                listView_vokabeln.BeginUpdate();

                vokabelheft_code = "";
                edit_vokabelheft = false;

                save_vokabelheft.Enabled = false;
                SpeichernToolStripMenuItem.Enabled = false;

                uebersetzungsrichtung = "";

                pfad_vokabelheft = "";

                save_new = false;


                // Read file and decrypt
                string plaintext;
                using (StreamReader reader = new StreamReader(file_path, Encoding.UTF8))
                    plaintext = Crypto.Decrypt(reader.ReadToEnd());

                // Zeilen der Datei in ein Array abspeichern
                string[] lines = plaintext.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

                // Anzahl Arrayfelder bestimmen
                string version = lines[0];

                //Schauen ob Vokabeln vorhanden sind
                if (lines.Length > 4 && version == "1.0")
                {
                    //Anzahl vokabeln in einem String 
                    string[,] vokabeln = new string[lines.Length - 4, 3];

                    // vokabeln splitten
                    for (int i = 4; i <= lines.Length - 1; i++)
                    {
                        //linesnumb + 1 ^= Vokabel (in einem Array wird bei 0 begonnen) | 1. Vokabel bei zeile Nr. 5
                        string[] line = lines[i].Split('#');

                        //Vokabeln von lines zu vokabeln
                        vokabeln[i - 4, 0] = line[0];
                        vokabeln[i - 4, 1] = line[1];
                        vokabeln[i - 4, 2] = line[2];
                    }

                    //Header

                    listView_vokabeln.BeginUpdate();

                    if (listView_vokabeln.Items.Count > 0)
                    {
                        listView_vokabeln.Clear();
                    }
                    if (listView_vokabeln.Columns.Count > 0)
                    {
                        listView_vokabeln.Columns.Clear();
                    }

                    listView_vokabeln.Invalidate();

                    listView_vokabeln.Columns.Add("", 20);

                    //Header anpassen, falls Fenster maximiert ist
                    if (WindowState == FormWindowState.Maximized)
                    {
                        int size = (listView_vokabeln.Width - 20 - 100 - 22) / 2;

                        listView_vokabeln.Columns.Add(lines[2], size);
                        listView_vokabeln.Columns.Add(lines[3], size);
                    }
                    else
                    {
                        listView_vokabeln.Columns.Add(lines[2], 155);
                        listView_vokabeln.Columns.Add(lines[3], 200);
                    }

                    listView_vokabeln.Columns.Add(Words.LastPracticed, 100);

                    listView_vokabeln.EndUpdate();

                    //code speichern
                    vokabelheft_code = lines[1];

                    // Datei mit Ergebnissen aufrufen
                    string ergebnisse_pfad = Path.Combine(Properties.Settings.Default.path_vhr, vokabelheft_code + ".vhr");
                    FileInfo check_file = new FileInfo(ergebnisse_pfad);

                    if (check_file.Exists == true)
                    {
                        try
                        {
                            // Read and decrypt file
                            string plaintext2;
                            using (StreamReader ergebnisse_reader = new StreamReader(ergebnisse_pfad, Encoding.UTF8))
                                plaintext2 = Crypto.Decrypt(ergebnisse_reader.ReadToEnd());

                            // Zeilen der Datei in ein Array abspeichern
                            string[] ergebnisse_lines = plaintext2.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

                            // Array splitten
                            string[,] ergebnisse = new string[ergebnisse_lines.Length - 2, 2];

                            //Übungsvariante speichern
                            uebersetzungsrichtung = ergebnisse_lines[1];

                            for (int counter = 2; counter <= ergebnisse_lines.Length - 1; counter++)
                            {
                                string[] i = ergebnisse_lines[counter].Split('#');

                                //Vokabeln von ergebnisse_lines zu ergebnisse

                                ergebnisse[counter - 2, 0] = i[0];
                                ergebnisse[counter - 2, 1] = i[1];
                            }

                            bool result = false;

                            //Vokabeln einlesen falls Ergebnisse vorhanden sind
                            //und der Pfad stimmt

                            if (ergebnisse_lines[0] == file_path && ergebnisse_lines.Length - 2 == lines.Length - 4)
                            {
                                result = true;
                            }

                            // Falls Pfad stimmt, aber nicht gleiche Anzahl Linien

                            else if (ergebnisse_lines[0] == file_path && ergebnisse_lines.Length - 2 != lines.Length - 4)
                            {
                                MessageBox.Show(Properties.language.messagebox_file_copy,
                                AppInfo.Name,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                                vokabelheft_code = "";

                                save_new = true;
                            }

                            //Falls Pfad nicht stimmt , aber gleiche Anzahl Linien

                            else if (ergebnisse_lines[0] != file_path && ergebnisse_lines.Length - 2 == lines.Length - 4)
                            {

                                result = true;

                                //Überprüfen, ob es eine Datei zum Pfad gibt --> Falls nicht, wird der Pfad geändert. Ansonsten neue Datei erstellen

                                FileInfo info = new FileInfo(ergebnisse_lines[0]);

                                if (info.Exists == true)
                                {
                                    vokabelheft_code = "";
                                }

                                save_new = true;
                            }

                            //Falls Pfad und Anzahl Linien nicht stimmen

                            else if (ergebnisse_lines[0] != file_path && ergebnisse_lines.Length - 2 != lines.Length - 4)
                            {
                                MessageBox.Show(Properties.language.messagebox_file_copy,
                                AppInfo.Name,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);


                                vokabelheft_code = "";

                                save_new = true;
                            }

                            //Falls Datei fehlerhaft ist
                            else
                            {
                                MessageBox.Show(Properties.language.messagebox_fautly_results,
                                    AppInfo.Name,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);

                                vokabelheft_code = "";

                                save_new = true;
                            }


                            if (result == true)
                            {
                                //Max. beim üben ermitteln

                                int max = Properties.Settings.Default.max;

                                for (int voknum = 0; voknum < (vokabeln.Length) / 3; voknum++)
                                {

                                    int anzahl_geübt = Convert.ToInt32(ergebnisse[voknum, 0]);

                                    string[] subitems = new string[4];

                                    subitems[0] = "";
                                    subitems[1] = vokabeln[voknum, 0];
                                    subitems[2] = vokabeln[voknum, 1];


                                    //Falls ein Synonym vorhanden ist

                                    if (vokabeln[voknum, 2] != "")
                                    {
                                        subitems[2] = subitems[2] + "=" + vokabeln[voknum, 2];
                                    }

                                    subitems[3] = ergebnisse[voknum, 1];

                                    //Neues Listview-Item erstellen
                                    ListViewItem new_item = new ListViewItem(subitems);

                                    // Ergebnis eintragen

                                    if (anzahl_geübt == 0)
                                    {
                                        new_item.ImageIndex = 0;
                                    }
                                    else if (anzahl_geübt == 1)
                                    {
                                        new_item.ImageIndex = 1;
                                    }
                                    else if (anzahl_geübt > 1 && anzahl_geübt < max + 1)
                                    {
                                        new_item.ImageIndex = 2;
                                    }
                                    else if (anzahl_geübt >= max + 1)
                                    {
                                        new_item.ImageIndex = 3;
                                    }

                                    new_item.Tag = anzahl_geübt;

                                    //Item Hinzufügen
                                    listView_vokabeln.Items.Add(new_item);
                                }
                                //Statistik 

                            }
                            else
                            {
                                //Vokabeln einlesen falls ergebnisse nicht vorhanden sind

                                for (int voknum = 0; voknum < (vokabeln.Length) / 3; voknum++)
                                {
                                    string[] subitems = new string[4];

                                    subitems[0] = "";
                                    subitems[1] = vokabeln[voknum, 0];
                                    subitems[2] = vokabeln[voknum, 1];

                                    //Falls ein Synonym vorhanden ist
                                    if (vokabeln[voknum, 2] != "")
                                    {
                                        subitems[2] = subitems[2] + "=" + vokabeln[voknum, 2];
                                    }

                                    subitems[3] = "";

                                    ListViewItem new_item = new ListViewItem(subitems);

                                    new_item.ImageIndex = 0;
                                    new_item.Tag = 0;

                                    //Item Hinzufügen
                                    listView_vokabeln.Items.Add(new_item);
                                }
                            }
                        }
                        catch
                        {
                            MessageBox.Show(Properties.language.messagebox_fautly_results,
                                   AppInfo.Name,
                                   MessageBoxButtons.OK,
                                   MessageBoxIcon.Error);

                            FileInfo info = new FileInfo(ergebnisse_pfad);

                            try
                            {
                                info.Delete();
                            }
                            catch
                            {

                            }
                            readfile(file_path);

                        }
                    }
                    else
                    {
                        //Vokabeln einlesen falls ergebnisse nicht vorhanden sind

                        for (int voknum = 0; voknum < (vokabeln.Length) / 3; voknum++)
                        {
                            string foreign = vokabeln[voknum, 1];
                            if (!string.IsNullOrWhiteSpace(vokabeln[voknum, 2])) // Display a synonym
                                foreign += "=" + vokabeln[voknum, 2];
                            string[] subitems = { "", vokabeln[voknum, 0], foreign, "" };

                            ListViewItem new_item = new ListViewItem(subitems);

                            new_item.ImageIndex = 0;
                            new_item.Tag = 0;

                            listView_vokabeln.Items.Add(new_item); //Item Hinzufügen
                        }
                    }

                    //ListView sortieren
                    listView_vokabeln.ListViewItemSorter = new ListViewComparer(1, SortOrder.Ascending);

                    // Pfad in Vokabel_infos speichern
                    pfad_vokabelheft = file_path;

                    //Pfad speichern
                    Properties.Settings.Default.last_file = file_path;
                    Properties.Settings.Default.Save();

                    //Infos
                    infos_vokabelhefte_text();

                    listView_vokabeln.Enabled = true;
                    listView_vokabeln.BackColor = SystemColors.Window;

                    //group-boxes aktivieren
                    listView_vokabeln.GridLines = Properties.Settings.Default.GridLines;

                    groupBox1.Enabled = true;
                    groupBox2.Enabled = true;
                    groupBox3.Enabled = true;
                    statistik.Visible = true;

                    practice_vokabelheft.Enabled = true;
                    vokabelnÜbenToolStripMenuItem.Enabled = true;

                    neueVokabelHinzufügenToolStripMenuItem.Enabled = true;
                    vokabelheftOptionenToolStripMenuItem.Enabled = true;

                    edit_vokabel.Enabled = false;
                    vokabelBearbeitenToolStripMenuItem.Enabled = false;

                    delet_vokabel.Enabled = false;
                    vokabelLöschenToolStripMenuItem.Enabled = false;

                    search_vokabel_field.Enabled = true;
                    search_vokabel_field.Text = "";

                    SpeichernUnterToolStripMenuItem.Enabled = true;

                    vokabelheftSchliessenToolStripMenuItem.Enabled = true;

                    druckenToolStripMenuItem.Enabled = true;
                    print_vokabelheft.Enabled = true;

                    vokabelheftSendenToolStripMenuItem.Enabled = true;

                    exportierenToolStripMenuItem.Enabled = true;

                    //Titelleiste
                    string file_name = Path.GetFileNameWithoutExtension(pfad_vokabelheft);

                    Text = $"{Words.Vocup} - {Path.GetFileNameWithoutExtension(pfad_vokabelheft)}";

                    //Falls save_new == true wird der neue Pfad in die Ergebnis-Datei geschrieben und falls vokabelheft_code = "" wird die ganze datei neu geschrieben
                    //--> Siehe Close()
                }
                else if (lines.Length == 4 && version == "1.0")
                {
                    vokabelheft_code = "";
                    // Listview vorbereiten

                    //Header
                    listView_vokabeln.Clear();
                    listView_vokabeln.Columns.Clear();
                    listView_vokabeln.Columns.Add("", 20);

                    //Header anpassen, falls Fenster maximiert ist
                    if (WindowState == FormWindowState.Maximized)
                    {
                        int size = (listView_vokabeln.Width - 20 - 100 - 22) / 2;

                        listView_vokabeln.Columns.Add(lines[2], size);
                        listView_vokabeln.Columns.Add(lines[3], size);
                    }
                    else
                    {
                        listView_vokabeln.Columns.Add(lines[2], 155);
                        listView_vokabeln.Columns.Add(lines[3], 200);
                    }

                    listView_vokabeln.Columns.Add(Words.LastPracticed, 100);

                    // Pfad in Vokabel_infos speichern
                    pfad_vokabelheft = file_path;
                    vokabelheft_code = lines[1];
                    edit_vokabelheft = false;

                    //Pfad speichern
                    Properties.Settings.Default.last_file = file_path;
                    Properties.Settings.Default.Save();

                    //Infos
                    infos_vokabelhefte_text();
                    listView_vokabeln.Enabled = true;
                    listView_vokabeln.BackColor = SystemColors.Window;

                    //group-boxes aktivieren

                    groupBox1.Enabled = true;
                    groupBox2.Enabled = true;
                    groupBox3.Enabled = true;
                    statistik.Visible = true;

                    practice_vokabelheft.Enabled = false;
                    vokabelnÜbenToolStripMenuItem.Enabled = false;

                    search_vokabel_button.Enabled = false;
                    search_vokabel_field.Text = "";
                    search_vokabel_field.Enabled = false;

                    neueVokabelHinzufügenToolStripMenuItem.Enabled = true;
                    vokabelheftOptionenToolStripMenuItem.Enabled = true;

                    edit_vokabel.Enabled = false;
                    vokabelBearbeitenToolStripMenuItem.Enabled = false;

                    delet_vokabel.Enabled = false;
                    vokabelLöschenToolStripMenuItem.Enabled = false;

                    save_vokabelheft.Enabled = false;
                    SpeichernToolStripMenuItem.Enabled = false;
                    SpeichernUnterToolStripMenuItem.Enabled = true;

                    vokabelheftSchliessenToolStripMenuItem.Enabled = true;

                    druckenToolStripMenuItem.Enabled = false;
                    print_vokabelheft.Enabled = false;

                    vokabelheftSendenToolStripMenuItem.Enabled = false;

                    exportierenToolStripMenuItem.Enabled = false;


                    //Titelleiste
                    Text = $"{Words.Vocup} - {Path.GetFileNameWithoutExtension(pfad_vokabelheft)}";
                }
                else if (version != "1.0" && lines[0].Length <= 4 || lines[0].Length <= 4 && Convert.ToInt32(version) > 1)
                {

                    close_vokabelheft();

                    // Datei zu neu

                    DialogResult result = MessageBox.Show(Properties.language.messagebox_new_program_version,
                      AppInfo.Name,
                      MessageBoxButtons.YesNo,
                      MessageBoxIcon.Information);

                    //Schauen, ob nach Updates gesucht werden soll

                    if (result == DialogResult.Yes)
                    {
                        search_update();
                    }

                }
                else if (lines[0].Length > 4 || lines.Length < 4)
                {
                    close_vokabelheft();

                    MessageBox.Show(Properties.language.messagebox_faulty_file,
                                    Properties.language.error,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }
            catch
            {
                close_vokabelheft();

                MessageBox.Show(Properties.language.messagebox_reading_fault,
                                 Properties.language.error,
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Error);

            }

            listView_vokabeln.EndUpdate();

            //Cursor auf normal setzen
            Cursor.Current = Cursors.Default;
        }
        */

        // Fragen, ob Änderungen gespeichert werden sollen

        private bool vokabelheft_ask_to_save()
        {
            DialogResult result = MessageBox.Show(Messages.GeneralSaveChanges, Messages.GeneralSaveChangesT, MessageBoxButtons.YesNoCancel);

            if (result == DialogResult.Yes)
            {
                return savefile(false); // Save file and return true which means to continue with the next action.
            }
            else if (result == DialogResult.No)
            {
                return true; // Do not save file and return true.
            }
            else
            {
                return false; // Return false to indicate the users choice to stay at the current screen.
            }
        }

        private bool savefile(bool saveAsNewFile)
        {
            //Datei-Speichern-unter Dialogfeld öffnen
            if (string.IsNullOrWhiteSpace(CurrentBook.FilePath)
                || string.IsNullOrWhiteSpace(CurrentBook.VhrCode)
                || saveAsNewFile)
            {
                SaveFileDialog save = new SaveFileDialog
                {
                    Title = Words.SaveVocabularyBook,
                    FileName = CurrentBook.MotherTongue + " - " + CurrentBook.ForeignLang,
                    InitialDirectory = Settings.Default.VhfPath,
                    Filter = Words.VocupVocabularyBookFile + " (*.vhf)|*.vhf"
                };

                if (save.ShowDialog() == DialogResult.OK)
                {
                    CurrentBook.FilePath = save.FileName;
                    CurrentBook.GenerateVhrCode();
                }
                else
                {
                    return false;
                }
            }

            Cursor.Current = Cursors.WaitCursor;

            // TODO: Check result codes
            VocabularyFile.WriteVhfFile(CurrentBook.FilePath, CurrentBook);
            VocabularyFile.WriteVhrFile(CurrentBook);

            CurrentBook.UnsavedChanges = false;

            Settings.Default.last_file = pfad_vokabelheft;
            Settings.Default.Save();

            Cursor.Current = Cursors.Default;

            return true;
        }

        // Öffnen Dialog

        private void open_file()
        {
            //Dateien öffnen

            listView_vokabeln.Enabled = false;

            OpenFileDialog open = new OpenFileDialog
            {
                Title = Words.OpenVocabularyBook,
                InitialDirectory = Properties.Settings.Default.VhfPath,
                Filter = Words.VocupVocabularyBookFile + " (*.vhf)|*.vhf"
            };

            if (open.ShowDialog() == DialogResult.OK)
            {
                //Datei einlesen

                bool result = true;
                if (edit_vokabelheft == true)
                {
                    result = vokabelheft_ask_to_save();
                }
                if (result == true)
                {
                    listView_vokabeln.BeginUpdate();

                    readfile(open.FileName);

                    listView_vokabeln.EndUpdate();

                    listView_vokabeln.Enabled = true;
                    listView_vokabeln.BackColor = SystemColors.Window;

                    // TODO: Ensure selection works
                    //treeView.SelectedNode = treeView.Nodes[0];
                }
            }
            else
            {
                listView_vokabeln.Enabled = true;
            }
        }

        //neues Vokabelheft erstellen

        private void create_new_vokabelheft()
        {

            listView_vokabeln.Enabled = false;

            //Dialog starten
            VocabularyBookSettings add_vokabelheft = new VocabularyBookSettings(out VocabularyBook book)
            {
                Owner = this
            };

            DialogResult new_vokabelheft_result = add_vokabelheft.ShowDialog();

            if (DialogResult.OK == new_vokabelheft_result)
            {
                //Listview vorbereiten
                bool result = true;

                if (edit_vokabelheft == true)
                {
                    result = vokabelheft_ask_to_save();
                }

                if (result == true)
                {

                    listView_vokabeln.BeginUpdate();

                    listView_vokabeln.GridLines = Properties.Settings.Default.GridLines;
                    listView_vokabeln.Clear();
                    listView_vokabeln.BackColor = SystemColors.Window;

                    listView_vokabeln.Columns.Add("", 20);

                    //Header anpassen, falls Fenster maximiert ist
                    if (WindowState == FormWindowState.Maximized)
                    {
                        int size = (listView_vokabeln.Width - 20 - 100 - 22) / 2;

                        listView_vokabeln.Columns.Add(add_vokabelheft.TbMotherTongue.Text, size);
                        listView_vokabeln.Columns.Add(add_vokabelheft.TbForeignLang.Text, size);
                    }
                    else
                    {
                        listView_vokabeln.Columns.Add(add_vokabelheft.TbMotherTongue.Text, 155);
                        listView_vokabeln.Columns.Add(add_vokabelheft.TbForeignLang.Text, 200);
                    }

                    listView_vokabeln.Columns.Add(Words.LastPracticed, 100);

                    listView_vokabeln.Enabled = true;

                    listView_vokabeln.EndUpdate();


                    pfad_vokabelheft = "";

                    //vokabelheft_edited();

                    GroupBook.Enabled = true;
                    GroupWord.Enabled = true;
                    GroupSearch.Enabled = true;
                    StatisticsPanel.Visible = true;

                    BtnPractice.Enabled = false;
                    TsmiPractice.Enabled = false;

                    TsmiAddWord.Enabled = true;
                    TsmiBookOptions.Enabled = true;

                    BtnDeleteWord.Enabled = false;
                    TsmiDeleteWord.Enabled = false;

                    BtnSearchWord.Enabled = false;
                    TbSearchWord.Text = "";
                    TbSearchWord.Enabled = false;

                    TsmiSaveAs.Enabled = true;

                    TsmiPrint.Enabled = false;
                    TsbPrint.Enabled = false;

                    TsmiOpenInExplorer.Enabled = false;

                    TsmiExport.Enabled = false;


                    BtnAddWord.Focus();

                    //Titelleiste

                    // TODO: Check if this can be deleted
                    Text = Words.Vocup;

                    // TODO: Ensure selection works
                    //treeView.SelectedNode = treeView.Nodes[0];

                    // TODO: Übersetzungsrichtung speichern
                }
            }
            else
            {
                listView_vokabeln.Enabled = true;
            }
        }

        //Vokabelheft schliessen

        private void close_vokabelheft()
        {
            // Error-Dialog anzeigen

            listView_vokabeln.Clear();
            listView_vokabeln.Columns.Clear();
            listView_vokabeln.Enabled = false;
            listView_vokabeln.BackColor = DefaultBackColor;
            listView_vokabeln.Update();

            GroupBook.Enabled = false;
            GroupWord.Enabled = false;
            GroupSearch.Enabled = false;
            StatisticsPanel.Visible = false;

            BtnPractice.Enabled = false;
            TsmiPractice.Enabled = false;

            TsmiAddWord.Enabled = false;
            TsmiBookOptions.Enabled = false;

            BtnEditWord.Enabled = false;
            TsmiEditWord.Enabled = false;

            BtnDeleteWord.Enabled = false;
            TsmiDeleteWord.Enabled = false;

            TbSearchWord.Enabled = false;
            TbSearchWord.Text = "";

            TsmiSaveAs.Enabled = false;
            TsbSave.Enabled = false;
            TsmiSave.Enabled = false;

            TsmiCloseBook.Enabled = false;

            TsmiPrint.Enabled = false;
            TsbPrint.Enabled = false;

            TsmiOpenInExplorer.Enabled = false;
            TsmiExport.Enabled = false;

            edit_vokabelheft = false;

            pfad_vokabelheft = "";

            //Titelleiste

            Text = Words.Vocup;

            // Ensure selection words
            //treeView.SelectedNode = treeView.Nodes[0];
        }

        //Vokabel hinzufügen

        private void add_vokabel(bool fertig, bool show_specialchars)
        {
            string own_language = listView_vokabeln.Columns[1].Text;
            string foreign_language = listView_vokabeln.Columns[2].Text;

            EditWordDialog new_vokabel = new EditWordDialog
            {
                Owner = this,
                Icon = Icon.FromHandle(Icons.Plus.GetHicon())
            };

            // Sprachen anzeigen
            new_vokabel.LbMotherTongue.Text = own_language;
            new_vokabel.LbForeignLang.Text = foreign_language;
            new_vokabel.LbSynonym.Text = $"{foreign_language} ({Words.Synonym})";

            //Optionen abschalten

            new_vokabel.GroupOptions.Enabled = false;

            //Abbrechen oder Fertig

            if (fertig == true)
            {
                new_vokabel.BtnCancel.Text = Words.Finish;
                new_vokabel.BtnCancel.TabIndex = 0;
                new_vokabel.AcceptButton = new_vokabel.BtnCancel;

                new_vokabel.TbMotherTongue.Select();
            }

            //Sonderzeichen-Dialog einschalten

            if (show_specialchars == true)
            {
                new_vokabel.showSpecialChars = true;
            }

            DialogResult result = new_vokabel.ShowDialog();

            //Vokabel hinzufügen

            //Wird ausgeführt, sobald auf OK geklickt wird

            if (DialogResult.OK == result)
            {
                // Schauen, ob die Vokabel schon vorhanden ist

                bool search = false;

                for (int i = 0; i < listView_vokabeln.Items.Count; i++)
                {
                    if (listView_vokabeln.Items[i].SubItems[1].Text == new_vokabel.TbMotherTongue.Text && listView_vokabeln.Items[i].SubItems[2].Text == new_vokabel.TbForeignLang.Text)
                    {
                        search = true;
                    }
                }

                //Falls die Vokabel noch nicht vorhanden ist: Schauen, ob ein Synonym eingegeben wurde

                if (search == false)
                {
                    if (new_vokabel.TbForeignLangSynonym.Text == "")
                    {
                        ListViewItem item = new ListViewItem(new string[] { "", new_vokabel.TbMotherTongue.Text, new_vokabel.TbForeignLang.Text, "" });

                        item.ImageIndex = 0;
                        item.Tag = 0;
                        listView_vokabeln.Items.Add(item);

                        item.Selected = true;
                        item.Focused = true;
                        item.EnsureVisible();
                    }
                    else
                    {
                        ListViewItem item = new ListViewItem(new string[] { "", new_vokabel.TbMotherTongue.Text, new_vokabel.TbForeignLang.Text + "=" + new_vokabel.TbForeignLangSynonym.Text, "" });

                        item.ImageIndex = 0;
                        item.Tag = 0;
                        listView_vokabeln.Items.Add(item);

                        item.Selected = true;
                        item.Focused = true;
                        item.EnsureVisible();
                    }

                    // Anzahl Vokabeln ermitteln

                    //vokabelheft_edited();

                    BtnPractice.Enabled = true;
                    TsmiPractice.Enabled = true;

                    TsmiPrint.Enabled = true;
                    TsbPrint.Enabled = true;

                    TsmiOpenInExplorer.Enabled = true;

                    TsmiExport.Enabled = true;

                    //infos_vokabelhefte_text();


                    //Neues Fenster öffnen

                    add_vokabel(true, new_vokabel.showSpecialChars);

                }
                else
                {
                    MessageBox.Show(Properties.language.messagebox_vocable_exists,
                                 "Warnung",
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Warning);

                    //Neues Fenster öffnen

                    add_vokabel(true, new_vokabel.showSpecialChars);
                }
            }

            //Suchfenster anzeigen

            TbSearchWord.Enabled = true;

        }

        //Vokabel bearbeiten

        private void edit_vokabel_dialog()
        {
            string own_language = listView_vokabeln.Columns[1].Text;
            string foreign_language = listView_vokabeln.Columns[2].Text;

            EditWordDialog edit_vokabel = new EditWordDialog();

            //Titel
            edit_vokabel.Text = Words.EditWord;

            //Icon
            edit_vokabel.Icon = Icon.FromHandle(Icons.Edit.GetHicon());

            //Weiter_Button zu OK_Button machen
            edit_vokabel.BtnContinue.Text = Words.Ok;

            // Sprachen anzeigen
            edit_vokabel.LbMotherTongue.Text = own_language;
            edit_vokabel.LbForeignLang.Text = foreign_language;
            edit_vokabel.LbSynonym.Text = $"{foreign_language} ({Words.Synonym})";

            //Vokabel einlesen

            string own_language_vokabel = listView_vokabeln.SelectedItems[0].SubItems[1].Text;

            string[] split = listView_vokabeln.SelectedItems[0].SubItems[2].Text.Split('=');

            string foreign_language_vokabel = split[0];
            string foreign_language_vokabel_2 = "";

            if (split.Length > 1)
            {
                foreign_language_vokabel_2 = split[1];
            }

            // Vokabel einlesen
            edit_vokabel.TbMotherTongue.Text = own_language_vokabel;
            edit_vokabel.TbForeignLang.Text = foreign_language_vokabel;
            edit_vokabel.TbForeignLangSynonym.Text = foreign_language_vokabel_2;

            //Optionen einschalten
            edit_vokabel.GroupOptions.Enabled = true;

            DialogResult result = edit_vokabel.ShowDialog();

            // Vokabel bearbeiten

            if (edit_vokabel.TbMotherTongue.Text != own_language_vokabel || edit_vokabel.TbForeignLang.Text != foreign_language_vokabel || edit_vokabel.TbForeignLangSynonym.Text != foreign_language_vokabel_2 || edit_vokabel.CbResetResults.Checked == true)
            {
                //Wird ausgeführt, sobald auf OK geklickt wird

                if (DialogResult.OK == result)
                {

                    // Schauen, ob die Vokabel bereits vorhanden ist 
                    bool search = false;

                    for (int i = 0; i < listView_vokabeln.Items.Count; i++)
                    {
                        if (listView_vokabeln.Items[i].SubItems[1].Text == edit_vokabel.TbMotherTongue.Text
                            && listView_vokabeln.Items[i].SubItems[2].Text == edit_vokabel.TbForeignLang.Text
                            && !listView_vokabeln.Items[i].Selected)
                        {
                            search = true;
                            break;
                        }
                    }

                    if (search == false) // falls die Vokabel nicht vorhanden ist
                    {
                        //Schaut, ob Synonym eingegeben wurde

                        if (edit_vokabel.TbForeignLangSynonym.Text == "")
                        {
                            listView_vokabeln.SelectedItems[0].SubItems[1].Text = edit_vokabel.TbMotherTongue.Text;
                            listView_vokabeln.SelectedItems[0].SubItems[2].Text = edit_vokabel.TbForeignLang.Text;
                        }
                        else
                        {
                            listView_vokabeln.SelectedItems[0].SubItems[1].Text = edit_vokabel.TbMotherTongue.Text;
                            listView_vokabeln.SelectedItems[0].SubItems[2].Text = edit_vokabel.TbForeignLang.Text + "=" + edit_vokabel.TbForeignLangSynonym.Text;
                        }

                        listView_vokabeln.SelectedItems[0].EnsureVisible();

                        // Schauen, ob die Ergebnisse zurückgesetzt werden sollen

                        if (edit_vokabel.CbResetResults.Checked == true)
                        {
                            listView_vokabeln.SelectedItems[0].ImageIndex = 0;
                            listView_vokabeln.SelectedItems[0].Tag = 0;
                        }

                        //vokabelheft_edited();
                    }
                    else // Falls die Vokabel bereits vorhanden ist
                    {
                        MessageBox.Show(Properties.language.messagebox_cant_edit,
                                     AppInfo.Name,
                                     MessageBoxButtons.OK,
                                     MessageBoxIcon.Warning);
                    }

                    //infos_vokabelhefte_text();
                }
            }

            BtnAddWord.Focus();
        }

        //Vokabel löschen

        private void vokabel_delete()
        {
            listView_vokabeln.BeginUpdate();

            int i = listView_vokabeln.FocusedItem.Index;

            if (listView_vokabeln.Items.Count > 1)
            {
                if (i == 0)
                {
                    listView_vokabeln.FocusedItem.Remove();
                    listView_vokabeln.Items[0].Selected = true;
                }
                else
                {
                    listView_vokabeln.FocusedItem.Remove();
                    listView_vokabeln.Items[i - 1].Selected = true;
                }
            }
            else if (listView_vokabeln.Items.Count == 1)
            {
                listView_vokabeln.FocusedItem.Remove();

                BtnPractice.Enabled = false;
                TsmiPractice.Enabled = false;

                BtnEditWord.Enabled = false;
                TsmiEditWord.Enabled = false;

                BtnDeleteWord.Enabled = false;
                TsmiDeleteWord.Enabled = false;

                BtnSearchWord.Enabled = false;
                TbSearchWord.Enabled = false;

                TsbPrint.Enabled = false;
                TsmiPrint.Enabled = false;

                TsmiOpenInExplorer.Enabled = false;

                TsmiExport.Enabled = false;
            }

            //vokabelheft_edited();
            //infos_vokabelhefte_text();

            listView_vokabeln.EndUpdate();

            BtnAddWord.Focus();
        }

        //Vokabelheft Optionen

        private void edit_vokabelheft_dialog()
        {
            new VocabularyBookSettings(CurrentBook) { Owner = this }.ShowDialog();
            BtnAddWord.Focus();
        }


        //Vokabeln Üben

        private void vokabeln_üben()
        {
            PracticeCountDialog countDialog = new PracticeCountDialog(CurrentBook);
            if (countDialog.ShowDialog() != DialogResult.OK)
                return;

            List<VocabularyWordPractice> practiceList = countDialog.PracticeList;

            int anzahl = practiceList.Count;

            CurrentController.ListView.Visible = false;

            new PracticeDialog(CurrentBook, practiceList) { Owner = this }.ShowDialog();

            if (Settings.Default.show_practise_result_list)
            {
                new PracticeResultList(CurrentBook, practiceList).ShowDialog();
            }

            CurrentController.ListView.Visible = true;

            BtnAddWord.Focus();
        }

        //Vokabelhefte zusammenführen

        private void TsmiMerge_Click(object sender, EventArgs e)
        {
            new MergeFiles().ShowDialog();
        }

        //Datensicherung erstellen

        private void TsmiBackupCreate_Click(object sender, EventArgs e)
        {
            new CreateBackup().ShowDialog();
        }

        //Datensicherung wiederherstellen

        private void restore_backup(string file_path)
        {
            //Neue Form vorbereiten
            backup_restore restore = new backup_restore();

            //Falls ein Backup geöffnet wurde
            if (file_path != "")
            {
                restore.path_field.Text = file_path;
                restore.path_button.Enabled = false;
                restore.path_backup = file_path;
            }


            //Fragen, ob das Vokabelheft gespeichert werden soll

            bool result = true;

            if (edit_vokabelheft == true)
            {
                result = vokabelheft_ask_to_save();
            }

            if (result == true)
            {
                //Falls auf wiederherstellen geklickt wurde
                if (restore.ShowDialog() == DialogResult.OK)
                {
                    //Variablen für Fehlermeldungen
                    int error_vhf = 0;
                    int error_vhr = 0;
                    int error_chars = 0;
                    bool error = false;

                    List<string> error_vhf_name = new List<string>();
                    List<string> error_chars_name = new List<string>();

                    try
                    {

                        //Cursor auf Warten setzen
                        Cursor.Current = Cursors.WaitCursor;
                        Update();


                        //Schliesst das geöffnete Vokabelheft
                        close_vokabelheft();

                        //Backup-Datei vorbereiten

                        ZipFile backup_file = new ZipFile(restore.path_backup);

                        //Vokabelhefte wiederherstellen
                        if (restore.vhf_restore.Count > 0)
                        {
                            for (int i = 0; i < restore.vhf_restore.Count; i++)
                            {
                                try
                                {

                                    string[] temp = restore.vhf_restore[i];

                                    ZipEntry entry = backup_file.GetEntry(@"vhf\" + temp[0] + ".vhf");

                                    byte[] buffer = new byte[entry.Size + 4096];

                                    FileInfo info = new FileInfo(temp[1]);

                                    if (Directory.Exists(info.DirectoryName) == false)
                                    {
                                        Directory.CreateDirectory(info.DirectoryName);
                                    }


                                    FileStream writer = new FileStream(temp[1], FileMode.Create);

                                    StreamUtils.Copy(backup_file.GetInputStream(entry), writer, buffer);

                                    writer.Close();
                                }
                                catch
                                {
                                    error_vhf++;

                                    string[] temp = restore.vhf_restore[i];
                                    error_vhf_name.Add(temp[1]);
                                }
                            }
                        }

                        //Ergebnisse wiederherstellen

                        if (restore.vhr_restore.Count > 0)
                        {
                            for (int i = 0; i < restore.vhr_restore.Count; i++)
                            {
                                try
                                {
                                    ZipEntry entry = backup_file.GetEntry(@"vhr\" + restore.vhr_restore[i]);

                                    byte[] buffer = new byte[entry.Size + 4096];


                                    FileStream writer = new FileStream(Properties.Settings.Default.VhrPath + "\\" + restore.vhr_restore[i], FileMode.Create);

                                    StreamUtils.Copy(backup_file.GetInputStream(entry), writer, buffer);

                                    writer.Close();
                                }

                                catch
                                {
                                    error_vhr++;
                                }
                            }
                        }

                        //Sonderzeichentabellen sichern

                        if (restore.chars_restore.Count > 0)
                        {

                            for (int i = 0; i < restore.chars_restore.Count; i++)
                            {
                                try
                                {
                                    ZipEntry entry = backup_file.GetEntry(@"chars\" + restore.chars_restore[i]);

                                    byte[] buffer = new byte[entry.Size + 4096];

                                    if (Directory.Exists(Properties.Settings.Default.VhrPath + "\\specialchar\\") == false)
                                    {
                                        Directory.CreateDirectory(Properties.Settings.Default.VhrPath + "\\specialchar\\");
                                    }

                                    FileStream writer = new FileStream(Properties.Settings.Default.VhrPath + "\\specialchar\\" + restore.chars_restore[i], FileMode.Create);

                                    StreamUtils.Copy(backup_file.GetInputStream(entry), writer, buffer);

                                    writer.Close();
                                }

                                catch
                                {
                                    error_chars++;

                                    error_chars_name.Add(restore.chars_restore[i]);
                                }
                            }
                        }

                        backup_file.Close();

                        Cursor.Current = Cursors.Default;
                        Update();
                    }
                    catch
                    {
                        error = true;

                        Cursor.Current = Cursors.Default;

                        //fehlermeldung anzeigen
                        MessageBox.Show(Properties.language.messagebox_backup_restore_error,
                               Properties.language.error,
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error);
                    }

                    //Falls nötig Fehlermeldungen anzeigen

                    if (error_vhf > 0)
                    {
                        string messange = Properties.language.messagebox_backup_restore_error_vhf + Environment.NewLine;

                        for (int i = 0; i < error_vhf_name.Count; i++)
                        {
                            messange = messange + Environment.NewLine + error_vhf_name[i];
                        }

                        //Fehlermeldung anzeigen
                        MessageBox.Show(messange,
                               Properties.language.error,
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error);
                    }
                    if (error_vhr > 0)
                    {
                        //Fehlermeldung anzeigen
                        MessageBox.Show(error_vhr.ToString() + " " + Properties.language.messagebox_backup_restore_error_vhr,
                               Properties.language.error,
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error);
                    }
                    if (error_chars > 0)
                    {
                        string messange = Properties.language.messagebox_backup_restore_error_chars + Environment.NewLine;

                        for (int i = 0; i < error_chars_name.Count; i++)
                        {
                            messange = messange + Environment.NewLine + error_chars_name[i];
                        }

                        //Fehlermeldung anzeigen
                        MessageBox.Show(messange,
                               Properties.language.error,
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error);
                    }

                    //Dialog anzeigen, dass der Prozess erfolgreich war

                    if (error == false && error_vhf == 0 && error_vhr == 0 && error_chars == 0)
                    {
                        MessageBox.Show(Properties.language.messagebox_backup_restore_success,
                                  AppInfo.Name,
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void TsmiBackupRestore_Click(object sender, EventArgs e)
        {
            restore_backup("");
        }

        //Vokabelheft drucken

        private void print_file()
        {
            if (pfad_vokabelheft != "" && pfad_vokabelheft != null)
            {
                //Dialog starten der abfrägt, was gedruckt werden soll
                PrintWordSelection choose_vocables = new PrintWordSelection();

                //Vokabeln in ListBox eintragen

                choose_vocables.vocable_state = new int[listView_vokabeln.Items.Count];

                for (int i = 0; i < listView_vokabeln.Items.Count; i++)
                {
                    choose_vocables.ListBox.Items.Add(listView_vokabeln.Items[i].SubItems[1].Text + " - " + listView_vokabeln.Items[i].SubItems[2].Text, true);

                    //Status ins Array eintragen
                    choose_vocables.vocable_state[i] = Convert.ToInt32(listView_vokabeln.Items[i].Tag);
                }

                //Checkboxen falls nötig ausblenden

                choose_vocables.CbUnpracticed.Enabled = StatisticsPanel.Unpracticed > 0;
                choose_vocables.CbWronglyPracticed.Enabled = StatisticsPanel.WronglyPracticed > 0;
                choose_vocables.CbCorrectlyPracticed.Enabled = StatisticsPanel.CorrectlyPracticed > 0;
                choose_vocables.CbFullyPracticed.Enabled = StatisticsPanel.FullyPracticed > 0;

                //Dialog anzeigen

                DialogResult choose_vocables_result = choose_vocables.ShowDialog();

                //Vokabeln in Vokabelliste speichern

                if (choose_vocables_result == DialogResult.OK)
                {
                    int status = 0;

                    //Feststellen, wie viele Vokabeln markiert wurden

                    int count = 0;

                    for (int i = 0; i < choose_vocables.ListBox.Items.Count; i++)
                    {
                        if (choose_vocables.ListBox.GetItemCheckState(i) == CheckState.Checked)
                        {
                            count++;
                        }
                    }

                    if (count == 0)
                    {
                        print_file();
                    }
                    else
                    {
                        //Array erstellen

                        int[] list = new int[count];

                        for (int i = 0; i < choose_vocables.ListBox.Items.Count; i++)
                        {
                            if (choose_vocables.ListBox.GetItemCheckState(i) == CheckState.Checked)
                            {
                                list[status] = i;
                                status++;

                            }
                        }
                        //überschreibt die Vokabelliste
                        vokabelliste = list;
                        //Anzahl Vokabeln schreiben
                        anz_vok = count;


                        //Liste
                        if (choose_vocables.RbList.Checked == true)
                        {
                            //Feststellen, ob von Mutter- zu Fremdsprache, oder umgekehrt gedruckt werden soll
                            if (choose_vocables.RbAskForForeignLang.Checked == true)
                            {
                                if_own_to_foreign = true;
                            }
                            else
                            {
                                if_own_to_foreign = false;
                            }

                            //Den Druckdialog starten
                            PrintDialog dialog = new PrintDialog();

                            dialog.AllowCurrentPage = false;
                            dialog.AllowSomePages = false;
                            dialog.UseEXDialog = true;

                            DialogResult result = dialog.ShowDialog();

                            if (result == DialogResult.OK)
                            {
                                printList.PrinterSettings = dialog.PrinterSettings;
                                printList.DocumentName = "Vokabelliste";
                                printList.Print();
                            }
                        }

                        //Kärtchen
                        else
                        {
                            //Anzahl Seiten ermitteln

                            double anz_vokD = anz_vok;

                            double i = Math.Ceiling(anz_vokD / 16);

                            anzahl_seiten = (int)i;

                            //---

                            PrintCardsDialog dialog = new PrintCardsDialog();

                            //Anzahl seiten
                            dialog.LbPaperCount.Text = anzahl_seiten.ToString();

                            //Papiereinzug

                            if (Properties.Settings.Default.papiereinzug == false)
                            {
                                dialog.RbFrontSide.Checked = false;
                                dialog.RbRearSide.Checked = true;
                            }
                            else
                            {
                                dialog.RbFrontSide.Checked = true;
                                dialog.RbRearSide.Checked = false;
                            }

                            //Dialog starten
                            DialogResult result = dialog.ShowDialog();

                            if (result == DialogResult.Ignore)
                            {
                                //Falls Die VorderSeite gedruckt werden soll

                                if_foreside = true;

                                //Ermittle Papiereinzug

                                is_front = dialog.RbFrontSide.Checked;
                                Properties.Settings.Default.papiereinzug = is_front;
                                Properties.Settings.Default.Save();

                                dialog.Close();

                                //Drucken

                                PrintDialog print_dialog = new PrintDialog();
                                print_dialog.AllowCurrentPage = false;
                                print_dialog.AllowSomePages = false;
                                print_dialog.UseEXDialog = true;

                                DialogResult print_result = print_dialog.ShowDialog();

                                if (print_result == DialogResult.OK)
                                {

                                    printCards.PrinterSettings = print_dialog.PrinterSettings;
                                    printCards.DocumentName = "Vokabel Kärtchen";
                                    printCards.Print();


                                    //Dialog nochmals starten
                                    //anderer Button deaktivieren

                                    PrintCardsDialog dialog2 = new PrintCardsDialog();

                                    dialog2.BtnPrintForeside.Enabled = false;
                                    dialog2.BtnPrintBackside.Enabled = true;

                                    dialog2.LbPaperCount.Text = anzahl_seiten.ToString();

                                    //Papiereinzug


                                    if (Properties.Settings.Default.papiereinzug == false)
                                    {
                                        dialog2.RbFrontSide.Checked = false;
                                        dialog2.RbRearSide.Checked = true;
                                    }
                                    else
                                    {
                                        dialog2.RbRearSide.Checked = false;
                                        dialog2.RbFrontSide.Checked = true;
                                    }

                                    DialogResult result2 = dialog2.ShowDialog();

                                    if (result2 == DialogResult.OK)
                                    {
                                        if_foreside = false;

                                        //Papiereinzug

                                        is_front = dialog2.RbFrontSide.Checked;
                                        Properties.Settings.Default.papiereinzug = is_front;
                                        Properties.Settings.Default.Save();

                                        //Drucken

                                        printCards.Print();

                                    }
                                }
                            }
                            else if (result == DialogResult.OK)
                            {

                                if_foreside = false;

                                //Ermittle Papiereinzug

                                is_front = dialog.RbFrontSide.Checked;
                                Properties.Settings.Default.papiereinzug = is_front;
                                Properties.Settings.Default.Save();

                                //Drucken

                                dialog.Close();

                                //Drucken

                                PrintDialog print_dialog = new PrintDialog
                                {
                                    AllowCurrentPage = false,
                                    AllowSomePages = false
                                };

                                DialogResult print_result = print_dialog.ShowDialog();

                                if (print_result == DialogResult.OK)
                                {

                                    printCards.PrinterSettings = print_dialog.PrinterSettings;
                                    printCards.DocumentName = "Vokabel Kärtchen";
                                    printCards.Print();

                                    //Dialog nochmals starten
                                    //anderer Button deaktivieren

                                    PrintCardsDialog dialog3 = new PrintCardsDialog();

                                    dialog3.BtnPrintForeside.Enabled = true;
                                    dialog3.BtnPrintBackside.Enabled = false;

                                    dialog3.LbPaperCount.Text = anzahl_seiten.ToString();


                                    //Papiereinzug


                                    if (Properties.Settings.Default.papiereinzug == false)
                                    {
                                        dialog3.RbFrontSide.Checked = false;
                                        dialog3.RbRearSide.Checked = true;
                                    }
                                    else
                                    {
                                        dialog3.RbRearSide.Checked = false;
                                        dialog3.RbFrontSide.Checked = true;
                                    }

                                    DialogResult result3 = dialog3.ShowDialog();

                                    if (result3 == DialogResult.Ignore)
                                    {
                                        if_foreside = true;

                                        //Papiereinzug

                                        is_front = dialog3.RbFrontSide.Checked;
                                        Properties.Settings.Default.papiereinzug = is_front;
                                        Properties.Settings.Default.Save();


                                        //Drucken

                                        printCards.Print();

                                    }
                                }
                            }
                        }
                    }
                }
            }

            //Falls die Datei noch nicht gespeichert wurde
            else
            {
                MessageBox.Show(Properties.language.have_to_save,
                                   AppInfo.Name,
                                   MessageBoxButtons.OK,
                                   MessageBoxIcon.Information);
            }

        }
        //Liste drucken
        private void printDocument_list_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            Graphics g = e.Graphics;
            g.PageUnit = GraphicsUnit.Display;

            //Schrift
            Font font = new Font("Arial", 10);
            Font font_bold = new Font("Arial", 10, FontStyle.Bold);
            Font font_vocable = new Font("Arial", 8);

            //stift
            Pen pen = new Pen(Brushes.Black, 1);

            //Ein zentriertes Format für Schrift erstellen
            StringFormat format_center = new StringFormat();
            format_center.Alignment = StringAlignment.Center;

            //Rechtsbündig
            StringFormat format_near = new StringFormat();
            format_near.Alignment = StringAlignment.Near;

            //Ränder
            int left = Convert.ToInt32(Math.Round(e.PageSettings.PrintableArea.Left, 1, MidpointRounding.AwayFromZero));
            int right = Convert.ToInt32(Math.Round(e.PageSettings.PrintableArea.Right, 1, MidpointRounding.AwayFromZero));
            int top = Convert.ToInt32(Math.Round(e.PageSettings.PrintableArea.Top, 1, MidpointRounding.AwayFromZero));
            int bottom = Convert.ToInt32(Math.Round(e.PageSettings.PrintableArea.Bottom, 1, MidpointRounding.AwayFromZero));


            //Seitenzahl ganz oben
            g.DrawString(Words.Site + " " + aktuelle_seite.ToString(), font, Brushes.Black, new Point(414 - left, 25 - left), format_center);

            //Dateiname ermitteln
            FileInfo file_name = new FileInfo(pfad_vokabelheft);

            if (aktuelle_seite == 1)
            {
                if (if_own_to_foreign == true)
                {
                    g.DrawString(file_name.Name.Remove(file_name.Name.Length - 4) + ":  " + listView_vokabeln.Columns[1].Text + " - " + listView_vokabeln.Columns[2].Text, font_bold, Brushes.Black, new Point(414 - left, 40 - top), format_center);
                }
                else
                {
                    g.DrawString(file_name.Name.Remove(file_name.Name.Length - 4) + ":  " + listView_vokabeln.Columns[2].Text + " - " + listView_vokabeln.Columns[1].Text, font_bold, Brushes.Black, new Point(414 - left, 40 - top), format_center);
                }
            }


            //Linien und Wörter einfügen

            int noch_nicht_gedruckt = anz_vok - (aktuelle_seite - 1) * 42;
            int vok_beginnen = (aktuelle_seite - 1) * 42 + 1;

            //Falls volle Seiten gedruckt werden können
            if (noch_nicht_gedruckt >= 42)
            {
                //Oberste Linie
                g.DrawLine(pen, 60 - left, 65 - top, 767 - left, 65 - top);
                //Mittellinie
                g.DrawLine(pen, 413 - left, 65 - top, 413 - left, 1115 - top);
                //Seitenlinien
                g.DrawLine(pen, 60 - left, 65 - top, 60 - left, 1115 - top);
                g.DrawLine(pen, 767 - left, 65 - top, 767 - left, 1115 - top);
                //unterste Linie
                //g.DrawLine(pen, 60 - left, 1095 - top, 767 - left, 1120 - top);

                for (int i = 0; i < 42; i++)
                {

                    SizeF size_own = g.MeasureString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text, font_vocable);
                    SizeF size_foreign = g.MeasureString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[2].Text, font_vocable);

                    //Falls der Text zu gross ist
                    if (size_own.Width > 413 - 62 - left)
                    {
                        bool is_good;
                        int font_size = 8;
                        do
                        {
                            font_size--;
                            Font font_new = new Font("Arial", font_size);

                            SizeF string_size = g.MeasureString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text, font_new);

                            if (string_size.Width > 413 - 62 - left && font_size > 1)
                            {
                                is_good = false;
                            }
                            else
                            {
                                is_good = true;

                                //kleinerer Text schreiben
                                if (if_own_to_foreign == true)
                                {
                                    g.DrawString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text, font_new, Brushes.Black, new Point(62 - left, 70 + i * 25 - top), format_near);
                                }
                                else
                                {
                                    g.DrawString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text, font_new, Brushes.Black, new Point(415 - left, 70 + i * 25 - top), format_near);
                                }
                            }

                        } while (is_good == false);

                    }
                    else //Falls Text nicht zu gross
                    {
                        if (if_own_to_foreign == true)
                        {
                            g.DrawString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text, font_vocable, Brushes.Black, new Point(62 - left, 70 + i * 25 - top), format_near);
                        }
                        else
                        {
                            g.DrawString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text, font_vocable, Brushes.Black, new Point(415 - left, 70 + i * 25 - top), format_near);
                        }
                    }
                    //Falls Text zu gross || Synonym
                    if (size_foreign.Width > 413 - 62 - left)
                    {
                        bool is_good;
                        int font_size = 8;
                        do
                        {
                            font_size--;
                            Font font_new = new Font("Arial", font_size);

                            SizeF string_size = g.MeasureString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[2].Text, font_new);

                            if (string_size.Width > 413 - 62 - left && font_size > 1)
                            {
                                is_good = false;
                            }
                            else
                            {
                                is_good = true;

                                //kleinerer Text schreiben
                                if (if_own_to_foreign == true)
                                {
                                    g.DrawString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[2].Text, font_new, Brushes.Black, new Point(415 - left, 70 + i * 25 - top), format_near);
                                }
                                else
                                {
                                    g.DrawString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[2].Text, font_new, Brushes.Black, new Point(62 - left, 70 + i * 25 - top), format_near);

                                }
                            }

                        } while (is_good == false);

                    }
                    else //Falls Text nicht zu gross
                    {
                        if (if_own_to_foreign == true)
                        {
                            g.DrawString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[2].Text, font_vocable, Brushes.Black, new Point(415 - left, 70 + i * 25 - top), format_near);
                        }
                        else
                        {
                            g.DrawString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[2].Text, font_vocable, Brushes.Black, new Point(62 - left, 70 + i * 25 - top), format_near);
                        }
                    }


                    //Untere Linie zeichnen
                    g.DrawLine(pen, 60 - left, 90 + i * 25 - top, 767 - left, 90 + i * 25 - top);
                }
            }
            else //Falls letzte Seite, und nicht voll
            {
                //Oberste Linie
                g.DrawLine(pen, 60 - left, 65 - top, 767 - left, 65 - top);
                //Mittellinie
                g.DrawLine(pen, 413 - left, 65 - top, 413 - left, 65 + 25 * noch_nicht_gedruckt - top);
                //Seitenlinien
                g.DrawLine(pen, 60 - left, 65 - top, 60 - left, 65 + 25 * noch_nicht_gedruckt - top);
                g.DrawLine(pen, 767 - left, 65 - top, 767 - left, 65 + 25 * noch_nicht_gedruckt - top);


                for (int i = 0; i < noch_nicht_gedruckt; i++)
                {

                    SizeF size_own = g.MeasureString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text, font_vocable);
                    SizeF size_foreign = g.MeasureString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[2].Text, font_vocable);

                    //Falls der Text zu gross ist
                    if (size_own.Width > 413 - 62 - left)
                    {
                        bool is_good;
                        int font_size = 8;
                        do
                        {
                            font_size--;
                            Font font_new = new Font("Arial", font_size);

                            SizeF string_size = g.MeasureString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text, font_new);

                            if (string_size.Width > 413 - 62 - left && font_size > 1)
                            {
                                is_good = false;
                            }
                            else
                            {
                                is_good = true;

                                //kleinerer Text schreiben
                                if (if_own_to_foreign == true)
                                {
                                    g.DrawString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text, font_new, Brushes.Black, new Point(62 - left, 70 + i * 25 - top), format_near);
                                }
                                else
                                {
                                    g.DrawString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text, font_new, Brushes.Black, new Point(415 - left, 70 + i * 25 - top), format_near);
                                }
                            }

                        } while (is_good == false);

                    }
                    //Falls Text nicht zu gross
                    else
                    {
                        if (if_own_to_foreign == true)
                        {
                            g.DrawString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text, font_vocable, Brushes.Black, new Point(62 - left, 70 + i * 25 - top), format_near);
                        }
                        else
                        {
                            g.DrawString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text, font_vocable, Brushes.Black, new Point(415 - left, 70 + i * 25 - top), format_near);
                        }
                    }
                    //Falls Text zu gross || Synonym
                    if (size_foreign.Width > 413 - 62 - left)
                    {
                        bool is_good;
                        int font_size = 8;
                        do
                        {
                            font_size--;
                            Font font_new = new Font("Arial", font_size);

                            SizeF string_size = g.MeasureString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[2].Text, font_new);

                            if (string_size.Width > 413 - 62 - left && font_size > 1)
                            {
                                is_good = false;
                            }
                            else
                            {
                                is_good = true;

                                //kleinerer Text schreiben
                                if (if_own_to_foreign == true)
                                {
                                    g.DrawString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[2].Text, font_new, Brushes.Black, new Point(415 - left, 70 + i * 25 - top), format_near);
                                }
                                else
                                {
                                    g.DrawString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[2].Text, font_new, Brushes.Black, new Point(62 - left, 70 + i * 25 - top), format_near);
                                }
                            }

                        } while (is_good == false);

                    }
                    //Falls Text nicht zu gross
                    else
                    {
                        if (if_own_to_foreign == true)
                        {
                            g.DrawString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[2].Text, font_vocable, Brushes.Black, new Point(415 - left, 70 + i * 25 - top), format_near);
                        }
                        else
                        {
                            g.DrawString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[2].Text, font_vocable, Brushes.Black, new Point(62 - left, 70 + i * 25 - top), format_near);
                        }
                    }

                    //Untere Linie zeichnen
                    g.DrawLine(pen, 60 - left, 90 + i * 25 - top, 767 - left, 90 + i * 25 - top);
                }
            }


            //Schauen, ob noch mehr Seiten gedruckt werden müssen

            if (aktuelle_seite != anzahl_seiten)
            {
                e.HasMorePages = true;
                aktuelle_seite++;
            }
            else
            {
                e.HasMorePages = false;
            }

        }
        private void printDocument_list_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //Anzahl Seiten festlegen
            anzahl_seiten = (int)Math.Ceiling(anz_vok / 42d);
            aktuelle_seite = 1;
        }

        //Kärtchen drucken
        private void printDocument_cards_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            g.PageUnit = GraphicsUnit.Display;
            //1/100 zoll * 0.254 = mm
            //1169|827 (Daten A4-Seite)
            //Seitenränder abfragen

            int left = (int)Math.Round(e.PageSettings.PrintableArea.Left, 1, MidpointRounding.AwayFromZero);
            int right = (int)Math.Round(e.PageSettings.PrintableArea.Right, 1, MidpointRounding.AwayFromZero);
            int top = (int)Math.Round(e.PageSettings.PrintableArea.Top, 1, MidpointRounding.AwayFromZero);
            int bottom = (int)Math.Round(e.PageSettings.PrintableArea.Bottom, 1, MidpointRounding.AwayFromZero);

            //Stift

            Pen pen = new Pen(Color.Black, 1);

            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;

            Font font = new Font("Arial", 12);

            //Vorderseite
            if (if_foreside == true)
            {
                //Linien zeichnen

                //Vertikal
                g.DrawLine(pen, 207 - left, 0, 207 - left, 1180);
                g.DrawLine(pen, 413 - left, 0, 413 - left, 1180);
                g.DrawLine(pen, 620 - left, 0, 620 - left, 1180);

                //Horizontal
                g.DrawLine(pen, 0, 292 - top, 866, 292 - top);
                g.DrawLine(pen, 0, 585 - top, 866, 585 - top);
                g.DrawLine(pen, 0, 877 - top, 866, 877 - top);

                //Seite rotieren ||X-Koordinaten negativ, Y-Koordinaten positiv
                g.RotateTransform(-90f);

                //Linien und Wörter einfügen

                int noch_nicht_gedruckt = anz_vok - (aktuelle_seite - 1) * 16;
                int vok_beginnen = (aktuelle_seite - 1) * 16 + 1;

                //Falls noch mehr Seiten gedruckt werden müssen
                if (noch_nicht_gedruckt >= 16)
                {
                    for (int i = 0; i < 16; i++)
                    {
                        //Koordinaten abfragen
                        int[] coordinates = get_coordinates(i + 1);

                        //Grösse des Textes abfragen

                        SizeF size_string = g.MeasureString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text, font);

                        int height = Convert.ToInt32(size_string.Height / 2);

                        //Vokabel schreiben
                        //Schriftgrösse anpassen

                        //Falls Text zu gross, string auf mehrere Zeilen aufteilen falls möglich
                        if (size_string.Width > 292)
                        {
                            bool is_good = false;
                            int font_size = 12;

                            if (listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text.Trim().Contains(" ") == true)
                            {
                                //Falls der String leerschläge enthält

                                string[] splitter = listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text.Trim().Split(' ');

                                do
                                {
                                    Font font_new = new Font("Arial", font_size);

                                    for (int y = 1; y < splitter.Length; y++)
                                    {
                                        string part1 = "";
                                        string part2 = "";

                                        for (int x = 1; x <= splitter.Length - y; x++)
                                        {
                                            part1 = part1 + " " + splitter[x - 1];

                                            if (x == splitter.Length - y)
                                            {
                                                for (int z = splitter.Length - y; z < splitter.Length; z++)
                                                {
                                                    part2 = part2 + " " + splitter[z];
                                                }
                                            }
                                        }

                                        SizeF size_part1 = g.MeasureString(part1, font_new);
                                        SizeF size_part2 = g.MeasureString(part2, font_new);

                                        if (size_part1.Width <= 292 && size_part2.Width <= 292)
                                        {
                                            is_good = true;

                                            //zwei Zeilen schreiben

                                            g.DrawString(part1, font_new, Brushes.Black, new Point(coordinates[0] + top, coordinates[1] - left - height - 20), format);
                                            g.DrawString(part2, font_new, Brushes.Black, new Point(coordinates[0] + top, coordinates[1] - left - height + 20), format);

                                            break;
                                        }
                                    }

                                    if (is_good == false)
                                    {
                                        font_size--;
                                    }

                                } while (is_good == false);
                            }
                            else
                            {
                                do
                                {
                                    font_size--;
                                    Font font_new = new Font("Arial", font_size);

                                    SizeF string_size = g.MeasureString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text, font_new);

                                    if (string_size.Width > 292 && font_size > 1)
                                    {
                                        is_good = false;
                                    }
                                    else
                                    {
                                        is_good = true;

                                        //kleinerer Text schreiben
                                        g.DrawString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text, font_new, Brushes.Black, new Point(coordinates[0] + top, coordinates[1] - left - height), format);
                                    }

                                } while (is_good == false);
                            }
                        }
                        else //Falls Text nicht zu gross
                        {
                            g.DrawString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text, font, Brushes.Black, new Point(coordinates[0] + top, coordinates[1] - left - height), format);
                        }
                    }
                }
                else //Falls dies die letzte Seite ist
                {
                    for (int i = 0; i < noch_nicht_gedruckt; i++)
                    {
                        //Koordinaten abfragen
                        int[] coordinates = get_coordinates(i + 1);

                        //Grösse des Textes abfragen

                        SizeF size_string = g.MeasureString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text, font);

                        int height = Convert.ToInt32(size_string.Height / 2);

                        //Vokabel schreiben
                        //Schriftgrösse anpassen

                        //Falls Text zu gross, string auf mehrere Zeilen aufteilen falls möglich
                        if (size_string.Width > 292)
                        {

                            bool is_good = false;
                            int font_size = 12;

                            if (listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text.Trim().Contains(" ") == true)
                            {
                                //Falls der String leerschläge enthält

                                string[] splitter = listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text.Trim().Split(' ');

                                do
                                {
                                    Font font_new = new Font("Arial", font_size);

                                    for (int y = 1; y < splitter.Length; y++)
                                    {
                                        string part1 = "";
                                        string part2 = "";

                                        for (int x = 1; x <= splitter.Length - y; x++)
                                        {
                                            part1 = part1 + " " + splitter[x - 1];

                                            if (x == splitter.Length - y)
                                            {
                                                for (int z = splitter.Length - y; z < splitter.Length; z++)
                                                {
                                                    part2 = part2 + " " + splitter[z];
                                                }
                                            }
                                        }


                                        SizeF size_part1 = g.MeasureString(part1, font_new);
                                        SizeF size_part2 = g.MeasureString(part2, font_new);

                                        if (size_part1.Width <= 292 && size_part2.Width <= 292)
                                        {
                                            is_good = true;

                                            //zwei Zeilen schreiben

                                            g.DrawString(part1, font_new, Brushes.Black, new Point(coordinates[0] + top, coordinates[1] - left - height - 20), format);
                                            g.DrawString(part2, font_new, Brushes.Black, new Point(coordinates[0] + top, coordinates[1] - left - height + 20), format);

                                            break;
                                        }
                                    }

                                    if (is_good == false)
                                    {
                                        font_size--;
                                    }

                                } while (is_good == false);
                            }
                            else // Falls keine Leerzeichen vorhanden sind
                            {
                                do
                                {
                                    font_size--;
                                    Font font_new = new Font("Arial", font_size);

                                    SizeF string_size = g.MeasureString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text, font_new);

                                    if (string_size.Width > 292 && font_size > 1)
                                    {
                                        is_good = false;
                                    }
                                    else
                                    {
                                        is_good = true;

                                        //kleinerer Text schreiben
                                        g.DrawString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text, font_new, Brushes.Black, new Point(coordinates[0] + top, coordinates[1] - left - height), format);
                                    }

                                } while (is_good == false);
                            }
                        }
                        else
                        {
                            //Falls Text nicht zu gross
                            g.DrawString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text, font, Brushes.Black, new Point(coordinates[0] + top, coordinates[1] - left - height), format);
                        }
                    }

                    //nicht benötigte Linien entfernen
                    g.RotateTransform(+90);

                    if (noch_nicht_gedruckt <= 4)
                    {
                        g.FillRectangle(Brushes.White, new Rectangle(0, 292 - top + 1, 866, 1180));
                    }
                    else if (noch_nicht_gedruckt > 4 && noch_nicht_gedruckt <= 8)
                    {
                        g.FillRectangle(Brushes.White, new Rectangle(0, 585 - top + 1, 866, 1180));
                    }
                    else if (noch_nicht_gedruckt > 8 && noch_nicht_gedruckt <= 12)
                    {
                        g.FillRectangle(Brushes.White, new Rectangle(0, 877 - top - top + 1, 866, 1180));
                    }

                    //Vertikale Linien


                    //Vertikal
                    //g.DrawLine(pen, 207 - left, 0, 207 - left, 1180);
                    //g.DrawLine(pen, 413 - left, 0, 413 - left, 1180);
                    //g.DrawLine(pen, 620 - left, 0, 620 - left, 1180);

                    ////Horizontal
                    //g.DrawLine(pen, 0, 292 - top, 866, 292 - top);
                    //g.DrawLine(pen, 0, 585 - top, 866, 585 - top);
                    //g.DrawLine(pen, 0, 877 - top, 866, 877 - top);

                    Rectangle rect = new Rectangle();

                    if (noch_nicht_gedruckt < 4)
                    {
                        rect.Y = 0;
                        rect.Height = 1180;
                    }
                    else if (noch_nicht_gedruckt > 4 && noch_nicht_gedruckt < 8)
                    {
                        rect.Y = 292 - top + 1;
                        rect.Height = 888;
                    }
                    else if (noch_nicht_gedruckt > 8 & noch_nicht_gedruckt < 12)
                    {
                        rect.Y = 585 - top + 1;
                        rect.Height = 593;
                    }
                    else if (noch_nicht_gedruckt > 12 & noch_nicht_gedruckt < 16)
                    {
                        rect.Y = 877 - top + 1;
                        rect.Height = 298;
                    }

                    if (noch_nicht_gedruckt == 1 || noch_nicht_gedruckt == 5 || noch_nicht_gedruckt == 9 || noch_nicht_gedruckt == 13)
                    {
                        rect.X = 207 - left + 1;
                        rect.Width = 650;

                        g.FillRectangle(Brushes.White, rect);
                    }
                    else if (noch_nicht_gedruckt == 2 || noch_nicht_gedruckt == 6 || noch_nicht_gedruckt == 10 || noch_nicht_gedruckt == 14)
                    {
                        rect.X = 413 - left + 1;
                        rect.Width = 435;

                        g.FillRectangle(Brushes.White, rect);
                    }
                    else if (noch_nicht_gedruckt == 3 || noch_nicht_gedruckt == 7 || noch_nicht_gedruckt == 11)
                    {
                        rect.X = 620 - left + 1;
                        rect.Width = 218;

                        g.FillRectangle(Brushes.White, rect);
                    }
                    g.RotateTransform(-90);
                }

                //Pfeil zeichnen
                if (aktuelle_seite == anzahl_seiten || aktuelle_seite == 1)
                {
                    //rotieren

                    g.RotateTransform(+90);
                    Font pfeil = new Font("Arial", 12, FontStyle.Bold);

                    g.DrawString("↑", pfeil, Brushes.Black, new Point(413 - left - 30, 0), format);
                    g.DrawString("↑", pfeil, Brushes.Black, new Point(413 - left + 30, 0), format);
                }

            }
            else //Rückseite
            {
                //Seite rotieren ||X-Koordinaten positiv, Y-Koordinaten negativ
                g.RotateTransform(+90);

                int noch_nicht_gedruckt;
                int vok_beginnen;

                if (is_front == true)
                {
                    noch_nicht_gedruckt = anz_vok - (anzahl_seiten - aktuelle_seite) * 16;
                    vok_beginnen = ((anzahl_seiten) - (aktuelle_seite)) * 16 + 1;
                }
                else
                {
                    noch_nicht_gedruckt = anz_vok - (aktuelle_seite - 1) * 16;
                    vok_beginnen = (aktuelle_seite - 1) * 16 + 1;
                }

                //Falls noch mehr Seiten gedruckt werden müssen
                if (noch_nicht_gedruckt >= 16)
                {

                    //Positionsverschiebung der Rückseite
                    int links_rechts_verschiebung = -3;

                    for (int i = 0; i < 16; i++)
                    {
                        //Positionszugabe ändern
                        switch (links_rechts_verschiebung)
                        {
                            case 1:
                                links_rechts_verschiebung = -1;
                                break;
                            case -1:
                                links_rechts_verschiebung = -3;
                                break;
                            case -3:
                                links_rechts_verschiebung = 3;
                                break;
                            case 3:
                                links_rechts_verschiebung = 1;
                                break;
                        }

                        //Koordinaten abfragen
                        int[] coordinates = get_coordinates(i + 1 + links_rechts_verschiebung);

                        //Grösse des Textes abfragen

                        SizeF size_string = g.MeasureString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[2].Text, font);

                        int height = Convert.ToInt32(size_string.Height / 2);

                        //Falls es ein Synonym gibt
                        if (listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[2].Text.Contains("=") == true)
                        {
                            string[] split_text = listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[2].Text.Split('=');

                            SizeF size_foreign = g.MeasureString(split_text[0], font);
                            SizeF size_synonym = g.MeasureString(split_text[1], font);

                            if (size_foreign.Width > 292)
                            {
                                bool is_good = false;
                                int font_size = 12;

                                if (split_text[0].Trim().Contains(" ") == true)
                                {
                                    //Falls der String leerschläge enthält

                                    string[] splitter = split_text[0].Trim().Split(' ');

                                    do
                                    {
                                        Font font_new = new Font("Arial", font_size);

                                        for (int y = 1; y < splitter.Length; y++)
                                        {
                                            string part1 = "";
                                            string part2 = "";

                                            for (int x = 1; x <= splitter.Length - y; x++)
                                            {
                                                part1 = part1 + " " + splitter[x - 1];

                                                if (x == splitter.Length - y)
                                                {
                                                    for (int z = splitter.Length - y; z < splitter.Length; z++)
                                                    {
                                                        part2 = part2 + " " + splitter[z];
                                                    }
                                                }
                                            }

                                            SizeF size_part1 = g.MeasureString(part1, font_new);
                                            SizeF size_part2 = g.MeasureString(part2, font_new);

                                            if (size_part1.Width <= 292 && size_part2.Width <= 292)
                                            {
                                                is_good = true;

                                                //zwei Zeilen schreiben
                                                g.DrawString(part1, font_new, Brushes.Black, new Point(-coordinates[0] - top, -coordinates[1] + left - height - 60 - height), format);
                                                g.DrawString(part2, font_new, Brushes.Black, new Point(-coordinates[0] - top, -coordinates[1] + left - height - 20 - height), format);

                                                break;
                                            }
                                        }

                                        if (is_good == false)
                                        {
                                            font_size--;
                                        }

                                    } while (is_good == false);
                                }
                            }
                            else
                            {
                                //Foreign normal schreiben
                                g.DrawString(split_text[0], font, Brushes.Black, new Point((coordinates[0] * (-1)) - top, (coordinates[1] * (-1)) + left - height - 20 - height), format);
                            }

                            //Trennlinie zeichnen
                            g.DrawLine(pen, new Point((coordinates[0] * (-1)) - top - 10, (coordinates[1] * (-1)) + left - height / 2), new Point((coordinates[0] * (-1)) - top + 10, (coordinates[1] * (-1)) + left - height / 2));

                            //Falls Synonym zu gross
                            if (size_foreign.Width > 292)
                            {
                                bool is_good = false;
                                int font_size = 12;

                                if (split_text[1].Trim().Contains(" ") == true)
                                {
                                    //Falls der String leerschläge enthält

                                    string[] splitter = split_text[1].Trim().Split(' ');

                                    do
                                    {
                                        Font font_new = new Font("Arial", font_size);

                                        for (int y = 1; y < splitter.Length; y++)
                                        {
                                            string part1 = "";
                                            string part2 = "";

                                            for (int x = 1; x <= splitter.Length - y; x++)
                                            {
                                                part1 = part1 + " " + splitter[x - 1];

                                                if (x == splitter.Length - y)
                                                {
                                                    for (int z = splitter.Length - y; z < splitter.Length; z++)
                                                    {
                                                        part2 = part2 + " " + splitter[z];
                                                    }
                                                }
                                            }


                                            SizeF size_part1 = g.MeasureString(part1, font_new);
                                            SizeF size_part2 = g.MeasureString(part2, font_new);

                                            if (size_part1.Width <= 292 && size_part2.Width <= 292)
                                            {
                                                is_good = true;

                                                //zwei Zeilen schreiben
                                                g.DrawString(part1, font_new, Brushes.Black, new Point((coordinates[0] * (-1)) - top, (coordinates[1] * (-1)) + left - height + 20), format);
                                                g.DrawString(part2, font_new, Brushes.Black, new Point((coordinates[0] * (-1)) - top, (coordinates[1] * (-1)) + left - height + 60), format);

                                                break;
                                            }
                                        }

                                        if (is_good == false)
                                        {
                                            font_size--;
                                        }

                                    } while (is_good == false);
                                }
                            }
                            else
                            {
                                //Synonym normal schreiben
                                g.DrawString(split_text[1], font, Brushes.Black, new Point((coordinates[0] * (-1)) - top, (coordinates[1] * (-1)) + left - height + 20), format);
                            }
                            //Falls es kein Synonym gibt
                        }
                        else
                        {
                            //Schriftgrösse anpassen
                            //Falls Text zu gross

                            //Falls Text zu gross, string auf mehrere Zeilen aufteilen falls möglich
                            if (size_string.Width > 292)
                            {

                                bool is_good = false;
                                int font_size = 12;

                                if (listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[2].Text.Trim().Contains(" ") == true)
                                {
                                    //Falls der String leerschläge enthält

                                    string[] splitter = listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[2].Text.Trim().Split(' ');

                                    do
                                    {
                                        Font font_new = new Font("Arial", font_size);

                                        for (int y = 1; y < splitter.Length; y++)
                                        {
                                            string part1 = "";
                                            string part2 = "";

                                            for (int x = 1; x <= splitter.Length - y; x++)
                                            {
                                                part1 = part1 + " " + splitter[x - 1];

                                                if (x == splitter.Length - y)
                                                {
                                                    for (int z = splitter.Length - y; z < splitter.Length; z++)
                                                    {
                                                        part2 = part2 + " " + splitter[z];
                                                    }
                                                }
                                            }

                                            SizeF size_part1 = g.MeasureString(part1, font_new);
                                            SizeF size_part2 = g.MeasureString(part2, font_new);

                                            if (size_part1.Width <= 292 && size_part2.Width <= 292)
                                            {
                                                is_good = true;

                                                //zwei Zeilen schreiben
                                                g.DrawString(part1, font_new, Brushes.Black, new Point((coordinates[0] * (-1)) - top, (coordinates[1] * (-1)) + left - height - 20), format);
                                                g.DrawString(part2, font_new, Brushes.Black, new Point((coordinates[0] * (-1)) - top, (coordinates[1] * (-1)) + left - height + 20), format);

                                                break;
                                            }
                                        }

                                        if (is_good == false)
                                        {
                                            font_size--;
                                        }

                                    } while (is_good == false);
                                }
                                else
                                {
                                    do
                                    {
                                        font_size--;
                                        Font font_new = new Font("Arial", font_size);

                                        SizeF string_size = g.MeasureString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text, font_new);

                                        if (string_size.Width > 292 && font_size > 1)
                                        {
                                            is_good = false;
                                        }
                                        else
                                        {
                                            is_good = true;

                                            //kleinerer Text schreiben
                                            g.DrawString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text, font_new, Brushes.Black, new Point(coordinates[0] + top, coordinates[1] - left - height), format);
                                        }

                                    } while (is_good == false);
                                }
                            }
                            else
                            {
                                //Normal schreiben
                                g.DrawString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[2].Text, font, Brushes.Black, new Point((coordinates[0] * (-1)) - top, (coordinates[1] * (-1)) + left - height), format);
                            }
                        }
                    }
                    //Falls letzte Seite
                }
                else
                {
                    //Positionsverschiebung der Rückseite
                    int links_rechts_verschiebung = -3;

                    for (int i = 0; i < noch_nicht_gedruckt; i++)
                    {

                        //Positionszugabe ändern
                        switch (links_rechts_verschiebung)
                        {
                            case 1:
                                links_rechts_verschiebung = -1;
                                break;
                            case -1:
                                links_rechts_verschiebung = -3;
                                break;
                            case -3:
                                links_rechts_verschiebung = 3;
                                break;
                            case 3:
                                links_rechts_verschiebung = 1;
                                break;
                        }

                        //Koordinaten abfragen
                        int[] coordinates = get_coordinates(i + 1 + links_rechts_verschiebung);

                        //Grösse des Textes abfragen

                        SizeF size_string = g.MeasureString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[2].Text, font);

                        int height = Convert.ToInt32(size_string.Height / 2);

                        //Schriftgrösse anpassen

                        //Vokabel schreiben
                        //Falls es ein Synonym gibt
                        if (listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[2].Text.Contains("=") == true)
                        {
                            string[] split_text = listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[2].Text.Split('=');

                            SizeF size_foreign = g.MeasureString(split_text[0], font);
                            SizeF size_synonym = g.MeasureString(split_text[1], font);

                            //Falls Foreign zu gross
                            if (size_foreign.Width > 292)
                            {
                                bool is_good = false;
                                int font_size = 12;

                                if (split_text[0].Trim().Contains(" ") == true)
                                {
                                    //Falls der String leerschläge enthält

                                    string[] splitter = split_text[0].Trim().Split(' ');

                                    do
                                    {
                                        Font font_new = new Font("Arial", font_size);

                                        for (int y = 1; y < splitter.Length; y++)
                                        {
                                            string part1 = "";
                                            string part2 = "";

                                            for (int x = 1; x <= splitter.Length - y; x++)
                                            {
                                                part1 = part1 + " " + splitter[x - 1];

                                                if (x == splitter.Length - y)
                                                {
                                                    for (int z = splitter.Length - y; z < splitter.Length; z++)
                                                    {
                                                        part2 = part2 + " " + splitter[z];
                                                    }
                                                }
                                            }

                                            SizeF size_part1 = g.MeasureString(part1, font_new);
                                            SizeF size_part2 = g.MeasureString(part2, font_new);

                                            if (size_part1.Width <= 292 && size_part2.Width <= 292)
                                            {
                                                is_good = true;

                                                //zwei Zeilen schreiben
                                                g.DrawString(part1, font_new, Brushes.Black, new Point(-coordinates[0] - top, -coordinates[1] + left - height - 60 - height), format);
                                                g.DrawString(part2, font_new, Brushes.Black, new Point(-coordinates[0] - top, -coordinates[1] + left - height - 20 - height), format);

                                                break;
                                            }
                                        }

                                        if (is_good == false)
                                        {
                                            font_size--;
                                        }

                                    } while (is_good == false);
                                }
                            }
                            else
                            {
                                //Foreign normal schreiben
                                g.DrawString(split_text[0], font, Brushes.Black, new Point(-coordinates[0] - top, -coordinates[1] + left - height - 20 - height), format);
                            }

                            //Trennlinie zeichnen
                            g.DrawLine(pen, new Point(-coordinates[0] - top - 10, -coordinates[1] + left - height / 2), new Point(-coordinates[0] - top + 10, -coordinates[1] + left - height / 2));

                            //Falls synonym zu gross
                            if (size_foreign.Width > 292)
                            {

                                bool is_good = false;
                                int font_size = 12;

                                if (split_text[1].Trim().Contains(" ") == true)
                                {
                                    //Falls der String leerschläge enthält

                                    string[] splitter = split_text[1].Trim().Split(' ');

                                    do
                                    {
                                        Font font_new = new Font("Arial", font_size);

                                        for (int y = 1; y < splitter.Length; y++)
                                        {
                                            string part1 = "";
                                            string part2 = "";

                                            for (int x = 1; x <= splitter.Length - y; x++)
                                            {
                                                part1 = part1 + " " + splitter[x - 1];

                                                if (x == splitter.Length - y)
                                                {
                                                    for (int z = splitter.Length - y; z < splitter.Length; z++)
                                                    {
                                                        part2 = part2 + " " + splitter[z];
                                                    }
                                                }
                                            }


                                            SizeF size_part1 = g.MeasureString(part1, font_new);
                                            SizeF size_part2 = g.MeasureString(part2, font_new);

                                            if (size_part1.Width <= 292 && size_part2.Width <= 292)
                                            {
                                                is_good = true;

                                                //zwei Zeilen schreiben


                                                g.DrawString(part1, font_new, Brushes.Black, new Point((coordinates[0] * (-1)) - top, (coordinates[1] * (-1)) + left - height + 20), format);
                                                g.DrawString(part2, font_new, Brushes.Black, new Point((coordinates[0] * (-1)) - top, (coordinates[1] * (-1)) + left - height + 60), format);

                                                break;
                                            }
                                        }

                                        if (is_good == false)
                                        {
                                            font_size--;
                                        }

                                    } while (is_good == false);
                                }
                            }
                            else
                            {
                                //Synonym normal schreiben
                                g.DrawString(split_text[1], font, Brushes.Black, new Point((coordinates[0] * (-1)) - top, (coordinates[1] * (-1)) + left - height + 20), format);
                            }
                        }
                        else //Falls es kein Synonym gibt
                        {
                            //Schriftgrösse anpassen
                            //Falls Text zu gross
                            if (size_string.Width > 292)
                            {

                                bool is_good = false;
                                int font_size = 12;

                                if (listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[2].Text.Trim().Contains(" ") == true)
                                {
                                    //Falls der String leerschläge enthält

                                    string[] splitter = listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[2].Text.Trim().Split(' ');

                                    do
                                    {
                                        Font font_new = new Font("Arial", font_size);

                                        for (int y = 1; y < splitter.Length; y++)
                                        {
                                            string part1 = "";
                                            string part2 = "";

                                            for (int x = 1; x <= splitter.Length - y; x++)
                                            {
                                                part1 = part1 + " " + splitter[x - 1];

                                                if (x == splitter.Length - y)
                                                {
                                                    for (int z = splitter.Length - y; z < splitter.Length; z++)
                                                    {
                                                        part2 = part2 + " " + splitter[z];
                                                    }
                                                }
                                            }

                                            SizeF size_part1 = g.MeasureString(part1, font_new);
                                            SizeF size_part2 = g.MeasureString(part2, font_new);

                                            if (size_part1.Width <= 292 && size_part2.Width <= 292)
                                            {
                                                is_good = true;

                                                //zwei Zeilen schreiben

                                                g.DrawString(part1, font_new, Brushes.Black, new Point((coordinates[0] * (-1)) - top, (coordinates[1] * (-1)) + left - height - 20), format);
                                                g.DrawString(part2, font_new, Brushes.Black, new Point((coordinates[0] * (-1)) - top, (coordinates[1] * (-1)) + left - height + 20), format);

                                                break;
                                            }
                                        }

                                        if (is_good == false)
                                        {
                                            font_size--;
                                        }

                                    } while (is_good == false);
                                }
                            }
                            else
                            {
                                //Text normal schreiben
                                g.DrawString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[2].Text, font, Brushes.Black, new Point((coordinates[0] * (-1)) - top, (coordinates[1] * (-1)) + left - height), format);
                            }
                        }
                    }
                }
            }


            //Schauen, ob noch mehr Seiten gedruckt werden müssen

            if (aktuelle_seite != anzahl_seiten)
            {
                e.HasMorePages = true;
                aktuelle_seite++;
            }
            else
            {
                e.HasMorePages = false;
            }
        }

        private void printDocument_cards_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //Anzahl Seiten festlegen
            anzahl_seiten = (int)Math.Ceiling(anz_vok / 16d);
            aktuelle_seite = 1;
        }

        private int[] get_coordinates(int number)
        {
            switch (number)
            {
                case 01: return new int[] { -146, 103 };
                case 02: return new int[] { -146, 310 };
                case 03: return new int[] { -146, 516 };
                case 04: return new int[] { -146, 723 };
                case 05: return new int[] { -438, 103 };
                case 06: return new int[] { -438, 310 };
                case 07: return new int[] { -438, 516 };
                case 08: return new int[] { -438, 723 };
                case 09: return new int[] { -731, 103 };
                case 10: return new int[] { -731, 310 };
                case 11: return new int[] { -731, 516 };
                case 12: return new int[] { -731, 723 };
                case 13: return new int[] { -1023, 103 };
                case 14: return new int[] { -1023, 310 };
                case 15: return new int[] { -1023, 516 };
                default: return new int[] { -1023, 723 };
            }
        }

        private void TsmiOpenInExplorer_Click(object sender, EventArgs e)
        {
            if (CurrentBook.UnsavedChanges)
            {
                DialogResult dialogResult = MessageBox.Show(Messages.OpenInExplorerSave,
                    Messages.OpenInExplorerSaveT, MessageBoxButtons.YesNoCancel);

                if (dialogResult == DialogResult.Yes)
                {
                    // TODO: Save changes
                }
                else if (dialogResult == DialogResult.Cancel)
                {
                    return;
                }
            }
            FileInfo info = new FileInfo(CurrentBook.FilePath);
            if (info.Exists)
            {
                Process.Start("explorer.exe", $"/select,\"{info.FullName}\"");
            }
            else
            {
                MessageBox.Show(Messages.OpenInExplorerNotFound, Messages.OpenInExplorerNotFoundT, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TsmiImport_Click(object sender, EventArgs e)
        {
            if (UnsavedChanges)
            {
                DialogResult dialogResult = MessageBox.Show(Messages.CsvExportSave,
                    Messages.CsvExportSaveT, MessageBoxButtons.YesNoCancel);

                if (dialogResult == DialogResult.Yes)
                {
                    // TODO: Save changes
                }
                else if (dialogResult == DialogResult.Cancel)
                {
                    return;
                }
            }

            OpenFileDialog openDialog = new OpenFileDialog
            {
                Title = Words.Import,
                Filter = "CSV (*.csv)|*.csv"
            };

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                if (CurrentBook != null)
                {
                    VocabularyFile.ImportCsvFile(openDialog.FileName, CurrentBook, false);
                }
                else
                {
                    CurrentBook = new VocabularyBook();
                    VocabularyFile.ImportCsvFile(openDialog.FileName, CurrentBook, true);
                }
            }

            openDialog.Dispose();

            // TODO: Load book if created from scratch
        }

        private void TsmiExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog
            {
                Title = Words.Export,
                Filter = "CSV (*.csv)|*.csv"
            };

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                VocabularyFile.ExportCsvFile(saveDialog.FileName, CurrentBook);
            }

            saveDialog.Dispose();
        }

        private void search_update()
        {
            //Nach updates suchen

            K_Updater.Settings KSettings;
            KSettings.AuthenticateMode = K_Updater.SelfUpdate.authentication.none;
            KSettings.AuthenticateUsername = "";
            KSettings.AuthenticatePassword = "";
            KSettings.CurrentAppVersion = Application.ProductVersion;
            KSettings.Language = K_Updater.SelfUpdate.language.german;
            KSettings.Proxy = "";
            KSettings.ProxyUsername = "";
            KSettings.ProxyPassword = "";
            KSettings.UpdatePath = "http://update.vocup.ch";

            K_Updater.SelfUpdate SUpdate = new K_Updater.SelfUpdate(KSettings);

            K_Updater.UpdateCheckResult KResult = SUpdate.Check();


            //Nach updates suchen

            if (KResult.Code == 0)
            {
                Activate();

                MessageBox.Show("Es sind keine neuen Updates verfügbar.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (KResult.Code == 1)
            {
                Activate();

                DialogResult result = MessageBox.Show("Es ist ein neues Update verfügbar.\r\n\r\n" +
                                                      "Neue Version: " + KResult.NewVersion + "\r\n\r\n" +
                                                      "Beschreibung:\r\n\r\n" + KResult.Description + "\r\n\r\n" +
                                                      "Möchten Sie die neue Version herunterladen?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                bool do_update = true;

                if (result == DialogResult.Yes)
                {
                    if (edit_vokabelheft == true)
                    {
                        DialogResult result2 = MessageBox.Show(Properties.language.save_before_update,
                                                 Application.ProductName,
                                                 MessageBoxButtons.YesNoCancel,
                                                 MessageBoxIcon.Warning);
                        if (result2 == DialogResult.Yes)
                        {
                            savefile(false);

                            edit_vokabelheft = false;
                        }
                        else if (result2 == DialogResult.No)
                        {
                            edit_vokabelheft = false;
                        }
                        else if (result2 == DialogResult.Cancel)
                        {
                            do_update = false;
                        }
                    }

                    //Update durchführen
                    if (do_update == true)
                    {
                        SUpdate.DoUpdate();
                    }

                }
            }
            else //Falls Fehler aufgetaucht sind
            {
                Activate();

                MessageBox.Show("Während der Updateprüfung ist ein Fehler aufgetreten.\r\n\r\n" +
                                "Fehlercode: " + KResult.Code.ToString() + "\r\n" +
                                "Beschreibung: " + KResult.Description.ToString() + "\r\n",
                                Application.ProductName,
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //Nach Vokabel suchen
        private void search_vokabel(string search_text)
        {
            search_text = search_text.ToUpper();

            if (search_text == "EASTER EGG")
            {
                if (CurrentBook != null)
                {
                    // Save and close current book
                    savefile(false);
                    UnloadBook();
                }

                VocabularyBook book = new VocabularyBook()
                {
                    MotherTongue = "Deutsch",
                    ForeignLang = "Esperanto"
                };

                //Vokabeln einlesen
                book.Words.Add(new VocabularyWord() { MotherTongue = "klicken", ForeignLang = "klaki" });
                book.Words.Add(new VocabularyWord() { MotherTongue = "klicken", ForeignLang = "klaki" });
                book.Words.Add(new VocabularyWord() { MotherTongue = "chatten", ForeignLang = "babili" });
                book.Words.Add(new VocabularyWord() { MotherTongue = "Bildschirm", ForeignLang = "ekrano" });
                book.Words.Add(new VocabularyWord() { MotherTongue = "Fenster", ForeignLang = "fenestro" });
                book.Words.Add(new VocabularyWord() { MotherTongue = "Browser", ForeignLang = "retumilo" });
                book.Words.Add(new VocabularyWord() { MotherTongue = "Computer", ForeignLang = "komputilo" });
                book.Words.Add(new VocabularyWord() { MotherTongue = "Link", ForeignLang = "ligilo" });
                book.Words.Add(new VocabularyWord() { MotherTongue = "Linux", ForeignLang = "Linukso" });
                book.Words.Add(new VocabularyWord() { MotherTongue = "Macintosh", ForeignLang = "Makintoŝo" });
                book.Words.Add(new VocabularyWord() { MotherTongue = "Webseiten", ForeignLang = "paĝaro" });
                book.Words.Add(new VocabularyWord() { MotherTongue = "Webseite", ForeignLang = "retpaĝo" });
                book.Words.Add(new VocabularyWord() { MotherTongue = "E-Mail-Adresse", ForeignLang = "retpoŝto" });
                book.Words.Add(new VocabularyWord() { MotherTongue = "Server", ForeignLang = "servilo" });
                book.Words.Add(new VocabularyWord() { MotherTongue = "Benutzername", ForeignLang = "uzantnomo" });
                book.Words.Add(new VocabularyWord() { MotherTongue = "Kennwort", ForeignLang = "pasvorto" });
                book.Words.Add(new VocabularyWord() { MotherTongue = "Windows", ForeignLang = "Vindozo" });
                book.Words.Add(new VocabularyWord() { MotherTongue = "Datei", ForeignLang = "dosiero" });
                book.Words.Add(new VocabularyWord() { MotherTongue = "Ordner", ForeignLang = "dosierujo" });
                book.Words.Add(new VocabularyWord() { MotherTongue = "Herunterladen", ForeignLang = "elŝuti" });
                book.Words.Add(new VocabularyWord() { MotherTongue = "Internet", ForeignLang = "interreto" });

                book.Notify();

                LoadBook(book);

                TbSearchWord.Text = "";
            }
            else if (search_text == "TRANSPARENT") // EasterEgg 2
            {
                Opacity -= 0.25;
                TbSearchWord.Text = "";

                if (Opacity == 0d)
                {
                    Thread.Sleep(3000);
                    Opacity = 1d;
                }
            }
            else // ListView durchsuchen
            {
                //Index bestimmen der durchsucht werden soll

                int index_of = 0;

                //Wird ausgeführt, falls ein Item markiert ist

                if (listView_vokabeln.SelectedItems.Count > 0)
                {

                    //Falls das Letzte Item markiert ist, wird der Index auf 0 gesetzt

                    if (listView_vokabeln.FocusedItem.Index + 1 == listView_vokabeln.Items.Count)
                    {
                        index_of = 0;
                    }
                    else //Ansonsten wird der Index um 1 erhöht
                    {
                        index_of = listView_vokabeln.FocusedItem.Index + 1;
                    }
                }

                //Die Variable dient dazu, damit es keine Endlosschleife gibt 

                int controll = 0;

                for (int i = index_of; i < listView_vokabeln.Items.Count; i++)
                {

                    controll++;

                    //Wird ausgeführt, sobald ein Treffer gefunden wurde

                    if (listView_vokabeln.Items[i].SubItems[1].Text.ToUpper().Contains(search_text) == true || listView_vokabeln.Items[i].SubItems[2].Text.ToUpper().Contains(search_text) == true)
                    {
                        listView_vokabeln.BeginUpdate();

                        listView_vokabeln.Focus();
                        listView_vokabeln.Items[i].Selected = true;
                        listView_vokabeln.Items[i].Focused = true;
                        listView_vokabeln.Items[i].EnsureVisible();
                        listView_vokabeln.EndUpdate();

                        //Grün aufblinken

                        TbSearchWord.BackColor = Color.FromArgb(144, 238, 144);
                        TbSearchWord.Update();
                        Thread.Sleep(300);

                        TbSearchWord.BackColor = Color.White;
                        TbSearchWord.Update();
                        break;
                    }

                    //Verhindert eine Endlosschleife

                    if (controll == listView_vokabeln.Items.Count + 1)
                    {
                        //Rot aufblinken

                        TbSearchWord.BackColor = Color.FromArgb(255, 192, 203);
                        TbSearchWord.Update();
                        Thread.Sleep(300);

                        TbSearchWord.BackColor = Color.White;
                        TbSearchWord.Update();
                        break;
                    }

                    //Falls der Index beim letzten Item liegt, springt der Index zum ersten Item

                    if (i == listView_vokabeln.Items.Count - 1)
                    {
                        i = -1;
                    }
                }

                //Fokus auf Einfügen-Button zurücksetzen
            }

            AcceptButton = BtnAddWord;
            BtnAddWord.Update();
        }

        //-----

        //Beenden

        private void TsmiExitAppliaction_Click(object sender, EventArgs e)
        {
            if (UnsavedChanges && vokabelheft_ask_to_save())
            {
                Close();
            }
        }

        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (UnsavedChanges)
            {
                e.Cancel = !vokabelheft_ask_to_save();
            }
        }

        //-----

    }
}