using System;
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
            using var connection = new Connection();

            connection.Start();

            var channel = connection.CreateChannel();


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

            //channel.Send(exchangeDeclareMethodFrame);



            //var bytes = channel.Receive();


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

            //channel.Send(queueDeclareMethodFrame);


            //var bindMethod = new Methods.Queue.Bind
            //{
            //    ExchangeName = "test",
            //    NoWait = false,
            //    QueueName = "testqueue",
            //    RoutingKey = "hello",
            //    Arguments = new FieldTable()
            //};

            //var bindMethodMethodFrame = new MethodFrame(2, bindMethod);

            //channel.Send(bindMethodMethodFrame);

            var sds = channel.Receive();

            var sds2 = channel.Receive();

            var basicPublishMethiod = new Methods.Basic.Publish
            {
                ExchangeName = "test",
                Immediate = false,
                Mandatory = false,
                RoutingKey = "hello"
            };

            var basicPubFrame = new MethodFrame(2, basicPublishMethiod);

            channel.Send(basicPubFrame);

            var contentHeaderFrame = new ContentHeaderFrame
            {
                ClassId = 60,
                BodySize = 12,
                Channel = 2,
            };

            channel.Send(contentHeaderFrame);

            var bodyBytes = new Span<byte>(new byte[131073]);

            //Need to split up the message with a max size of 131072

            var maxBytesPerMessage = 131072;

            while (bodyBytes.Length > 0)
            {
                var maxRemainingBytes = bodyBytes.Length > maxBytesPerMessage
                    ? maxBytesPerMessage
                    : bodyBytes.Length;

                var currentMessageBytes = bodyBytes.Slice(0, maxRemainingBytes);

                var bodyFrame = new BodyFrame
                {
                    Body = currentMessageBytes.ToArray(),
                    Channel = 2
                };

                channel.Send(bodyFrame);

                bodyBytes = bodyBytes.Slice(maxRemainingBytes);
            }

            var a = channel.Receive();
        }
    }
}
