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
using ProjetAssociationSports.ViewModels;
using Microsoft.AspNet.Identity;
using System.Dynamic;

namespace ProjetAssociationSports.Controllers
{

    [Authorize]
    public class AdherentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Adherents
        public ActionResult Index()
        {
            return View(db.Adherents.ToList());
        }

        // GET: Adherents/Details/5
        public ActionResult Details()
        {
            if (User.Identity.GetUserId() == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdherentDetailsViewModel adherent = new AdherentDetailsViewModel
            {
                Adherent = db.Adherents.Find(User.Identity.GetUserId()),
                DocumentViewModel = new DocumentViewModel()
                
            };

            if (adherent.Adherent == null)
            {
                return HttpNotFound();
            }
           
            return View(adherent);
        }

   
     
        public ActionResult Create()
        {
            string user = User.Identity.GetUserId();
            // si l'utilisateur est déja un adhérent on le redirige vers les détails de son adhésion
            if (db.Adherents.Any(a => a.Id == user))
            {
                return RedirectToAction("Details");
            }
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Create([Bind(Include = "DateNaissance,NumTel,Nom,Prenom")] AdherentViewModel adherentViewModel)
        {

            string user = User.Identity.GetUserId();
            if (db.Adherents.Any(a => a.Id == user))
            {
                return RedirectToAction("Details");
            }
            if (ModelState.IsValid)
            {
                Adherent adherent = new Adherent
                {
                    Id = User.Identity.GetUserId(),
                    DateNaissance = adherentViewModel.DateNaissance,
                    Prenom = adherentViewModel.Prenom,
                    Nom = adherentViewModel.Nom,
                    NumTel = adherentViewModel.NumTel,
                    ResteaPayer = 0
                    
                    
                };
                db.Adherents.Add(adherent);
                db.SaveChanges();
                return RedirectToAction("Details");
            }

            return View(adherentViewModel);
        }

        // GET: Adherents/Edit/5
        public ActionResult Edit()
        {
            if (User.Identity.GetUserId() == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Adherent adherent = db.Adherents.Find(User.Identity.GetUserId());
            if (adherent == null)
            {
                return HttpNotFound();
              
            }
            AdherentViewModel ad = new AdherentViewModel
            {
                Nom = adherent.Nom,
                Prenom = adherent.Prenom,
                DateNaissance = adherent.DateNaissance,
                NumTel = adherent.NumTel

            };
            return View(ad);
        }

        // POST: Adherents/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DateNaissance,NumTel,Nom,Prenom")] AdherentViewModel adherentViewModel)
        {
            if (ModelState.IsValid)
            {
                Adherent ad = db.Adherents.Find(User.Identity.GetUserId());


                ad.DateNaissance = adherentViewModel.DateNaissance;
                ad.Prenom = adherentViewModel.Prenom;
                ad.Nom = adherentViewModel.Nom;
                ad.NumTel = adherentViewModel.NumTel;
                db.Entry(ad).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Details");
        }
        [HttpGet]
        public ActionResult Inscription(int id)
        {

            Creneau creneau = db.Creneaux.Find(id);
            Adherent adherent = db.Adherents.Find(User.Identity.GetUserId());
                 
            if(creneau == null)
            {
                return new HttpNotFoundResult("ce creneau n'existe pas ");
            }
            if(adherent == null)
            {
                TempData["erreur"] = "vous devez être adhérent pour pouvoir vous inscrire"; 
                return RedirectToAction("create", "Adherents");
            }
            else
            {
                if (creneau.NbrPlaceRestantes == 0)
                {
                    Response.Redirect(Request.UrlReferrer.ToString());
                }
                db.CreneauxAdherents.Find(adherent.Id, creneau.Id);
                if (db.CreneauxAdherents.Find(adherent.Id, creneau.Id) != null)
                {
                    TempData["erreur"] = "vous êtes déja inscrit à ce créneau";
                    RedirectToAction("VoirInscriptions");
                }
                adherent.ResteaPayer += creneau.Section.Prix;
                CreneauxAdherents c = new CreneauxAdherents
                {
                    AdherentID = adherent.Id,
                    CreneauID = creneau.Id,
                    estPaye = false

                };
                creneau.NbrPlaceRestantes--;
                adherent.CreneauxAdherents.Add(c);
                creneau.CreneauxAdherents.Add(c);
                db.SaveChanges();
                TempData["message"] = "Inscription au créneau réussie";
                return RedirectToAction("VoirInscriptions");

            }
        }
        [HttpGet]
        public ActionResult VoirInscriptions()
        {
            Adherent adherent = db.Adherents.Find(User.Identity.GetUserId());
            if (adherent == null)
            {
                return RedirectToAction("create");
            }
            return View(adherent.CreneauxAdherents);
        }
  
        [HttpGet]
        public ActionResult Payement(int idCreneau)
        {
            Adherent ad = db.Adherents.Find(User.Identity.GetUserId());
            if(ad == null)
            {
                return RedirectToAction("create");
            }
            CreneauxAdherents c = db.CreneauxAdherents.Find(ad.Id, idCreneau);
            if (c == null)
            {
                TempData["erreur"] = "Erreur vous n'êtes pas inscrit à ce créneau";
                return RedirectToAction("VoirInscriptions");
            }
            return View(c);
        }
        [HttpGet]
        public ActionResult Payer(int idCreneau)
        {
            Adherent ad = db.Adherents.Find(User.Identity.GetUserId());
            if (ad == null)
            {
                return RedirectToAction("create");
            }
            CreneauxAdherents ca = db.CreneauxAdherents.Find(ad.Id, idCreneau);
            if (ca == null)
            {
                TempData["erreur"] = "Erreur vous n'êtes pas inscrit à ce créneau";
                return RedirectToAction("VoirInscriptions");
            }
            ca.estPaye = true;
            ad.ResteaPayer -= ca.Creneau.Section.Prix; 
            db.Entry(ad).State = EntityState.Modified;
            db.Entry(ca).State = EntityState.Modified;
            db.SaveChanges();
            TempData["message"] = "Votre paiement a été bien été effectué";
            return RedirectToAction("VoirInscriptions");
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
