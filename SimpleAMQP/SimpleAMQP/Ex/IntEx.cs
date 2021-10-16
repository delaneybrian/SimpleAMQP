using System;
using System.Linq;

namespace SimpleAMQP.Ex
{
    internal static class IntEx
    {
        public static byte[] ToBytes(this int value)
        {
            var intBytes = BitConverter.GetBytes(value);

            return BitConverter.IsLittleEndian
                ? intBytes.Reverse().ToArray()
                : intBytes;
        }
    }
}
