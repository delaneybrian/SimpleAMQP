using System;
using SimpleAMQP.Ex;

namespace SimpleAMQP.Methods.Basic
{
    internal class GetOk : IDeMarshallableMethod
    {
        public short ClassId => 60;

        public short MethodId => 71;

        public long DeliveryTag { get; set; }

        public bool Redelivered { get; set; }


        public string Exchange { get; set; }


        public string RoutingKey { get; set; }


        public int MessageCount { get; set; }

        public bool IsFor(Span<byte> bytes)
        {
            return true;
        }

        public GetOk(Span<byte> bytes)
        {
            bytes = bytes.Slice(4);

            bytes = bytes.ExtractLongLongInt(out var deliveryTag);
            DeliveryTag = deliveryTag;

            bytes = bytes.ExtractBoolean(out var redelivered);
            Redelivered = redelivered;

            bytes = bytes.ExtractShortString(out var exchange);
            Exchange = exchange;

            bytes = bytes.ExtractShortString(out var routingKey);
            RoutingKey = routingKey;

            bytes = bytes.ExtractLongInt(out var messageCount);
            MessageCount = messageCount;
        }

    }
}
