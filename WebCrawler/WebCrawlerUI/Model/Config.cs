using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace WebCrawlerUI.Model
{
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://bsuir.by/webcrawler")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://bsuir.by/webcrawler", IsNullable = false, ElementName ="root")]
    public class Config
    {

        [System.Xml.Serialization.XmlElement(ElementName = "depth")]
        public int Depth { get; set; }

        [System.Xml.Serialization.XmlArray(ElementName = "rootResources")]
        [System.Xml.Serialization.XmlArrayItemAttribute("resource", DataType = "anyURI", IsNullable = false )]
        public string[] RootUrls { get; set; }
    }
}
