using ArtistManagement.Models.ViewModels;
using ArtistManagement.Models;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Web.WebPages;


namespace ArtistManagement.Controllers
{
    public class ArtistController : Controller
    {
        private ArtistDbContext _db = new ArtistDbContext();


        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var artists = _db.Artists.Include(c => c.RoleEntries.Select(b => b.Role));

            if (!String.IsNullOrEmpty(searchString))
            {
                artists = artists.Where(s => s.ArtistName.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    artists = artists.OrderByDescending(a => a.ArtistName);
                    break;
                case "Date":
                    artists = artists.OrderBy(a => a.DateOfBirth);
                    break;
                case "date_desc":
                    artists = artists.OrderByDescending(a => a.DateOfBirth);
                    break;
                default:
                    artists = artists.OrderByDescending(a => a.ArtistId);
                    break;
            }

            int pageSize = 10; 
            int pageNumber = (page ?? 1);
            return View(artists.ToPagedList(pageNumber, pageSize));
        }


        


        public ActionResult AddNewRole(int? id)
        {
            ViewBag.roles = new SelectList(_db.Roles.ToList(), "RoleId", "RoleTitle", (id != null) ? id.ToString() : "");
            return PartialView("_AddNewRoles");
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ArtistViewModel avm, int[] roleId)
        {
            if (ModelState.IsValid)
            {
                Artist artist = new Artist()
                {
                    ArtistName = avm.ArtistName,
                    DateOfBirth = avm.DateOfBirth,
                    Age = avm.Age,
                    Email = avm.Email,
                    MobileNo = avm.MobileNo,
                    MaritalStatus = avm.MaritalStatus
                };

                // Image upload handling
                HttpPostedFileBase file = avm.PictureFile;
                if (file != null && file.ContentLength > 0)
                {
                    string fileName = DateTime.Now.Ticks.ToString() + Path.GetExtension(file.FileName);
                    string filePath = Path.Combine(Server.MapPath("~/Images"), fileName);
                    file.SaveAs(filePath);
                    artist.Picture = "/Images/" + fileName;
                }
                else
                {
                    artist.Picture = "/Images/noimage.jpg";
                }

                // Role handling
                foreach (var item in roleId)
                {
                    RoleEntry roleEntry = new RoleEntry()
                    {
                        Artist = artist,
                        RoleId = item
                    };
                    _db.RoleEntries.Add(roleEntry);
                }

                _db.Artists.Add(artist);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(avm);
        }

        


        [HttpGet]
        public ActionResult Edit(int? id)
        {
            Artist artist = _db.Artists.First(x => x.ArtistId == id);
            var artistRoles = _db.RoleEntries.Where(x => x.ArtistId == id).ToList();
            ArtistViewModel artistViewModel = new ArtistViewModel()
            {
                ArtistID = artist.ArtistId,
                ArtistName = artist.ArtistName,               
                DateOfBirth = artist.DateOfBirth,
                Email = artist.Email,
                MobileNo = artist.MobileNo,
                Picture = artist.Picture,
                MaritalStatus = artist.MaritalStatus
            };
            if (artistRoles.Count() > 0)
            {
                foreach (var item in artistRoles)
                {
                    artistViewModel.RoleList.Add(item.RoleId);
                }
            }
            return View(artistViewModel);
        }



        [HttpPost]
        public ActionResult Edit(ArtistViewModel avm, int[] roleId)
        {
            if (ModelState.IsValid)
            {
                Artist artist = new Artist()
                {
                    ArtistId = avm.ArtistID,
                    ArtistName = avm.ArtistName,
                    DateOfBirth = avm.DateOfBirth,
                    Age = avm.Age,
                    Email = avm.Email,
                    MobileNo = avm.MobileNo,
                    MaritalStatus = avm.MaritalStatus
                };


                //Image
                HttpPostedFileBase file = avm.PictureFile;
                string uniqueFileName = null;
                if (file != null)
                {
                    string fileName = DateTime.Now.Ticks.ToString() + Path.GetExtension(file.FileName);
                    string filePath = Path.Combine("/Images", fileName);
                    file.SaveAs(Server.MapPath(filePath));
                    artist.Picture = filePath;
                }
                else
                {
                    artist.Picture = avm.Picture;

                }
               

                //Role Delete
                var existsRoleEntry = _db.RoleEntries.Where(x => x.ArtistId == artist.ArtistId).ToList();
                foreach (var roleEntry in existsRoleEntry)
                {
                    _db.RoleEntries.Remove(roleEntry);
                }

                //Add Role
                foreach (var item in roleId)
                {
                    RoleEntry roleEntry = new RoleEntry()
                    {
                        Artist = artist,
                        ArtistId = artist.ArtistId,
                        RoleId = item
                    };
                    _db.RoleEntries.Add(roleEntry);
                }
                _db.Entry(artist).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }





        public ActionResult Delete(int? id)
        {
            var artist = _db.Artists.Find(id);
            var existsRoleEntry = _db.RoleEntries.Where(x => x.ArtistId == artist.ArtistId).ToList();
            foreach (var roleEntries in existsRoleEntry)
            {
                _db.RoleEntries.Remove(roleEntries);
            }
            _db.Entry(artist).State = EntityState.Deleted;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}