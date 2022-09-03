﻿using System;
using Vocup.IO.Internal;
using Vocup.Models;

namespace Vocup.IO;

/// <summary>
/// Provides static methods for reading und writing vocabulary files.
/// </summary>
public static class VocabularyFile
{
    private static readonly VhfFile vhfFile = new VhfFile();
    private static readonly VhrFile vhrFile = new VhrFile();
    private static readonly CsvFile csvFile = new CsvFile();

    public static bool ReadVhfFile(string path, VocabularyBook book)
    {
        if (path == null)
            throw new ArgumentNullException(nameof(path));
        if (book == null)
            throw new ArgumentNullException(nameof(book));

        return vhfFile.Read(path, book);
    }

    public static bool WriteVhfFile(string path, VocabularyBook book)
    {
        if (path == null)
            throw new ArgumentNullException(nameof(path));
        if (book == null)
            throw new ArgumentNullException(nameof(book));

        return vhfFile.Write(path, book);
    }

    public static bool ReadVhrFile(VocabularyBook book)
    {
        if (book == null)
            throw new ArgumentNullException(nameof(book));

        return vhrFile.Read(book);
    }

    public static bool WriteVhrFile(VocabularyBook book)
    {
        if (book == null)
            throw new ArgumentNullException(nameof(book));

        return vhrFile.Write(book);
    }

    public static bool ImportCsvFile(string path, VocabularyBook book, bool importSettings)
    {
        if (path == null)
            throw new ArgumentNullException(nameof(path));
        if (book == null)
            throw new ArgumentNullException(nameof(book));

        return csvFile.Import(path, book, importSettings);
    }

    public static bool ExportCsvFile(string path, VocabularyBook book)
    {
        if (path == null)
            throw new ArgumentNullException(nameof(path));
        if (book == null)
            throw new ArgumentNullException(nameof(book));

        return csvFile.Export(path, book);
    }
}
