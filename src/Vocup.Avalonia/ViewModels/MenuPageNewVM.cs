using ReactiveUI;
using System;
using System.Reactive;
using Vocup.Models;

namespace Vocup.Avalonia.ViewModels
{
    public class MenuPageNewVM : ViewModelBase
    {
        private string motherTongue;
        private string foreignLanguage;

        public MenuPageNewVM(Action<Book> showBook)
        {
            void commit() => showBook(new Book
            {
                MotherTongue = MotherTongue,
                ForeignLanguage = ForeignLanguage
            });

            IObservable<bool> canCommit = this.WhenAnyValue(
                x => x.MotherTongue,
                x => x.ForeignLanguage,
                (p1, p2) => !string.IsNullOrWhiteSpace(p1) && !string.IsNullOrWhiteSpace(p2));

            Commit = ReactiveCommand.Create(commit, canCommit);
        }

        public string MotherTongue
        {
            get => motherTongue;
            set => this.RaiseAndSetIfChanged(ref motherTongue, value);
        }

        public string ForeignLanguage
        {
            get => foreignLanguage;
            set => this.RaiseAndSetIfChanged(ref foreignLanguage, value);
        }

        public ReactiveCommand<Unit, Unit> Commit { get; }
    }
}
