using SimpleAMQP.Frames;

namespace SimpleAMQP
{
    internal class Channel
    {
        private Connection _connection;

        public void Start(Connection connection)
        {
            _connection = connection;
        }

        public void OpenChannel(int channelNumber)
        {
            var channelOpen = new Methods.Channel.Open();

            var channelOpenMethodFrame = new MethodFrame(2, channelOpen);

            _connection.Send(channelOpenMethodFrame.Marshall());
        }

        public void Send(IFrame frame)
        {
            _connection.Send(frame.Marshall());
        }
    }
}
