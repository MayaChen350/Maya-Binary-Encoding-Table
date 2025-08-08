using System.Globalization;
using System.Reflection;
using System.Text;

namespace MayaBinTable.Common;

public static unsafe class MayaTable
{
    public const string MAGIC_NUMBER = "\x4D\x41\x59\x41\x20\x3A\x33";

    /// <summary>
    /// Where all the entries are from.
    /// </summary>
    public static readonly Stream EntryStream =
        Assembly.GetExecutingAssembly().GetManifestResourceStream("MayaBinTable.Common.ENTRIES.txt")!;

    /// <summary>
    /// Where the offset for the start of each entry are from.
    ///
    /// This is a list of ushort.
    /// </summary>
    public static readonly Stream OffsetStream =
        Assembly.GetExecutingAssembly().GetManifestResourceStream("MayaBinTable.Common.OFFSETS.bin")!;

    /// <summary>
    /// Get the offset in the OffsetStream from the specified index 
    /// </summary>
    /// <param name="index">The MayaByte/index value</param>
    public static ushort GetOffset(int index)
    {
        // The file contains offsets as ushort values
        OffsetStream.Position = index * sizeof(ushort);

        Span<byte> result = stackalloc byte[sizeof(ushort)];
        OffsetStream.ReadExactly(result);
        result.Reverse();
        return BitConverter.ToUInt16(result);
    }

    /// <summary>
    /// Get an entry in the EntryStream from the specified offset
    /// </summary>
    /// <param name="offset">Usually from the OffsetStream</param>
    public static string GetEntry(ushort offset)
    {
        EntryStream.Position = offset;

        Span<byte> currChar = stackalloc byte[1];
        sbyte[] byteArrays = new sbyte[35]; // Buffer the rest will probably get garbage collected
        int size = 0;

        EntryStream.ReadExactly(currChar);
        do
        {
            byteArrays[size] = (sbyte)currChar[0];
            size++;
            EntryStream.ReadExactly(currChar);
        } while (currChar[0] != '\0');


        fixed (sbyte* bytePtr = byteArrays)
        {
            return new string(bytePtr);
        }
    }

    /// <summary>
    /// Make a new indexed maya table
    /// </summary>
    public static string[] GetCompleteEntryTable()
    {
        long length = OffsetStream.Length / sizeof(ushort);
        var table = new string[length];
        table[0] = "\0";

        for (int i = 1 /* 0 is NULL*/; i < length; i++)
        {
            table[i] = GetEntry(GetOffset(i));
        }

        return table;
    }
}