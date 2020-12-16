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
using System.IO.Compression;
using ICSharpCode.SharpZipLib.Zip;

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
                if (d != null) // si il existait déja un document de ce type(assurance, certficat médicale ou dossier d'inscription) on le supprime pour le remplacer par le nouveau
                {

                    FileInfo fi = new FileInfo(Server.MapPath(d.Chemin));
                    fi.Delete();
                    db.Entry(d).State = EntityState.Deleted;
                    // on enregistre le document dans le dossier upload à la racine du projet 
                }

                documentViewModel.PostedFile.SaveAs(Server.MapPath(filePath + fileName));
                // on enregistre ensuite le document dans la base de donnée sous forme de chemin vers le fichier
                Document document = new Document
                {
                    Chemin = filePath + fileName,
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
        [Authorize(Roles = "Encadrant")]
        public FileResult Telecharger(string adherentId) // on permet à l'encandrant de télécharger les documents d'un adhérent inscripts à son creneau son forme de zip 
        {
            Adherent ad = db.Adherents.Find(adherentId);
            string encadrantId = User.Identity.GetUserId();
            var ms = new MemoryStream();
            CreneauxAdherents c = db.CreneauxAdherents.FirstOrDefault(c => c.AdherentID == adherentId && c.Creneau.Encadrant.Id == encadrantId);
            if (c == null)
            {
                return null;
            }
            var fileName = string.Format("{0}_ImageFiles.zip", DateTime.Today.Date.ToString("dd-MM-yyyy") + "_1");
            var tempOutPutPath = Server.MapPath(Url.Content("/Downloads/")) + fileName;

            using (ZipOutputStream s = new ZipOutputStream(System.IO.File.Create(tempOutPutPath)))
            {
                s.SetLevel(5);

                byte[] buffer = new byte[4096];

                var DocumentList = new List<string>();
                foreach (Document d in ad.Documents)
                {
                    DocumentList.Add(Server.MapPath(d.Chemin));
                }


                for (int i = 0; i < DocumentList.Count; i++)
                {
                    ZipEntry entry = new ZipEntry(Path.GetFileName(DocumentList[i]));
                    entry.DateTime = DateTime.Now;
                    entry.IsUnicodeText = true;
                    s.PutNextEntry(entry);

                    using (FileStream fs = System.IO.File.OpenRead(DocumentList[i]))
                    {
                        int sourceBytes;
                        do
                        {
                            sourceBytes = fs.Read(buffer, 0, buffer.Length);
                            s.Write(buffer, 0, sourceBytes);
                        } while (sourceBytes > 0);
                    }
                }
                s.Finish();
                s.Flush();
                s.Close();

            }

            byte[] finalResult = System.IO.File.ReadAllBytes(tempOutPutPath);
            if (System.IO.File.Exists(tempOutPutPath))
                System.IO.File.Delete(tempOutPutPath);

            if (finalResult == null || !finalResult.Any())
                throw new Exception(String.Format("No Files found with Image"));

            return File(finalResult, "application/zip", fileName);

        }
    }
}



