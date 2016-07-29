using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GFS.Models.Policies
{
    public class FileDetail
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
        public int deceasedN { get; set; }
        public virtual Deceased Deceased { get; set; }
    }
}