using System;
using System.Linq;
using System.Text;

namespace SimpleAMQP
{
    internal static class StringEx
    {
        public static byte[] ToLongStringBytes(this string input)
        {
            var bytes = Encoding.UTF8.GetBytes(input).ToList();

            var byteCount = bytes.LongCount();

            if (byteCount > Int32.MaxValue)
                throw new ArgumentException();

            var stringLength = BitConverter.GetBytes(byteCount).ToList();

            stringLength.AddRange(bytes);

            return stringLength.ToArray();
        }

        public static byte[] ToShortStringBytes(this string input)
        {
            var bytes = Encoding.UTF8.GetBytes(input).ToList();

            var byteCount = bytes.Count();

            var stringLength = BitConverter.GetBytes(byteCount).ToList();

            stringLength.AddRange(bytes);

            return stringLength.ToArray();
        }
    }
}
