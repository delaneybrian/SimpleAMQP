using System;

namespace SimpleAMQP.Methods
{
    internal class ConnectionOpenOk : IAMQPMethod
    {
        public short ClassId => 10;

        public short MethodId => 41;

        public bool IsFor(int classId, int methodId) =>
            classId == ClassId && methodId == MethodId;

        private ConnectionOpenOk(Span<byte> bytes)
        {
            
        }

        public static IAMQPMethod Construct(Span<byte> bytes)
        {
            return new ConnectionOpenOk(bytes);
        }

        public byte[] Marshall()
        {
            throw new NotImplementedException();
        }
    }
}
