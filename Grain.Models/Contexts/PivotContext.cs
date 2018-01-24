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

        static public PivotShow GeneratePivotShowModel(GrainContext _db, int colId, int rowId, int dataId)
        {
            List<PivotHeaderElement> Columns;
            List<PivotHeaderElement> Rows;
            int ColumnsCount;

            db = _db;
            try
            {
                var col = HeaderFields[colId - 1];
                var row = HeaderFields[rowId - 1];
                var data = DataFields[dataId - HeaderFields.Length - 1];
                string sql = string.Format("select {0} as ColId, {1} as RowId, convert(decimal(18,2), sum({2})) as data from Farms group by {0},{1}", col.Field, row.Field, data.Field);
                var dataElements = db.Database.SqlQuery<PivotDataElement>(sql).ToList();

                Columns = (from id in (from item in dataElements select item.ColId).Distinct() select new PivotHeaderElement { Id = id }).ToList();
                Rows = (from id in (from item in dataElements select item.RowId).Distinct() select new PivotHeaderElement { Id = id }).ToList();
                FillHeader(ref Columns, colId);
                FillHeader(ref Rows, rowId);

                var dColumns = Columns.ToDictionary(x => x.Id);
                var dRows = Rows.ToDictionary(x => x.Id);
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
                PivotShow pivotShow = new Models.PivotShow();
                pivotShow.Columns = Columns;
                pivotShow.ColumnsCount = ColumnsCount;
                pivotShow.Rows = Rows;
                return pivotShow;
            }
            finally
            {
                db = null;
            }
        }

        static private void FillHeader(ref List<PivotHeaderElement> fieldValues, int field)
        {
            Func<int, string> get;
            if (field == 1) { get = x => db.Agricultures.Find(x).Name; }
            else { get = x => db.Regions.Find(x).Name; }
            foreach (var item in fieldValues)
            {
                item.Name = get(item.Id);
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