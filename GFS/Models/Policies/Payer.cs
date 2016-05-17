using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GFS.Models.Policies
{
    public class Payer
    {
        [Key]
        public int payerNo { get; set; }

        [Required]
        [DisplayName("Payment Method")]
        public string paymentType { get; set; }

        [DisplayName("Initial Premium:")]
        public double initialPremium { get; set; }

        [Required]
        [DisplayName("First Name")]
        public string firstName { get; set; }

        [Required]
        [DisplayName("Last Name")]
        public string lastName { get; set; }

        [DisplayName("ID Number")]
        [StringLength(13, ErrorMessage = "ID No. Must Be 13 Digits Long", MinimumLength = 13)]
        [RegularExpression(@"^\d+$", ErrorMessage = "Please enter a valid ID No.")]
        public string idNo { get; set; }

        [DisplayName("Relation to Member")]
        public string relation { get; set; }

        [DisplayName("Bank Name")]
        public string bankName { get; set; }

        [DisplayName("Account Number")]
        public string accNo { get; set; }

        [DisplayName("Branch Code")]
        public string branchcode { get; set; }

        [DisplayName("Branch Name")]
        public string branchName { get; set; }

        [DisplayName("Type of Account")]
        public string accountType { get; set; }

        [DisplayName("Policy No:")]
        public string policyNo { get; set; }
        public virtual NewMember NewMembers { get; set; }

        
    }
}