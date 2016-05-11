using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GFS.Models
{
    public class StockFile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int stockNumber { get; set; }

        [Required]
        [Display(Name = "Stock Code")]
        public string stockCode { get; set; }

        [Required]
        [Display(Name = "Category")]
        public string category { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string description { get; set; }

        [Display(Name = "Picture")]
        public string albumArtUrl { get; set; }

        [Required]
        [Display(Name = "Status")]
        public String status { get; set; }

        [Required]
        [Display(Name = "Quantity")]
        public int quantity { get; set; }

        [Required]
        [Display(Name = "Price Per Item")]
        public double pricePerItem { get; set; }

        [Required]
        [Display(Name = "Cost Price")]
        public double costPrice { get; set; }

        [Required]
        [Display(Name = "Selling Price")]
        public double sellingPrice { get; set; }
    }
}