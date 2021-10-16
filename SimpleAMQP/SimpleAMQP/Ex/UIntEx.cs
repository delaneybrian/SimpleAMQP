using System;
using System.Linq;

namespace SimpleAMQP.Ex
{
    internal static class UIntEx
    {
        public static byte[] ToBytes(this uint value)
        {
            var uIntBytes = BitConverter.GetBytes(value);

            return BitConverter.IsLittleEndian
                ? uIntBytes.Reverse().ToArray()
                : uIntBytes;
        }
    }
}
