using System;
using System.Linq;

namespace SimpleAMQP.Ex
{
    internal static class ULongEx
    {
        public static byte[] ToBytes(this ulong value)
        {
            var ulongBytes = BitConverter.GetBytes(value);

            return BitConverter.IsLittleEndian
                ? ulongBytes.Reverse().ToArray()
                : ulongBytes;
        }
    }
}
