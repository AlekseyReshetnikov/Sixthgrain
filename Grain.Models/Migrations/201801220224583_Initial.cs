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
                        Name = c.String(nullable: false, maxLength: 100),
                        FarmerName = c.String(nullable: false, maxLength: 100),
                        RegionId = c.Int(nullable: false),
                        AgricultureId = c.Int(nullable: false),
                        HarvestLastYear = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Area = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Agricultures", t => t.AgricultureId, cascadeDelete: true)
                .ForeignKey("dbo.Regions", t => t.RegionId, cascadeDelete: true)
                .Index(t => t.RegionId)
                .Index(t => t.AgricultureId);
            
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
            DropIndex("dbo.Farms", new[] { "AgricultureId" });
            DropIndex("dbo.Farms", new[] { "RegionId" });
            DropTable("dbo.Regions");
            DropTable("dbo.Farms");
            DropTable("dbo.Agricultures");
        }
    }
}
