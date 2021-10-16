using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleAMQP.Ex;

namespace SimpleAMQP
{
    internal static class StringEx
    {
        public static byte[] ToLongStringBytes(this string input)
        {
            var bytes = Encoding.UTF8.GetBytes(input).ToList();

            var byteCount = bytes.Count();

            if (byteCount > Int32.MaxValue)
                throw new ArgumentException();

            var stringLength = byteCount.ToBytes().ToList();

            stringLength.AddRange(bytes);

            return stringLength.ToArray();
        }

        public static byte[] ToShortStringBytes(this string input)
        {
            var bytes = Encoding.UTF8.GetBytes(input).ToList();

            var byteCount = (byte) bytes.Count();

            var stringLength = new List<byte> { byteCount };

            stringLength.AddRange(bytes);

            return stringLength.ToArray();
        }
    }
}
