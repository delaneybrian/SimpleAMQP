using System.Collections.Generic;
using SimpleAMQP.Ex;

namespace SimpleAMQP.Methods.Exchange
{
    internal class Declare : IMarshallableMethod
    {
        public short ClassId => 40;

        public short MethodId => 10;

        public string ExchangeName { get; set; }

        public string Type { get; set; }

        public bool IsPassive { get; set; }

        public bool IsDurable { get; set; }

        public bool NoWait { get; set; }

        public FieldTable Arguments { get; set; } = new FieldTable();

        public bool IsFor(short classId, short methodId) =>
            ClassId == classId && MethodId == methodId;

        public byte[] Marshall()
        {
            var bytes = new List<byte>();

            bytes.AddRange(ClassId.ToBytes());

            bytes.AddRange(MethodId.ToBytes());

            bytes.Add(0);
            bytes.Add(0);

            bytes.AddRange(ExchangeName.ToShortStringBytes());

            bytes.AddRange(Type.ToShortStringBytes());

            bytes.Add(IsPassive.ToByte());
            
            bytes.Add(IsDurable.ToByte());

            bytes.Add(0);
            bytes.Add(0);

            bytes.Add(0);
            bytes.Add(0);

            bytes.Add(NoWait.ToByte());

            bytes.AddRange(Arguments.Marshall());

            return bytes.ToArray();
        }
    }
}
