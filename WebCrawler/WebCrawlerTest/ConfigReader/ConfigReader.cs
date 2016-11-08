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

        private bool _isFileLoaded;
        private XElement _rootElement;

        public ConfigReader()
        {
            _isFileLoaded = false;
        }
        
        public void LoadFile(string fileName)
        {
            if (fileName == null) throw new ArgumentNullException(nameof(fileName));

            var document = XDocument.Load(fileName);
            _rootElement = document.Root;
            _isFileLoaded = true;
        }

        public int GetDepth()
        {
            if (!_isFileLoaded) throw new ConfigurationReaderException();

            var depthElement = _rootElement.Element(DepthTag);
            if (depthElement == null) throw new ConfigurationReaderException();
            
            return int.Parse(depthElement.Value);
        }

        public List<string> GetUrls()
        {
            if (!_isFileLoaded) throw new ConfigurationReaderException();
            
            var rootResources = _rootElement.Element(RootResourcesTag);
            if (rootResources == null) throw new ConfigurationReaderException();

            return rootResources.Elements(ResourceTag).Select(resource => resource.Value).ToList();
        }
    }
}