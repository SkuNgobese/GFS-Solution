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

        [DisplayName("Holder Name:")]
        public string payingFor { get; set; }

        [Required]
        [DisplayName("Payment Method")]
        public string paymentType { get; set; }

        [DisplayName("Initial Premium:")]
        [DisplayFormat(DataFormatString = "{0:f2}", ApplyFormatInEditMode = true)]
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

        [StringLength(10, ErrorMessage = "Cell No. Must Be 10 Digits Long", MinimumLength = 10)]
        [RegularExpression(@"^\d+$", ErrorMessage = "Please enter a valid Cell number")]
        [DisplayName("Contact Number:")]
        [DataType(DataType.PhoneNumber)]
        public string contactNo { get; set; }

        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Invalid email address.")]
        [DisplayName("Email Address:")]
        [DataType(DataType.EmailAddress)]
        public string payerEmail { get; set; }

        [DisplayName("Policy No:")]
        public string policyNo { get; set; }
        public virtual NewMember NewMembers { get; set; }

        
    }
}