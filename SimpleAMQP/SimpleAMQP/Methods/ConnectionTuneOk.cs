using System;
using System.Collections.Generic;
using SimpleAMQP.Ex;

namespace SimpleAMQP.Methods
{
    internal class ConnectionTuneOk : IAMQPMethod
    {
        public short ClassId => 10;

        public short MethodId => 31;

        public short MaxChannels { get; private set; }

        public int MaxFrameSize { get; private set; }

        public short HeartBeatDelay { get; private set; }

        private ConnectionTuneOk(short maxChannels, int maxFrameSize, short heartBeatDelay)
        {
            MaxChannels = maxChannels;
            MaxFrameSize = maxFrameSize;
            HeartBeatDelay = heartBeatDelay;
        }

        private ConnectionTuneOk(Span<byte> bytes)
        {
            bytes = bytes.ExtractShortInt(out var maxChannels);
            MaxChannels = maxChannels;

            bytes = bytes.ExtractLongInt(out var maxFrameSize);
            MaxFrameSize = maxFrameSize;

            bytes = bytes.ExtractShortInt(out var heartBeatDelay);
            HeartBeatDelay = heartBeatDelay;
        }

        public static IAMQPMethod Construct(
            Span<byte> bytes)
        {
            return new ConnectionTuneOk(bytes);
        }

        public static IAMQPMethod Construct(
            short maxChannels, 
            int maxFrameSize, 
            short heartBeatDelay)
        {
            return new ConnectionTuneOk(maxChannels, maxFrameSize, heartBeatDelay);
        }

        public bool IsFor(int classId, int methodId)
            => ClassId == classId && MethodId == methodId;

        public byte[] Marshall()
        {
            var bytes = new List<byte>();

            var classIdBytes = ClassId.ToBytes();
            bytes.AddRange(classIdBytes);

            var methodIdBytes = ClassId.ToBytes();
            bytes.AddRange(methodIdBytes);

            var maxChannelsBytes = MaxChannels.ToBytes();
            bytes.AddRange(maxChannelsBytes);

            var maxFrameSizeBytes = MaxFrameSize.ToBytes();
            bytes.AddRange(maxFrameSizeBytes);

            var heartBeatDelayBytes = HeartBeatDelay.ToBytes();
            bytes.AddRange(heartBeatDelayBytes);

            return bytes.ToArray();
        }
    }
}
