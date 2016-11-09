using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.IO;

namespace WebCrawlerTest.Model
{
    internal class XmlConfigReader : IConfigReader
    {
        private string configPath;
        public int Depth { get; set; }
        public string[] RootUrls { get; set; } 

        public XmlConfigReader(string configPath)
        {
            this.configPath = configPath;
        }

        public void ReadConfigInformation()
        {
            if (!File.Exists(configPath))
                throw new FileNotFoundException("Wrong path to the config file.");

            XDocument xDocument = XDocument.Load(configPath);
            GetDepth(xDocument);
            GetRootUrls(xDocument);
        }

        private void GetDepth(XDocument doc)
        {
            try
            {
                string value = doc.Element("root").Element("depth").Value;
                ParseDepthValue(value);
            }
            catch (FileFormatException e)
            {
                throw new FileFormatException($"Check the config file with path: {configPath}");
            }

            if (Depth <= 0)
                throw new FormatException($"Check the value of {nameof(Depth)} in the config file");
        }

        private void GetRootUrls(XDocument doc)
        {
            IEnumerable<XElement> nodes;
            try
            {
                nodes = doc.Element("root").Element("rootResources").Descendants("resource");
            }
            catch
            {
                throw new FileFormatException($"Check the config file with path: {configPath}");
            }

            RootUrls = new string[nodes.Count()];
            int i = 0;
            foreach (var node in nodes)
                RootUrls[i++] = node.Value;
        }

        private void ParseDepthValue(string value)
        {
            int depth;
            if (Int32.TryParse(value, out depth))
            {
                Depth = depth;
            }
            else
            {
                Depth = 0;
            }
        }
    }
}
