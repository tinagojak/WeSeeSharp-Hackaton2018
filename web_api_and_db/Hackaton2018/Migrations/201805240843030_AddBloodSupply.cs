namespace Hackaton2018.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBloodSupply : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BloodSupplies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BloodTypeId = c.Int(nullable: false),
                        AmountStored = c.Int(nullable: false),
                        SupplyDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BloodTypes", t => t.BloodTypeId, cascadeDelete: true)
                .Index(t => t.BloodTypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BloodSupplies", "BloodTypeId", "dbo.BloodTypes");
            DropIndex("dbo.BloodSupplies", new[] { "BloodTypeId" });
            DropTable("dbo.BloodSupplies");
        }
    }
}
