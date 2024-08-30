namespace ArtistManagement.Migrations
{
	using ArtistManagement.Models;
	using System;
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Data.Entity.Migrations;
	using System.Linq;

	internal sealed class Configuration : DbMigrationsConfiguration<ArtistDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

		protected override void Seed(ArtistManagement.Models.ArtistDbContext context)
		{
			// Seed Roles
			var roles = new Dictionary<string, Role>
	{
		{"Actor", new Role { RoleId=1, RoleTitle = "Actor" }},
		{"Director", new Role { RoleId=2, RoleTitle = "Director" }},
		{"Playwright", new Role { RoleId=3, RoleTitle = "Playwright" }},
		{"Music Director", new Role { RoleId=4, RoleTitle = "Music Director" }},
		{"Makeup Artist", new Role { RoleId=5, RoleTitle = "Makeup Artist" }},
		{"Stage Manager", new Role { RoleId=6, RoleTitle = "Stage Manager" }},
		{"Production Designer", new Role { RoleId=7, RoleTitle = "Production Designer" }},
		{"Costume Designer", new Role { RoleId=8, RoleTitle = "Costume Designer" }}
	};

			foreach (var role in roles.Values)
			{
				context.Roles.AddOrUpdate(r => r.RoleTitle, role);
			}
			context.SaveChanges();

			// Seed Artists
			var artists = new List<dynamic>
	{
		new { ArtistName = "Monsur Sarkar", DateOfBirth = new DateTime(1994, 4, 25), Email = "monsur@gmail.com", MobileNo = "01719983377", Picture = "/Images/Monsur.jpg", MaritalStatus = false, Roles = new[] {"Actor", "Director"} },
		new { ArtistName = "Shouvik Paul", DateOfBirth = new DateTime(2000, 8, 22), Email = "paul@gmail.com", MobileNo = "+9876543210", Picture = "/Images/Shouvik.jpg", MaritalStatus = true, Roles = new[] {"Music Director"} },
		new { ArtistName = "Santi Debnath", DateOfBirth = new DateTime(2004, 3, 10), Email = "santi@gmail.com", MobileNo = "9836478392", Picture = "/Images/Santi.jpg", MaritalStatus = false, Roles = new[] {"Music Director"} },
		new { ArtistName = "Saroar Hasnat", DateOfBirth = new DateTime(2000, 3, 10), Email = "saroar@gmail.com", MobileNo = "01827364589", Picture = "/Images/Saroar.jpg", MaritalStatus = false, Roles = new[] {"Stage Manager"} },
		new { ArtistName = "Turjo Hore ", DateOfBirth = new DateTime(1997, 3, 10), Email = "turjo@gmail.com", MobileNo = "01856827364", Picture = "/Images/Turjo.jpg", MaritalStatus = false, Roles = new[] {"Music Director"} },
		new { ArtistName = "Akhi Rani", DateOfBirth = new DateTime(1997, 3, 10), Email = "akhi@gmail.com", MobileNo = "+6354789387", Picture = "/Images/Akhi.jpg", MaritalStatus = false, Roles = new[] {"Costume Designer", "Playwright"} },
		new { ArtistName = "Mohin Uddin", DateOfBirth = new DateTime(1994, 3, 10), Email = "mohin@gmail.com", MobileNo = "01728948576", Picture = "/Images/Mohin.jpg", MaritalStatus = true, Roles = new[] {"Stage Manager"} },
		new { ArtistName = "Bristy Chaterjee", DateOfBirth = new DateTime(2004, 3, 10), Email = "santi@gmail.com", MobileNo = "01756453678", Picture = "/Images/Bristy.jpg", MaritalStatus = true, Roles = new[] {"Makeup Artist"} },
		new { ArtistName = "Mredul Rahman", DateOfBirth = new DateTime(1995, 3, 10), Email = "mredul@gmail.com", MobileNo = "01829765678", Picture = "/Images/Mredul.jpg", MaritalStatus = true, Roles = new[] {"Music Director", "Costume Designer"} },
		new { ArtistName = "Sonia Akter", DateOfBirth = new DateTime(1994, 10, 10), Email = "sonia@gmail.com", MobileNo = "1736879867", Picture = "/Images/Sonia.jpg", MaritalStatus = false, Roles = new[] {"Actor", "Production Designer" } },
		new { ArtistName = "Nusrat Sharmeen", DateOfBirth = new DateTime(1990, 3, 25), Email = "nusrat@gmail.com", MobileNo = "0123456576", Picture = "/Images/Nusrat.jpg", MaritalStatus = true, Roles = new[] {"Director"} },
		new { ArtistName = "Nila Shaha", DateOfBirth = new DateTime(1989, 4, 10), Email = "nila@gmail.com", MobileNo = "01789980767", Picture = "/Images/Nila.jpg", MaritalStatus = true, Roles = new[] {"Music Director"} },
		new { ArtistName = "Syed Mamun Reza", DateOfBirth = new DateTime(1985, 6, 10), Email = "mamun@gmail.com", MobileNo = "01729678543", Picture = "/Images/Mamun.jpg", MaritalStatus = true, Roles = new[] {"Actor", "Director"} }
        // Add more artists here...
    };

			foreach (var artistData in artists)
			{
				var artist = new Artist
				{
					ArtistName = artistData.ArtistName,
					DateOfBirth = artistData.DateOfBirth,
					Age = (int)((DateTime.Now - artistData.DateOfBirth).TotalDays / 365.25),
					Email = artistData.Email,
					MobileNo = artistData.MobileNo,
					Picture = artistData.Picture,
					MaritalStatus = artistData.MaritalStatus
				};

				context.Artists.AddOrUpdate(a => a.Email, artist);
				context.SaveChanges(); 

				foreach (var roleTitle in artistData.Roles)
				{
					if (roles.TryGetValue(roleTitle, out Role role)) 
					{
						var roleEntry = new RoleEntry
						{
							RoleId = role.RoleId,
							Role = role,
							ArtistId = artist.ArtistId,
							Artist = artist
						};
						context.RoleEntries.AddOrUpdate(re => new { re.ArtistId, re.RoleId }, roleEntry);
					}
					else
					{
						
						Console.WriteLine($"Error: Role '{roleTitle}' not found for artist '{artistData.ArtistName}'.");
						
					}
				}
			}
			context.SaveChanges();
		}
	}
}
