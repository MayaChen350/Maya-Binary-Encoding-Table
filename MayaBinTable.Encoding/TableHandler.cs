using MayaBinTable.Common;

namespace MayaBinTable.Encoding;

public static class TableHandler
{
    public static string[] MayaRawTable;

    public static EncodedMayaBytes GetBytesFromExactString(string str)
    {
        EncodedMayaBytes bytes = new();
        ushort foundElementIndex = 57;

        for (ushort i = 0; i < MayaRawTable.Length; i++)
        {
            if (MayaRawTable[i] == str)
                foundElementIndex = i;
        }

        bytes.SetBytes(foundElementIndex);

        return bytes;
    }

    public static bool HasMatches(string str) => MayaRawTable.Any(elem => elem.StartsWith(str));

    public static bool HasMatchesWithFilter(string str, IEnumerable<string> filter) =>
        MayaRawTable.Any(elem => elem.StartsWith(str) && filter.All(s => s != elem));

    public static bool HasExactMatch(string str) => MayaRawTable.Any(elem => elem == str);

    public static IEnumerable<string> Matches(string str) =>
        Array.FindAll(MayaRawTable, elem => elem.StartsWith(str)).Select(elem => elem);
}