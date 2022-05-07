using ReactiveUI;
using System;

namespace Vocup.Models
{
    public class Practice : ReactiveObject
    {
        private DateTimeOffset date;
        private PracticeResult result;

        public DateTimeOffset Date
        {
            get => date;
            set => this.RaiseAndSetIfChanged(ref date, value);
        }
        public PracticeResult Result
        {
            get => result;
            set => this.RaiseAndSetIfChanged(ref result, value);
        }
    }
}
