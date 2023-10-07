using mds3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mds3.Controllers
{
    public class FormatsController : Controller
    {
        private mds3Entities db= new mds3Entities();
        // GET: Formats
        public ActionResult Index()
        {
            return View(db.Formats.ToList());
        }
        public ActionResult Create()
        {
            ViewBag.formats = new SelectList(db.Formats, "FormatId", "FormatName");

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Format form)
        {
            if (ModelState.IsValid)
            {
                Format format = new Format
                {
                    FormatName = form.FormatName
                };
                db.Formats.Add(format);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.formats = new SelectList(db.Formats, "FormatId", "FormatName");

            return RedirectToAction("Create");
        }
    }
}