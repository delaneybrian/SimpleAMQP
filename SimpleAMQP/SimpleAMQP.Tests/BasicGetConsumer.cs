using System;
using System.Net.Sockets;
using NUnit.Framework;
using SimpleAMQP.Methods;

namespace SimpleAMQP.Tests
{
    [TestFixture]
    public class BasicGetConsumer
    {
        [Test]
        public void Consume()
        {
            byte[] bytes = new byte[1028];

            var tcpClient = new TcpClient("localhost", 5672);

            var stream = tcpClient.GetStream();

            var sender = stream.Socket;

            var protocolHeaderBytes = new byte[] { 65, 77, 81, 80, 0, 0, 9, 1 };

            _ = sender.Send(protocolHeaderBytes);

            _ = sender.Receive(bytes);

            var methodFrame = new MethodFrame(bytes);

            var connectionStartMethod = methodFrame.Method as Methods.Connection.Start;

            var fieldTable = new FieldTable();

            fieldTable.Add("information", new FieldValue("Licensed under the MPL 2.0. Website: https://rabbitmq.com"));

            var connectionStartOk = new Methods.Connection.StartOk(fieldTable, "PLAIN", "\0guest\0guest",
                connectionStartMethod.Locals);

            var startOkMethodFrame = new MethodFrame(0, connectionStartOk);

            var startOkMethodFrameBytes = startOkMethodFrame.Marshall();

            _ = sender.Send(startOkMethodFrameBytes);




            bytes = new byte[1028];
            _ = sender.Receive(bytes);




            var connectionTuneMethodFrame = new MethodFrame(bytes);

            var connectionTuneMethod = connectionTuneMethodFrame.Method as Methods.Connection.Tune;

            var connectionTuneOkMethod = Methods.Connection.TuneOk.Construct(connectionTuneMethod.MaxChannels,
                connectionTuneMethod.MaxFrameSize, connectionTuneMethod.HeartBeatDelay);

            var connectionTuneOkMethodFrame = new MethodFrame(0, connectionTuneOkMethod);

            _ = sender.Send(connectionTuneOkMethodFrame.Marshall());




            var connectionOpenMethod = Methods.Connection.Open.Construct(@"/");

            var connectionOpenFrame = new MethodFrame(0, connectionOpenMethod);

            _ = sender.Send(connectionOpenFrame.Marshall());




            bytes = new byte[1028];
            _ = sender.Receive(bytes);





            var channelOpen = new Methods.Channel.Open();

            var channelOpenMethodFrame = new MethodFrame(2, channelOpen);

            _ = sender.Send(channelOpenMethodFrame.Marshall());


            bytes = new byte[1028];
            _ = sender.Receive(bytes);


            var get = new Methods.Basic.Get
            {
                NoAck = false,
                QueueName = "testqueue"
            };

            var getMethodFrame = new MethodFrame(2, get);

            var getMethodFrameBytes = getMethodFrame.Marshall();

            _ = sender.Send(getMethodFrameBytes);


            bytes = new byte[1028];
            _ = sender.Receive(bytes);



            var byteSpan = new Span<byte>(bytes);

            
            var index = byteSpan.IndexOf((byte)206);

            var getOkMethodFrame = byteSpan.Slice(0, index + 1);

            var remaining = byteSpan.Slice(index + 1);

            
            var nextIndex = remaining.IndexOf((byte)206);

            var contentHeaderFrame = remaining.Slice(0, nextIndex + 1);

            remaining = remaining.Slice(nextIndex + 1);


            var nextNextIndex = remaining.IndexOf((byte)206);

            var bodyFrame = remaining.Slice(0, nextNextIndex + 1);

            remaining = remaining.Slice(nextNextIndex + 1);


            var b = 1;
        }
    }
}
