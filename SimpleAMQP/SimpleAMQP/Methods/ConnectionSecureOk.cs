﻿using System;

namespace SimpleAMQP.Methods
{
    internal class ConnectionSecureOk : IAMQPMethod
    {
        public short ClassId => 10;

        public short MethodId => 21;

        public IAMQPMethod Construct(Span<byte> bytes)
        {
            throw new NotImplementedException();
        }

        public bool IsFor(int classId, int methodId)
        {
            throw new NotImplementedException();
        }

        public byte[] Marshall()
        {
            throw new NotImplementedException();
        }
    }
}