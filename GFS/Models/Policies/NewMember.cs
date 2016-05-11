using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GFS.Models.Policies
{
    public class NewMember
    {
        [Key]
        [Required]
        [DisplayName("Policy No:")]
        public string policyNo { get; set; }

        [Required]
        [DisplayName("Title:")]
        public string title { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z]* {0,1}[a-zA-Z]*$", ErrorMessage = "Invalid First Name(s)")]
        [DisplayName("First Name(s):")]
        public string fName { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z]* {0,1}[a-zA-Z]*$", ErrorMessage = "Invalid Last Name")]
        [DisplayName("Last Name:")]
        public string lName { get; set; }

        [DisplayName("ID Number:")]
        [StringLength(13, ErrorMessage = "ID No. Must Be 13 Digits Long", MinimumLength = 13)]
        [RegularExpression(@"^\d+$", ErrorMessage = "Please enter valid ID No.")]
        public string IdNo { get; set; }

        [DisplayName("Date of Birth:")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        //[RegularExpression("dd / MM / yyyy", ErrorMessage = "Date is dd-MM-yyyy.")]
        public DateTime dOb { get; set; }

        [Required]
        [DisplayName("Age:")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Please enter valid Age")]
        public int age { get; set; }

        [Required]
        [DisplayName("Gender:")]
        public string gender { get; set; }

        [Required]
        [DisplayName("Marital Status:")]
        public string maritalStat { get; set; }

        [StringLength(10, ErrorMessage = "Telephone No. Must Be 10 Digits Long", MinimumLength = 10)]
        [RegularExpression(@"^\d+$", ErrorMessage = "Please enter a valid telephone number")]
        [DisplayName("Telephone Number:")]
        [DataType(DataType.PhoneNumber)]
        public string telNo { get; set; }

        [StringLength(10, ErrorMessage = "Cell No. Must Be 10 Digits Long", MinimumLength = 10)]
        [RegularExpression(@"^\d+$", ErrorMessage = "Please enter a valid Cell number")]
        [DisplayName("Cell Number:")]
        [DataType(DataType.PhoneNumber)]
        public string cellNo { get; set; }

        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Invalid email address.")]
        [DisplayName("Email Address:")]
        [DataType(DataType.EmailAddress)]
        public string CustEmail { get; set; }

        [StringLength(10, ErrorMessage = "Fescimile No. Must Be 10 Digits Long", MinimumLength = 10)]
        [RegularExpression(@"^\d+$", ErrorMessage = "Please enter a valid Fascimile number")]
        [DisplayName("Fascimile No:")]
        public string fascimileNo { get; set; }

        [DisplayName("Physical Address:")]
        [DataType(DataType.PostalCode)]
        public string physicalAddress { get; set; }

        [DisplayName("Postal Address:")]
        [DataType(DataType.PostalCode)]
        public string postalAddress { get; set; }

        [Required]
        [DisplayName("Date Added:")]
        [DataType(DataType.DateTime), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime dateAdded { get; set; }

        [Required]
        [DisplayName("Policy Plan:")]
        public string Policyplan { get; set; }

        [Required]
        [DisplayName("Premium:")]
        public double Premium { get; set; }

        [Required]
        [DisplayName("Category:")]
        public string Category { get; set; }

        [NotMapped]
        public string PolicyPlanNo { get; set; }
        public virtual ICollection<PolicyPlan> PolicyPlans { get; set; }

        [NotMapped]
        [DisplayName("Add Dependant?:")]
        public bool addDep { get; set; }

        [NotMapped]
        [DisplayName("Member Paying?:")]
        public bool paying { get; set; }
    }
}