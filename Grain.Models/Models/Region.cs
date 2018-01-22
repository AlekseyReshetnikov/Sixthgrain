using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Grain.Models
{
    [Table("Regions")]
    public class Region
    {
        public int Id { get; set; }
        [Display(Name = "Регион")]
        public string Name { get; set; }
    }
}