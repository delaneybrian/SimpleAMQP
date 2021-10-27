using System;
using SimpleAMQP.Frames;

namespace SimpleAMQP
{
    internal class Channel
    {
        private Connection _connection;

        public void OpenChannel(Connection connection, int channelNumber)
        {
            var channelOpen = new Methods.Channel.Open();

            var channelOpenMethodFrame = new MethodFrame(2, channelOpen);

            _connection = connection;

            _connection.Send(channelOpenMethodFrame.Marshall());
        }

        public void Send(IFrame frame)
        {
            _connection.Send(frame.Marshall());
        }

        public Span<byte> Receive()
        {
            return _connection.Read();
        }
    }
}
