using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OgrenciNotMvc.Models.Entity_Framework;
using OgrenciNotMvc.Models;

namespace OgrenciNotMvc.Controllers
{
    public class Notlar1Controller : Controller
    {
        DbMvcOkulEntities db = new DbMvcOkulEntities();
        // GET: Notlar1
        public ActionResult Index()
        {
            var notlar = db.TBLNOTLAR.ToList();
            return View(notlar);
        }


        [HttpGet]
        public ActionResult YeniSinav()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniSinav(TBLNOTLAR tbn)
        {
            if (ModelState.IsValid)
            {
                db.TBLNOTLAR.Add(tbn);
                db.SaveChanges();
            }
            return View();
        }
        [HttpGet]
        public ActionResult Guncelle(int? id)
        {
            var not = db.TBLNOTLAR.Find(id);
            //var ad = from i in db.TBLOGRENCİ select new { i.OgrAd };
            return View(not);
        }
        [HttpPost]
        public ActionResult Guncelle(Class1 model, TBLNOTLAR tbl, int Sinav1 = 0, int Sinav2 = 0, int Sinav3 = 0, int Proje = 0, Boolean Durum = false)
        {



            if (model.islem == "hesapla")
            {
                int Ortalama = (Sinav1 + Sinav2 + Sinav3 + Proje) / 4;
                ViewBag.ort = Ortalama;
                if (Ortalama > 50)
                {
                    Durum = true;
                    ViewBag.durum = Durum;
                }
                else
                {
                    Durum = false;
                    ViewBag.durum = Durum;
                }

            }
            if (model.islem == "notguncelle")
            {
                var nt = db.TBLNOTLAR.Find(tbl.NotId);
                nt.DersId = tbl.DersId;
                nt.OgrId = tbl.OgrId;
                nt.Sinav1 = tbl.Sinav1;
                nt.Sinav2 = tbl.Sinav2;
                nt.Sinav3 = tbl.Sinav3;
                nt.Proje = tbl.Proje;
                nt.Ortalama = tbl.Ortalama;
                nt.Durum = tbl.Durum;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}