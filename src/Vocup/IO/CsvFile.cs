using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Vocup.Models;
using Vocup.Properties;

namespace Vocup.IO.Internal;

internal class CsvFile
{
    public bool Import(string path, VocabularyBook book, bool importSettings)
    {
        try
        {
            using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                CsvConfiguration config = new(CultureInfo.CurrentCulture);

                Encoding fallbackEncoding = DetectEncoding(fileStream, 16384);
                fileStream.Seek(0, SeekOrigin.Begin);

                using (StreamReader streamReader = new(fileStream, fallbackEncoding, detectEncodingFromByteOrderMarks: true, 256, leaveOpen: true))
                {
                    char delimiter = DetectDelimiter(streamReader, 10, new[] { ',', ';', '\t', '|' });
                    if (delimiter != 0) config.Delimiter = delimiter.ToString();
                }

                // Reset to start of file and create a new StreamReader to detect byte order marks again
                fileStream.Seek(0, SeekOrigin.Begin);

                using (StreamReader streamReader = new(fileStream, fallbackEncoding, detectEncodingFromByteOrderMarks: true))
                using (CsvReader reader = new(streamReader, config))
                {
                    reader.Context.RegisterClassMap(new EntryMap());

                    if (!reader.Read() || !reader.ReadHeader())
                    {
                        MessageBox.Show(Messages.CsvInvalidHeader, Messages.CsvInvalidHeaderT, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    if (reader.HeaderRecord?.Length != 2)
                    {
                        MessageBox.Show(string.Format(Messages.CsvInvalidHeaderColumns, reader.HeaderRecord?.Length),
                            Messages.CsvInvalidHeaderT, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    if (importSettings)
                    {
                        book.MotherTongue = reader.HeaderRecord[0];
                        book.ForeignLang = reader.HeaderRecord[1];
                    }
                    else
                    {
                        if (!book.MotherTongue.Equals(reader.HeaderRecord[0], StringComparison.OrdinalIgnoreCase)
                            || !book.ForeignLang.Equals(reader.HeaderRecord[1], StringComparison.OrdinalIgnoreCase))
                        {
                            DialogResult dialogResult = MessageBox.Show(
                                string.Format(Messages.CsvInvalidLanguages, reader.HeaderRecord[0], reader.HeaderRecord[1]),
                                Messages.CsvInvalidHeaderT, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                            if (dialogResult == DialogResult.No)
                                return false;
                        }
                    }

                    foreach (Entry entry in reader.GetRecords<Entry>())
                    {
                        if (!book.Words.Any(x => x.MotherTongue == entry.MotherTongue && x.ForeignLangText == entry.ForeignLang))
                        {
                            int idx = entry.ForeignLang.LastIndexOf('=');
                            if (idx == -1)
                            {
                                book.Words.Add(new VocabularyWord(entry.MotherTongue, entry.ForeignLang)
                                {
                                    Owner = book,
                                });
                            }
                            else
                            {
                                book.Words.Add(new VocabularyWord(entry.MotherTongue, entry.ForeignLang.Remove(idx))
                                {
                                    ForeignLangSynonym = entry.ForeignLang.Substring(idx + 1),
                                    Owner = book,
                                });
                            }
                        }
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
            using (StreamWriter file = new(path, false, Encoding.UTF8))
            using (CsvWriter writer = new(file, CultureInfo.CurrentCulture))
            {
                writer.Context.RegisterClassMap(new EntryMap(book.MotherTongue, book.ForeignLang));
                writer.WriteRecords(book.Words.Select(x => new Entry(x.MotherTongue, x.ForeignLangText)));
            }

            return true;
        }
        catch (Exception ex)
        {
            MessageBox.Show(string.Format(Messages.CsvExportError, ex), Messages.UnexpectedErrorT, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        return false;
    }

    private static Encoding DetectEncoding(Stream stream, int testByteCount)
    {
        Encoding strictUtf8 = Encoding.GetEncoding("utf-8", new EncoderExceptionFallback(), new DecoderExceptionFallback());
        Span<byte> byteBuffer = stackalloc byte[4096];
        Span<char> charBuffer = stackalloc char[4096];

        int bytesRead = 0;
        while (bytesRead < testByteCount)
        {
            int readFromStream = stream.Read(byteBuffer);
            bytesRead += readFromStream;
            try
            {
                strictUtf8.GetChars(byteBuffer[..readFromStream], charBuffer);
            }
            catch (DecoderFallbackException)
            {
                return Encoding.Latin1;
            }
            if (readFromStream < 4096) break;
        }

        return Encoding.UTF8;
    }

    private static char DetectDelimiter(TextReader reader, int rowCount, IList<char> separators)
    {
        // Taken from https://stackoverflow.com/questions/33341307/csvhelper-how-to-detect-the-delimiter-from-the-given-csv-file

        IList<int> separatorsCount = new int[separators.Count];

        int character;
        int row = 0;

        bool quoted = false;
        bool firstChar = true;

        while (row < rowCount)
        {
            character = reader.Read();

            switch (character)
            {
                case '"':
                    if (quoted)
                    {
                        if (reader.Peek() != '"') // Value is quoted and current character is " and next character is not ".
                            quoted = false;
                        else
                            reader.Read(); // Value is quoted and current and next characters are "" - read (skip) peeked qoute.
                    }
                    else
                    {
                        if (firstChar)  // Set value as quoted only if this quote is the first char in the value.
                            quoted = true;
                    }
                    break;
                case '\n':
                    if (!quoted)
                    {
                        ++row;
                        firstChar = true;
                        continue;
                    }
                    break;
                case -1:
                    row = rowCount;
                    break;
                default:
                    if (!quoted)
                    {
                        int index = separators.IndexOf((char)character);
                        if (index != -1)
                        {
                            ++separatorsCount[index];
                            firstChar = true;
                            continue;
                        }
                    }
                    break;
            }

            if (firstChar)
                firstChar = false;
        }

        int maxCount = separatorsCount.Max();

        return maxCount == 0 ? '\0' : separators[separatorsCount.IndexOf(maxCount)];
    }

    private class Entry
    {
        // Parameterless constructor is required for CsvHelper
        public Entry()
        {
            // All mapped properties will be set or exception thrown
            MotherTongue = null!;
            ForeignLang = null!;
        }

        public Entry(string motherTongue, string foreignLang)
        {
            MotherTongue = motherTongue;
            ForeignLang = foreignLang;
        }

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
