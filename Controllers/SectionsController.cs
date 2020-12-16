using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjetAssociationSports.Models;
using ProjetAssociationSports.dal;

namespace ProjetAssociationSports.Controllers
{
    public class SectionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Sections
        public ActionResult Index()
        {
            var sections = db.Sections.Include(s => s.Discipline);
            return View(sections.ToList());
        }

        // GET: Sections/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Section section = db.Sections.Find(id);
            if (section == null)
            {
                return HttpNotFound();
            }
            return View(section);
        }


        public ActionResult Payement()
        {
            return View();
        }

        // GET: Sections/Create
    

   

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
