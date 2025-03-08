using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MayaBinaryTable
{
	internal class Encoder
	{
		private MayaTable mayaTable;

		private string characterStack;

		private bool symbolFound;

		public Encoder(MayaTable _table)
		{
			mayaTable = _table;
			characterStack = string.Empty;
		}

		public byte[] Encode(StreamReader file)
		{
			List<byte> encodedStream = new List<byte>();
			while (!file.EndOfStream)
			{
				EncodedMayaBytes? bytes = TryEncodeChar((char)file.Read());

				if (bytes != null)
				{
					encodedStream.Add(bytes.Value);
				}
			}

			if (characterStack != string.Empty)
				foreach (char _char in characterStack)
					encodedStream.Add(mayaTable.GetBytesFromExactString(_char.ToString()));

			return encodedStream.ToArray();
		}
		private EncodedMayaBytes? TryEncodeChar(char _char)
		{
			EncodedMayaBytes bytes = new EncodedMayaBytes();
			string charStack = characterStack;

			charStack.Append(_char);
			List<string> elementsFound = mayaTable.TableElements.Where(elem => elem.StartsWith(characterStack)).ToList();

			if (symbolFound && !elementsFound.Any())
			{
				bytes = mayaTable.GetBytesFromExactString(characterStack);
				symbolFound = false;
				characterStack = _char.ToString();
			}

			characterStack.Append(_char);
			symbolFound = elementsFound.Count == 1;
			return null;
		}
	}
}
