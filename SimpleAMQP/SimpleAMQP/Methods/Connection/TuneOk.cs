using System;
using System.Collections.Generic;
using SimpleAMQP.Ex;

namespace SimpleAMQP.Methods.Connection
{
    internal class TuneOk : IMarshallableMethod
    {
        public short ClassId => 10;

        public short MethodId => 31;

        public short MaxChannels { get; private set; }

        public int MaxFrameSize { get; private set; }

        public short HeartBeatDelay { get; private set; }

        private TuneOk(short maxChannels, int maxFrameSize, short heartBeatDelay)
        {
            MaxChannels = maxChannels;
            MaxFrameSize = maxFrameSize;
            HeartBeatDelay = heartBeatDelay;
        }

        private TuneOk(Span<byte> bytes)
        {
            bytes = bytes.ExtractShortInt(out var maxChannels);
            MaxChannels = maxChannels;

            bytes = bytes.ExtractLongInt(out var maxFrameSize);
            MaxFrameSize = maxFrameSize;

            bytes = bytes.ExtractShortInt(out var heartBeatDelay);
            HeartBeatDelay = heartBeatDelay;
        }

        public static IMarshallableMethod Construct(
            Span<byte> bytes)
        {
            return new TuneOk(bytes);
        }

        public static IMarshallableMethod Construct(
            short maxChannels, 
            int maxFrameSize, 
            short heartBeatDelay)
        {
            return new TuneOk(maxChannels, maxFrameSize, heartBeatDelay);
        }

        public bool IsFor(short classId, short methodId)
            => ClassId == classId && MethodId == methodId;

        public byte[] Marshall()
        {
            var bytes = new List<byte>();

            var classIdBytes = ClassId.ToBytes();
            bytes.AddRange(classIdBytes);

            var methodIdBytes = MethodId.ToBytes();
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
