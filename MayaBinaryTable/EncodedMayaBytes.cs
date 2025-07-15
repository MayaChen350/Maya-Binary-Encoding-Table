// ReSharper disable UseCollectionExpression
// ReSharper disable ShiftExpressionZeroLeftOperand
namespace MayaBinaryTable
{
	public struct EncodedMayaBytes
	{
		public bool HasTwoBytes => FirstByte != null;

		public byte? FirstByte;

		public byte LastByte;

		public short Combined => (short)((FirstByte ?? 0 << 8) | LastByte);

		public byte[] ToArray() => HasTwoBytes ? new[] { FirstByte!.Value, LastByte } : new[] { LastByte };

		public void SetBytes(short value)
		{
			if (value >= 128)
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
