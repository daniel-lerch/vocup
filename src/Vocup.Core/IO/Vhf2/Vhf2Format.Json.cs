using System;
using System.Collections.Generic;
using System.Linq;
using Vocup.Models;

namespace Vocup.IO.Vhf2;

partial class Vhf2Format
{
    private record JsonBook(string MotherTongue, string ForeignLanguage, PracticeMode PracticeMode, List<JsonWord> Words)
    {
        public Book ToBook()
        {
            return new Book(
                MotherTongue,
                ForeignLanguage,
                PracticeMode,
                Words.Select(w => w.ToWord()));
        }
    }
    private record JsonWord(List<JsonSynonym> MotherTongue, List<JsonSynonym> ForeignLanguage, DateTimeOffset CreationDate)
    {
        public Word ToWord()
        {
            return new Word(
                MotherTongue.Select(s => s.ToSynonym()),
                ForeignLanguage.Select(s => s.ToSynonym()))
            {
                CreationDate = CreationDate
            };
        }
    }
    private record JsonSynonym(string Value, List<string> Flags, List<JsonPractice> Practices)
    {
        public Synonym ToSynonym()
        {
            return new Synonym(
                Value, 
                Flags,
                Practices.Select(p => p.ToPractice()));
        }
    }
    private record JsonPractice(DateTimeOffset Date, PracticeResult2 Result)
    {
        public Practice ToPractice()
        {
            return new Practice { Date = Date, Result = Result };
        }
    }
}
