using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleAMQP.Methods.Basic
{
    internal class GetEmpty : IDeMarshallableMethod
    {
        public short ClassId => 60;

        public short MethodId => 72;
    }
}
