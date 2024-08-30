using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc.Routing;

namespace ArtistManagement.Models
{
    public class Artist
    {
        public int ArtistId { get; set; }
        public string ArtistName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string Picture { get; set; }
        public bool MaritalStatus { get; set; }
        public virtual ICollection<RoleEntry> RoleEntries { get; set; } = new List<RoleEntry>();
    }

}