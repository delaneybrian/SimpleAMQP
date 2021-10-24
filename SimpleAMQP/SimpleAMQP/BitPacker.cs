using SimpleAMQP.Ex;

namespace SimpleAMQP
{
    internal static class BitPacker
    {
        public static byte Pack(bool value1, bool value2)
        {
            return (byte)(value1.ToByte() + value2.ToByte() * 2);
        }

        public static byte Pack(bool value1, bool value2, bool value3)
        {
            return (byte)(value1.ToByte() + value2.ToByte() * 2 + value3.ToByte() * 4);
        }

        public static byte Pack(bool value1, bool value2, bool value3, bool value4)
        {
            return (byte)(value1.ToByte() + value2.ToByte() * 2 + value3.ToByte() * 4 + value4.ToByte() * 8);
        }

        public static byte Pack(bool value1, bool value2, bool value3, bool value4, bool value5)
        {
            return (byte)(value1.ToByte() + value2.ToByte() * 2 + value3.ToByte() * 4 + value4.ToByte() * 8 + value5.ToByte() * 16);
        }

        public static byte Pack(bool value1, bool value2, bool value3, bool value4, bool value5, bool value6)
        {
            return (byte)(value1.ToByte() + value2.ToByte() * 2 + value3.ToByte() * 4 + value4.ToByte() * 8 + value5.ToByte() * 16 + value6.ToByte() * 32);
        }

        public static byte Pack(bool value1, bool value2, bool value3, bool value4, bool value5, bool value6, bool value7)
        {
            return (byte)(value1.ToByte() + value2.ToByte() * 2 + value3.ToByte() * 4 + value4.ToByte() * 8 + value5.ToByte() * 16 + value6.ToByte() * 32 + value6.ToByte() * 64);
        }

        public static byte Pack(bool value1, bool value2, bool value3, bool value4, bool value5, bool value6, bool value7, bool value8)
        {
            return (byte)(value1.ToByte() + value2.ToByte() * 2 + value3.ToByte() * 4 + value4.ToByte() * 8 + value5.ToByte() * 16 + value6.ToByte() * 32 + value6.ToByte() * 64 + value6.ToByte() * 128);
        }
    }
}
