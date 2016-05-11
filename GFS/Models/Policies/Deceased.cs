using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GFS.Models.Policies
{
    public class Deceased
    {
        [Required]
        [Key]
        public int deceasedNo { get; set; }

        [Required]
        [DisplayName("First Name(s):")]
        public string firstName { get; set; }

        [Required]
        [DisplayName("Last Name:")]
        public string lastName { get; set; }

        [NotMapped]
        public string albumArtUrl { get; set; }

        [Required]
        [DisplayName("ID Number:")]
        public string idNo { get; set; }

        [Required]
        [DisplayName("Age:")]
        public int age { get; set; }

        [Required]
        [DisplayName("Gender:")]
        public string gender { get; set; }

        [Required]
        [DisplayName("Cause Of Death:")]
        public string causeOfDeath { get; set; }

        [Required]
        [DisplayName("Date Of Death:")]
        [DataType(DataType.DateTime), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfDeath { get; set; }

        [DisplayName("Policy No:")]
        public int policyNo { get; set; }
        public virtual ICollection<NewMember> NewMembers { get; set; }
    }
}