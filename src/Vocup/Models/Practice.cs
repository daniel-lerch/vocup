using System;

namespace Vocup.Models;

public class Practice
{
    public Practice(DateTimeOffset date, PracticeResult2 result)
    {
        Date = date;
        Result = result;
    }

    public DateTimeOffset Date { get; }
    public PracticeResult2 Result { get; }
}
