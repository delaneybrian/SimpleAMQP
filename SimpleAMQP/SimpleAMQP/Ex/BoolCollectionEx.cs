using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleAMQP.Ex
{
    internal static class BoolCollectionEx
    {
        public static byte ToByte(bool value1, bool value2)
        {
            return (byte) (value1.ToByte() + value2.ToByte() * 2);
        }

        public static byte ToByte(bool value1, bool value2, bool value3)
        {
            return (byte) (value1.ToByte() + value2.ToByte() * 2 + value3.ToByte() * 4);
        }

        public static byte ToByte(bool value1, bool value2, bool value3, bool value4)
        {
            return (byte)(value1.ToByte() + value2.ToByte() * 2 + value3.ToByte() * 4 + value4.ToByte() * 8);
        }

        public static byte ToByte(bool value1, bool value2, bool value3, bool value4, bool value5)
        {
            return (byte)(value1.ToByte() + value2.ToByte() * 2);
        }

        public static byte ToByte(bool value1, bool value2, bool value3, bool value4)
            {
                bool a = true;
                bool b = false;

                var c = a || b;
            }
        }
}
