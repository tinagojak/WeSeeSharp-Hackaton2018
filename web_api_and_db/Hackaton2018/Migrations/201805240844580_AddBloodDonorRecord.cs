namespace Hackaton2018.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBloodDonorRecord : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BloodDonorRecords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LocationId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Locations", t => t.LocationId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.LocationId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BloodDonorRecords", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.BloodDonorRecords", "LocationId", "dbo.Locations");
            DropIndex("dbo.BloodDonorRecords", new[] { "UserId" });
            DropIndex("dbo.BloodDonorRecords", new[] { "LocationId" });
            DropTable("dbo.BloodDonorRecords");
        }
    }
}
