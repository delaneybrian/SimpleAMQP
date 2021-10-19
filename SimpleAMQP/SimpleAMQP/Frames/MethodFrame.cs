using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices.ComTypes;
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
            bytes = bytes.Slice(1);

            bytes = bytes.ExtractShortInt(out var channel);
            Channel = channel;

            bytes = bytes.ExtractLongInt(out var size);

            bytes[..2].ExtractShortInt(out var classId);
            ClassId = classId;

            bytes[2..4].ExtractShortInt(out var methodId);
            MethodId = methodId;

            var methodBytes = bytes.Slice(0, size);

            if (MethodId == 10)
                Method = ConnectionStart.Construct(methodBytes);

            if (MethodId == 20)
                Method = ConnectionSecure.Construct(methodBytes);

            if (MethodId == 30)
                Method = ConnectionTune.Construct(methodBytes);

            if (MethodId == 31)
                Method = ConnectionTuneOk.Construct(methodBytes);

            if (MethodId == 41)
                Method = ConnectionOpenOk.Construct(methodBytes);
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
