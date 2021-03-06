using System;
using System.Collections.Generic;
using SimpleAMQP.Ex;

namespace SimpleAMQP.Methods.Connection
{
    internal class StartOk : IMarshallableMethod
    {
        public short ClassId => 10;

        public short MethodId => 11;

        public FieldTable ClientProperties { get; set; }

        public string Mechanism { get; set; }

        public string Response { get; set; }

        public string Locale { get; set; }

        public StartOk(
            FieldTable clientProperties,
            string mechanism,
            string response,
            string locale)
        {
            ClientProperties = clientProperties;
            Mechanism = mechanism;
            Response = response;
            Locale = locale;
        }

        public static IMarshallableMethod Construct(Span<byte> bytes)
        {
            throw new NotImplementedException();
        }

        public bool IsFor(short classId, short methodId) =>
            ClassId == classId && methodId == MethodId;

        public byte[] Marshall()
        {
            var bytes = new List<byte>();

            var classIdBytes = ClassId.ToBytes();
            bytes.AddRange(classIdBytes);

            var methodIdBytes = MethodId.ToBytes();
            bytes.AddRange(methodIdBytes);

            var clientPropertiesBytes = ClientProperties.Marshall();
            bytes.AddRange(clientPropertiesBytes);

            var mechanismBytes = Mechanism.ToShortStringBytes();
            bytes.AddRange(mechanismBytes);

            var responseBytes = Response.ToLongStringBytes();
            bytes.AddRange(responseBytes);

            var localeBytes = Locale.ToShortStringBytes();
            bytes.AddRange(localeBytes);

            return bytes.ToArray();
        }
    }
}
