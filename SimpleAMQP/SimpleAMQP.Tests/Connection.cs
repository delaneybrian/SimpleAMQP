using NUnit.Framework;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using SimpleAMQP.Ex;

namespace SimpleAMQP.Tests
{
    [TestFixture]
    public class Connection
    {
        [Test]
        public void Tester()
        {
            byte[] bytes = new byte[1028];

            var ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            var ipAddress = ipHostInfo.AddressList[0];
            var remoteEP = new IPEndPoint(ipAddress, 5672);

            // Create a TCP/IP  socket.  
            Socket sender = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            sender.Connect(remoteEP);

            var protocolHeaderBytes = new byte[] { 65, 77, 81, 80, 0, 0, 9, 1 };

            // Send the data through the socket.  
            _ = sender.Send(protocolHeaderBytes);
            
            _ = sender.Receive(bytes);

            var methodFrame = new MethodFrame(bytes);

            var connectionStartMethod = methodFrame.Method as ConnectionStart;

            var fieldTable = new FieldTable();

            fieldTable.Add("information", new FieldValue("Licensed under the MPL 2.0. Website: https://rabbitmq.com"));

            var connectionStartOk = new ConnectionStartOk(fieldTable, "PLAIN", "null",
                connectionStartMethod.Locals);

            var startOkMethodFrame = new MethodFrame(0, connectionStartOk);

            var startOkMethodFrameBytes = startOkMethodFrame.Marshall();

            //var byte33s= new Span<byte>(startOkMethodFrameBytes).Slice(11);

            //foreach (var bbb in byte33s)
            //{
            //    Debug.WriteLine($"{bbb},");
            //}

            //var a = FieldTable.Extract(byte33s, out var fieldTable333);

            //a = a.ExtractShortString(out var b);
            //a = a.ExtractLongString(out var c);
            //a = a.ExtractShortString(out var db);

            _ = sender.Send(startOkMethodFrameBytes);

            _ = sender.Receive(bytes);
        }
    }
}
