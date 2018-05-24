namespace Hackaton2018.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLocationAndBloodTypeToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Address", c => c.String());
            AddColumn("dbo.AspNetUsers", "BloodTypeString", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "BloodTypeString");
            DropColumn("dbo.AspNetUsers", "Address");
        }
    }
}
