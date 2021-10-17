using System.Net.Sockets;
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

            _ = sender.Receive(bytes);


            var connectionSecureMethodFrame = new MethodFrame(bytes);

            var connectionSecureMethod = methodFrame.Method as ConnectionSecure;
        }
    }
}
