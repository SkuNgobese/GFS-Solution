using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace GFS.Models
{
    public class User
    {
        [Key]
        [Required]
        [DisplayName("ID Number:")]
        [StringLength(13, ErrorMessage = "ID No. Must Be 13 Digits Long", MinimumLength = 13)]
        [RegularExpression(@"^\d+$", ErrorMessage = "Please enter a valid ID No.")]
        public string userid { get; set; }

        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z]* {0,1}[a-zA-Z]*$", ErrorMessage = "Invalid First Name(s)")]
        [DisplayName("First Name(s):")]
        public string firstname { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z]* {0,1}[a-zA-Z]*$", ErrorMessage = "Invalid Last Name")]
        [DisplayName("Last Name:")]
        public string lastname { get; set; }

        [Required]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Invalid email address.")]
        [DisplayName("Email Address:")]
        [DataType(DataType.EmailAddress)]
        public string CustEmail { get; set; }

        [Required]
        [DisplayName("User Role:")]
        public string user { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [DisplayName("Password:")]
        public string password { get; set; }

        [Compare("password", ErrorMessage = "The password and confirmation password do not match.")]
        [DataType(DataType.Password)]
        [DisplayName("Confirm Password:")]
        public string ConfirmPassword { get; set; }

        public bool estatus { get; set; }

        public bool RememberMe { get; set; }
    }
}