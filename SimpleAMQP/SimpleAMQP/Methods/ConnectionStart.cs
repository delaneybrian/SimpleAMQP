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

        public static IAMQPMethod Construct(Span<byte> bytes)
        {
            return new ConnectionStart(bytes);
        }

        private ConnectionStart(Span<byte> bytes)
        {
            MajorVersion = bytes[4];
            MinorVersion = bytes[5];

            bytes = FieldTable.Extract(bytes.Slice(6), out var fieldTable);

            ServerProperties = fieldTable.PeerProperties;

            bytes = bytes.ExtractLongString(out var _mechanisms);

            Mechanisms = _mechanisms;

            bytes = bytes.ExtractLongString(out var _locals);

            Locals = _locals;
        }
    }
}
