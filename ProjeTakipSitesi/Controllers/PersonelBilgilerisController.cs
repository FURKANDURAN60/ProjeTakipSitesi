using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjeTakipSitesi.Models.DataContext;
using ProjeTakipSitesi.Models.Personel;

namespace ProjeTakipSitesi.Controllers
{
    public class PersonelBilgilerisController : Controller
    {
        private ProjeTakipDBContext db = new ProjeTakipDBContext(); // Veritabanı Bağlantısı

        // GET: PersonelBilgileris
        public ActionResult Index()
        {
            return View(db.PersonelBilgileris.ToList()); // Verileri Listeleme Yapar
        }

        public ActionResult PersonelKarti()
        {
            return View(db.PersonelBilgileris.OrderBy(model => model.Departman)
                    .ToList()); // Verileri Listeleme Yapar
        }

        // GET: PersonelBilgileris/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PersonelBilgileris/Create
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PersonelBilgileri personelBilgileri)
        {
            if (ModelState.IsValid)
            {
                db.PersonelBilgileris.Add(personelBilgileri);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(personelBilgileri);
        }

        // GET: PersonelBilgileris/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonelBilgileri personelBilgileri = db.PersonelBilgileris.Find(id);
            if (personelBilgileri == null)
            {
                return HttpNotFound();
            }
            return View(personelBilgileri);
        }

        

        // GET: PersonelBilgileris/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonelBilgileri personelBilgileri = db.PersonelBilgileris.Find(id);
            if (personelBilgileri == null)
            {
                return HttpNotFound();
            }
            return View(personelBilgileri);
        }

        // POST: PersonelBilgileris/Edit/5
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PersonelBilgileri personelBilgileri)
        {
            if (ModelState.IsValid)
            {
                db.Entry(personelBilgileri).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(personelBilgileri);
        }

        // GET: PersonelBilgileris/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonelBilgileri personelBilgileri = db.PersonelBilgileris.Find(id);
            if (personelBilgileri == null)
            {
                return HttpNotFound();
            }
            return View(personelBilgileri);
        }

        // POST: PersonelBilgileris/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PersonelBilgileri personelBilgileri = db.PersonelBilgileris.Find(id);
            db.PersonelBilgileris.Remove(personelBilgileri);
            db.SaveChanges();
            return RedirectToAction("Index");
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
