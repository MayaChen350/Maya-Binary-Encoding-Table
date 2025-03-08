using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MayaBinaryTable
{
	internal class MayaTable
	{
		private const int I_TABLE_INDEX = 0;
		private const int I_TABLE_STRING = 1;

		private string[][] mayaTable;

		//public string[][] Table { get => mayaTable; }

		public MayaTable()
		{
			const string resourceName = "MayaBinaryTable.table.csv";

			using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
			using (StreamReader reader = new StreamReader(stream))
			{
				int index = 0;
				while (!reader.EndOfStream)
				{
					string[] splittedLine = reader.ReadLine().Split(',');
					mayaTable[index][I_TABLE_INDEX] = splittedLine[I_TABLE_INDEX];
					mayaTable[index][I_TABLE_STRING] = splittedLine[I_TABLE_STRING];
					index++;
				}
			}
		}

		public string[] TableElements
		{
			get
			{
				string[] table = new string[mayaTable.Length];

				for (int i = 0; i < mayaTable.Length; i++)
				{
					table[i] = mayaTable[i][I_TABLE_STRING];
				}

				return table;
			}
		}

		public EncodedMayaBytes GetBytesFromExactString(string str)
		{
			EncodedMayaBytes bytes = new EncodedMayaBytes();

			short foundElementIndex = short.Parse(mayaTable.FirstOrDefault(elem => elem[I_TABLE_STRING].StartsWith(str))[I_TABLE_INDEX]);

			bytes.SetBytes(foundElementIndex);

			return bytes;
		}
	}
}