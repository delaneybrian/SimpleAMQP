using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace SimpleAMQP.Tests
{
    [TestFixture]
    public class Tester
    {
        [Test]
        public void Muncher()
        {
            using (var connection = new Connection())
            {
                connection.Start();

                var a = 1;
            }
        }
    }
}
