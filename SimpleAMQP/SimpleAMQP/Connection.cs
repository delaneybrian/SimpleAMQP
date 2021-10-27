using System;
using System.Collections.Generic;
using System.Net.Sockets;
using SimpleAMQP.Methods;

namespace SimpleAMQP
{
    internal class Connection : IDisposable
    {
        private const int DefaultChannel = 0;

        private NetworkStream _stream;

        private byte[] _buffer = new byte[1028];

        private bool _isOpen = true;
        
        public int MaxChannels { get; private set; }

        public int MaxFrameSize { get; private set; }

        private readonly HashSet<short> _channelsInUse = new HashSet<short>();

        public void Dispose()
        {
            _stream.Dispose();
        }

        public void Send(byte[] bytes)
        {
            if (!_isOpen)
                throw new Exception("Not Open");

            _stream.Write(bytes);
        }

        public Channel CreateChannel()
        {
            var channel = new Channel();

            channel.OpenChannel(this, 2);

            return channel;
        }

        public byte[] Read()
        {
            _stream.Read(_buffer);

            return _buffer;
        }

        public void Start()
        {
            var tcpClient = new TcpClient("localhost", 5672);

            _stream = tcpClient.GetStream();

            _stream.Write(ProtocolHeader.AMQP0091);

            _ = _stream.Read(_buffer);

            var connectionStartMethodFrame = new MethodFrame(_buffer);

            var connectionStartMethod = connectionStartMethodFrame.Method as Methods.Connection.Start;

            var connectionStartOk = new Methods.Connection.StartOk(new FieldTable(), "PLAIN", "\0guest\0guest",
                connectionStartMethod.Locals);

            var connectionStartOkMethodFrame = new MethodFrame(DefaultChannel, connectionStartOk);

            var connectionStartOkMethodFrameBytes = connectionStartOkMethodFrame.Marshall();

            _stream.Write(connectionStartOkMethodFrameBytes);

            _buffer = new byte[1028];
            _ = _stream.Read(_buffer);

            var connectionTuneMethodFrame = new MethodFrame(_buffer);

            var connectionTuneMethod = connectionTuneMethodFrame.Method as Methods.Connection.Tune;

            MaxChannels = connectionTuneMethod.MaxChannels;

            MaxFrameSize = connectionTuneMethod.MaxFrameSize;

            var connectionTuneOkMethod = Methods.Connection.TuneOk.Construct(connectionTuneMethod.MaxChannels,
                connectionTuneMethod.MaxFrameSize, connectionTuneMethod.HeartBeatDelay);

            var connectionTuneOkMethodFrame = new MethodFrame(DefaultChannel, connectionTuneOkMethod);

            _stream.Write(connectionTuneOkMethodFrame.Marshall());

            var connectionOpenMethod = Methods.Connection.Open.Construct(@"/");

            var connectionOpenFrame = new MethodFrame(0, connectionOpenMethod);

            _stream.Write(connectionOpenFrame.Marshall());

            _buffer = new byte[1028];
            _ = _stream.Read(_buffer);
        }
    }
}
