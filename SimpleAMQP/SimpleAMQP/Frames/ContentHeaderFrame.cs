using System.Collections.Generic;
using SimpleAMQP.Ex;

namespace SimpleAMQP.Frames
{
    internal class ContentHeaderFrame
    {
        public FrameType Type { get; set; } = FrameType.Header;

        public short Channel { get; set; }

        public short ClassId { get; set; }

        public short Weight { get; } = 0;

        public long BodySize { get; set; }

        public short PropertyFlags { get; set; }

        public List<int> PropertyList { get; set; }

        public byte[] Marshall()
        {
            var bytes = new List<byte>();

            bytes.Add((byte)Type);

            var channelBytes = Channel.ToBytes();
            bytes.AddRange(channelBytes);

            var contentBytes = new List<byte>();

            contentBytes.AddRange(ClassId.ToBytes());

            contentBytes.AddRange(Weight.ToBytes());

            contentBytes.AddRange(BodySize.ToBytes());
            
            var noProperties = (short) 0;

            contentBytes.AddRange(noProperties.ToBytes());
            
            var size = contentBytes.Count;

            bytes.AddRange(size.ToBytes());

            bytes.AddRange(contentBytes);

            bytes.Add(206);

            return bytes.ToArray();
        }
    }
}
