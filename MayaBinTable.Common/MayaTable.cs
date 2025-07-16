using System.Reflection;
using System.Reflection.Metadata;

namespace MayaBinTable.Common;

public static unsafe class MayaTable
{
    public const string MAGIC_NUMBER = "\x4D\x41\x59\x41\x20\x3A\x33";

    public static readonly Stream EntryStream =
        Assembly.GetExecutingAssembly().GetManifestResourceStream("MayaBinTable.Common.ENTRIES.txt")!;

    public static readonly Stream OffsetStream =
        Assembly.GetExecutingAssembly().GetManifestResourceStream("MayaBinTable.Common.OFFSETS.bin")!;

    public static ushort GetOffset(int index)
    {
        OffsetStream.Position = index * sizeof(ushort);
        byte[] result = new byte[sizeof(ushort)];
        OffsetStream.ReadExactly(result, 0, sizeof(ushort));

        fixed (byte* p = result)
        {
            return BitConverter.IsLittleEndian
                ? *(ushort*)(p + 1)
                : *(ushort*)p;
        }
    }

    public static string GetEntry(ushort offset)
    {
        EntryStream.Position = offset;
        
        byte[] currChar = new byte[sizeof(char)];       
        EntryStream.ReadExactlyAsync(currChar, 0, sizeof(char));
        List<char> newString = [];
        fixed (byte* ptrCurrChar = currChar)
        {
            do
            {
                EntryStream.ReadExactly(currChar, 0, sizeof(char));
                newString.Add(*(char*)&ptrCurrChar);
            } while (*(char*)&ptrCurrChar != '\0');
        }

        return new string(newString.ToArray());
    }

    public static string[] GetCompleteEntryTable()
    {
        long length = OffsetStream.Length / sizeof(ushort);
        var table = new string[length];
        table[0] = "\0";

        byte[] currChar = new byte[sizeof(char)];
        for (int i = 1 /* 0 is NULL*/; i < length; i++)
        {
            table[i] = GetEntry(GetOffset(i));
        }
        return table;
    }
}