using ProjeTakipSitesi.Models.DataContext;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjeTakipSitesi.Controllers
{
    public class GenelBakisController : Controller
    {
        private ProjeTakipDBContext db = new ProjeTakipDBContext(); // Veritabanı Bağlantısı  
        // GET: GenelBakis
        public ActionResult Index()
        {
            int projeSayisi = db.PersonelProjeleris.Count();
            ViewBag.ProjeSayisi = projeSayisi;

            int tamamlanmisProje = db.PersonelProjeleris.Where(p => p.TamamlanmaDurumu == true).Count();
            ViewBag.TamamlanmisProje = tamamlanmisProje;

            var yuksekOncelikliProjeler = db.PersonelProjeleris.Where(p => p.OncelikDurumu == "Yüksek Öncelikli").Count();
            ViewBag.YuksekOncelikli = yuksekOncelikliProjeler;

            var ortaOncelikliProjeler = db.PersonelProjeleris.Where(p => p.OncelikDurumu == "Orta Öncelikli").Count();
            ViewBag.OrtaOncelikli = ortaOncelikliProjeler;

            var dusukOncelikliProjeler = db.PersonelProjeleris.Where(p => p.OncelikDurumu == "Düşük Öncelikli").Count();
            ViewBag.DusukOncelikli = dusukOncelikliProjeler;

            var basariliYuksekOncelikliProjeler = db.PersonelProjeleris.Where(p => p.OncelikDurumu == "Yüksek Öncelikli" && p.TamamlanmaDurumu == true).Count();
            ViewBag.BasariliYuksekOncelikli = basariliYuksekOncelikliProjeler;

            var basariliOrtaOncelikliProjeler = db.PersonelProjeleris.Where(p => p.OncelikDurumu == "Orta Öncelikli" && p.TamamlanmaDurumu == true).Count();
            ViewBag.BasariliOrtaOncelikli = basariliOrtaOncelikliProjeler;

            var basariliDusukOncelikliProjeler = db.PersonelProjeleris.Where(p => p.OncelikDurumu == "Düşük Öncelikli" && p.TamamlanmaDurumu == true).Count();
            ViewBag.BasariliDusukOncelikli = basariliDusukOncelikliProjeler;

            int tamamlanmamisProje = db.PersonelProjeleris.Where(p => p.TamamlanmaDurumu == false).Count();
            ViewBag.TamamlanmamisProje = tamamlanmamisProje;

            var personelProjeListesi = db.PersonelProjeleris.ToList();
            var personelTamamlanmisProjeSayisi = new Dictionary<int, int>(); //Personel Id ve Tamamlanmış proje sayısı çiftlerini tutmak için kullandım.
            foreach(var personel in db.PersonelBilgileris.ToList())
            {
                int tamamlanmisProjeSayisi = 0;
                foreach(var proje in personel.PersonelProjeleris)
                {
                    if(proje.TamamlanmaDurumu== true)
                    {
                        tamamlanmisProjeSayisi++;
                    }
                }
                personelTamamlanmisProjeSayisi[personel.PersonelBilgileriId] = tamamlanmisProjeSayisi;
            }

            var siraliPersonelListesi = personelTamamlanmisProjeSayisi.OrderByDescending(x => x.Value); // tamamlanmış proje sayısına göre personelleri sıralama
            var enCokTamamlananPersonelId = siraliPersonelListesi.First().Key; // en çok proje tamamlama sayısına sahip personell
            var enCokTamamlananPersonel = db.PersonelBilgileris.FirstOrDefault(p => p.PersonelBilgileriId == enCokTamamlananPersonelId);
            ViewBag.EnCokTamamlayanPersonelBilgisi = enCokTamamlananPersonel.AdSoyad;

            return View();
        }
    }
}
