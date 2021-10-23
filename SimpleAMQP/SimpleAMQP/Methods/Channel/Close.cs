using System.Collections.Generic;
using SimpleAMQP.Ex;

namespace SimpleAMQP.Methods.Channel
{
    internal class Close : IMarshallableMethod
    {
        public short ClassId => 20;

        public short MethodId => 40;

        public short ReplyCode { get; set; }

        public string ReplyText { get; set; }

        public short ClosingClassId { get; set; }

        public short ClosingMethodId { get; set; }

        public bool IsFor(short classId, short methodId)
            => ClassId == classId && MethodId == methodId;

        public byte[] Marshall()
        {
            var bytes = new List<byte>();

            bytes.AddRange(ClassId.ToBytes());

            bytes.AddRange(MethodId.ToBytes());

            bytes.AddRange(ReplyCode.ToBytes());

            bytes.AddRange(ReplyText.ToShortStringBytes());

            bytes.AddRange(ClosingClassId.ToBytes());
            
            bytes.AddRange(ClosingMethodId.ToBytes());

            return bytes.ToArray();
        }
    }
}
