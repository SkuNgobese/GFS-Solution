using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GFS.Models.Policies
{
    public class Dependant
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int depNo { get; set; }

        [DisplayName("Holder Name:")]
        public string coveredby { get; set; }

        [Required]
        [DisplayName("First Name(s):")]
        public string fName { get; set; }

        [Required]
        [DisplayName("Last Name:")]
        public string lName { get; set; }

        [Required]
        [DisplayName("ID Number:")]
        [StringLength(13, ErrorMessage = "ID No. Must Be 13 Digits Long", MinimumLength = 13)]
        [RegularExpression(@"^\d+$", ErrorMessage = "Please enter a valid ID No.")]
        public string IdNo { get; set; }

        [Required]
        [DisplayName("Date of Birth:")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime dOb { get; set; }

        //[Required]
        //[DisplayName("Age:")]
        //[RegularExpression(@"^\d+$", ErrorMessage = "Please enter valid Age")]
        //public int age { get; set; }

        [Required]
        [DisplayName("Relation To Member:")]
        public string relationship { get; set; }

        [Required]
        [DisplayName("Amount Added:")]
        [DisplayFormat(DataFormatString = "{0:f2}", ApplyFormatInEditMode = true)]
        public double amount { get; set; }

        [Required]
        [DisplayName("Covered Under:")]
        public string policyPlan { get; set; }

        [NotMapped]
        [DisplayName("As Beneficiary?")]
        public bool asBeneficiary { get; set; }

        [NotMapped]
        [DisplayName("Add Another Dependant?")]
        public bool addAnotherDep { get; set; }

        [DisplayName("Policy No:")]
        public string policyNo { get; set; }
        public virtual ICollection<NewMember> NewMembers { get; set; }
    }
}