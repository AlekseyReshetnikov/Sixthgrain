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
        private GrainContext db = new GrainContext();

        // GET: Farms
        public async Task<ActionResult> Index()
        {
            var farms = db.Farms.Include(f => f.Agriculture).Include(f => f.Region);
            return View(await farms.ToListAsync());
        }

        // GET: Farms/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Farm farm = await db.Farms.FindAsync(id);
            if (farm == null)
            {
                return HttpNotFound();
            }
            return View(farm);
        }

        // GET: Farms/Create
        public ActionResult Create()
        {
            ViewBag.AgricultureId = new SelectList(db.Agricultures, "Id", "Name");
            ViewBag.RegionId = new SelectList(db.Regions, "Id", "Name");
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
                db.Farms.Add(farm);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.AgricultureId = new SelectList(db.Agricultures, "Id", "Name", farm.AgricultureId);
            ViewBag.RegionId = new SelectList(db.Regions, "Id", "Name", farm.RegionId);
            return View(farm);
        }

        // GET: Farms/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Farm farm = await db.Farms.FindAsync(id);
            if (farm == null)
            {
                return HttpNotFound();
            }
            ViewBag.AgricultureId = new SelectList(db.Agricultures, "Id", "Name", farm.AgricultureId);
            ViewBag.RegionId = new SelectList(db.Regions, "Id", "Name", farm.RegionId);
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
                db.Entry(farm).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.AgricultureId = new SelectList(db.Agricultures, "Id", "Name", farm.AgricultureId);
            ViewBag.RegionId = new SelectList(db.Regions, "Id", "Name", farm.RegionId);
            return View(farm);
        }

        // GET: Farms/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Farm farm = await db.Farms.FindAsync(id);
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
            Farm farm = await db.Farms.FindAsync(id);
            db.Farms.Remove(farm);
            await db.SaveChangesAsync();
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
