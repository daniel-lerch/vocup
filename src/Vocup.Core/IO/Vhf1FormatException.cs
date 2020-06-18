using System;
using System.Collections.Generic;
using System.Text;

namespace Vocup.IO
{
    internal class Vhf1FormatException : FormatException
    {
        public Vhf1FormatException(Vhf1Error errorCode)
        {
            ErrorCode = errorCode;
        }

        public Vhf1Error ErrorCode { get; }
    }
}
