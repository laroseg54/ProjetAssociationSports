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
using Microsoft.AspNet.Identity;

namespace ProjetAssociationSports.Controllers
{
    [Authorize(Roles = "Encadrant")]
    public class CreneauxController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Creneaux
        [HttpGet]
        public  ActionResult Encadrement() // on renvoie à l'encadrant tous les creneaux qu'il encadre
        {
            ApplicationUser encadrant = db.Users.Find(User.Identity.GetUserId());
            return View(encadrant.CreneauxEncadres);

        }
        [HttpGet]
        public ActionResult EncadrementInscrits(int idCreneau)
        {
            string userid = User.Identity.GetUserId();
           Creneau cre =  db.Creneaux.FirstOrDefault(c => c.Id == idCreneau && c.ApplicationUserID == userid); // on cherche si l'encadrant encandre bien le creneau qui posséde l'id idCreneau
            if(cre == null)
            {
                return new HttpNotFoundResult("Ce creneau n'existe pas ou n'est pas encadré par vous");
            }
            return View(cre.CreneauxAdherents);
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
