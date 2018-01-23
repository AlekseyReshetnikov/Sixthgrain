using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grain.Models
{

    public class PivotShow
    {
        public List<PivotHeaderElement> Columns;
        public List<PivotHeaderElement> Rows;
        public int ColumnsCount;
    }

    public class PivotHeaderElement
    {
        public int Id;
        public String Name;
        public int Ord;
        public Decimal[] Data;
    }

    public class PivotDataElement
    {
        public int ColId { get; set; }
        public int RowId { get; set; }
        public Decimal Data { get; set; }
    }

}
