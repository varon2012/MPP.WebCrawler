using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace WebCrawlerUI.Model
{
    public class XmlConfigReader : IXmlConfigReader
    {
        private readonly string _xmlFileName;
        private readonly string _xsdFileName;
        private readonly string _targetNamespace;

        protected XmlConfigReader() { }

        public XmlConfigReader(string xmlFileName, string xsdFileName, string targetNamespace)
        {
            _xmlFileName = xmlFileName;
            _xsdFileName = xsdFileName;
            _targetNamespace = targetNamespace;
        }

        public Config ReadConfig()
        {     
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.Schemas.Add(_targetNamespace, _xsdFileName);
            settings.ValidationType = ValidationType.Schema;
            XmlReader reader = XmlReader.Create(_xmlFileName, settings);
            XmlSerializer serializer = new XmlSerializer(typeof(Config));
            Config config = (Config)serializer.Deserialize(reader);
            return config;
        }
    }
}
