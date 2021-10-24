using System;
using System.Linq;

namespace SimpleAMQP.Ex
{
    internal static class LongEx
    {
        public static byte[] ToBytes(this long value)
        {
            var longBytes = BitConverter.GetBytes(value);

            return BitConverter.IsLittleEndian
                ? longBytes.Reverse().ToArray()
                : longBytes;
        }
    }
}
