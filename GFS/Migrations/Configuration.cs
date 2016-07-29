namespace GFS.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using GFS.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<GFS.Models.GFSContext>
    {
        public Configuration()
        {
            AutomaticMigrationDataLossAllowed = true;
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(GFS.Models.GFSContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            context.Users.AddOrUpdate(x => x.userid,
            new User()
            {
                userid = "1234567891011",
                Id = 1,
                firstname = "Admin",
                lastname = "Admin",
                CustEmail = "admin@admin.com",
                user = "Admin",
                password = "123456",
                ConfirmPassword = "123456",
                estatus = true,
                RememberMe = false
            }
            );
        }
    }
}
