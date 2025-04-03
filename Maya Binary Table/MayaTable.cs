using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace MayaBinaryTable
{
	public class MayaTable
	{
		// Lazy Singleton
		private static MayaTable mayaTable;

		private const int I_TABLE_INDEX = 0;
		private const int I_TABLE_STRING = 1;

		private static List<(string, string)> mayaRawTable;

		//public string[][] Table { get => mayaRawTable; }

		public static MayaTable Get()
		{
			if (mayaTable == null)
				mayaTable = new MayaTable();

			return mayaTable;
		}

		private MayaTable()
		{
			const string resourceName = "MayaBinaryTable.table.csv";

			mayaRawTable = new List<(string, string)>();

			using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
			using (StreamReader reader = new StreamReader(stream))
			{
				int index = 0;

				while (!reader.EndOfStream)
				{
					string[] splittedLine = reader.ReadLine().Split(',');
					string tableIndex = splittedLine[I_TABLE_INDEX];
					string tableString;
					if (index == 52 /*Offset is -1*/)
						tableString = "\t";
					else if (index == 53)
						tableString = "\n";
					else if (index == 74)
						tableString = ",";
					else
						tableString = splittedLine[I_TABLE_STRING];

					mayaRawTable.Add((tableIndex, tableString));
					index++;
				}
			}
		}

		//public string[] TableElements
		//{
		//	get
		//	{
		//		string[] table = new string[mayaRawTable.Count];

		//		for (int i = 0; i < mayaRawTable.Count; i++)
		//			if (mayaRawTable[i][I_TABLE_STRING] != null)
		//				table[i] = mayaRawTable[i][I_TABLE_STRING];
		//			else Array.Resize(ref table, table.Length - 1);

		//		return table;
		//	}
		//}

		public EncodedMayaBytes GetBytesFromExactString(string str)
		{
			EncodedMayaBytes bytes = new EncodedMayaBytes();
			short foundElementIndex;

			if (HasExactMatch(str))
			{
				foundElementIndex = short.Parse(mayaRawTable.First(elem => elem.Item2 == str).Item1);
			}
			else
			{
				foundElementIndex = 0;
			}

			bytes.SetBytes(foundElementIndex);

			return bytes;
		}

		public static bool HasMatches(string str) => mayaRawTable.Any(elem => elem.Item2.Contains(str));
		public static bool HasExactMatch(string str) => mayaRawTable.Any(elem => elem.Item2 == (str));
	}
}