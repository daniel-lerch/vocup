using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Windows.Forms;
using Vocup.Models;
using Vocup.Properties;
using Vocup.Util;

namespace Vocup.Forms
{
    public partial class RestoreBackup : Form
    {
        private string path;
        private BackupMeta meta;

        public RestoreBackup(string path)
        {
            this.path = path;
            InitializeComponent();
            Icon = Icon.FromHandle(Icons.DatabaseRestore.GetHicon());
        }

        private void Form_Shown(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            ListBooks.BeginUpdate();
            ListSpecialChars.BeginUpdate();

            if (TryOpen(path, out ZipArchive archive) && BackupMeta.TryRead(archive, out meta))
            {
                foreach (BackupMeta.BookMeta book in meta.Books)
                {
                    string fullName = BackupMeta.ExpandPath(book.VhfPath);
                    ListBooks.Items.Add(CbAbsolutePath.Checked ? fullName : Path.GetFileNameWithoutExtension(fullName), true);
                }

                foreach (string specialChar in meta.SpecialChars)
                {
                    ListSpecialChars.Items.Add(specialChar, true);
                }
            }
            else
            {
                DialogResult = DialogResult.Abort;
            }

            ListBooks.EndUpdate();
            ListSpecialChars.EndUpdate();
            Cursor.Current = Cursors.Default;
        }

        private LogItem[] vhf_vhr_log;
        // 0: int FileIndex
        // 1: string VhfPath
        // 2: string VhrCode
        // 3: int UiIndex

        private string[] vhr_log;

        public List<string[]> vhf_restore = new List<string[]>();
        public List<string> vhr_restore = new List<string>();
        public List<string> chars_restore = new List<string>();

        private void restore_backup()
        {
            //Variablen f�r Fehlermeldungen
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

                //Schliesst das ge�ffnete Vokabelheft
                //close_vokabelheft();

                //Backup-Datei vorbereiten

                var backup_file = new ICSharpCode.SharpZipLib.Zip.ZipFile(path);

                //Vokabelhefte wiederherstellen
                if (vhf_restore.Count > 0)
                {
                    for (int i = 0; i < vhf_restore.Count; i++)
                    {
                        try
                        {

                            string[] temp = vhf_restore[i];

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

                            string[] temp = vhf_restore[i];
                            error_vhf_name.Add(temp[1]);
                        }
                    }
                }

                //Ergebnisse wiederherstellen

                if (vhr_restore.Count > 0)
                {
                    for (int i = 0; i < vhr_restore.Count; i++)
                    {
                        try
                        {
                            ZipEntry entry = backup_file.GetEntry(@"vhr\" + vhr_restore[i]);

                            byte[] buffer = new byte[entry.Size + 4096];


                            FileStream writer = new FileStream(Properties.Settings.Default.VhrPath + "\\" + vhr_restore[i], FileMode.Create);

                            StreamUtils.Copy(backup_file.GetInputStream(entry), writer, buffer);

                            writer.Close();
                        }

                        catch
                        {
                            error_vhr++;
                        }
                    }
                }

                if (chars_restore.Count > 0) // Sonderzeichentabellen sichern
                {
                    for (int i = 0; i < chars_restore.Count; i++)
                    {
                        try
                        {
                            ZipEntry entry = backup_file.GetEntry(@"chars\" + chars_restore[i]);

                            byte[] buffer = new byte[entry.Size + 4096];

                            Directory.CreateDirectory(AppInfo.SpecialCharDirectory);

                            FileStream writer = new FileStream(Path.Combine(AppInfo.SpecialCharDirectory, chars_restore[i]), FileMode.Create);

                            StreamUtils.Copy(backup_file.GetInputStream(entry), writer, buffer);

                            writer.Close();
                        }
                        catch
                        {
                            error_chars++;
                            error_chars_name.Add(chars_restore[i]);
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

            //Falls n�tig Fehlermeldungen anzeigen

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

        private void CbAbsolutePath_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < meta.Books.Count; i++)
            {
                string fullName = BackupMeta.ExpandPath(meta.Books[i].VhfPath);
                ListBooks.Items[i] = CbAbsolutePath.Checked ? fullName : Path.GetFileNameWithoutExtension(fullName);
            }
        }

        private void BtnRestore_Click(object sender, EventArgs e)
        {
            try
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

                restore_backup();
                DialogResult = DialogResult.OK; //Form schliessen
            }
            catch
            {
                //Fehlermeldung anzeigen
            }
        }

        private bool TryOpen(string path, out ZipArchive archive)
        {
            try
            {
                archive = new ZipArchive(new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read), ZipArchiveMode.Read, leaveOpen: false);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(Messages.VdpInvalidFile, ex), Messages.VdpInvalidFileT, MessageBoxButtons.OK, MessageBoxIcon.Error);
                archive = null;
                return false;
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