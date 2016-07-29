namespace GFS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccTypes",
                c => new
                    {
                        typeId = c.Int(nullable: false, identity: true),
                        accNType = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.typeId);
            
            CreateTable(
                "dbo.ArchivedMembers",
                c => new
                    {
                        policyNo = c.String(nullable: false, maxLength: 128),
                        title = c.String(nullable: false),
                        fName = c.String(nullable: false),
                        lName = c.String(nullable: false),
                        IdNo = c.String(nullable: false, maxLength: 13),
                        dOb = c.DateTime(nullable: false),
                        age = c.Int(nullable: false),
                        gender = c.String(nullable: false),
                        maritalStat = c.String(nullable: false),
                        telNo = c.String(nullable: false, maxLength: 10),
                        cellNo = c.String(nullable: false, maxLength: 10),
                        CustEmail = c.String(nullable: false),
                        fascimileNo = c.String(maxLength: 10),
                        physicalAddress = c.String(),
                        postalAddress = c.String(nullable: false),
                        dateAdded = c.DateTime(nullable: false),
                        Policyplan = c.String(nullable: false),
                        Premium = c.Double(nullable: false),
                        Category = c.String(nullable: false),
                        dateArchived = c.DateTime(nullable: false),
                        reason = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.policyNo);
            
            CreateTable(
                "dbo.Banks",
                c => new
                    {
                        bankId = c.Int(nullable: false, identity: true),
                        bankN = c.String(nullable: false),
                        code = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.bankId);
            
            CreateTable(
                "dbo.Beneficiaries",
                c => new
                    {
                        beneficiaryNo = c.Int(nullable: false, identity: true),
                        coveredby = c.String(nullable: false),
                        idNo = c.String(nullable: false, maxLength: 13),
                        firstName = c.String(nullable: false),
                        lastName = c.String(nullable: false),
                        relation = c.String(nullable: false),
                        split = c.Double(nullable: false),
                        policyPlan = c.String(nullable: false),
                        policyNo = c.String(),
                    })
                .PrimaryKey(t => t.beneficiaryNo);
            
            CreateTable(
                "dbo.NewMembers",
                c => new
                    {
                        policyNo = c.String(nullable: false, maxLength: 128),
                        title = c.String(nullable: false),
                        fName = c.String(nullable: false),
                        lName = c.String(nullable: false),
                        IdNo = c.String(nullable: false, maxLength: 13),
                        dOb = c.DateTime(nullable: false),
                        gender = c.String(nullable: false),
                        maritalStat = c.String(nullable: false),
                        telNo = c.String(maxLength: 10),
                        cellNo = c.String(nullable: false, maxLength: 10),
                        CustEmail = c.String(),
                        fascimileNo = c.String(maxLength: 10),
                        physicalAddress = c.String(),
                        postalAddress = c.String(nullable: false),
                        dateAdded = c.DateTime(nullable: false),
                        Policyplan = c.String(nullable: false),
                        Premium = c.Double(nullable: false),
                        Category = c.String(nullable: false),
                        Branch = c.String(nullable: false),
                        SalesPerson = c.String(nullable: false),
                        capturedby = c.String(),
                        Active = c.Boolean(nullable: false),
                        Beneficiary_beneficiaryNo = c.Int(),
                        Deceased_deceasedNo = c.Int(),
                        Dependant_depNo = c.Int(),
                    })
                .PrimaryKey(t => t.policyNo)
                .ForeignKey("dbo.Beneficiaries", t => t.Beneficiary_beneficiaryNo)
                .ForeignKey("dbo.Deceaseds", t => t.Deceased_deceasedNo)
                .ForeignKey("dbo.Dependants", t => t.Dependant_depNo)
                .Index(t => t.Beneficiary_beneficiaryNo)
                .Index(t => t.Deceased_deceasedNo)
                .Index(t => t.Dependant_depNo);
            
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        referenceNo = c.Int(nullable: false, identity: true),
                        policyNo = c.String(nullable: false, maxLength: 128),
                        CustomerName = c.String(nullable: false),
                        plan = c.String(nullable: false),
                        dueAmount = c.Double(nullable: false),
                        amount = c.Double(nullable: false),
                        outstandingAmount = c.Double(nullable: false),
                        datePayed = c.DateTime(nullable: false),
                        cashierName = c.String(nullable: false),
                        branch = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.referenceNo)
                .ForeignKey("dbo.NewMembers", t => t.policyNo, cascadeDelete: true)
                .Index(t => t.policyNo);
            
            CreateTable(
                "dbo.PolicyPlans",
                c => new
                    {
                        PolicyPlanNo = c.String(nullable: false, maxLength: 128),
                        category = c.String(nullable: false),
                        policyID = c.Int(nullable: false),
                        policyType = c.String(nullable: false),
                        minAge = c.Int(nullable: false),
                        maxAge = c.Int(nullable: false),
                        PlanPremium = c.Double(nullable: false),
                        benefit = c.Double(nullable: false),
                        payout = c.Double(nullable: false),
                        dependantPrem = c.Double(nullable: false),
                        TandCs = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.PolicyPlanNo);
            
            CreateTable(
                "dbo.Branches",
                c => new
                    {
                        branchId = c.Int(nullable: false, identity: true),
                        branchN = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.branchId);
            
            CreateTable(
                "dbo.DebitOrderAuthorizations",
                c => new
                    {
                        debitAuthNo = c.Int(nullable: false, identity: true),
                        bankName = c.String(),
                        accNo = c.String(),
                        branchcode = c.String(),
                        accountType = c.String(),
                        commenceDate = c.DateTime(nullable: false),
                        amount = c.Double(nullable: false),
                        policyNo = c.String(maxLength: 128),
                        payerNo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.debitAuthNo)
                .ForeignKey("dbo.NewMembers", t => t.policyNo)
                .ForeignKey("dbo.Payers", t => t.payerNo, cascadeDelete: true)
                .Index(t => t.policyNo)
                .Index(t => t.payerNo);
            
            CreateTable(
                "dbo.Payers",
                c => new
                    {
                        payerNo = c.Int(nullable: false, identity: true),
                        payingFor = c.String(),
                        paymentType = c.String(nullable: false),
                        initialPremium = c.Double(nullable: false),
                        firstName = c.String(nullable: false),
                        lastName = c.String(nullable: false),
                        idNo = c.String(maxLength: 13),
                        relation = c.String(),
                        contactNo = c.String(maxLength: 10),
                        payerEmail = c.String(),
                        policyNo = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.payerNo)
                .ForeignKey("dbo.NewMembers", t => t.policyNo)
                .Index(t => t.policyNo);
            
            CreateTable(
                "dbo.Deceaseds",
                c => new
                    {
                        deceasedNo = c.Int(nullable: false, identity: true),
                        firstName = c.String(nullable: false),
                        lastName = c.String(nullable: false),
                        idNo = c.String(nullable: false),
                        age = c.Int(nullable: false),
                        placeofdeath = c.String(nullable: false),
                        causeOfDeath = c.String(nullable: false),
                        DateOfDeath = c.DateTime(nullable: false),
                        policyNo = c.String(),
                    })
                .PrimaryKey(t => t.deceasedNo);
            
            CreateTable(
                "dbo.FileDetails",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FileName = c.String(),
                        Extension = c.String(),
                        deceasedN = c.Int(nullable: false),
                        Deceased_deceasedNo = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Deceaseds", t => t.Deceased_deceasedNo)
                .Index(t => t.Deceased_deceasedNo);
            
            CreateTable(
                "dbo.Dependants",
                c => new
                    {
                        depNo = c.Int(nullable: false, identity: true),
                        coveredby = c.String(),
                        fName = c.String(nullable: false),
                        lName = c.String(nullable: false),
                        IdNo = c.String(nullable: false, maxLength: 13),
                        dOb = c.DateTime(nullable: false),
                        age = c.Int(nullable: false),
                        relationship = c.String(nullable: false),
                        amount = c.Double(nullable: false),
                        policyPlan = c.String(nullable: false),
                        policyNo = c.String(),
                    })
                .PrimaryKey(t => t.depNo);
            
            CreateTable(
                "dbo.JoiningFees",
                c => new
                    {
                        refNo = c.Int(nullable: false, identity: true),
                        policyNo = c.String(nullable: false),
                        CustomerName = c.String(),
                        AmountRendered = c.Double(nullable: false),
                        change = c.Double(nullable: false),
                        Fee = c.Double(nullable: false),
                        date = c.DateTime(nullable: false),
                        cashierName = c.String(),
                        branch = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.refNo);
            
            CreateTable(
                "dbo.LoginViewModels",
                c => new
                    {
                        UserName = c.String(nullable: false, maxLength: 128),
                        Password = c.String(nullable: false),
                        RememberMe = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserName);
            
            CreateTable(
                "dbo.Plan_Type",
                c => new
                    {
                        planId = c.Int(nullable: false, identity: true),
                        plan = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.planId);
            
            CreateTable(
                "dbo.Relations",
                c => new
                    {
                        relationNo = c.Int(nullable: false, identity: true),
                        relationsh = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.relationNo);
            
            CreateTable(
                "dbo.SalesPersons",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        fName = c.String(nullable: false),
                        lName = c.String(nullable: false),
                        IdNo = c.String(nullable: false, maxLength: 13),
                        telNo = c.String(maxLength: 10),
                        cellNo = c.String(maxLength: 10),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.StockCategories",
                c => new
                    {
                        stockCatNo = c.Int(nullable: false, identity: true),
                        category = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.stockCatNo);
            
            CreateTable(
                "dbo.StockFiles",
                c => new
                    {
                        stockNumber = c.Int(nullable: false, identity: true),
                        stockCode = c.String(nullable: false),
                        category = c.String(nullable: false),
                        description = c.String(nullable: false),
                        albumArtUrl = c.String(),
                        status = c.String(nullable: false),
                        quantity = c.Int(nullable: false),
                        pricePerItem = c.Double(nullable: false),
                        costPrice = c.Double(nullable: false),
                        sellingPrice = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.stockNumber);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        userid = c.String(nullable: false, maxLength: 13),
                        Id = c.Int(nullable: false),
                        firstname = c.String(nullable: false),
                        lastname = c.String(nullable: false),
                        CustEmail = c.String(nullable: false),
                        user = c.String(nullable: false),
                        password = c.String(nullable: false, maxLength: 100),
                        ConfirmPassword = c.String(),
                        estatus = c.Boolean(nullable: false),
                        RememberMe = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.userid);
            
            CreateTable(
                "dbo.PolicyPlanNewMembers",
                c => new
                    {
                        PolicyPlan_PolicyPlanNo = c.String(nullable: false, maxLength: 128),
                        NewMember_policyNo = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.PolicyPlan_PolicyPlanNo, t.NewMember_policyNo })
                .ForeignKey("dbo.PolicyPlans", t => t.PolicyPlan_PolicyPlanNo, cascadeDelete: true)
                .ForeignKey("dbo.NewMembers", t => t.NewMember_policyNo, cascadeDelete: true)
                .Index(t => t.PolicyPlan_PolicyPlanNo)
                .Index(t => t.NewMember_policyNo);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NewMembers", "Dependant_depNo", "dbo.Dependants");
            DropForeignKey("dbo.NewMembers", "Deceased_deceasedNo", "dbo.Deceaseds");
            DropForeignKey("dbo.FileDetails", "Deceased_deceasedNo", "dbo.Deceaseds");
            DropForeignKey("dbo.DebitOrderAuthorizations", "payerNo", "dbo.Payers");
            DropForeignKey("dbo.Payers", "policyNo", "dbo.NewMembers");
            DropForeignKey("dbo.DebitOrderAuthorizations", "policyNo", "dbo.NewMembers");
            DropForeignKey("dbo.NewMembers", "Beneficiary_beneficiaryNo", "dbo.Beneficiaries");
            DropForeignKey("dbo.PolicyPlanNewMembers", "NewMember_policyNo", "dbo.NewMembers");
            DropForeignKey("dbo.PolicyPlanNewMembers", "PolicyPlan_PolicyPlanNo", "dbo.PolicyPlans");
            DropForeignKey("dbo.Payments", "policyNo", "dbo.NewMembers");
            DropIndex("dbo.PolicyPlanNewMembers", new[] { "NewMember_policyNo" });
            DropIndex("dbo.PolicyPlanNewMembers", new[] { "PolicyPlan_PolicyPlanNo" });
            DropIndex("dbo.FileDetails", new[] { "Deceased_deceasedNo" });
            DropIndex("dbo.Payers", new[] { "policyNo" });
            DropIndex("dbo.DebitOrderAuthorizations", new[] { "payerNo" });
            DropIndex("dbo.DebitOrderAuthorizations", new[] { "policyNo" });
            DropIndex("dbo.Payments", new[] { "policyNo" });
            DropIndex("dbo.NewMembers", new[] { "Dependant_depNo" });
            DropIndex("dbo.NewMembers", new[] { "Deceased_deceasedNo" });
            DropIndex("dbo.NewMembers", new[] { "Beneficiary_beneficiaryNo" });
            DropTable("dbo.PolicyPlanNewMembers");
            DropTable("dbo.Users");
            DropTable("dbo.StockFiles");
            DropTable("dbo.StockCategories");
            DropTable("dbo.SalesPersons");
            DropTable("dbo.Relations");
            DropTable("dbo.Plan_Type");
            DropTable("dbo.LoginViewModels");
            DropTable("dbo.JoiningFees");
            DropTable("dbo.Dependants");
            DropTable("dbo.FileDetails");
            DropTable("dbo.Deceaseds");
            DropTable("dbo.Payers");
            DropTable("dbo.DebitOrderAuthorizations");
            DropTable("dbo.Branches");
            DropTable("dbo.PolicyPlans");
            DropTable("dbo.Payments");
            DropTable("dbo.NewMembers");
            DropTable("dbo.Beneficiaries");
            DropTable("dbo.Banks");
            DropTable("dbo.ArchivedMembers");
            DropTable("dbo.AccTypes");
        }
    }
}
