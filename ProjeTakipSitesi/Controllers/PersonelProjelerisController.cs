using ProjeTakipSitesi.Models.DataContext;
using ProjeTakipSitesi.Models.ProjeTakip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjeTakipSitesi.Controllers
{

    public class PersonelProjelerisController : Controller
    {
        private ProjeTakipDBContext db = new ProjeTakipDBContext(); // Veritabanı Bağlantısı  

        // GET: PersonelProjeleris  
        public ActionResult Index()
        {
            var projeListele = db.PersonelProjeleris.ToList(); // Projeleri Listeleme Yapar  
            return View(projeListele);
        }

        public ActionResult Create()
        {
            ViewBag.PersonelBilgileriId = new SelectList(db.PersonelBilgileris, "PersonelBilgileriId", "AdSoyad"); // Personel Bilgileri Dropdown Listesi  
            return View();
        }

        [HttpPost]
        public ActionResult Create(PersonelProjeleri projeObj, int[] PersonelBilgileriId)
        {
            foreach (var x in PersonelBilgileriId)
            {
                projeObj.PersonelBilgileris.Add(db.PersonelBilgileris.Find(x)); // Seçilen Personel Bilgilerini Projeye Ekleme  
            }
            projeObj.OlusturmaTarihi = DateTime.Now;
            db.PersonelProjeleris.Add(projeObj); // Projeyi Veritabanına Ekleme  
            db.SaveChanges(); // Değişiklikleri Kaydetme  
            return RedirectToAction("Index"); // Projeler Listesine Yönlendirme  
        }

        public ActionResult Edit(int Id)
        {
            var projeObj = db.PersonelProjeleris.Find(Id);
            return View(projeObj);
        }

        [HttpPost]
        public ActionResult Edit(PersonelProjeleri projeObj)
        {
            var projeDbObj = db.PersonelProjeleris.Find(projeObj.PersonelProjeId);
            projeDbObj.ProjeAciklama = projeObj.ProjeAciklama;
            projeDbObj.ProjeBaslik = projeObj.ProjeBaslik;
            projeDbObj.TamamlanmaOrani = projeObj.TamamlanmaOrani;
            projeDbObj.OncelikDurumu = projeObj.OncelikDurumu;
            db.SaveChanges();
            return RedirectToAction("Index");        
        }

        public ActionResult Tamamla(int id)
        {
            var projeObj = db.PersonelProjeleris.Find(id);
            projeObj.TamamlanmaDurumu = true;
            projeObj.TamamlanmaOrani = 100;
            db.SaveChanges();
            return RedirectToAction("Index");
        }








    }
}
