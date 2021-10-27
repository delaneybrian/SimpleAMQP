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
            using var connection = new Connection();

            connection.Start();

            var channel = connection.CreateChannel();


            var get = new Methods.Basic.Get
            {
                NoAck = false,
                QueueName = "testqueue"
            };

            var getMethodFrame = new MethodFrame(2, get);

            channel.Send(getMethodFrame);

            var bytes = channel.Receive();


            var byteSpan = channel.Receive();


            
            var index = byteSpan.IndexOf((byte)206);

            var getOkMethodFrame = byteSpan.Slice(0, index + 1);

            var aaa = getOkMethodFrame.Slice(7);

            var getOk = new Methods.Basic.GetOk(aaa);

            var remaining = byteSpan.Slice(index + 1);

            
            var nextIndex = remaining.IndexOf((byte)206);

            var contentHeaderFrame = remaining.Slice(0, nextIndex + 1);

            remaining = remaining.Slice(nextIndex + 1);


            var nextNextIndex = remaining.IndexOf((byte)206);

            var bodyFrame = remaining.Slice(0, nextNextIndex + 1);

            remaining = remaining.Slice(nextNextIndex + 1);


            var basicAckMethod = new Methods.Basic.Ack
            {
                DeliveryTag = 1,
                Multiple = false
            };

            var basicAckMethodFrame = new MethodFrame(2, basicAckMethod);

            channel.Send(basicAckMethodFrame);


            var b = 1;
        }
    }
}
