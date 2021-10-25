using System.Collections.Generic;
using SimpleAMQP.Ex;

namespace SimpleAMQP.Methods.Connection
{
    internal class Open : IMarshallableMethod
    {
        public short ClassId => 10;

        public short MethodId => 40;

        public string Path { get; private set; }

        private Open(string path)
        {
            Path = path;
        }

        public static IMarshallableMethod Construct(string path)
        {
            return new Open(path);
        }

        public bool IsFor(short classId, short methodId) =>
            classId == ClassId && methodId == MethodId;

        public byte[] Marshall()
        {
            var bytes = new List<byte>();

            var classIdBytes = ClassId.ToBytes();
            bytes.AddRange(classIdBytes);

            var methodIdBytes = MethodId.ToBytes();
            bytes.AddRange(methodIdBytes);

            var pathBytes = Path.ToShortStringBytes();
            bytes.AddRange(pathBytes);

            bytes.Add(0);
            bytes.Add(0);

            return bytes.ToArray();
        }
    }
}
