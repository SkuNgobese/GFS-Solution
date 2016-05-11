using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GFS.Models
{
    public class PolicyPlan
    {
        [Required]
        [DisplayName("Category")]
        public string category { get; set; }

        [Required]
        [Key]
        [DisplayName("Policy No.")]
        public string PolicyPlanNo { get; set; }

        [Required]
        [DisplayName("Policy ID")]
        public int policyID { get; set; }

        [Required]
        [DisplayName("Policy Type")]
        public string policyType { get; set; }

        [Required]
        [DisplayName("Minimum Age")]
        public int minAge { get; set; }

        [Required]
        [DisplayName("Maximum Age")]
        public int maxAge { get; set; }

        [Required]
        [DisplayName("Premium")]
        public double PlanPremium { get; set; }

        [Required]
        [DisplayName("Benefits Amount")]
        public double benefit { get; set; }

        [Required]
        [DisplayName("Cash Amount")]
        public double payout { get; set; }

        [Required]
        [DisplayName("Dependant Premium")]
        public double dependantPrem { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [DisplayName("Terms and Conditions")]
        public string TandCs { get; set; }
    }
}