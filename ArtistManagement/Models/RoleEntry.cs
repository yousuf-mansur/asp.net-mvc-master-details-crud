using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ArtistManagement.Models
{
    public partial class RoleEntry
    {
        public RoleEntry() { }

        public int RoleEntryID { get; set; }
        [ForeignKey("Artist")]
        public int ArtistId { get; set; }
        public virtual Artist Artist { get; set; }

        [ForeignKey("Role")]
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }

       
       
    }
}