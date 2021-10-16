using System;
using System.Collections.Generic;
using System.Linq;
using SimpleAMQP.Ex;

namespace SimpleAMQP
{
    internal class FieldTable
    {
        public Dictionary<string, FieldValue> PeerProperties { get; private set; }

        public FieldTable()
        {
            PeerProperties = new Dictionary<string, FieldValue>();
        }

        public void Add(string key, FieldValue value)
        {
            PeerProperties.Add(key, value);
        }
      
        public static Span<byte> Extract(Span<byte> bytes, out FieldTable fieldTable)
        {
            fieldTable = new FieldTable();
            
            bytes = bytes.ExtractLongUInt(out var fieldTableLength);

            var fieldTableBytes = bytes.Slice(0, (int) fieldTableLength);

            while (fieldTableBytes.Length > 0)
            {
                fieldTableBytes = fieldTableBytes.ExtractShortString(out var key);

                fieldTableBytes = FieldValue.Extract(fieldTableBytes, out var fieldValue);

                fieldTable.Add(key, fieldValue);
            }

            return bytes.Slice((int) fieldTableLength);
        }

        public byte[] Marshall()
        {
            var bytes = new List<byte>();

            foreach (var property in PeerProperties)
            {
                var fieldName = property.Key;

                var fieldValue = property.Value;

                var fieldNameBytes = fieldName.ToShortStringBytes();
                bytes.AddRange(fieldNameBytes);

                var fieldValueBytes = fieldValue.ToBytes();
                bytes.AddRange(fieldValueBytes);
            }

            var numberOfBytes = (uint) bytes.Count;

            var numberOfBytesBytes = numberOfBytes.ToBytes();

            var finalBytes = new List<byte>();

            finalBytes.AddRange(numberOfBytesBytes);
            finalBytes.AddRange(bytes);

            return finalBytes.ToArray();
        }
    }
}
