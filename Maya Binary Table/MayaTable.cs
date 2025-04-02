using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MayaBinaryTable
{
	public class MayaTable
	{
		private const int I_TABLE_INDEX = 0;
		private const int I_TABLE_STRING = 1;

		private string[][] mayaTable;

		//public string[][] Table { get => mayaTable; }

		public MayaTable()
		{
			const string resourceName = "MayaBinaryTable.table.csv";

			mayaTable = new string[255][];
			for (int i = 0; i < mayaTable.Length; i++)
			{
				mayaTable[i] = new string[2];
			}

			using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
			using (StreamReader reader = new StreamReader(stream))
			{
				int index = 0;

				while (!reader.EndOfStream)
				{
					string[] splittedLine = reader.ReadLine().Split(',');
					mayaTable[index][I_TABLE_INDEX] = splittedLine[I_TABLE_INDEX];
					if (index == 53 /*Offset is -1*/)
						mayaTable[index][I_TABLE_STRING] = "\n";
					else
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
					if (mayaTable[i][I_TABLE_STRING] != null)
						table[i] = mayaTable[i][I_TABLE_STRING];
					else Array.Resize(ref table, table.Length - 1);

				return table;
			}
		}

		public EncodedMayaBytes GetBytesFromExactString(string str)
		{
			EncodedMayaBytes bytes = new EncodedMayaBytes();
			short foundElementIndex;

			try
			{
				foundElementIndex = short.Parse(mayaTable.First(elem => elem[I_TABLE_STRING] == str)[I_TABLE_INDEX]);
			}
			catch (Exception e)
			{
				foundElementIndex = 0;
			}


			bytes.SetBytes(foundElementIndex);

			return bytes;
		}
	}
}