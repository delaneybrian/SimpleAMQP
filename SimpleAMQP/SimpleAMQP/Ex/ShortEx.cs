using System;
using System.Linq;

namespace SimpleAMQP.Ex
{
    internal static class ShortEx
    {
        public static byte[] ToBytes(this short value)
        {
            var shortBytes = BitConverter.GetBytes(value);

            if (BitConverter.IsLittleEndian)
                shortBytes.Reverse();

            return shortBytes;
        } 
    }
}
