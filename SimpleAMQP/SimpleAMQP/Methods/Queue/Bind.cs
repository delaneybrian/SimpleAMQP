using System.Collections.Generic;
using SimpleAMQP.Ex;

namespace SimpleAMQP.Methods.Queue
{
    internal class Bind : IMarshallableMethod
    {
        public short ClassId => 50;

        public short MethodId => 20;

        public string QueueName { get; set; }

        public string ExchangeName { get; set; }

        public string RoutingKey { get; set; }

        public bool NoWait { get; set; }

        public FieldTable Arguments { get; set; }

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

            bytes.AddRange(ExchangeName.ToShortStringBytes());

            bytes.AddRange(RoutingKey.ToShortStringBytes());

            bytes.Add(NoWait.ToByte());

            bytes.AddRange(Arguments.Marshall());

            return bytes.ToArray();
        }
    }
}
