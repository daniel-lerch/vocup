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
        private ZipArchive archive;
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

            if (TryOpen(path, out archive) && BackupMeta.TryRead(archive, out meta))
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

        private void Form_Closed(object sender, FormClosedEventArgs e)
        {
            archive?.Dispose();
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
            int restored = 0, skipped = 0, failed = 0;

            void stats(RestoreResult result)
            {
                switch (result)
                {
                    case RestoreResult.Success:
                        restored++;
                        break;
                    case RestoreResult.Skipped:
                        skipped++;
                        break;
                    case RestoreResult.Error:
                        failed++;
                        break;
                }
            }

            for (int i = 0; i < meta.Books.Count; i++)
            {
                if (!ListBooks.GetItemChecked(i)) continue;

                BackupMeta.BookMeta item = meta.Books[i];
                var destination = new FileInfo(BackupMeta.ExpandPath(item.VhfPath));
                RestoreResult result = Restore(archive, "vhf/" + item.FileId, destination, GetOverrideMode());
                stats(result);
                if (result == RestoreResult.Success && RbRestoreAssociatedResults.Checked && !string.IsNullOrWhiteSpace(item.VhrCode))
                {
                    var resultDestination = new FileInfo(Path.Combine(Settings.Default.VhrPath, item.VhrCode));
                    stats(Restore(archive, "vhr/" + item.VhrCode, resultDestination, GetOverrideMode()));
                }
            }

            if (RbRestoreAllResults.Checked)
            {
                for (int i = 0; i < meta.Results.Count; i++)
                {
                    var destination = new FileInfo(Path.Combine(Settings.Default.VhrPath, meta.Results[i]));
                    stats(Restore(archive, "vhr/" + meta.SpecialChars[i], destination, GetOverrideMode()));
                }
            }

            for (int i = 0; i < meta.SpecialChars.Count; i++)
            {
                if (!ListSpecialChars.GetItemChecked(i)) continue;

                var destination = new FileInfo(Path.Combine(AppInfo.SpecialCharDirectory, meta.SpecialChars[i]));
                stats(Restore(archive, "chars/" + meta.SpecialChars[i], destination, GetOverrideMode()));
            }

            // TODO: Show MessageBox with statistics
        }

        private OverrideMode GetOverrideMode()
        {
            if (RbReplaceAll.Checked)
                return OverrideMode.All;
            if (RbReplaceOlder.Checked)
                return OverrideMode.Older;
            if (RbReplaceNothing.Checked)
                return OverrideMode.Never;
            return (OverrideMode)(-1);
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

        private RestoreResult Restore(ZipArchive archive, string path, FileInfo destination, OverrideMode mode)
        {
            try
            {
                ZipArchiveEntry entry = archive.GetEntry(path);
                if (entry == null) return RestoreResult.Error;

                if (destination.Exists &&
                    (mode == OverrideMode.Never || (mode == OverrideMode.Older && destination.LastWriteTime > entry.LastWriteTime)))
                    return RestoreResult.Skipped;

                destination.Directory.Create();
                using (FileStream file = destination.Open(FileMode.Create, FileAccess.Write))
                using (Stream source = entry.Open())
                {
                    source.CopyTo(file);
                }
                return RestoreResult.Success;
            }
            catch
            {
                return RestoreResult.Error;
            }
        }

        private enum OverrideMode
        {
            Never,
            Older,
            All
        }

        private enum RestoreResult
        {
            Success,
            Skipped,
            Error
        }
    }
}