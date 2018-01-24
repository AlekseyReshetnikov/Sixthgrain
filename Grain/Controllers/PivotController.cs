using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Grain.Models;

namespace Grain.Controllers
{
    public class PivotController : Controller
    {

        private IGrainRepository Repo;

        public PivotController(IGrainRepository repository)
        {
            this.Repo = repository;
        }


        // GET: Pivot
        public ActionResult Index()
        {
  
            PivotFilter pivotFilter = new PivotFilter(1,2,3);
            ViewBag.ColId = new SelectList(Repo.PivotHeaderFields, "Id", "Name", pivotFilter.ColId);
            ViewBag.RowId = new SelectList(Repo.PivotHeaderFields, "Id", "Name", pivotFilter.RowId);
            ViewBag.DataId = new SelectList(Repo.PivotDataFields, "Id", "Name", pivotFilter.DataId);

            return View();
        }

        // GET: Pivot/Details/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(int colId, int rowId, int dataId)
        {
            if (((rowId<1) || (rowId > 2)) || ((colId < 1) || (colId > 2)) || ((dataId < 3) || (dataId > 4)) || (colId==rowId) )
            {
                ModelState.AddModelError("rowId", "Поля колонок и столбцов должны отличаться");
                ViewBag.Message = "Запрос не прошел валидацию";
                ViewBag.ColId = new SelectList(Repo.PivotHeaderFields, "Id", "Name");
                ViewBag.RowId = new SelectList(Repo.PivotHeaderFields, "Id", "Name");
                ViewBag.DataId = new SelectList(Repo.PivotDataFields, "Id", "Name");
                return View();
            }
            PivotView pivotShow = await Repo.GeneratePivotShowModel(colId,rowId, dataId);

            return View("Data", pivotShow);
        }

    }

}
