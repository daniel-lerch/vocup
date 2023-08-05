using DynamicData;
using System;
using System.Collections.Generic;
using System.Linq;
using Vocup.Models;
using Vocup.Settings;

namespace Vocup.IO.Vhf2;

partial class Vhf2Format
{
    private record JsonBook(string MotherTongue, string ForeignLanguage, PracticeMode PracticeMode, List<JsonWord> Words)
    {
        public static JsonBook FromBook(Book book)
        {
            return new JsonBook(
                book.MotherTongue,
                book.ForeignLanguage,
                book.PracticeMode,
                book.Words.Select(w => JsonWord.FromWord(w)).ToList());
        }

        public Book ToBook(IVocupSettings settings)
        {
            Book book = new(
                MotherTongue,
                ForeignLanguage,
                PracticeMode);
            book.Words.AddRange(Words.Select(w => w.ToWord(book, settings)));
            return book;
        }
    }
    private record JsonWord(List<JsonSynonym> MotherTongue, List<JsonSynonym> ForeignLanguage, DateTimeOffset CreationDate)
    {
        public static JsonWord FromWord(Word word)
        {
            return new JsonWord(
                word.MotherTongue.Select(s => JsonSynonym.FromSynonym(s)).ToList(),
                word.ForeignLanguage.Select(s => JsonSynonym.FromSynonym(s)).ToList(),
                word.CreationDate);
        }

        public Word ToWord(Book book, IVocupSettings settings)
        {
            return new Word(
                MotherTongue.Select(s => s.ToSynonym(settings)),
                ForeignLanguage.Select(s => s.ToSynonym(settings)),
                book,
                settings)
            {
                CreationDate = CreationDate
            };
        }
    }
    private record JsonSynonym(string Value, List<string> Flags, List<Practice> Practices)
    {
        public static JsonSynonym FromSynonym(Synonym synonym)
        {
            return new JsonSynonym(
                synonym.Value,
                new List<string>(synonym.Flags),
                synonym.Practices.ToList());
        }

        public Synonym ToSynonym(IVocupSettings settings)
        {
            return new Synonym(
                Value, 
                Flags,
                Practices,
                settings);
        }
    }
}
