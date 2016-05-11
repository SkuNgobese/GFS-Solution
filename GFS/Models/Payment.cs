using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GFS.Models
{
    public class Payment
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
        public double dueAmount { get; set; }

        [Required]
        [DisplayName("Amount Paying(R):")]
        public double amount { get; set; }

        [DisplayName("Amount Outstanding (R):")]
        public double outstandingAmount { get; set; }

        [Required]
        [DisplayName("Date:")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime datePayed { get; set; }

        [Required]
        [DisplayName("Captured By:")]
        public string cashierName { get; set; }

        [Required]
        [DisplayName("Branch:")]
        public string branch { get; set; }
    }
}