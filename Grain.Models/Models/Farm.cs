using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Grain.Models
{
    public class Farm
    {
        public int Id { get; set; }

        [Display(Name = "Наименование")]
        public string Name { get; set; }

        [Display(Name = "Имя фермера")]
        public string FarmerName { get; set; }

        public int? RegionId { get; set; }
        public virtual Region Region { get; set; }

        public int? AgricultureId { get; set; }
        public virtual Agriculture Agriculture { get; set; }

        [Display(Name = "Урожай в тоннах за прошлый год")]
        public decimal HarvestLastYear { get; set; }

        [Display(Name = "Площадь фермы")]
        public decimal Area { get; set; }


    }
}