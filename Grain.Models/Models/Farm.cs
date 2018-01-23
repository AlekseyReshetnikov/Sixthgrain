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
        [Required]
        [MinLength(2, ErrorMessage = "Короткая строка")]
        [MaxLength(100, ErrorMessage = "Длинная строка")]
        public string Name { get; set; }

        [Display(Name = "Имя фермера")]
        [Required]
        [MinLength(2, ErrorMessage = "Короткая строка")]
        [MaxLength(100, ErrorMessage = "Длинная строка")]
        public string FarmerName { get; set; }

        [Required]
        public int? RegionId { get; set; }
        public virtual Region Region { get; set; }

        [Required]
        public int? AgricultureId { get; set; }
        public virtual Agriculture Agriculture { get; set; }

        [Display(Name = "Урожай в тоннах за прошлый год")]
        [Required]
        public decimal HarvestLastYear { get; set; }

        [Display(Name = "Площадь фермы")]
        [Required]
        public decimal Area { get; set; }


    }
}