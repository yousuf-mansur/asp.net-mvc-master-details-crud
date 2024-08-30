using ArtistManagement.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;


namespace ArtistManagement.Controllers
{
    public class RoleController : Controller
    {
        private ArtistDbContext _db = new ArtistDbContext();
      
        public ActionResult Index()
        {
            return View(_db.Roles.ToList());
        }


       
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = _db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }


       
        public ActionResult Create()
        {
            return View();
        }


     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RoleId, RoleTitle")] Role role)
        {
            if (ModelState.IsValid)
            {
                _db.Roles.Add(role);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(role);
        }


      
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = _db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RoleId,RoleTitle")] Role role)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(role).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(role);
        }


       
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = _db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }


       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Role role = _db.Roles.Find(id);
            _db.Roles.Remove(role);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        
    }
}