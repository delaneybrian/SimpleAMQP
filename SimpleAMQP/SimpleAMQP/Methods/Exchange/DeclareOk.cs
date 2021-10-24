using System;

namespace SimpleAMQP.Methods.Exchange
{
    internal class DeclareOk : IDeMarshallableMethod
    {
        public short ClassId => 40;

        public short MethodId => 11;

        public void DeMarshall(Span<byte> bytes)
        {
            
        }
    }
}
