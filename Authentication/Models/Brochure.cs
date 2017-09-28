using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Authentication.Models
{
    [Serializable]
    [XmlRoot(ElementName = ("Document"))]
    public class Brochure
    {
        [XmlElement(ElementName = "Id")]
        public int Id { get; set; }
        [XmlElement(ElementName = "Name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "Theme")]
        public string Theme { get; set; }
        [XmlElement(ElementName = "Color")]
        public string Color { get; set; }
        [XmlElement(ElementName = "Price")]
        public int Price { get; set; }
    }
}