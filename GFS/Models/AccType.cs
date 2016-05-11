using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GFS.Models
{
    public class AccType
    {
        [Key]
        public int typeId { get; set; }

        [Required]
        [DisplayName("Account Type")]
        public string accNType { get; set; }
    }
}