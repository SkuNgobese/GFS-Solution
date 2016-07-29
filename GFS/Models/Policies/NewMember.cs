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
        public NewMember() { }
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

        [Required]
        [DisplayName("ID Number:")]
        [StringLength(13, ErrorMessage = "ID No. Must Be 13 Digits Long", MinimumLength = 13)]
        [RegularExpression(@"^\d+$", ErrorMessage = "Please enter valid ID No.")]
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

        [Required]
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

        [Required]
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
        [DisplayFormat(DataFormatString = "{0:f2}", ApplyFormatInEditMode = true)]
        public double Premium { get; set; }

        [Required]
        [DisplayName("Category:")]
        public string Category { get; set; }

        [Required]
        [DisplayName("Branch:")]
        public string Branch { get; set; }

        [Required]
        [DisplayName("Sales Person:")]
        public string SalesPerson { get; set; }

        [DisplayName("Captured By:")]
        public string capturedby { get; set; }

        [NotMapped]
        public string PolicyPlanNo { get; set; }

        [NotMapped]
        [DisplayName("Add Dependant?:")]
        public bool addDep { get; set; }

        [NotMapped]
        [DisplayName("Member Paying?:")]
        public bool paying { get; set; }

        [DisplayName("Policy status:")]
        public bool Active { get; set; }

        public virtual ICollection<PolicyPlan> PolicyPlans { get; set; }
        public virtual ICollection<Payment> payments { get; set; }
    }
}