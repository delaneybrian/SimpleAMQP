using System.Collections.Generic;
using SimpleAMQP.Ex;

namespace SimpleAMQP.Methods.Basic
{
    internal class Ack : IMarshallableMethod
    {
        public short ClassId => 60;

        public short MethodId => 80;

        public long DeliveryTag { get; set; }

        public bool Multiple { get; set; }

        public bool IsFor(short classId, short methodId)
            => ClassId == classId && MethodId == methodId;

        public byte[] Marshall()
        {
            var bytes = new List<byte>();

            bytes.AddRange(ClassId.ToBytes());

            bytes.AddRange(MethodId.ToBytes());

            bytes.AddRange(DeliveryTag.ToBytes());

            bytes.Add(Multiple.ToByte());

            return bytes.ToArray();
        }
    }
}
