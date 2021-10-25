using System.Collections.Generic;
using SimpleAMQP.Ex;

namespace SimpleAMQP.Methods.Basic
{
    internal class Get : IMarshallableMethod
    {
        public short ClassId => 60;

        public short MethodId => 70;

        public string QueueName { get; set; }

        public bool NoAck { get; set; }

        public bool IsFor(short classId, short methodId)
            => ClassId == classId && MethodId == methodId;

        public byte[] Marshall()
        {
            var bytes = new List<byte>();

            bytes.AddRange(ClassId.ToBytes());

            bytes.AddRange(MethodId.ToBytes());

            bytes.Add(0);
            bytes.Add(0);

            bytes.AddRange(QueueName.ToShortStringBytes());

            bytes.Add(NoAck.ToByte());

            return bytes.ToArray();
        }
    }
}
