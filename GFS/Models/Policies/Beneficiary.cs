using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GFS.Models.Policies
{
    public class Beneficiary
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int beneficiaryNo { get; set; }

        [Required]
        [DisplayName("Holder Name:")]
        public string coveredby { get; set; }

        [Required]
        [DisplayName("ID Number:")]
        [StringLength(13, ErrorMessage = "ID No. Must Be 13 Digits Long", MinimumLength = 13)]
        [RegularExpression(@"^\d+$", ErrorMessage = "Please enter a valid ID No.")]
        public string idNo { get; set; }

        [Required]
        [DisplayName("First Name(s):")]
        public string firstName { get; set; }

        [Required]
        [DisplayName("Last Name:")]
        public string lastName { get; set; }

        [Required]
        [DisplayName("Relation to Member:")]
        public string relation { get; set; }

        [Required]
        [DisplayName("Percent Split:")]
        public double split { get; set; }

        [Required]
        [DisplayName("Covered Under:")]
        public string policyPlan { get; set; }

        //[NotMapped]
        //[DisplayName("Add Another Beneficiary?")]
        //public bool addAnotherBen { get; set; }

        [DisplayName("Policy No:")]
        public string policyNo { get; set; }
        public virtual ICollection<NewMember> NewMembers { get; set; }
    }
}