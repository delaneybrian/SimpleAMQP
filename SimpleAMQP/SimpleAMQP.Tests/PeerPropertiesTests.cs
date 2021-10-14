using NUnit.Framework;

namespace SimpleAMQP.Tests
{
    [TestFixture]
    public class PeerPropertiesTests
    {
        [Test]
        public void MrMii()
        {
            var peerProperties = new PeerProperties();

            peerProperties.AddProperty("hello", "stringy");

            var bytes = peerProperties.ToBytes();
        }
    }
}
