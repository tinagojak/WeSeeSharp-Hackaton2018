namespace Hackaton2018.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBloodType : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BloodTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Symbol = c.String(),
                        MaxAmountStored = c.Int(nullable: false),
                        MinAmountStored = c.Int(nullable: false),
                        OptimalAmountStored = c.Int(nullable: false),
                        SpentAmountPerWeek = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BloodTypeGivesCompatibility",
                c => new
                    {
                        BloodTypeId = c.Int(nullable: false),
                        BloodTypeGivesId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BloodTypeId, t.BloodTypeGivesId })
                .ForeignKey("dbo.BloodTypes", t => t.BloodTypeId)
                .ForeignKey("dbo.BloodTypes", t => t.BloodTypeGivesId)
                .Index(t => t.BloodTypeId)
                .Index(t => t.BloodTypeGivesId);
            
            CreateTable(
                "dbo.BloodTypeTakesCompatibility",
                c => new
                    {
                        BloodTypeId = c.Int(nullable: false),
                        BloodTypeTakesId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BloodTypeId, t.BloodTypeTakesId })
                .ForeignKey("dbo.BloodTypes", t => t.BloodTypeId)
                .ForeignKey("dbo.BloodTypes", t => t.BloodTypeTakesId)
                .Index(t => t.BloodTypeId)
                .Index(t => t.BloodTypeTakesId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BloodTypeTakesCompatibility", "BloodTypeTakesId", "dbo.BloodTypes");
            DropForeignKey("dbo.BloodTypeTakesCompatibility", "BloodTypeId", "dbo.BloodTypes");
            DropForeignKey("dbo.BloodTypeGivesCompatibility", "BloodTypeGivesId", "dbo.BloodTypes");
            DropForeignKey("dbo.BloodTypeGivesCompatibility", "BloodTypeId", "dbo.BloodTypes");
            DropIndex("dbo.BloodTypeTakesCompatibility", new[] { "BloodTypeTakesId" });
            DropIndex("dbo.BloodTypeTakesCompatibility", new[] { "BloodTypeId" });
            DropIndex("dbo.BloodTypeGivesCompatibility", new[] { "BloodTypeGivesId" });
            DropIndex("dbo.BloodTypeGivesCompatibility", new[] { "BloodTypeId" });
            DropTable("dbo.BloodTypeTakesCompatibility");
            DropTable("dbo.BloodTypeGivesCompatibility");
            DropTable("dbo.BloodTypes");
        }
    }
}
