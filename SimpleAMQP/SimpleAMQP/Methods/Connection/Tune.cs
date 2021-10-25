using System;
using SimpleAMQP.Ex;

namespace SimpleAMQP.Methods.Connection
{
    internal class Tune : IMarshallableMethod
    {
        public short ClassId => 10;

        public short MethodId => 30;

        public short MaxChannels { get; private set; }

        public int MaxFrameSize { get; private set; }

        public short HeartBeatDelay { get; private set; }

        private Tune(Span<byte> bytes)
        {
            bytes = bytes.Slice(4);

            bytes = bytes.ExtractShortInt(out var maxChannels);

            MaxChannels = maxChannels;

            bytes = bytes.ExtractLongInt(out var maxFrameSize);

            MaxFrameSize = maxFrameSize;

            bytes = bytes.ExtractShortInt(out var heartBeatDelay);

            HeartBeatDelay = heartBeatDelay;
        }

        public static IMarshallableMethod Construct(Span<byte> bytes)
        {
            return new Tune(bytes);
        }

        public bool IsFor(short classId, short methodId)
            => ClassId == classId && MethodId == methodId;

        public byte[] Marshall()
        {
            throw new NotImplementedException();
        }
    }
}
