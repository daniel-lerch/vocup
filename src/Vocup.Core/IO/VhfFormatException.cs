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

        public VhfFormatException(VhfError errorCode, Exception innerException) : base(null, innerException)
        {
            ErrorCode = errorCode;
        }

        public VhfError ErrorCode { get; }
    }
}
