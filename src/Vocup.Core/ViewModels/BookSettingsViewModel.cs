﻿using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using Vocup.Models;
using Vocup.Util;

namespace Vocup.ViewModels;

public class BookSettingsViewModel : ReactiveObject
{
    private const string invalidChars = "#=:\\/|<>*?\"";

    public BookSettingsViewModel(Book book)
    {
        Book = book;
        MotherTongue = book.MotherTongue;
        ForeignLanguage = book.ForeignLanguage;
        AskForForeignLanguage = book.PracticeMode != PracticeMode.AskForMotherTongue;

        // schedulers have to be specified explicitly: https://github.com/reactiveui/ReactiveUI/issues/3339
        this.WhenAnyValue(x => x.MotherTongue)
            .Select(x => !x.ContainsAny(invalidChars))
            .ToPropertyEx(this, x => x.MotherTongueValid, scheduler: RxApp.MainThreadScheduler);
        this.WhenAnyValue(x => x.ForeignLanguage)
            .Select(x => !x.ContainsAny(invalidChars))
            .ToPropertyEx(this, x => x.ForeignLanguageValid, scheduler: RxApp.MainThreadScheduler);

        SaveCommand = ReactiveCommand.Create(Save,
            this.WhenAnyValue(x => x.MotherTongue, x => x.ForeignLanguage,
                (motherTongue, foreignLanguage) =>
                    !string.IsNullOrWhiteSpace(motherTongue) && !motherTongue.ContainsAny(invalidChars) &&
                    !string.IsNullOrWhiteSpace(foreignLanguage) && !foreignLanguage.ContainsAny(invalidChars)));
    }

    public ReactiveCommand<Unit, Unit> SaveCommand { get; }

    public Book Book { get; }

    [Reactive] public string MotherTongue { get; set; }
    [ObservableAsProperty] public bool MotherTongueValid { get; }

    [Reactive] public string ForeignLanguage { get; set; }
    [ObservableAsProperty] public bool ForeignLanguageValid { get; }

    [Reactive] public bool ResetPracticeResults { get; set; }
    [Reactive] public bool AskForForeignLanguage { get; set; } // Cheap workaround for radio button binding

    private void Save()
    {
        Book.MotherTongue = MotherTongue;
        Book.ForeignLanguage = ForeignLanguage;

        if (ResetPracticeResults)
        {
            foreach (Word word in Book.Words)
            {
                foreach (Synonym synonym in word.MotherTongue)
                {
                    synonym.Practices.Clear();
                }

                foreach (Synonym synonym in word.ForeignLanguage)
                {
                    synonym.Practices.Clear();
                }
            }
        }

        Book.PracticeMode = AskForForeignLanguage ? PracticeMode.AskForForeignLanguage : PracticeMode.AskForMotherTongue;
    }
}