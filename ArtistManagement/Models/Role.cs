using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc.Routing;

namespace ArtistManagement.Models
{
    public partial class Role
    {        
        public int RoleId { get; set; }
        public string RoleTitle { get; set; }

        public ICollection<RoleEntry> RoleEntries { get; set; }= new List<RoleEntry>();
    }
}