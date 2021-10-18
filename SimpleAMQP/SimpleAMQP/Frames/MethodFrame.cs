using System;
using System.Collections.Generic;
using SimpleAMQP.Ex;
using SimpleAMQP.Frames;
using SimpleAMQP.Methods;

namespace SimpleAMQP
{
    internal class MethodFrame
    {
        public FrameType Type { get; set; } = FrameType.Method;

        public short Channel { get; set; }

        public short ClassId { get; set; }

        public short MethodId { get; set; }

        public IAMQPMethod Method { get; set; }

        public MethodFrame(short channel, IAMQPMethod method)
        {
            Channel = channel;
            ClassId = method.ClassId;
            MethodId = method.MethodId;
            Method = method;
        }

        public MethodFrame(Span<byte> bytes)
        {
            var channelBytes = bytes.Slice(1, 2);

            if (BitConverter.IsLittleEndian)
                channelBytes.Reverse();

            Channel = BitConverter.ToInt16(channelBytes);

            var sizeBytes = bytes.Slice(3, 4);

            if (BitConverter.IsLittleEndian)
                sizeBytes.Reverse();

            var size = BitConverter.ToInt32(sizeBytes);

            var classIdBytes = bytes.Slice(7, 2);

            if (BitConverter.IsLittleEndian)
                classIdBytes.Reverse();

            ClassId = BitConverter.ToInt16(classIdBytes);

            var methodIdBytes = bytes.Slice(9, 2);

            if (BitConverter.IsLittleEndian)
                methodIdBytes.Reverse();

            MethodId = BitConverter.ToInt16(methodIdBytes);

            var methodBytes = bytes.Slice(7, size);

            if (MethodId == 10)
                Method = ConnectionStart.Construct(methodBytes);

            if (MethodId == 20)
                Method = ConnectionSecure.Construct(methodBytes);

            if (MethodId == 30)
                Method = ConnectionTune.Construct(methodBytes);
        }

        public byte[] Marshall()
        {
            var bytes = new List<byte>();

            bytes.Add((byte)Type);

            var channelBytes = Channel.ToBytes();
            bytes.AddRange(channelBytes);

            var methodBytes = Method.Marshall();
            var size = methodBytes.Length.ToBytes();
            bytes.AddRange(size);
            bytes.AddRange(methodBytes);

            bytes.Add(206);

            return bytes.ToArray();
        }
    }
}
