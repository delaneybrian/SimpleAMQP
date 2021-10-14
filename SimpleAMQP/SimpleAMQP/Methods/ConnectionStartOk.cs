using System;
using System.Collections.Generic;

namespace SimpleAMQP
{
    internal class ConnectionStartOk : IAMQPMethod
    {
        public short ClassId => 10;

        public short MethodId => 11;

        public FieldTable ClientProperties { get; set; }

        public string Mechanism { get; set; }

        public string Response { get; set; }

        public string Locale { get; set; }

        public ConnectionStartOk(
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

        public void Construct(byte[] bytes)
        {
            throw new NotImplementedException();
        }

        public bool IsFor(int classId, int methodId) =>
            ClassId == classId && methodId == MethodId;

        public byte[] Marshall()
        {
            var bytes = new List<byte>();

            var classIdBytes = BitConverter.GetBytes(ClassId);
            bytes.AddRange(classIdBytes);

            var methodIdBytes = BitConverter.GetBytes(MethodId);
            bytes.AddRange(methodIdBytes);

            var clientPropertiesBytes = ClientProperties.Marshall();
            bytes.AddRange(clientPropertiesBytes);

            var responseBytes = Response.ToLongStringBytes();
            bytes.AddRange(responseBytes);

            var localeBytes = Locale.ToShortStringBytes();
            bytes.AddRange(localeBytes);

            return bytes.ToArray();
        }
    }
}
