using System.Diagnostics;
using System.Net.Sockets;
using System.Runtime.InteropServices.ComTypes;
using NUnit.Framework;
using SimpleAMQP.Methods;

namespace SimpleAMQP.Tests
{
    [TestFixture]
    public class Connection
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

            var connectionStartMethod = methodFrame.Method as ConnectionStart;

            var fieldTable = new FieldTable();

            fieldTable.Add("information", new FieldValue("Licensed under the MPL 2.0. Website: https://rabbitmq.com"));

            var connectionStartOk = new ConnectionStartOk(fieldTable, "PLAIN", "\0guest\0guest",
                connectionStartMethod.Locals);

            var startOkMethodFrame = new MethodFrame(0, connectionStartOk);

            var startOkMethodFrameBytes = startOkMethodFrame.Marshall();

            _ = sender.Send(startOkMethodFrameBytes);

            bytes = new byte[1028];
            _ = sender.Receive(bytes);

            var connectionTuneMethodFrame = new MethodFrame(bytes);

            var connectionTuneMethod = connectionTuneMethodFrame.Method as ConnectionTune;

            var connectionTuneOkMethod = ConnectionTuneOk.Construct(connectionTuneMethod.MaxChannels,
                connectionTuneMethod.MaxFrameSize, connectionTuneMethod.HeartBeatDelay);

            var connectionTuneOkMethodFrame = new MethodFrame(0, connectionTuneOkMethod);

            _ = sender.Send(connectionTuneOkMethodFrame.Marshall());
            
            var connectionOpenMethod = ConnectionOpen.Construct(@"/");

            var connectionOpenFrame = new MethodFrame(0, connectionOpenMethod);

            _ = sender.Send(connectionOpenFrame.Marshall());

            bytes = new byte[1028];
            _ = sender.Receive(bytes);


            var exchangeDeclareMethod = new Methods.Exchange.Declare
            {
                ExchangeName = "test",
                IsDurable = false,
                IsPassive = false,
                NoWait = false,
                Type = "direct",
                Arguments = new FieldTable()
                {
                    PeerProperties =
                    {
                        {"test", new FieldValue("test")}
                    }
                }
            };

            var exchangeDeclareMethodFrame = new MethodFrame(0, exchangeDeclareMethod);

            var byeee = exchangeDeclareMethodFrame.Marshall();

            foreach (var ereei in byeee)
            {
                Debug.WriteLine(ereei);
            }

            sender.Send(exchangeDeclareMethodFrame.Marshall());

        }
    }
}
