using System;

namespace SimpleAMQP.Methods
{
    internal class ConnectionOpenOk : IMarshallableMethod
    {
        public short ClassId => 10;

        public short MethodId => 41;

        public bool IsFor(short classId, short methodId) =>
            classId == ClassId && methodId == MethodId;

        private ConnectionOpenOk(Span<byte> bytes)
        {
            
        }

        public static IMarshallableMethod Construct(Span<byte> bytes)
        {
            return new ConnectionOpenOk(bytes);
        }

        public byte[] Marshall()
        {
            throw new NotImplementedException();
        }
    }
}
