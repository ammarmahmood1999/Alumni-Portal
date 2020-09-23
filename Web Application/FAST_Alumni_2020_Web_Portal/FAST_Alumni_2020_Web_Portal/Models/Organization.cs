using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FAST_Alumni_2020_Web_Portal.Models
{
    public class Organization
    {
        public int id { get; set; }
        public string name { get; set; }
        public string location { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string email { get; set; }
    }
}