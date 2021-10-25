using System.Net.Sockets;
using NUnit.Framework;
using SimpleAMQP.Frames;
using SimpleAMQP.Methods;

namespace SimpleAMQP.Tests
{
    [TestFixture]
    public class ConnectionTests
    {
        [Test]
        public void Tester()
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





            //var exchangeDeclareMethod = new Methods.Exchange.Declare
            //{
            //    ExchangeName = "test",
            //    IsDurable = false,
            //    IsPassive = false,
            //    NoWait = false,
            //    Type = "direct",
            //    Arguments = new FieldTable()
            //    {
            //        PeerProperties =
            //        {
            //            {"test", new FieldValue("test")}
            //        }
            //    }
            //};

            //var exchangeDeclareMethodFrame = new MethodFrame(2, exchangeDeclareMethod);

            //sender.Send(exchangeDeclareMethodFrame.Marshall());



            //bytes = new byte[1028];
            //_ = sender.Receive(bytes);



            //var queueDeclareMethod = new Methods.Queue.Declare
            //{
            //    Name = "testqueue",
            //    AutoDelete = false,
            //    Durable = false,
            //    Exclusive = false,
            //    NoWait = false,
            //    Passive = false
            //};

            //var queueDeclareMethodFrame = new MethodFrame(2, queueDeclareMethod);

            //_ = sender.Send(queueDeclareMethodFrame.Marshall());


            //var bindMethod = new Methods.Queue.Bind
            //{
            //    ExchangeName = "test",
            //    NoWait = false,
            //    QueueName = "testqueue",
            //    RoutingKey = "hello",
            //    Arguments = new FieldTable()
            //};

            //var mf = new MethodFrame(2, bindMethod);

            var basicPublishMethiod = new Methods.Basic.Publish
            {
                ExchangeName = "test",
                Immediate = false,
                Mandatory = false,
                RoutingKey = "hello"
            };

            var basicPubFrame = new MethodFrame(2, basicPublishMethiod);

            _ = sender.Send(basicPubFrame.Marshall());

            var contentHeaderFrame = new ContentHeaderFrame
            {
                ClassId = 60,
                BodySize = 12,
                Channel = 2,
            };

            var contentHeaderFrameBytes = contentHeaderFrame.Marshall();

            _ = sender.Send(contentHeaderFrameBytes);

            var bodyFrame = new BodyFrame
            {
                Body = new byte[]
                {
                    1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12
                },
                Channel = 2
            };

            var bodyFrameBytes = bodyFrame.Marshall();

            _ = sender.Send(bodyFrameBytes);

        }
    }
}
