using System;
using System.Collections.Generic;
using System.Text;

namespace Vocup.IO
{
    public class VhfFormatException : FormatException
    {
        public VhfFormatException(VhfError errorCode)
        {
            ErrorCode = errorCode;
        }

        public VhfError ErrorCode { get; }
    }
}
