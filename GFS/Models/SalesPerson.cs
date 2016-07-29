using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GFS.Models
{
    public class SalesPerson
    {
        [Key]
        public int ID { get; set; }

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
        public string Email { get; set; }
    }
}