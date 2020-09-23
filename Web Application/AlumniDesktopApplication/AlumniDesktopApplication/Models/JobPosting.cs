using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlumniDesktopApplication.Models
{
    class JobPosting
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
