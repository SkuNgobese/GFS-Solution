using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GFS.Models
{
    public class StockCategory
    {
        [Key]
        public int stockCatNo { get; set; }

        [Required]
        [DisplayName("Category")]
        public string category { get; set; }
    }
}