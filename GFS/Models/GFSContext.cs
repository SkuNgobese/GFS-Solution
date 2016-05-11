using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GFS.Models
{
    public class GFSContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public GFSContext() : base("name=GFSContext")
        {
        }

        public System.Data.Entity.DbSet<GFS.Models.User> Users { get; set; }

        public System.Data.Entity.DbSet<GFS.Models.Policies.NewMember> NewMembers { get; set; }

        public System.Data.Entity.DbSet<GFS.Models.PolicyPlan> PolicyPlans { get; set; }

        public System.Data.Entity.DbSet<GFS.Models.Policies.Dependant> Dependants { get; set; }

        public System.Data.Entity.DbSet<GFS.Models.Relation> Relations { get; set; }

        public System.Data.Entity.DbSet<GFS.Models.Policies.Beneficiary> Beneficiaries { get; set; }

        public System.Data.Entity.DbSet<GFS.Models.Policies.Payer> Payers { get; set; }

        public System.Data.Entity.DbSet<GFS.Models.Bank> Banks { get; set; }

        public System.Data.Entity.DbSet<GFS.Models.AccType> AccTypes { get; set; }

        public System.Data.Entity.DbSet<GFS.Models.Policies.DebitOrderAuthorization> DebitOrderAuthorizations { get; set; }

        public System.Data.Entity.DbSet<GFS.Models.Policies.ArchivedMember> ArchivedMembers { get; set; }

        public System.Data.Entity.DbSet<GFS.Models.Policies.Deceased> Deceaseds { get; set; }

        public System.Data.Entity.DbSet<GFS.Models.StockFile> StockFiles { get; set; }

        public System.Data.Entity.DbSet<GFS.Models.StockCategory> StockCategories { get; set; }

        public System.Data.Entity.DbSet<GFS.Models.Payment> Payments { get; set; }
    
    }
}
