using mds3.Models;
using mds3.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mds3.Controllers
{
    public class PlayersController : Controller
    {
        private readonly mds3Entities db=new mds3Entities();
        // GET: Players
        public ActionResult Index()
        {
            var pr = db.Players.Include(p => p.SeriesEnties.Select(s => s.Format)).OrderByDescending(x => x.PlayerId).ToList();
            return View(pr);
        }
        public ActionResult AddNewFormat(int? id)
        {
            ViewBag.formats = new SelectList(db.Formats.ToList(), "FormatId", "FormatName", (id != null) ? id.ToString() : "");
            return PartialView("_addNewFormat");
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]




        public ActionResult Create(PlayerVM pVM, int[] formatId)
        {
            if (ModelState.IsValid)
            {
                Player pr = new Player
                {
                    PlayerName = pVM.PlayerName,
                    BirthDate = pVM.BirthDate,
                    Age = pVM.Age,
                    MaritualStatus = pVM.MaritualStatus
                };
                //for Image
                HttpPostedFileBase file = pVM.PicturePath;
                if (file != null)
                {
                    string filePath = Path.Combine("/Images/", Guid.NewGuid().ToString() + Path.GetExtension(file.FileName));
                    file.SaveAs(Server.MapPath(filePath));
                    pr.Picture = filePath;
                }

                //for spot
                foreach (var item in formatId)
                {
                    SeriesEnty sEs = new SeriesEnty()
                    {
                        Player = pr,
                        PlayerId = pr.PlayerId,
                        FormatId = item,
                    };
                    db.SeriesEnties.Add(sEs);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }



        public ActionResult Edit(int? id)
        {
            Player pr = db.Players.First(x => x.PlayerId == id);
            var pFs = db.SeriesEnties.Where(x => x.PlayerId == id).ToList();

            PlayerVM pVM = new PlayerVM()
            {
                PlayerId = pr.PlayerId,
                PlayerName = pr.PlayerName,
                Age = pr.Age,
                BirthDate = pr.BirthDate,
                Picture = pr.Picture,
                MaritualStatus = pr.MaritualStatus,
            };
            if (pFs.Count() > 0)
            {
                foreach (var item in pFs)
                {
                    pVM.FormatList.Add((int)item.FormatId);
                }
            }


            return View(pVM);
        }
        [HttpPost]
        public ActionResult Edit(PlayerVM pVM, int[] formatId)
        {
            if (ModelState.IsValid)
            {
                Player pr = new Player
                {
                    PlayerId = pVM.PlayerId,
                    PlayerName = pVM.PlayerName,
                    Age = pVM.Age,
                    BirthDate = pVM.BirthDate,
                    MaritualStatus = pVM.MaritualStatus
                };
                HttpPostedFileBase file = pVM.PicturePath;
                if (file != null)
                {
                    string filePath = Path.Combine("/Images/", Guid.NewGuid().ToString() + Path.GetExtension(file.FileName));
                    file.SaveAs(Server.MapPath(filePath));
                    pr.Picture = filePath;
                }
                else
                {
                    pr.Picture = pVM.Picture;
                }
                //spot
                var eFE = db.SeriesEnties.Where(x => x.PlayerId == pr.PlayerId).ToList();
                //Delete
                foreach (var sE in eFE)
                {
                    db.SeriesEnties.Remove(sE);
                }
                // save all spot
                foreach (var item in formatId)
                {
                    SeriesEnty sE = new SeriesEnty()
                    {
                        PlayerId = pr.PlayerId,
                        FormatId = item
                    };
                    db.Entry(pr).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
        public ActionResult Delete(int? id)
        {
            var pr = db.Players.Find(id);
            var eFE = db.SeriesEnties.Where(x => x.PlayerId == id).ToList();
            foreach (var sE in eFE)
            {
                db.SeriesEnties.Remove(sE);
            }
            db.Entry(pr).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}