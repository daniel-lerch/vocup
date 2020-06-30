using System;
using System.Collections.Generic;
using System.Text;

namespace Vocup.IO
{
    public enum VhfError
    {
        InvalidCiphertext,
        InvalidVersion,
        InvalidVhrCode,
        InvalidLanguages,
        InvalidRow,
        CorruptedArchive,
        UpdateRequired,
        EmptyArchive
    }
}
