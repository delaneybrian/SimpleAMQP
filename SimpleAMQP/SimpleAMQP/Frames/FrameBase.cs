namespace SimpleAMQP.Frames
{
    internal abstract class FrameBase
    {
        public abstract FrameType Type { get; }

        public abstract short Channel { get; }

        public abstract long Size { get; }
    }
}
