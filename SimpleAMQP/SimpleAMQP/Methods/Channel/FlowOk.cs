using System.Collections.Generic;
using SimpleAMQP.Ex;

namespace SimpleAMQP.Methods.Channel
{
    internal class FlowOk : IMarshallableMethod, IDeMarshallableMethod
    {
        public short ClassId => 20;

        public short MethodId => 21;

        public bool IsActive { get; set; }

        public bool IsFor(short classId, short methodId)
            => ClassId == classId && MethodId == methodId;

        public byte[] Marshall()
        {
            var bytes = new List<byte>();

            bytes.AddRange(ClassId.ToBytes());

            bytes.AddRange(MethodId.ToBytes());

            bytes.Add(IsActive.ToByte());

            return bytes.ToArray();
        }
    }
}
