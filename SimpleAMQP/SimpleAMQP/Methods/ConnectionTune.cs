using System;
using SimpleAMQP.Ex;

namespace SimpleAMQP.Methods
{
    internal class ConnectionTune : IAMQPMethod
    {
        public short ClassId => 10;

        public short MethodId => 30;

        public short MaxChannels { get; private set; }

        public int MaxFrameSize { get; private set; }

        public short HeartBeatDelay { get; private set; }

        private ConnectionTune(Span<byte> bytes)
        {
            bytes = bytes.Slice(4);

            bytes = bytes.ExtractShortInt(out var maxChannels);

            MaxChannels = maxChannels;

            bytes = bytes.ExtractLongInt(out var maxFrameSize);

            MaxFrameSize = maxFrameSize;

            bytes = bytes.ExtractShortInt(out var heartBeatDelay);

            HeartBeatDelay = heartBeatDelay;
        }

        public static IAMQPMethod Construct(Span<byte> bytes)
        {
            return new ConnectionTune(bytes);
        }

        public bool IsFor(int classId, int methodId)
            => ClassId == classId && MethodId == methodId;

        public byte[] Marshall()
        {
            throw new NotImplementedException();
        }
    }
}
