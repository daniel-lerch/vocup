using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vocup.Avalonia.Controls
{
    public class MenuStripViewModel : ReactiveObject
    {
        public bool CanClose => true;
        public bool CanSave => false;
        public bool CanSaveAs => true;
        public bool CanReveal => false;
        public bool CanAddWord => true;
        public bool CanEditWord => false;
        public bool CanDeleteWord => false;
        public bool CanExport => false;
        public bool CanPractice => false;
        public bool CanConfigure => true;
    }
}
