using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GFS.Models.Policies;

namespace GFS.Models
{
    public class Payment
    {
        public Payment() { }
        [Key]
        [DisplayName("Reference Number:")]
        public int referenceNo { get; set; }

        [Required]
        [DisplayName("Policy No:")]
        public string policyNo { get; set; }

        [Required]
        [DisplayName("Customer Name:")]
        public string CustomerName { get; set; }

        [Required]
        [DisplayName("Policy Plan:")]
        public string plan { get; set; }

        [DisplayName("Due Amount (R):")]
        [DisplayFormat(DataFormatString ="{0:f2}", ApplyFormatInEditMode =true)]
        public double dueAmount { get; set; }

        [Required]
        [DisplayName("Amount (R):")]
        [DisplayFormat(DataFormatString = "{0:f2}", ApplyFormatInEditMode = true)]
        public double amount { get; set; }

        [DisplayName("Amount Outstanding (R):")]
        [DisplayFormat(DataFormatString = "{0:f2}", ApplyFormatInEditMode = true)]
        public double outstandingAmount { get; set; }

        [Required]
        [DisplayName("Date:")]
        [DataType(DataType.DateTime), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime datePayed { get; set; }

        [Required]
        [DisplayName("Captured By:")]
        public string cashierName { get; set; }

        [Required]
        [DisplayName("Branch:")]
        public string branch { get; set; }

        [NotMapped]
        [DisplayName("Send Slip By Email?:")]
        public bool emailSlip { get; set; }

        public virtual NewMember principals { get; set; }
    }
}