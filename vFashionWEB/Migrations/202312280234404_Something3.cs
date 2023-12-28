namespace vFashionWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Something3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MauSPs",
                c => new
                    {
                        Masp = c.Int(nullable: false, identity: true),
                        MaMau = c.Int(nullable: false),
                        TenMau = c.String(),
                    })
                .PrimaryKey(t => t.Masp);
            
            CreateTable(
                "dbo.SizeSPs",
                c => new
                    {
                        Masp = c.Int(nullable: false, identity: true),
                        MaSize = c.Int(nullable: false),
                        TenSize = c.String(),
                    })
                .PrimaryKey(t => t.Masp);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SizeSPs");
            DropTable("dbo.MauSPs");
        }
    }
}
