using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Grain.Models;

namespace Grain.Controllers
{
    public class FarmsController : Controller
    {
        private IGrainRepository Repo;

        public FarmsController(IGrainRepository repository)
        {
            this.Repo = repository;
        }

        // GET: Farms
        public async Task<ActionResult> Index()
        {
            List<Farm> farms = await Repo.FarmsList();
            return View(farms);
        }

        // GET: Farms/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var farm = Repo.Farms.Find(id);
            if (farm == null)
            {
                return HttpNotFound();
            }
            return View(farm);
        }

        // GET: Farms/Create
        public ActionResult Create()
        {
            ViewBag.AgricultureId = new SelectList(Repo.Agricultures, "Id", "Name");
            ViewBag.RegionId = new SelectList(Repo.Regions, "Id", "Name");
            return View();
        }

        // POST: Farms/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,FarmerName,RegionId,AgricultureId,HarvestLastYear,Area")] Farm farm)
        {
            if (ModelState.IsValid)
            {
                await Repo.SaveFarmAsync(farm);
                return RedirectToAction("Index");
            }

            ViewBag.AgricultureId = new SelectList(Repo.Agricultures, "Id", "Name", farm.AgricultureId);
            ViewBag.RegionId = new SelectList(Repo.Regions, "Id", "Name", farm.RegionId);
            return View(farm);
        }

        // GET: Farms/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Farm farm = await Repo.FarmsFindAsync(id);
            if (farm == null)
            {
                return HttpNotFound();
            }
            ViewBag.AgricultureId = new SelectList(Repo.Agricultures, "Id", "Name", farm.AgricultureId);
            ViewBag.RegionId = new SelectList(Repo.Regions, "Id", "Name", farm.RegionId);
            return View(farm);
        }

        // POST: Farms/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,FarmerName,RegionId,AgricultureId,HarvestLastYear,Area")] Farm farm)
        {
            if (ModelState.IsValid)
            {
                Repo.SetEntryEntityState(farm, EntityState.Modified);
                Repo.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AgricultureId = new SelectList(Repo.Agricultures, "Id", "Name", farm.AgricultureId);
            ViewBag.RegionId = new SelectList(Repo.Regions, "Id", "Name", farm.RegionId);
            return View(farm);
        }

        // GET: Farms/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Farm farm = await Repo.FarmsFindAsync(id);
            if (farm == null)
            {
                return HttpNotFound();
            }
            return View(farm);
        }

        // POST: Farms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Farm farm = await Repo.FarmsFindAsync(id);
            Repo.Farms.Remove(farm);
            Repo.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
