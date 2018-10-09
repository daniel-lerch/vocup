using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Vocup.Properties;
using Vocup.Util;

namespace Vocup.Forms
{
    public partial class CreateBackup : Form
    {
        public CreateBackup()
        {
            InitializeComponent();
            Icon = Icon.FromHandle(Icons.DatabaseAdd.GetHicon());
        }

        public string pfad;

        private void Form_Load(object sender, EventArgs e)
        {
            {
                // Check for vocabulary book files
                DirectoryInfo booksInfo = new DirectoryInfo(Settings.Default.VhfPath);
                int count = 0;
                if (booksInfo.Exists && (count = booksInfo.EnumerateFiles("*.vhf", SearchOption.AllDirectories).Count()) > 0)
                {
                    CbSaveAllBooks.Checked = true;
                    CbSaveAllBooks.Enabled = true;
                }
                CbSaveAllBooks.Text = string.Format(CbSaveAllBooks.Text, count);
            }
            {
                // Check for pratice result files
                DirectoryInfo resultsInfo = new DirectoryInfo(Settings.Default.VhrPath);
                int count = 0;
                if (resultsInfo.Exists && (count = resultsInfo.EnumerateFiles("*.vhr", SearchOption.TopDirectoryOnly).Count()) > 0)
                {
                    RbSaveAssociatedResults.Enabled = true;
                    RbSaveAllResults.Enabled = true;
                    RbSaveAssociatedResults.Checked = true;
                }
                RbSaveAllResults.Text = string.Format(RbSaveAllResults.Text, count);
            }
            {
                // Check for special char files
                DirectoryInfo specialCharInfo = new DirectoryInfo(AppInfo.SpecialCharDirectory);
                if (specialCharInfo.Exists)
                {
                    foreach (FileInfo file in specialCharInfo.GetFiles("*.txt", SearchOption.TopDirectoryOnly))
                    {
                        ListSpecialChars.Items.Add(Path.GetFileNameWithoutExtension(file.FullName));
                    }

                    if (ListSpecialChars.Items.Count > 0)
                    {
                        GroupSpecialChar.Enabled = true;
                    }
                }
            }
        }

        private void CbSaveAllBooks_CheckedChanged(object sender, EventArgs e)
        {
            UpdateUI();
        }

        private void ResultRadioButtons_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            if (rb.Checked)
                UpdateUI();
        }

        private void ListSpecialChars_SelectedValueChanged(object sender, EventArgs e)
        {
            UpdateUI();
        }

        /// <summary>
        /// Enables or disables the AcceptButton depending on the user's settings.
        /// </summary>
        private void UpdateUI()
        {
            RbSaveAssociatedResults.Enabled = RbSaveAllResults.Enabled && (CbSaveAllBooks.Checked || ListVocabularyBooks.Items.Count > 0);
            if (!RbSaveAssociatedResults.Enabled && RbSaveAssociatedResults.Checked)
                RbSaveNoResults.Checked = true;
            BtnCreateBackup.Enabled = CbSaveAllBooks.Checked || ListVocabularyBooks.Items.Count > 0 || RbSaveAllResults.Checked || ListSpecialChars.CheckedItems.Count > 0;
        }

        private void BtnAddVocabularyBook_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog add_file = new OpenFileDialog
            {
                Title = Words.AddVocabularyBooks,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                Filter = Words.VocupVocabularyBookFile + " (*.vhf)|*.vhf",
                Multiselect = true
            })
            {
                if (add_file.ShowDialog() == DialogResult.OK)
                {
                    foreach (string path in add_file.FileNames)
                    {
                        if (!ListVocabularyBooks.Items.Contains(path))
                        {
                            // TODO: Warning: A user might add a file that is backuped by setting CbSaveAllBooks.Checked to true
                            ListVocabularyBooks.Items.Add(path);
                        }
                    }

                    UpdateUI();
                }
            }
        }

        private void BtnDeleteVocabularyBook_Click(object sender, EventArgs e)
        {
            while (ListVocabularyBooks.SelectedItems.Count > 0)
            {
                ListVocabularyBooks.Items.Remove(ListVocabularyBooks.SelectedItems[0]);
            }

            BtnDeleteVocabularyBook.Enabled = false;

            UpdateUI();
        }

        private void ListVocabularyBooks_SelectedIndexChanged(object sender, EventArgs e)
        {
            BtnDeleteVocabularyBook.Enabled = ListVocabularyBooks.SelectedItems.Count > 0;
        }

        private void BtnCreateBackup_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog save = new SaveFileDialog
            {
                Title = Words.SaveBackup,
                Filter = Words.VocupBackupFile + " (*.vdp)|*.vdp",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal)
            })
            {
                if (save.ShowDialog() == DialogResult.OK)
                {
                    pfad = save.FileName;
                    DialogResult = DialogResult.OK;

                    ExecuteCreateBackup();
                }
                else
                {
                    DialogResult = DialogResult.Cancel;
                }
            }
        }

        private void ExecuteCreateBackup()
        {
            //Variablen für Fehlermeldungen
            int error_vhf = 0;
            int error_vhr = 0;
            int error_chars = 0;
            bool error = false;

            List<string> error_vhf_name = new List<string>();
            List<string> error_chars_name = new List<string>();

            bool file_exists = false;
            string temp_pfad = "";

            //Backup erstellen beim ersetzen, falls ein Fehler auftauchen sollte
            FileInfo pfad_info = new FileInfo(pfad);
            temp_pfad = Path.GetTempFileName();

            if (pfad_info.Exists)
            {
                file_exists = true;
                pfad_info.CopyTo(temp_pfad, true);
            }

            //Backup erstellen
            //Daten zusammenstellen
            //Cursor auf warten setzen
            Cursor.Current = Cursors.WaitCursor;

            List<string[]> files_vhf = new List<string[]>();
            List<string> files_vhr = new List<string>();
            List<string> files_chars = new List<string>();

            if (CbSaveAllBooks.Checked)
            {
                DirectoryInfo check_personal = new DirectoryInfo(Settings.Default.VhfPath);

                if (check_personal.Exists)
                {
                    List<string> subfolders = new List<string>()
                    {
                        check_personal.FullName
                    };

                    do
                    {
                        try
                        {
                            int position = subfolders.Count - 1;

                            DirectoryInfo folder_info = new DirectoryInfo(subfolders[position].ToString());

                            DirectoryInfo[] folders = folder_info.GetDirectories();

                            //Unterordner einlesen
                            for (int i = 0; i < folders.Length; i++)
                            {
                                subfolders.Add(folders[i].FullName);
                            }

                            //Vokabelhefte suchen
                            FileInfo[] files = folder_info.GetFiles("*.vhf");

                            for (int i = 0; i < files.Length; i++)
                            {
                                string[] coresponding_files = new string[2];
                                coresponding_files[0] = files[i].FullName;
                                coresponding_files[1] = "";
                                files_vhf.Add(coresponding_files);
                            }

                            subfolders.RemoveAt(position);
                        }
                        catch
                        {
                        }

                    } while (subfolders.Count > 0);
                }
            }

            //Vokabelhefte einlesen, die im Listbox vorhanden sind
            if (ListVocabularyBooks.Items.Count > 0)
            {
                for (int i = 0; i < ListVocabularyBooks.Items.Count; i++)
                {
                    bool contains_file = false;

                    foreach (string[] file_vhf in files_vhf)
                    {
                        if (ListVocabularyBooks.Items[i].ToString() == file_vhf[0])
                        {
                            contains_file = true;
                        }
                    }

                    if (!contains_file)
                    {
                        string[] corresponding_files = new string[2];
                        corresponding_files[0] = ListVocabularyBooks.Items[i].ToString();
                        corresponding_files[1] = "";

                        files_vhf.Add(corresponding_files);
                    }
                }
            }

            //Ergebnisse einlesen falls notwendig

            if (RbSaveAssociatedResults.Checked)
            {
                for (int i = 0; i < files_vhf.Count; i++)
                {
                    try
                    {
                        string vhf_name = files_vhf[i][0];

                        //Datei entschlüsseln
                        string plaintext;
                        using (StreamReader reader = new StreamReader(vhf_name, Encoding.UTF8))
                            plaintext = Crypto.Decrypt(reader.ReadToEnd());

                        // Zeilen der Datei in ein Array abspeichern
                        string[] lines = plaintext.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

                        FileInfo info = new FileInfo(Path.Combine(Settings.Default.VhrPath, lines[1] + ".vhr"));

                        if (info.Exists)
                        {
                            string[] corresponding_files = new string[2];

                            corresponding_files[0] = vhf_name;
                            corresponding_files[1] = lines[1];

                            files_vhf[i] = corresponding_files;

                            files_vhr.Add(info.FullName);
                        }
                    }
                    catch //Ergebnisse konnten nicht in die ArrayList geschrieben werden
                    {
                        error_vhr++;
                    }
                }
            }
            else if (RbSaveAllResults.Checked)
            {
                //Korespondierende Ergebnisse einlesen 
                for (int i = 0; i < files_vhf.Count; i++)
                {
                    try
                    {
                        string vhf_name = files_vhf[i][0];

                        //Datei entschlüsseln
                        string plaintext;
                        using (StreamReader reader = new StreamReader(vhf_name, Encoding.UTF8))
                            plaintext = Crypto.Decrypt(reader.ReadToEnd());

                        // Zeilen der Datei in ein Array abspeichern
                        string[] lines = plaintext.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

                        FileInfo info = new FileInfo(Properties.Settings.Default.VhrPath + "\\" + lines[1] + ".vhr");

                        if (info.Exists == true)
                        {
                            string[] corresponding_files = new string[2];

                            corresponding_files[0] = vhf_name;
                            corresponding_files[1] = lines[1];

                            files_vhf[i] = corresponding_files;
                        }
                    }
                    catch
                    {

                    }
                }

                //Alle Ergebnisse in files_vhr einlesen
                files_vhr.AddRange(new DirectoryInfo(Settings.Default.VhrPath).GetFiles("*.vhr").Select(x => x.FullName));
            }

            //Sonderzeichen sichern falls nötig
            for (int i = 0; i < ListSpecialChars.Items.Count; i++)
            {
                if (ListSpecialChars.GetItemChecked(i))
                {
                    files_chars.Add(Path.Combine(AppInfo.SpecialCharDirectory, ListSpecialChars.Items[i].ToString() + ".txt"));
                }
            }

            //Corresponding_files in MemoryStream speichern

            string correspond = "";

            if (files_vhf.Count > 0)
            {
                for (int i = 0; i < files_vhf.Count; i++)
                {
                    string[] vhf_vhr = new string[2];
                    vhf_vhr[0] = files_vhf[i][0];
                    vhf_vhr[1] = files_vhf[i][1];

                    //Datei-Pfade durch lokale variablen ersetzen

                    vhf_vhr[0] = vhf_vhr[0].Replace(Settings.Default.VhfPath, "%vhf%");
                    vhf_vhr[0] = vhf_vhr[0].Replace(Settings.Default.VhrPath, "%vhr%");
                    vhf_vhr[0] = vhf_vhr[0].Replace(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "%personal%");
                    vhf_vhr[0] = vhf_vhr[0].Replace(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "%desktop%");
                    vhf_vhr[0] = vhf_vhr[0].Replace(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "%program%");
                    vhf_vhr[0] = vhf_vhr[0].Replace(Environment.GetFolderPath(Environment.SpecialFolder.System), "%system%");

                    correspond = correspond + i.ToString() + "|" + vhf_vhr[0] + "|" + vhf_vhr[1];

                    if (i < files_vhf.Count - 1)
                    {
                        correspond = correspond + Environment.NewLine;
                    }
                }
            }

            //Chars.log erstellen
            string chars = string.Join(Environment.NewLine, files_chars.Select(x => Path.GetFileName(x)));

            //vhr.log erstellen
            string vhr = string.Join(Environment.NewLine, files_vhr.Select(x => Path.GetFileName(x)));

            //MemoryStreams erstellen
            MemoryStream correspond_memory_stream = new MemoryStream(Encoding.UTF8.GetBytes(correspond));
            MemoryStream chars_memory_stream = new MemoryStream(Encoding.UTF8.GetBytes(chars));
            MemoryStream vhr_memory_stream = new MemoryStream(Encoding.UTF8.GetBytes(vhr));

            //Dateien in den Backup-Zip-Folder speichern
            FileStream zip_file = new FileStream(pfad, FileMode.Create, FileAccess.Write);
            ZipOutputStream zip_stream = new ZipOutputStream(zip_file);

            zip_stream.SetLevel(8);

            byte[] buffer = new byte[4096];

            try
            {
                //Corresponing_files-Datei erstellen

                ZipEntry correspond_entry = new ZipEntry("vhf_vhr.log");

                correspond_entry.CompressionMethod = CompressionMethod.Deflated;
                correspond_entry.DateTime = DateTime.Now;
                correspond_entry.Size = correspond_memory_stream.Length;

                zip_stream.PutNextEntry(correspond_entry);

                StreamUtils.Copy(correspond_memory_stream, zip_stream, buffer);

                correspond_memory_stream.Close();


                //Vhr-Datei erstellen

                ZipEntry vhr_entry = new ZipEntry("vhr.log")
                {
                    CompressionMethod = CompressionMethod.Deflated,
                    DateTime = DateTime.Now,
                    Size = vhr_memory_stream.Length
                };

                zip_stream.PutNextEntry(vhr_entry);

                StreamUtils.Copy(vhr_memory_stream, zip_stream, buffer);

                vhr_memory_stream.Close();


                //chars-Datei erstellen

                ZipEntry chars_entry = new ZipEntry("chars.log")
                {
                    CompressionMethod = CompressionMethod.Deflated,
                    DateTime = DateTime.Now,
                    Size = chars_memory_stream.Length
                };

                zip_stream.PutNextEntry(chars_entry);

                StreamUtils.Copy(chars_memory_stream, zip_stream, buffer);

                chars_memory_stream.Close();


                //Vokabelhefte speichern

                for (int i = 0; i < files_vhf.Count; i++)
                {
                    try
                    {
                        string file_name = files_vhf[i][0];
                        FileInfo info = new FileInfo(file_name);

                        if (info.Exists == true)
                        {
                            ZipEntry entry = new ZipEntry(@"vhf\" + i + ".vhf")
                            {
                                CompressionMethod = CompressionMethod.Deflated,
                                DateTime = File.GetLastWriteTime(file_name),
                                Size = info.Length
                            };

                            zip_stream.PutNextEntry(entry);

                            using (FileStream stream = new FileStream(file_name, FileMode.Open))
                            {
                                StreamUtils.Copy(stream, zip_stream, buffer);
                            }
                        }
                    }
                    catch //Vokabelheft konnte nicht nicht kopiert werden
                    {
                        error_vhf++;

                        FileInfo info = new FileInfo(files_vhf[i][0]);

                        error_vhf_name.Add(info.Name);
                    }
                }

                //Ergebnisse abspeichern

                for (int i = 0; i < files_vhr.Count; i++)
                {
                    try
                    {
                        string file_name = files_vhr[i].ToString();
                        FileInfo info = new FileInfo(file_name);

                        if (info.Exists == true)
                        {
                            ZipEntry entry = new ZipEntry(@"vhr\" + info.Name);

                            entry.CompressionMethod = CompressionMethod.Deflated;

                            entry.DateTime = File.GetLastWriteTime(file_name);
                            entry.Size = info.Length;

                            zip_stream.PutNextEntry(entry);

                            using (FileStream stream = new FileStream(file_name, FileMode.Open))
                            {
                                StreamUtils.Copy(stream, zip_stream, buffer);
                            }
                        }
                    }
                    catch //Ergebnisse konnten nicht kopiert werden
                    {
                        error_vhr++;
                    }
                }

                //Sonderzeichentabellen sichern
                foreach (string file_name in files_chars)
                {
                    FileInfo info = new FileInfo(file_name);

                    try
                    {
                        if (info.Exists)
                        {
                            zip_stream.PutNextEntry(new ZipEntry(Path.Combine("chars", info.Name))
                            {
                                CompressionMethod = CompressionMethod.Deflated,
                                DateTime = info.LastWriteTime,
                                Size = info.Length
                            });

                            using (FileStream stream = new FileStream(file_name, FileMode.Open))
                            {
                                StreamUtils.Copy(stream, zip_stream, buffer);
                            }
                        }
                    }
                    catch //Ergebnisse konnten nicht nicht kopiert werden
                    {
                        error_chars++;
                        error_chars_name.Add(info.Name);
                    }
                }

                zip_stream.Close();
                zip_file.Close();
            }
            catch
            {
                zip_stream.Close();
                zip_file.Close();

                error = true;

                //Cursor auf normal setzen

                Cursor.Current = Cursors.Default;

                FileInfo info = new FileInfo(pfad);
                info.Delete();

                if (file_exists == true)
                {
                    FileInfo temp_file = new FileInfo(temp_pfad);

                    temp_file.MoveTo(pfad);
                }

                //Fehlermeldung anzeigen, falls ein allgemeiner Fehler aufgetaucht ist

                MessageBox.Show(Properties.language.messagebox_backup_error,
                                 Properties.language.error,
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Error);
            }
            zip_stream.Close();
            zip_file.Close();

            //Cursor auf normal setzen

            Cursor.Current = Cursors.Default;


            //Fehlermeldungen anzeigen, falls gewisse Dateien nicht gesichert werden konnten

            if (error_vhf > 0)
            {
                string message = Properties.language.messagebox_backup_error_vhf + Environment.NewLine;

                for (int i = 0; i < error_vhf; i++)
                {
                    message += Environment.NewLine + error_vhf_name[i];
                }

                MessageBox.Show(message,
                                Properties.language.error,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);

            }
            if (error_vhr > 0)
            {
                string message = error_vhr.ToString() + " " + Properties.language.messagebox_backup_error_vhr;


                MessageBox.Show(message,
                                Properties.language.error,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
            if (error_chars > 0)
            {
                string message = Properties.language.messagebox_backup_error_chars + Environment.NewLine;

                for (int i = 0; i < error_chars; i++)
                {
                    message += Environment.NewLine + error_chars_name[i];
                }

                MessageBox.Show(message,
                                Properties.language.error,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }

            //Meldung anzeigen, dass der Prozess erfolgreich war

            if (error == false && error_vhf == 0 && error_vhr == 0 && error_chars == 0)
            {
                MessageBox.Show(Properties.language.messagebox_backup_success,
                                   AppInfo.Name,
                                   MessageBoxButtons.OK,
                                   MessageBoxIcon.Information);
            }
        }
    }
}