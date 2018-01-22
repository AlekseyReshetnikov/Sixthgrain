using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Grain.Models
{
    public class Agriculture
    {
        public int Id { get; set; }
        [Display(Name="Агрокультура")]
        public string Name { get; set; }
    }
}