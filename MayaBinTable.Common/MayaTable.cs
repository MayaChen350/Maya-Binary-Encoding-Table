using System.Reflection;

namespace MayaBinTable.Common;

public static class MayaTable
{
    private const int INDEX_TABLE_INDEX = 0;
    private const int INDEX_TABLE_STRING = 1;

    private static readonly List<(string, string)> MayaRawTable = [];

    private const string RESOURCE_NAME = "MayaBinTable.Common.table.csv";

    static MayaTable()
    {
        using Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(RESOURCE_NAME)!;
        using (StreamReader reader = new StreamReader(stream))
        {
            int index = 0;

            while (!reader.EndOfStream)
            {
                string[] splitLine = reader.ReadLine()!.Split(',');
                string tableIndex = splitLine[INDEX_TABLE_INDEX];
                string tableString = index switch
                {
                    /*Offset is -1*/
                    52 => " ",
                    53 => "\n",
                    54 => "\t",
                    74 => ",",
                    _ => splitLine[INDEX_TABLE_STRING]
                };

                MayaRawTable.Add((tableIndex, tableString));
                index++;
            }
        }

        if (MayaRawTable.Count == 0) throw new ApplicationException("The table was empty.");
    }

    public static EncodedMayaBytes GetBytesFromExactString(string str)
    {
        EncodedMayaBytes bytes = new();
        ushort foundElementIndex;

        if (HasExactMatch(str))
        {
            foundElementIndex = ushort.Parse(MayaRawTable.First(elem => elem.Item2 == str).Item1);
        }
        else
        {
            foundElementIndex = 57;
        }

        bytes.SetBytes(foundElementIndex);

        return bytes;
    }

    public static bool HasMatches(string str) => MayaRawTable.Any(elem => elem.Item2.StartsWith(str));

    public static bool HasMatchesWithFilter(string str, IEnumerable<string> filter) =>
        MayaRawTable.Any(elem => elem.Item2.StartsWith(str) && filter.All(s => s != elem.Item2));

    public static bool HasExactMatch(string str) => MayaRawTable.Any(elem => elem.Item2 == (str));

    public static IEnumerable<string> Matches(string str) =>
        MayaRawTable.FindAll(elem => elem.Item2.StartsWith(str)).Select(elem => elem.Item2);
}