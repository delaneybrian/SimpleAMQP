using System;
using System.Text;

namespace SimpleAMQP.Ex
{
    internal static class ByteSpanEx
    {
        public static Span<byte> ExtractDecimal(this Span<byte> bytes, out decimal _decimal)
        {
            var decimalBytes = bytes.Slice(1, 5);

            var scale = bytes[0];

            if (BitConverter.IsLittleEndian)
                decimalBytes.Reverse();

            //todo fix this
            _decimal = BitConverter.ToUInt32(decimalBytes);

            return bytes.Slice(5);
        }

        public static Span<byte> ExtractTimeStamp(this Span<byte> bytes, out DateTime timeStampUtc)
        {
            var timestampBytes = bytes.Slice(0, 8);

            if (BitConverter.IsLittleEndian)
                timestampBytes.Reverse();

            var timeStampTicks = BitConverter.ToInt64(timestampBytes.ToArray(), 0);

            timeStampUtc = new DateTime(timeStampTicks);

            return bytes.Slice(8);
        }

        public static Span<byte> ExtractFloat(this Span<byte> bytes, out float _float)
        {
            var floatBytes = bytes.Slice(0, 4);

            if (BitConverter.IsLittleEndian)
                floatBytes.Reverse();

            _float = BitConverter.ToSingle(floatBytes.ToArray(), 0);

            return bytes.Slice(4);
        }

        public static Span<byte> ExtractDouble(this Span<byte> bytes, out double _double)
        {
            var doubleBytes = bytes.Slice(0, 8);

            if (BitConverter.IsLittleEndian)
                doubleBytes.Reverse();

            _double = BitConverter.ToDouble(doubleBytes.ToArray(), 0);

            return bytes.Slice(8);
        }

        public static Span<byte> ExtractBoolean(this Span<byte> bytes, out bool boolean)
        {
            var boolByte = bytes[0];

            if (boolByte == 0)
                boolean = false;
            else
                boolean = true;

            return bytes.Slice(1);
        }

        public static Span<byte> ExtractShortShortInt(this Span<byte> bytes, out byte shortShortInt)
        {
            shortShortInt = bytes[0];

            return bytes.Slice(1);
        }

        public static Span<byte> ExtractShortInt(this Span<byte> bytes, out short shortInt)
        {
            var shortIntBytes = bytes.Slice(0, 2);

            if (BitConverter.IsLittleEndian)
                shortIntBytes.Reverse();

            shortInt = BitConverter.ToInt16(shortIntBytes);

            return bytes.Slice(2);
        }

        public static Span<byte> ExtractShortUInt(this Span<byte> bytes, out ushort shortUInt)
        {
            var shortIntBytes = bytes.Slice(0, 2);

            if (BitConverter.IsLittleEndian)
                shortIntBytes.Reverse();

            shortUInt = BitConverter.ToUInt16(shortIntBytes);

            return bytes.Slice(2);
        }


        public static Span<byte> ExtractLongInt(this Span<byte> bytes, out int item)
        {
            var longIntBytes = bytes.Slice(0, 4);

            if (BitConverter.IsLittleEndian)
                longIntBytes.Reverse();

            item = BitConverter.ToInt32(longIntBytes);

            return bytes.Slice(4);
        }

        public static Span<byte> ExtractLongUInt(this Span<byte> bytes, out uint item)
        {
            var longIntUBytes = bytes.Slice(0, 4);

            if (BitConverter.IsLittleEndian)
                longIntUBytes.Reverse();

            item = BitConverter.ToUInt32(longIntUBytes);

            return bytes.Slice(4);
        }

        public static Span<byte> ExtractLongLongInt(this Span<byte> bytes, out long item)
        {
            var longLongBytes = bytes.Slice(0, 8);

            if (BitConverter.IsLittleEndian)
                longLongBytes.Reverse();

            item = BitConverter.ToInt64(longLongBytes);

            return bytes.Slice(4);
        }

        public static Span<byte> ExtractLongLongUInt(this Span<byte> bytes, out ulong item)
        {
            var longLongUBytes = bytes.Slice(0, 8);

            if (BitConverter.IsLittleEndian)
                longLongUBytes.Reverse();

            item = BitConverter.ToUInt64(longLongUBytes);

            return bytes.Slice(8);
        }

        public static Span<byte> ExtractShortString(this Span<byte> bytes, out string item)
        {
            var shortStringLengthByte = bytes[0];

            item = Encoding.UTF8.GetString(bytes.Slice(1, shortStringLengthByte));

            return bytes.Slice(1 + shortStringLengthByte);
        }

        public static Span<byte> ExtractLongString(this Span<byte> bytes, out string item)
        {
            var longStringLengthBytes = bytes.Slice(0, 4);

            if (BitConverter.IsLittleEndian)
                longStringLengthBytes.Reverse();

            var longStringLength = BitConverter.ToInt32(longStringLengthBytes);

            item = Encoding.UTF8.GetString(bytes.Slice(4, longStringLength));

            return bytes.Slice(4 + longStringLength);
        }
    }
}
