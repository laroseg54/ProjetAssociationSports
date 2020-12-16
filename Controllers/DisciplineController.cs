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
    [RoutePrefix("discipline")]
    public class DisciplineController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Discipline
        [Route("~/disciplines")]
        [Route("index")]
        public ActionResult Index()
        {
            return View(db.Disciplines.ToList());
        }

        // GET: Discipline/Details/5 ou Discipline/5
        [Route("{id}")]
        [Route("details/{id}")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Discipline discipline = db.Disciplines.Find(id);
            if (discipline == null)
            {
                return HttpNotFound();
            }
            return View(discipline);
        }

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
