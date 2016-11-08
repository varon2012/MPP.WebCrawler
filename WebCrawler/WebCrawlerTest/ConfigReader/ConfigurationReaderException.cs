using System;
using System.Runtime.Serialization;

namespace WebCrawlerTest.ConfigReader
{
    [Serializable]
    internal class ConfigurationReaderException : Exception
    {
        public ConfigurationReaderException()
        {
        }

        public ConfigurationReaderException(string message) : base(message)
        {
        }

        public ConfigurationReaderException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ConfigurationReaderException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}