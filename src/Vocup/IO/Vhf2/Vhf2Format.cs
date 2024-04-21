using System;
using System.IO;
using System.Threading.Tasks;
using Vocup.Models;

namespace Vocup.IO.Vhf2;

public class Vhf2Format : BookFileFormat
{
    public static Vhf2Format Instance { get; } = new();

    private Vhf2Format() { }

    public async ValueTask Read(Stream stream, VocabularyBook book)
    {
        throw new NotImplementedException();
    }
}
