using System;
using System.Collections.Generic;
using SimpleAMQP.Ex;

namespace SimpleAMQP.Methods.Basic
{
    internal class Publish : IMarshallableMethod
    {
        public short ClassId => 60;

        public short MethodId => 40;

        public string ExchangeName { get; set; }

        public string RoutingKey { get; set; }

        public bool Mandatory { get; set; }

        public bool Immediate { get; set; }

        public bool IsFor(short classId, short methodId)
        {
            throw new NotImplementedException();
        }

        public byte[] Marshall()
        {
            var bytes = new List<byte>();

            bytes.AddRange(ClassId.ToBytes());

            bytes.AddRange(MethodId.ToBytes());

            bytes.Add(0);
            bytes.Add(0);

            bytes.AddRange(ExchangeName.ToShortStringBytes());

            bytes.AddRange(RoutingKey.ToShortStringBytes());

            bytes.Add(BitPacker.Pack(Mandatory, Immediate));

            return bytes.ToArray();
        }
    }
}
