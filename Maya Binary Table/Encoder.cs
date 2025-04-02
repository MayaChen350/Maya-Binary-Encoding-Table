using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MayaBinaryTable
{
	public class Encoder
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
				TryEncodeChar(ref encodedStream, (char)file.Read());
			}

			if (characterStack != string.Empty)
				foreach (char _char in characterStack)
					encodedStream.Add(mayaTable.GetBytesFromExactString(_char.ToString()));

			return encodedStream.ToArray();
		}

		// TODO: Solve what the heck is this (DO BETTER!!!)
		private void TryEncodeChar(ref List<byte> stream, char _char)
		{
			EncodedMayaBytes bytes = new EncodedMayaBytes();
			string charStack = characterStack;

			charStack += _char;
			List<string> elementsFound = mayaTable.TableElements.Where(elem => elem.StartsWith(charStack)).ToList();

			if (symbolFound && !elementsFound.Any())
			{
				while (!elementsFound.Any())
				{
					charStack.Remove(charStack.Length - 1);
					elementsFound = mayaTable.TableElements.Where(elem => elem.StartsWith(charStack)).ToList();
				}

				List<String> invalidSymbolsFound = new List<string>();
				invalidSymbolsFound.Add(elementsFound.First());

				var pseudoCharFile = characterStack + _char;
				charStack = string.Empty;

				while (pseudoCharFile != string.Empty)
				{
					charStack += pseudoCharFile.First();
					pseudoCharFile = pseudoCharFile.Remove(0);
					elementsFound = mayaTable.TableElements.Where(elem => elem.StartsWith(charStack)).ToList();
				}

				bytes = mayaTable.GetBytesFromExactString(characterStack);
				symbolFound = false;
				characterStack = _char.ToString();
				stream.Add(bytes);
			}

			characterStack += _char;
			symbolFound = elementsFound.Any();
		}
	}
}
