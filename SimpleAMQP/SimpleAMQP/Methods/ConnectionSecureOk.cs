using System;

namespace SimpleAMQP.Methods
{
    internal class ConnectionSecureOk : IMarshallableMethod
    {
        public short ClassId => 10;

        public short MethodId => 21;

        public IMarshallableMethod Construct(Span<byte> bytes)
        {
            throw new NotImplementedException();
        }

        public bool IsFor(short classId, short methodId) =>
            classId == ClassId && methodId == MethodId;

        public byte[] Marshall()
        {
            throw new NotImplementedException();
        }
    }
}
