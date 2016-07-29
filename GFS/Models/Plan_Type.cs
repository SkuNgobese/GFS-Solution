using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GFS.Models
{
    public class Plan_Type
    {
        [Key]
        public int planId { get; set; }

        [Required]
        [DisplayName("Plan")]
        public string plan { get; set; }
    }
}