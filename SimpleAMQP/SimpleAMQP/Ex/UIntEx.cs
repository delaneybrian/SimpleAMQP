using System;
using System.Linq;

namespace SimpleAMQP.Ex
{
    internal static class UIntEx
    {
        public static byte[] ToBytes(this uint value)
        {
            var ulongBytes = BitConverter.GetBytes(value);

            return BitConverter.IsLittleEndian
                ? ulongBytes.Reverse().ToArray()
                : ulongBytes;
        }
    }
}
