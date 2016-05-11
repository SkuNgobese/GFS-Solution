using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GFS.Models
{
    public class Relation
    {
        [Key]
        public int relationNo { get; set; }

        [Required]
        [DisplayName("Relationship")]
        public string relationsh { get; set; }
    }
}