using System;
using System.Linq;

namespace SimpleAMQP.Ex
{
    internal static class IntEx
    {
        public static byte[] ToBytes(this int value)
        {
            var intBytes = BitConverter.GetBytes(value);

            if (BitConverter.IsLittleEndian)
                intBytes.Reverse();

            return intBytes;
        }
    }
}
