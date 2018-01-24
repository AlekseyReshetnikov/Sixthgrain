using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grain.Models
{

    public class PivotContext
    {
        static public DataField[] HeaderFields { get; } = new DataField[] {
                new DataField { Id = 1, Name = "Агрикультура", Field="AgricultureId" },
                new DataField { Id = 2, Name = "Регион", Field="RegionId" } };
        static public DataField[] DataFields { get; } = new DataField[] {
                new DataField { Id = 3, Name = "Урожай в тоннах за прошлый год", Field="HarvestLastYear" },
                new DataField { Id = 4, Name = "Площадь ферм", Field="Area" } };

        static private GrainContext db;

        static public async Task<PivotView> GeneratePivotViewModel(GrainContext _db, int colId, int rowId, int dataId)
        {
            List<PivotHeaderElement> Columns=null;
            List<PivotHeaderElement> Rows = null;
            int ColumnsCount;

            db = _db;
            try
            {
                var col = HeaderFields[colId - 1];
                var row = HeaderFields[rowId - 1];
                var data = DataFields[dataId - HeaderFields.Length - 1];
                string sql = string.Format("select {0} as ColId, {1} as RowId, convert(decimal(18,2), sum({2})) as data from Farms group by {0},{1}", col.Field, row.Field, data.Field);
                var dataElements = await db.Database.SqlQuery<PivotDataElement>(sql).ToListAsync();

                Dictionary<int,PivotHeaderElement> dColumns = null;
                Dictionary<int, PivotHeaderElement> dRows = null;
                Task t1 = Task.Run(()=>
                {
                    Columns = (from id in (from item in dataElements select item.ColId).Distinct() select new PivotHeaderElement { Id = id }).ToList();
                });
                Task t2 = Task.Run(() => {
                    Rows = (from id in (from item in dataElements select item.RowId).Distinct() select new PivotHeaderElement { Id = id }).ToList();
                });
                await t1;
                await t2;
                // Попытка асинхронного обращения к базе данных из двух потоков привела к ошибке
                FillHeader(ref Columns, colId);
                dColumns = Columns.ToDictionary(x => x.Id);
                FillHeader(ref Rows, rowId);
                dRows = Rows.ToDictionary(x => x.Id);

                ColumnsCount = Columns.Count;
                // Создать под данные место
                foreach (var item in Rows)
                {
                    item.Data = new decimal[ColumnsCount];
                    for (int i = 0; i < ColumnsCount; i++) { item.Data[i] = 0; }
                }
                // Заполнить данные
                foreach (var item in dataElements)
                {
                    int c = dColumns[item.ColId].Ord;
                    dRows[item.RowId].Data[c] = item.Data;
                }
                PivotView pivotView = new Models.PivotView();
                pivotView.Columns = Columns;
                pivotView.ColumnsCount = ColumnsCount;
                pivotView.Rows = Rows;
                return pivotView;
            }
            finally
            {
                db = null;
            }
        }

        static async Task<String> GetAgricultureName(int x)
        {
            var a = await db.Agricultures.FindAsync(x); return a.Name;
        }

        static async Task<String> GetRegionName(int x)
        {
            var a = await db.Regions.FindAsync(x); return a.Name;
        }

        static private void FillHeader(ref List<PivotHeaderElement> fieldValues, int field)
        {
            Func<int, Task<String>> get;
            if (field == 1) { get = x => GetAgricultureName(x); }
            else { get = x => GetAgricultureName(x); }
            foreach (var item in fieldValues)
            {
                Task<String> t = get(item.Id);
                t.Wait(); // К сожалению не работает в нескольких потоках!
                item.Name = t.Result;
            }
            fieldValues = fieldValues.OrderBy(x => x.Name).ToList();
            int i = 0;
            foreach (var item in fieldValues)
            {
                item.Ord = i; i++;
            }
        }

    }


}