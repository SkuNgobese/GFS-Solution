using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GFS.Models.Policies;

namespace GFS.Models
{
    public class JoiningFee
    {
        [Key]
        [DisplayName("Reference Number:")]
        public int refNo { get; set; }

        [Required]
        [DisplayName("Policy No:")]
        public string policyNo { get; set; }
        [DisplayName("Customer Name:")]
        public string CustomerName { get; set; }

        [DisplayName("Amount Rendered (R):")]
        [DisplayFormat(DataFormatString = "{0:f2}", ApplyFormatInEditMode = true)]
        public double AmountRendered { get; set; }

        [Required]
        [DisplayName("Change (R):")]
        [DisplayFormat(DataFormatString = "{0:f2}", ApplyFormatInEditMode = true)]
        public double change { get; set; }

        [Required]
        [DisplayName("Fee (R):")]
        [DisplayFormat(DataFormatString = "{0:f2}", ApplyFormatInEditMode = true)]
        public double Fee { get; set; }

        [Required]
        [DisplayName("Date:")]
        [DataType(DataType.DateTime), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime date { get; set; }
        [DisplayName("Captured By:")]
        public string cashierName { get; set; }

        [Required]
        [DisplayName("Branch:")]
        public string branch { get; set; }
    }
}