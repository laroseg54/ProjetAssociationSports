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
using System.IO;
using ProjetAssociationSports.ViewModels;

namespace ProjetAssociationSports.Controllers
{

    [Authorize]
    public class DocumentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Documents
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "PostedFile,TypePiece")] DocumentViewModel documentViewModel)
        {

            if (ModelState.IsValid)
            {
                string fileName = Path.GetFileName(documentViewModel.PostedFile.FileName);
                Adherent ad = db.Adherents.Find(User.Identity.GetUserId());

                /*     string filePath = "~/Uploads/Documents/" + ad.Nom + ad.Prenom + "/";*/
                /*   System.IO.Directory.CreateDirectory(filePath);*/
                string filePath = "~/Uploads/Documents/";
                Document d = ad.Documents.FirstOrDefault(d => d.TypePiece == documentViewModel.TypePiece);
                if (d != null)
                {
                  
                    FileInfo fi = new FileInfo(Server.MapPath(d.Chemin));
                    fi.Delete();
                    db.Entry(d).State = EntityState.Deleted;
                }
               
                documentViewModel.PostedFile.SaveAs(Server.MapPath(filePath+fileName));
             
                Document document = new Document
                {
                    Chemin = filePath+fileName,
                    AdherentID = ad.Id,
                    Nom = documentViewModel.PostedFile.FileName,
                    TypePiece = documentViewModel.TypePiece,
                    TypeDoc = Path.GetExtension(fileName),
                    Adherent = ad
                };
                    db.Documents.Add(document);
                    db.SaveChanges();

            }

        
            //Redirect to Index Action.
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}
