using System;
using SimpleAMQP.Ex;

namespace SimpleAMQP.Methods
{
    internal class ConnectionSecure : IMarshallableMethod
    {
        public short ClassId => 10;

        public short MethodId => 20;

        public string Challenge { get; private set; }

        public ConnectionSecure(Span<byte> bytes)
        {
            bytes = bytes.ExtractLongString(out var challenge);

            Challenge = challenge;
        }

        public static IMarshallableMethod Construct(Span<byte> bytes)
        {
            return new ConnectionSecure(bytes);
        }

        public bool IsFor(short classId, short methodId)
        {
            throw new NotImplementedException();
        }

        public byte[] Marshall()
        {
            throw new NotImplementedException();
        }
    }
}
