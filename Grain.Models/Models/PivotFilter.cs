using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grain.Models
{
    public class PivotFilter
    {

        public PivotFilter(int ColId, int RowId, int DataId)
        {
            this.ColId = ColId;
            this.RowId = RowId;
            this.DataId = DataId;
        }

        [Display(Name = "Колонка")]
        public int ColId { get; set; }
        [Display(Name = "Столбец")]
        public int RowId { get; set; }
        [Display(Name = "Данные")]
        public int DataId { get; set; }
    }

}

