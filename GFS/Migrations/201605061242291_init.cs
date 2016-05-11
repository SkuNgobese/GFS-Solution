namespace GFS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        userid = c.String(nullable: false, maxLength: 13),
                        Id = c.Int(nullable: false),
                        firstname = c.String(nullable: false),
                        lastname = c.String(nullable: false),
                        CustEmail = c.String(nullable: false),
                        user = c.String(),
                        password = c.String(nullable: false, maxLength: 100),
                        ConfirmPassword = c.String(),
                        estatus = c.Boolean(nullable: false),
                        RememberMe = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.userid);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
        }
    }
}
