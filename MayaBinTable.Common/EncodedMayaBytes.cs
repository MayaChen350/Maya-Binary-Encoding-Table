namespace MayaBinTable.Common;

public unsafe struct EncodedMayaBytes
{
    private bool _hasTwoBytes;

    private fixed byte _bytes[2];

    public byte? FirstByte
    {
        get => _hasTwoBytes ? _bytes[0] : null;
        private set => _bytes[0] = value!.Value;
    }

    public byte LastByte
    {
        get => _bytes[1];
        private set => _bytes[1] = value;
    }

    public ushort Combined
    {
        get
        {
            fixed (byte* p = &_bytes[0])
            {
                return BitConverter.IsLittleEndian 
                    ? *(ushort*)(p + 1)
                    : *(ushort*)p;
            }
        }
    }

    public byte[] AsArray()
    {
        byte[] bytes;

        if (_hasTwoBytes)
        {
            bytes = new byte[2];
            bytes[0] = _bytes[0];
            bytes[1] = _bytes[1];
        }
        else
        {
            bytes = new byte[1];
            bytes[0] = _bytes[1];
        }
        return bytes;
    }

    public void SetBytes(ushort value)
    {
        if (value >= 128)
        {
            _hasTwoBytes = true;
            FirstByte = (byte)(0b10000000 | ((value & 0xFF00) >> 8));
            LastByte = (byte)(value & 0x00FF);
        }
        else
        {
            _hasTwoBytes = false;
            LastByte = (byte)value;
        }
    }
}