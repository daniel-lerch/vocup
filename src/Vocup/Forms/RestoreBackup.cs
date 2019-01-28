using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Vocup.Models;
using Vocup.Properties;

namespace Vocup.Forms
{
    public partial class RestoreBackup : Form
    {
        public RestoreBackup()
        {
            InitializeComponent();
            Icon = Icon.FromHandle(Icons.DatabaseRestore.GetHicon());
        }

        public string path_backup;

        private LogItem[] vhf_vhr_log;
        // 0: int FileIndex
        // 1: string VhfPath
        // 2: string VhrCode
        // 3: int UiIndex

        private string[] vhr_log;

        public List<string[]> vhf_restore = new List<string[]>();
        public List<string> vhr_restore = new List<string>();
        public List<string> chars_restore = new List<string>();

        private void Form_Load(object sender, EventArgs e)
        {
            //Falls das Feld mit dem Pfad nicht leer ist

            if (TbFilePath.Text != "")
            {
                OpenFile();
            }
        }

        //OpenFile-Dialog anzeigen
        private void BtnFilePath_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog
            {
                Title = Words.SaveBackup,
                Filter = Words.VocupBackupFile + " (*.vdp)|*.vdp",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal)
            };

            if (open.ShowDialog() == DialogResult.OK)
            {
                path_backup = open.FileName;
                TbFilePath.Text = open.FileName;
            }
        }

        //Falls ein anderer Pfad gewählt worden ist
        private void path_field_TextChanged(object sender, EventArgs e)
        {
            OpenFile();
        }

        private bool TryOpen(string path, out ZipFile file)
        {
            try
            {
                file = new ZipFile(path);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(Messages.VdpInvalidFile, ex), Messages.VdpInvalidFileT, MessageBoxButtons.OK, MessageBoxIcon.Error);
                file = null;
                return false;
            }
        }

        private void OpenFile()
        {
            Cursor.Current = Cursors.WaitCursor;
            Update();

            ListBooks.Items.Clear();
            ListSpecialChars.Items.Clear();

            //Neue Datei öffnen
            if (!TryOpen(TbFilePath.Text, out ZipFile backup_file))
            {
                DialogResult = DialogResult.Abort;
                Close();
                return;
            }

            if (backup_file.Count > 0)
            {
                ZipEntry log_entry = backup_file.GetEntry("vhf_vhr.log");

                //Versucht die Log-Datei aus dem Archiv zu lesen
                try
                {
                    string[] log_lines;

                    using (Stream log_stream = backup_file.GetInputStream(log_entry))
                    {
                        byte[] buffer = new byte[log_entry.Size];
                        StreamUtils.ReadFully(log_stream, buffer);
                        string logstring = Encoding.UTF8.GetString(buffer);
                        log_lines = logstring.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                    }

                    if (log_lines.Length > 0)
                    {
                        //Daten in ein Array lesen
                        vhf_vhr_log = new LogItem[log_lines.Length];

                        for (int i = 0; i < log_lines.Length; i++)
                        {
                            string[] y = log_lines[i].Split('|');
                            vhf_vhr_log[i] = new LogItem(int.Parse(y[0]), BackupMeta.ExpandPath(y[1]), y[2]);
                        }

                        //Dateien in die Listbox einlesen
                        ListBooks.BeginUpdate();

                        //Radiobuttons ausschalten
                        RbReplaceOlder.Enabled = false;
                        RbReplaceNothing.Enabled = false;

                        for (int i = 0; i < vhf_vhr_log.Length; i++)
                        {
                            FileInfo vhf_info = new FileInfo(vhf_vhr_log[i].VhfPath);

                            try
                            {
                                ZipEntry vhf_entry = backup_file.GetEntry(@"vhf\" + vhf_vhr_log[i].FileIndex + ".vhf");

                                //Aktiviert die Radiobuttons falls nötig
                                if (!vhf_info.Exists)
                                {
                                    RbReplaceNothing.Enabled = true;
                                }
                                if (vhf_entry.DateTime > vhf_info.LastWriteTime)
                                {
                                    RbReplaceOlder.Enabled = true;
                                }

                                //Falls alle Vokabelhefte ersetzt werden sollen
                                if (RbReplaceAll.Checked)
                                {
                                    //Schaltet die Groupboxs wieder ein
                                    GroupReplace.Enabled = true;
                                    GroupBooks.Enabled = true;

                                    //Exakte Pfadangaben
                                    string text = CbAbsolutePath.Checked ? vhf_vhr_log[i].VhfPath : Path.GetFileNameWithoutExtension(vhf_vhr_log[i].VhfPath);
                                    vhf_vhr_log[i].UiIndex = ListBooks.Items.Add(text, true);
                                }
                                else if (RbReplaceOlder.Checked) // Falls nur neuere Vokabelhefte ersetzt werden sollen
                                {
                                    //Schaltet die Groupboxs wieder ein
                                    GroupReplace.Enabled = true;
                                    GroupBooks.Enabled = true;

                                    if (!vhf_info.Exists || vhf_entry.DateTime > vhf_info.LastWriteTime)
                                    {
                                        //Exakte Pfadangaben
                                        string text = CbAbsolutePath.Checked ? vhf_vhr_log[i].VhfPath : Path.GetFileNameWithoutExtension(vhf_vhr_log[i].VhfPath);
                                        vhf_vhr_log[i].UiIndex = ListBooks.Items.Add(text, true);
                                    }
                                }
                                else //Falls nichts ersetzt werden soll
                                {
                                    //Schaltet die Groupboxs wieder ein
                                    GroupReplace.Enabled = true;
                                    GroupBooks.Enabled = true;
                                    GroupResults.Enabled = true;

                                    if (!vhf_info.Exists)
                                    {
                                        //Exakte Pfadangaben
                                        string text = CbAbsolutePath.Checked ? vhf_vhr_log[i].VhfPath : Path.GetFileNameWithoutExtension(vhf_vhr_log[i].VhfPath);
                                        vhf_vhr_log[i].UiIndex = ListBooks.Items.Add(text, true);
                                    }
                                }

                                //Wiederherstellen-Button aktivieren
                                if (ListBooks.Items.Count != 0)
                                {
                                    RbRestoreAssociatedResults.Enabled = true;
                                    RbRestoreAssociatedResults.Checked = true;
                                    BtnRestore.Enabled = true;
                                    BtnRestore.Focus();
                                    AcceptButton = BtnRestore;
                                }
                            }
                            catch
                            {
                                //Falls beim Lesen der VHF-Datei ein Fehler aufgetaucht ist
                            }
                        }

                        ListBooks.EndUpdate();
                    }
                    else
                    {
                        //listbox-Vokabelhefte und radiobuttons ausblenden, weil keine Vokabelhefte vorhanden sind

                        GroupReplace.Enabled = false;
                        GroupBooks.Enabled = false;

                        RbRestoreAssociatedResults.Enabled = false;

                        RbRestoreAllResults.Checked = true;
                    }

                    //Nach Sonderzeichentabellen suchen

                    ZipEntry log_entry_chars = backup_file.GetEntry("chars.log");

                    try
                    {
                        Stream log_stream_chars = backup_file.GetInputStream(log_entry_chars);

                        byte[] buffer_chars = new byte[log_entry_chars.Size];

                        StreamUtils.ReadFully(log_stream_chars, buffer_chars);

                        string logstring_chars = Encoding.UTF8.GetString(buffer_chars);

                        log_stream_chars.Close();

                        if (logstring_chars != "")
                        {
                            string[] log_lines_chars = logstring_chars.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

                            //Dateien in die Listbox einlesen

                            ListSpecialChars.Update();

                            for (int i = 0; i < log_lines_chars.Length; i++)
                            {
                                try
                                {
                                    ZipEntry chars_entry = new ZipEntry(@"chars\" + log_lines_chars[i]);

                                    if (chars_entry.IsFile == true)
                                    {
                                        ListSpecialChars.Items.Add(Path.GetFileNameWithoutExtension(log_lines_chars[i]), true);
                                    }
                                }
                                catch
                                {
                                }

                            }

                            //Wiederherstellen-Button aktivieren

                            if (ListSpecialChars.Items.Count != 0)
                            {
                                GroupSpecialChars.Enabled = true;
                                BtnRestore.Enabled = true;

                                BtnRestore.Focus();

                                AcceptButton = BtnRestore;
                            }
                        }
                        else
                        {
                            GroupSpecialChars.Enabled = false;
                        }
                    }
                    catch
                    {
                        ListSpecialChars.Items.Clear();
                        GroupSpecialChars.Enabled = false;
                    }


                    //Nach Ergebnissen suchen

                    ZipEntry log_entry_vhr = backup_file.GetEntry("vhr.log");

                    try
                    {
                        Stream log_stream_vhr = backup_file.GetInputStream(log_entry_vhr);

                        byte[] buffer_vhr = new byte[log_entry_vhr.Size];

                        StreamUtils.ReadFully(log_stream_vhr, buffer_vhr);

                        string logstring_vhr = Encoding.UTF8.GetString(buffer_vhr);

                        log_stream_vhr.Close();

                        if (logstring_vhr != "")
                        {
                            vhr_log = logstring_vhr.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

                            //Wiederherstellen-Button aktivieren

                            GroupResults.Enabled = true;
                            BtnRestore.Enabled = true;

                            BtnRestore.Focus();

                            AcceptButton = BtnRestore;
                        }
                        else
                        {
                            GroupResults.Enabled = false;
                        }

                        //Stream schliessen
                        backup_file.Close();
                    }
                    catch
                    {
                        GroupResults.Enabled = false;
                    }
                }
                catch
                {
                    //Radiobuttons etc. wieder ausblenden und Fehlermeldung anzeigen

                    ListSpecialChars.Items.Clear();
                    ListBooks.Items.Clear();

                    GroupReplace.Enabled = false;
                    GroupBooks.Enabled = false;
                    GroupResults.Enabled = false;
                    GroupSpecialChars.Enabled = false;

                    BtnRestore.Enabled = false;

                    AcceptButton = BtnFilePath;
                }
            }
            else
            {
                //Es sind im Archiv keine Dateien vorhanden

                GroupReplace.Enabled = false;
                GroupBooks.Enabled = false;
                GroupResults.Enabled = false;
                GroupSpecialChars.Enabled = false;

                BtnRestore.Enabled = false;

                AcceptButton = BtnFilePath;
            }
        }

        private void CbAbsolutePath_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < ListBooks.Items.Count; i++)
            {
                for (int j = 0; j < vhf_vhr_log.Length; j++)
                {
                    if (vhf_vhr_log[j].UiIndex == i)
                    {
                        ListBooks.Items[i] = CbAbsolutePath.Checked ? vhf_vhr_log[j].VhfPath : Path.GetFileNameWithoutExtension(vhf_vhr_log[j].VhfPath);
                        break;
                    }
                }
            }
        }

        //Falls die Auswahl geändert wird, Backup neu untersuchen
        private void replace_all_CheckedChanged(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void replace_newer_CheckedChanged(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void replace_nothing_CheckedChanged(object sender, EventArgs e)
        {
            OpenFile();
        }


        //Falls nötig wiederherstellen-Button deaktivieren oder aktivieren

        private void results_restore_nothing_CheckedChanged(object sender, EventArgs e)
        {
            coordinater();
        }

        private void results_restore_choosed_CheckedChanged(object sender, EventArgs e)
        {
            coordinater();
        }

        private void results_restore_all_CheckedChanged(object sender, EventArgs e)
        {
            coordinater();
        }

        private void listbox_vhf_SelectedValueChanged(object sender, EventArgs e)
        {
            coordinater();
        }

        private void listbox_special_chars_SelectedValueChanged(object sender, EventArgs e)
        {
            coordinater();
        }

        private void restore_button_MouseEnter(object sender, EventArgs e)
        {
            coordinater();
        }

        private void coordinater()
        {
            bool activate = false;

            if (ListBooks.CheckedItems.Count != 0 && GroupBooks.Enabled == true)
            {
                activate = true;
            }
            if (RbRestoreNoResults.Enabled == false & GroupResults.Enabled == true)
            {
                activate = true;
            }
            if (ListSpecialChars.CheckedItems.Count != 0 && GroupSpecialChars.Enabled == true)
            {
                activate = true;
            }

            BtnRestore.Enabled = activate;
        }

        private void BtnRestore_Click(object sender, EventArgs e)
        {
            try
            {
                bool activate = false;

                if (ListBooks.CheckedItems.Count != 0 && GroupBooks.Enabled == true)
                {
                    activate = true;
                }
                if (RbRestoreNoResults.Enabled == false & GroupResults.Enabled == true)
                {
                    activate = true;
                }
                if (ListSpecialChars.CheckedItems.Count != 0 && GroupSpecialChars.Enabled == true)
                {
                    activate = true;
                }

                if (activate == true)
                {
                    //Falls Daten zum sichern vorhanden sind ind Arrays einlesen

                    //Vokabelhefte in Array einlesen
                    if (ListBooks.Items.Count != 0)
                    {
                        for (int i = 0; i < vhf_vhr_log.Length; i++)
                        {
                            try
                            {
                                if (ListBooks.GetItemCheckState(vhf_vhr_log[i].UiIndex) == CheckState.Checked)
                                {
                                    string[] temp = new string[2];

                                    temp[0] = vhf_vhr_log[i].FileIndex.ToString();
                                    temp[1] = vhf_vhr_log[i].VhfPath;

                                    vhf_restore.Add(temp);

                                    if (RbRestoreAssociatedResults.Checked && RbRestoreAssociatedResults.Enabled && !string.IsNullOrWhiteSpace(vhf_vhr_log[i].VhrCode))
                                    {
                                        vhr_restore.Add(vhf_vhr_log[i].VhrCode + ".vhr");
                                    }
                                }
                            }
                            catch
                            {
                            }
                        }
                    }
                    //Ergebnisse in Array einlesen, falls alle Ergebnisse wiederhergestellt werden sollen

                    if (RbRestoreAllResults.Checked == true && RbRestoreAllResults.Enabled == true)
                    {
                        for (int i = 0; i < vhr_log.Length; i++)
                        {
                            vhr_restore.Add(vhr_log[i]);
                        }
                    }

                    //Sonderzeichentabellen in Array einlesen

                    for (int i = 0; i < ListSpecialChars.Items.Count; i++)
                    {
                        if (ListSpecialChars.GetItemCheckState(i) == CheckState.Checked)
                        {
                            chars_restore.Add(ListSpecialChars.Items[i] + ".txt");
                        }
                    }

                }
                
                DialogResult = DialogResult.OK; //Form schliessen
            }
            catch
            {
                //Fehlermeldung anzeigen
            }
        }

        private struct LogItem
        {
            public LogItem(int fileIndex, string vhfPath, string vhrCode)
            {
                FileIndex = fileIndex;
                VhfPath = vhfPath;
                VhrCode = vhrCode;
                UiIndex = -1;
            }

            public int FileIndex { get; }
            public string VhfPath { get; }
            public string VhrCode { get; }
            public int UiIndex { get; set; }
        }
    }
}