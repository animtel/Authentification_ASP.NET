using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Authentication.Models
{
    public class Journal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Number { get; set; }
        public int Price { get; set; }
    }
}