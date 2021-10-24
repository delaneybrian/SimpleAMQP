using System;
using SimpleAMQP.Ex;

namespace SimpleAMQP.Methods.Queue
{
    internal class DeclareOk : IDeMarshallableMethod
    {
        public short ClassId => 50;

        public short MethodId => 11;

        public string Name { get; set; }

        public int MessageCount { get; set; }

        public int ConsumerCount { get; set; }

        public bool IsFor(Span<byte> bytes)
        {
            bytes = bytes.ExtractShortInt(out var classId);

            bytes = bytes.ExtractShortInt(out var methodId);

            return ClassId == classId && MethodId == methodId;
        }

        public void DeMarshall(Span<byte> bytes)
        {
            bytes = bytes.Slice(4);

            bytes = bytes.ExtractShortString(out var name);
            Name = name;

            bytes = bytes.ExtractLongInt(out var messageCount);
            MessageCount = messageCount;

            bytes = bytes.ExtractLongInt(out var consumerCount);
            ConsumerCount = consumerCount;
        }
    }
}
