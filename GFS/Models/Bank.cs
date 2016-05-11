using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GFS.Models
{
    public class Bank
    {
        [Key]
        public int bankId { get; set; }

        [Required]
        [DisplayName("Bank")]
        public string bankN { get; set; }
    }
}