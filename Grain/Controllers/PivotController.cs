﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
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
        public ActionResult Index(int colId, int rowId, int dataId)
        {
            if (((rowId<1) || (rowId > 2)) || ((colId < 1) || (colId > 2)) || ((dataId < 3) || (dataId > 4)) || (colId==rowId) )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PivotShow pivotShow = Repo.GeneratePivotShowModel(colId,rowId, dataId);

            return View("Data", pivotShow);
        }

    }

}
