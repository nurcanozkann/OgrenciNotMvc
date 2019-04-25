using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OgrenciNotMvc.Models.Entity_Framework;

namespace OgrenciNotMvc.Controllers
{
    public class KuluplerController : Controller
    {
        DbMvcOkulEntities db = new DbMvcOkulEntities();
        // GET: Kulupler
        public ActionResult Index()
        {
            var kulupler = db.TBLKULUP.ToList();
            return View(kulupler);
        }


        [HttpGet]
        public ActionResult YeniKulup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniKulup(TBLKULUP p)
        {
            if (ModelState.IsValid)
            {
                db.TBLKULUP.Add(p);
                db.SaveChanges();
            }

            return View();
        }
        public ActionResult Sil(int? id)
        {
            var kulup = db.TBLKULUP.Find(id);
            if (kulup != null)
            {               
                db.TBLKULUP.Remove(kulup);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }
        [HttpGet]
        public ActionResult Guncelle(int? id)
        {
            var kulup = db.TBLKULUP.Where(x => x.KulupId == id).FirstOrDefault();
            List<SelectListItem> items = (from i in db.TBLKULUP.ToList()
                                          select new SelectListItem
                                          {
                                              Text = i.KulupAd.ToString(),
                                              Value = i.KulupId.ToString(),
                                          }).ToList();
            ViewBag.kulup = items;
            return View(kulup);
        }
        [HttpPost]
        public ActionResult Guncelle(TBLKULUP tbl)
        {
            var kulp = db.TBLKULUP.Where(m => m.KulupId == tbl.KulupId).FirstOrDefault();
            kulp.KulupAd = tbl.KulupAd;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}