using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MayaBinaryTable
{
	internal class Encoder
	{
		private MayaTable mayaTable;

		private string characterStack;

		private bool SymbolFound;

		public Encoder(MayaTable _table)
		{
			mayaTable = _table;
			characterStack = "";
		}

		// TODO
		public EncodedMayaBytes? EncodeChar(char _char)
		{
			EncodedMayaBytes bytes = new EncodedMayaBytes();

			//characterStack.Append(_char);
			//var elementsFound = mayaTable.TableElements.FirstOrDefault(characterStack);
			//var new_ = elementsFound.Where(e);

			//if (!)
			//{

			//}

			return bytes;
		}
	}
}
