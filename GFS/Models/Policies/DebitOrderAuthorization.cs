using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GFS.Models.Policies
{
    public class DebitOrderAuthorization
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int debitAuthNo { get; set; }

        [Required]
        [DisplayName("Commence Date:")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime commenceDate { get; set; }

        [Required]
        [DisplayName("Amount:")]
        public double amount { get; set; }

        [DisplayName("Policy No:")]
        public string policyNo { get; set; }
        public virtual NewMember NewMembers { get; set; }

        [DisplayName("Payer No:")]
        public int payerNo { get; set; }
        public virtual Payer Payers { get; set; }
    }
}