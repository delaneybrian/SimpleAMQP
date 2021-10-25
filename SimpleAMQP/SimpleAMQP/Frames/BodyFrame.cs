using SimpleAMQP.Ex;
using System.Collections.Generic;

namespace SimpleAMQP.Frames
{
    internal class BodyFrame
    {
        public FrameType Type { get; } = FrameType.Body;

        public short Channel { get; set; }

        public byte[] Body { get; set; }

        public byte[] Marshall()
        {
            var bytes = new List<byte>();

            bytes.Add((byte)Type);

            var channelBytes = Channel.ToBytes();
            bytes.AddRange(channelBytes);

            bytes.AddRange(Body.Length.ToBytes());

            bytes.AddRange(Body);

            bytes.Add(206);

            return bytes.ToArray();
        }
    }
}
