using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vocup.Avalonia.Controls
{
    public class ToolBarViewModel : ReactiveObject
    {
        public bool CanSave => true;
        public bool CanPrint => false;
    }
}
