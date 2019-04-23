using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Vocup.Models;
using Vocup.Properties;

namespace Vocup.IO.Internal
{
    internal class CsvFile
    {
        public bool Import(string path, VocabularyBook book, bool importSettings)
        {
            try
            {
                Configuration config = new Configuration();
                config.RegisterClassMap(new EntryMap());

                using (StreamReader file = new StreamReader(path, detectEncodingFromByteOrderMarks: true))
                using (CsvReader reader = new CsvReader(file, config))
                {
                    if (!reader.Read() || !reader.ReadHeader())
                    {
                        MessageBox.Show(Messages.CsvInvalidHeader, Messages.CsvInvalidHeaderT, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    if (reader.Context.HeaderRecord.Length != 2)
                    {
                        MessageBox.Show(string.Format(Messages.CsvInvalidHeaderColumns, reader.Context.HeaderRecord.Length),
                            Messages.CsvInvalidHeaderT, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    if (importSettings)
                    {
                        book.MotherTongue = reader.Context.HeaderRecord[0];
                        book.ForeignLang = reader.Context.HeaderRecord[1];
                    }
                    else if (reader.Context.HeaderRecord[0] != book.MotherTongue || reader.Context.HeaderRecord[1] != book.ForeignLang)
                    {
                        DialogResult dialogResult = MessageBox.Show(
                            string.Format(Messages.CsvInvalidLanguages, reader.Context.HeaderRecord[0], reader.Context.HeaderRecord[1]),
                            Messages.CsvInvalidHeaderT, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                        if (dialogResult == DialogResult.No)
                            return false;
                    }

                    foreach (Entry entry in reader.GetRecords<Entry>())
                    {
                        if (!book.Words.Any(x => x.MotherTongue == entry.MotherTongue && x.ForeignLangText == entry.ForeignLang))
                        {
                            book.Words.Add(new VocabularyWord
                            {
                                Owner = book,
                                MotherTongue = entry.MotherTongue,
                                ForeignLangText = entry.ForeignLang
                            });
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(Messages.CsvImportError, ex), Messages.UnexpectedErrorT, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }

        public bool Export(string path, VocabularyBook book)
        {
            try
            {
                Configuration config = new Configuration();
                config.RegisterClassMap(new EntryMap(book.MotherTongue, book.ForeignLang));

                using (TextWriter file = new StreamWriter(path, false, Encoding.UTF8))
                using (CsvWriter writer = new CsvWriter(file, config))
                {
                    writer.WriteRecords(book.Words.Select(x => new Entry() { MotherTongue = x.MotherTongue, ForeignLang = x.ForeignLangText }));
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(Messages.CsvExportError, ex), Messages.UnexpectedErrorT, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }

        private class Entry
        {
            public string MotherTongue { get; set; }
            public string ForeignLang { get; set; }
        }

        private class EntryMap : ClassMap<Entry>
        {
            public EntryMap()
            {
                Map(x => x.MotherTongue).Index(0);
                Map(x => x.ForeignLang).Index(1);
            }

            public EntryMap(string motherTongue, string foreignLang)
            {
                Map(x => x.MotherTongue).Name(motherTongue);
                Map(x => x.ForeignLang).Name(foreignLang);
            }
        }
    }
}
