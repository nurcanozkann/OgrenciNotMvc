using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OgrenciNotMvc.Models.Entity_Framework;

namespace OgrenciNotMvc.Controllers
{
    public class OgrencilerController : Controller
    {
        DbMvcOkulEntities db = new DbMvcOkulEntities();
        // GET: Ogrenciler
        public ActionResult Index()
        {
            var ogrenciler = db.TBLOGRENCİ.ToList();
            return View(ogrenciler);
        }
        [HttpGet]
        public ActionResult YeniOgrenci()
        {
            List<SelectListItem> items = (from i in db.TBLKULUP.ToList()
                                          select new SelectListItem
                                          {
                                              Text = i.KulupAd,
                                              Value = i.KulupId.ToString(),
                                          }).ToList();
            //TempData["kulup"]= items;
            ViewBag.kulup = items;
            return View();
        }
        [HttpPost]
        public ActionResult YeniOgrenci(TBLOGRENCİ p)
        {
                var klp = db.TBLKULUP.Where(m => m.KulupId == p.TBLKULUP.KulupId).FirstOrDefault();
                p.TBLKULUP = klp;
                db.TBLOGRENCİ.Add(p);
                db.SaveChanges();
                return RedirectToAction("Index");
        
        }
        public ActionResult Sil(int? id)
        {
            var ogrenci = db.TBLOGRENCİ.Find(id);
            db.TBLOGRENCİ.Remove(ogrenci);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Guncelle(int? id)
        {
            var ogrenci = db.TBLOGRENCİ.Find(id);
            List<SelectListItem> items = (from i in db.TBLKULUP.ToList()
                                          select new SelectListItem
                                          {
                                              Text = i.KulupAd,
                                              Value = i.KulupId.ToString(),
                                          }).ToList();
            ViewBag.kulup = items;
            return View(ogrenci);
        }
        [HttpPost]
        public ActionResult Guncelle(TBLOGRENCİ ogr)
        {
            var ogrenci = db.TBLOGRENCİ.Where(m => m.OgrId == ogr.OgrId).FirstOrDefault();
            ogrenci.OgrAd = ogr.OgrAd;
            ogrenci.OgrSoyad = ogr.OgrSoyad;
            ogrenci.OgrCinsiyet = ogr.OgrCinsiyet;
            ogrenci.OgrKulup = ogr.OgrKulup;
            ogrenci.OgrFotograf = ogr.OgrFotograf;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}