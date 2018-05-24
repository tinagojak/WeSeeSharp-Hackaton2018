namespace Hackaton2018.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BloodRecordDonorDateToDateNull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BloodDonorRecords", "Date", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BloodDonorRecords", "Date", c => c.DateTime(nullable: false));
        }
    }
}
