using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace ArtistManagement.Models
{
    public class ArtistDbContext : DbContext
    {
        public ArtistDbContext() : base("ArtistDbContext")
        { }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleEntry> RoleEntries { get; set; }
    }
}