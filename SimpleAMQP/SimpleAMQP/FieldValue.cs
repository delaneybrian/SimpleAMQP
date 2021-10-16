using System;
using System.Collections.Generic;
using System.Text;
using SimpleAMQP.Ex;

namespace SimpleAMQP
{
    internal class FieldValue
    {
        private const char _booleanIdentifier = 't';
        private const char _shortStringIdentifier = 's';
        private const char _longStringIdentifier = 'S';

        public object Value { get; private set; }

        public FieldValue(object value)
        {
            Value = value;
        }

        public static Span<byte> Extract(Span<byte> bytes, out FieldValue fieldValue)
        {
            var elementType = Convert.ToChar(bytes[0]);

            bytes = bytes.Slice(1);

            switch (elementType)
            {
                //Boolean
                case (_booleanIdentifier):
                    bytes = bytes.ExtractBoolean(out var boolean);
                    fieldValue = new FieldValue(boolean);
                    break;
                //Short Short Int
                case ('b'):
                    bytes = bytes.ExtractShortShortInt(out var shortShortInt);
                    fieldValue = new FieldValue(shortShortInt);
                    break;
                //case ('B'):
                //    break;
                case ('U'):
                    bytes = bytes.ExtractShortInt(out var shortInt);
                    fieldValue = new FieldValue(shortInt);
                    break;
                //Short UInt
                case ('u'):
                    bytes = bytes.ExtractShortUInt(out var shortUInt);
                    fieldValue = new FieldValue(shortUInt);
                    break;
                //Long Int
                case ('I'):
                    bytes = bytes.ExtractLongInt(out var longInt);
                    fieldValue = new FieldValue(longInt);
                    break;
                //Long UInt
                case ('i'):
                    bytes = bytes.ExtractLongUInt(out var longUInt);
                    fieldValue = new FieldValue(longUInt);
                    break;
                //Long Long int
                case ('L'):
                    bytes = bytes.ExtractLongLongInt(out var longLongInt);
                    fieldValue = new FieldValue(longLongInt);
                    break;
                //Long Long UInt
                case ('l'):
                    bytes = bytes.ExtractLongLongUInt(out var longLongUIInt);
                    fieldValue = new FieldValue(longLongUIInt);
                    break;
                //Float
                case ('f'):
                    bytes = bytes.ExtractFloat(out var _float);
                    fieldValue = new FieldValue(_float);
                    break;
                //Double
                case ('d'):
                    bytes = bytes.ExtractDouble(out var _double);
                    fieldValue = new FieldValue(_double);
                    break;
                //Decimal
                case ('D'):
                    bytes = bytes.ExtractDecimal(out var _decimal);
                    fieldValue = new FieldValue(_decimal);
                    break;
                //Short String
                case ('s'):
                    bytes = bytes.ExtractShortString(out var shortString);
                    fieldValue = new FieldValue(shortString);
                    break;
                //Long String
                case ('S'):
                    bytes = bytes.ExtractLongString(out var longString);
                    fieldValue = new FieldValue(longString);
                    break;
                //Field Array
                case ('A'):
                    bytes = FieldArray.Extract(bytes,out var fieldArray);
                    fieldValue = new FieldValue(fieldArray.Array);
                    break;
                //Timestamp
                case ('T'):
                    bytes = bytes.ExtractTimeStamp(out var timestamp);
                    fieldValue = new FieldValue(timestamp);
                    break;
                //Field Table
                case ('F'):
                    bytes = FieldTable.Extract(bytes, out var innerFieldTable);
                    fieldValue = new FieldValue(innerFieldTable.PeerProperties);
                    break;
                default:
                    throw new ArgumentException();
            }

            return bytes;
        }

        public byte[] ToBytes()
        {
            var bytes = new List<byte>();

            var type = Value.GetType();

            if (type == typeof(string))
            {
                var longStringByte = (byte) _longStringIdentifier;
                bytes.Add(longStringByte);

                var valueString = (string)Value;

                var stringBytes = valueString.ToLongStringBytes();
                bytes.AddRange(stringBytes);
            }
            else if (type == typeof(bool))
            {

            }
            else
            {
                throw new NotImplementedException();
            }

            return bytes.ToArray();
        }
    }
}
