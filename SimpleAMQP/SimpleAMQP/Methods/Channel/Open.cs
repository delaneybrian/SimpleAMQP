using System.Collections.Generic;
using SimpleAMQP.Ex;

namespace SimpleAMQP.Methods.Channel
{
    internal class Open : IMarshallableMethod
    {
        public short ClassId => 20;

        public short MethodId => 10;

        public bool IsFor(short classId, short methodId)
            => ClassId == classId && MethodId == methodId;
        
        public byte[] Marshall()
        {
            var bytes = new List<byte>();

            bytes.AddRange(ClassId.ToBytes());

            bytes.AddRange(MethodId.ToBytes());

            bytes.Add(0);

            return bytes.ToArray();
        }
    }
}
