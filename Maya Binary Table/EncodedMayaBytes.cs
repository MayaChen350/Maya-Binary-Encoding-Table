using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MayaBinaryTable
{
	internal struct EncodedMayaBytes
	{
		public bool HasTwoBytes
		{
			get => FirstByte != null;
		}

		public byte? FirstByte;

		public byte LastByte;

		public short Combined { get => (short)((FirstByte << 8) | LastByte); }

		public void SetBytes(short value)
		{
			if (value <= 128)
			{
				FirstByte = (byte)(0b10000000 | ((value & 0xFF00) >> 8));
				LastByte = (byte)(value & 0x00FF);
			}
			else
			{
				FirstByte = null;
				LastByte = (byte)value;
			}
		}
	}
}
