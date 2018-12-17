using System;

namespace Vocup.Controls
{
    public class FileSelectedEventArgs : EventArgs
    {
        public FileSelectedEventArgs(string fullName)
        {
            FullName = fullName;
        }

        public string FullName { get; }
    }
}
