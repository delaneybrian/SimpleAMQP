using System;
using SimpleAMQP.Ex;

namespace SimpleAMQP.Methods.Connection
{
    internal class Secure : IMarshallableMethod
    {
        public short ClassId => 10;

        public short MethodId => 20;

        public string Challenge { get; private set; }

        public Secure(Span<byte> bytes)
        {
            bytes = bytes.ExtractLongString(out var challenge);

            Challenge = challenge;
        }

        public static IMarshallableMethod Construct(Span<byte> bytes)
        {
            return new Secure(bytes);
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
