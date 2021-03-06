﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Authentication.Models
{
    [Serializable]
    [XmlRoot(ElementName = ("Document"))]
    public class Journal
    {
        [XmlElement(ElementName = "Id")]
        public int Id { get; set; }
        [XmlElement(ElementName = "Name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "Author")]
        public string Author { get; set; }
        [XmlElement(ElementName = "Year")]
        public string Year { get; set; }
        [XmlElement(ElementName = "Publishing")]
        public string Publishing { get; set; }
        [XmlElement(ElementName = "Number")]
        public string Number { get; set; }
        [XmlElement(ElementName = "Price")]
        public int Price { get; set; }
    }
}