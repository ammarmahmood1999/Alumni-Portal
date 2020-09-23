using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FAST_Alumni_2020_Web_Portal.Models
{
    public class JobPosting
    {

        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string skils { get; set; }
        public Nullable<int> experience { get; set; }
        public Nullable<int> organizationId { get; set; }
        public Nullable<int> alumniId { get; set; }
    }
}