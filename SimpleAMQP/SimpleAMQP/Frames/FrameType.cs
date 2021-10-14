namespace SimpleAMQP.Frames
{
    internal enum FrameType
    {
        NotSet = 0,
        Method = 1,
        Header = 2,
        Body = 3,
        Heartbeat = 4
    }
}
