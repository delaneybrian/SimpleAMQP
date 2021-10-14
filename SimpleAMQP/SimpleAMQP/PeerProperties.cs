using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleAMQP
{
    internal class PeerProperties
    {

        private readonly Dictionary<string, object> _properties;

        public PeerProperties()
        {
            _properties = new Dictionary<string, object>();
        }

        public void AddProperty<T>(string key, T property)
        {
            _properties.Add(key, property);
        }

        public Byte[] ToBytes()
        {
            var bytes = new List<byte>();

            foreach (var property in _properties)
            {
                var fieldNameBytes = property.Key.ToShortStringBytes();

                bytes.AddRange(fieldNameBytes);

                var type = property.Value.GetType();

                if (type == typeof(string))
                {
                    var stringValue = (string)property.Value;
                    bytes.AddRange(stringValue.ToLongStringBytes());
                }
                else if (type == typeof(bool))
                {

                }
                else if (type == typeof(short))
                {

                }
            }

            return bytes.ToArray();
        }
    }
}
