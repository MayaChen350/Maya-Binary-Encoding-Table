using System.Collections.Generic;

namespace MayaBinaryTable
{
	static class Extensions
	{
		public static void Add(this List<byte> list, EncodedMayaBytes bytes)
		{
			if (bytes.HasTwoBytes)
				list.Add((byte)bytes.FirstByte);

			list.Add(bytes.LastByte);
		}
	}
}
