namespace Grain.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Agricultures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Farms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        FarmerName = c.String(),
                        HarvestLastYear = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Area = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AgricultureId = c.Int(),
                        RegionId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Agricultures", t => t.AgricultureId)
                .ForeignKey("dbo.Regions", t => t.RegionId)
                .Index(t => t.AgricultureId)
                .Index(t => t.RegionId);
            
            CreateTable(
                "dbo.Regions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Farms", "RegionId", "dbo.Regions");
            DropForeignKey("dbo.Farms", "AgricultureId", "dbo.Agricultures");
            DropIndex("dbo.Farms", new[] { "RegionId" });
            DropIndex("dbo.Farms", new[] { "AgricultureId" });
            DropTable("dbo.Regions");
            DropTable("dbo.Farms");
            DropTable("dbo.Agricultures");
        }
    }
}
