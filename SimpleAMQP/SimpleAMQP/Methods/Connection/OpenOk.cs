using System;

namespace SimpleAMQP.Methods.Connection
{
    internal class OpenOk : IMarshallableMethod
    {
        public short ClassId => 10;

        public short MethodId => 41;

        public bool IsFor(short classId, short methodId) =>
            classId == ClassId && methodId == MethodId;

        private OpenOk(Span<byte> bytes)
        {
            
        }

        public static IMarshallableMethod Construct(Span<byte> bytes)
        {
            return new OpenOk(bytes);
        }

        public byte[] Marshall()
        {
            throw new NotImplementedException();
        }
    }
}
