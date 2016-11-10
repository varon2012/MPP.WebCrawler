using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace WebCrawlerTest.ConfigReader
{
    internal class ConfigReader : IConfigReader
    {
        private const string DepthTag = "depth";
        private const string RootResourcesTag = "rootResources";
        private const string ResourceTag = "resource";
        
        private XElement _rootElement;

        public void LoadFile(string fileName)
        {
            try
            {
                var document = XDocument.Load(fileName);
                _rootElement = document.Root;
            }
            catch (Exception)
            {
                throw new ConfigurationReaderException("Could not load file");
            }
        }

        public int GetDepth()
        {
            try
            {
                int depth;
                var depthElement = _rootElement.Element(DepthTag);
                int.TryParse(depthElement.Value, out depth);
                return depth;
            }
            catch (Exception)
            {
                throw new ConfigurationReaderException("Failed to read the property Depth");
            }
        }

        public List<string> GetUrls()
        {
            try
            {
                var rootResources = _rootElement.Element(RootResourcesTag);
                return rootResources.Elements(ResourceTag).Select(resource => resource.Value).ToList();
            }
            catch (Exception)
            {
                throw new ConfigurationReaderException("Failed to read the property RootResources");
            }
        }
    }
}