using System;
using System.Collections.Generic;
using SimpleAMQP.Ex;

namespace SimpleAMQP.Methods
{
    internal class FieldArray
    {
        public ICollection<object> Array { get; private set; }

        private FieldArray()
        {
            Array = new List<object>();
        }

        public static Span<byte> Extract(Span<byte> bytes, out FieldArray fieldArray)
        {
            fieldArray = new FieldArray();

            bytes = bytes.ExtractLongInt(out var filedArrayLength);

            var fieldArrayBytes = bytes.Slice(4, filedArrayLength);

            while (fieldArrayBytes.Length > 0)
            {
                fieldArrayBytes = FieldValue.Extract(fieldArrayBytes, out var fieldValue);

                fieldArray.Array.Add(fieldValue.Value);
            }

            return bytes;
        }
    }
}
