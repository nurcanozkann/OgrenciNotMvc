using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OgrenciNotMvc.Models.Entity_Framework;

namespace OgrenciNotMvc.Controllers
{
    public class DefaultController : Controller
    {
        DbMvcOkulEntities db = new DbMvcOkulEntities();

        // GET: Default
        public ActionResult Index()
        {
            var dersler = db.TBLDERSLER.ToList();
            return View(dersler);
        }

        [HttpGet]
        public ActionResult YeniDers()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniDers(TBLDERSLER p)
        {
            if (ModelState.IsValid)
            {
                db.TBLDERSLER.Add(p);
                db.SaveChanges();
            }
            return View();
        }
        public ActionResult Sil(int? id)
        {
            var ders = db.TBLDERSLER.Find(id);
            db.TBLDERSLER.Remove(ders);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Guncelle(int? id)
        {
            var dersler = db.TBLDERSLER.Find(id);
            return View(dersler);
        }
        [HttpPost]
        public ActionResult Guncelle(TBLDERSLER drs)
        {
            var ders = db.TBLDERSLER.Where(m => m.DersId == drs.DersId).FirstOrDefault();
            ders.DersAd = drs.DersAd;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}