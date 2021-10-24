using System.Collections.Generic;
using SimpleAMQP.Ex;

namespace SimpleAMQP.Methods.Queue
{
    internal class Declare : IMarshallableMethod
    {
        public short ClassId => 50;

        public short MethodId => 10;

        public string Name { get; set; }

        public bool Passive { get; set; }

        public bool Durable { get; set; }

        public bool Exclusive { get; set; }

        public bool AutoDelete { get; set; }

        public bool NoWait { get; set; }

        public FieldTable Arguments { get; set; } = new FieldTable();
        

        public bool IsFor(short classId, short methodId)
            => ClassId == classId && MethodId == methodId;

        public byte[] Marshall()
        {
            var bytes = new List<byte>();

            bytes.AddRange(ClassId.ToBytes());

            bytes.AddRange(MethodId.ToBytes());

            bytes.Add(0);
            bytes.Add(0);

            bytes.AddRange(Name.ToShortStringBytes());

            bytes.Add(BitPacker.Pack(Passive, Durable, Exclusive, AutoDelete, NoWait));

            bytes.AddRange(Arguments.Marshall());

            return bytes.ToArray();
        }
    }
}
