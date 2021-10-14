using System;
using System.Collections.Generic;
using SimpleAMQP.Ex;

namespace SimpleAMQP
{
    internal class ConnectionStart : IAMQPMethod
    {
        public byte MajorVersion { get; private set; }

        public byte MinorVersion { get; private set; }

        public Dictionary<string, FieldValue> ServerProperties { get; private set; }

        public string Mechanisms { get; private set; }

        public string Locals { get; private set; }

        public short ClassId { get; } = 10;

        public short MethodId { get; } = 10;

        public bool IsFor(int classId, int methodId) =>
            classId == ClassId && methodId == MethodId;

        public byte[] Marshall()
        {
            throw new NotImplementedException();
        }

        public void Construct(byte[] bytes)
        {
            throw new NotImplementedException();
        }

        public ConnectionStart(Span<byte> msg)
        {
            MajorVersion = msg[0];
            MinorVersion = msg[1];

            msg = FieldTable.Extract(msg.Slice(2), out var fieldTable);

            ServerProperties = fieldTable.PeerProperties;

            msg = msg.ExtractLongString(out var _mechanisms);

            Mechanisms = _mechanisms;

            msg = msg.ExtractLongString(out var _locals);

            Locals = _locals;
        }
    }
}
