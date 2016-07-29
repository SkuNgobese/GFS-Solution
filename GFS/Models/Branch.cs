using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GFS.Models
{
    public class Branch
    {
        [Key]
        public int branchId { get; set; }

        [Required]
        [DisplayName("Branch Name")]
        public string branchN { get; set; }
    }
}