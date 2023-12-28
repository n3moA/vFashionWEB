namespace vFashionWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Something2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Colors", "SanPham_Masp", c => c.Int());
            AddColumn("dbo.SanPham", "LoaiSP", c => c.Int());
            AddColumn("dbo.Sizes", "SanPham_Masp", c => c.Int());
            CreateIndex("dbo.Colors", "SanPham_Masp");
            CreateIndex("dbo.Sizes", "SanPham_Masp");
            AddForeignKey("dbo.Colors", "SanPham_Masp", "dbo.SanPham", "Masp");
            AddForeignKey("dbo.Sizes", "SanPham_Masp", "dbo.SanPham", "Masp");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SanPham", "Size", c => c.Int());
            DropForeignKey("dbo.Sizes", "SanPham_Masp", "dbo.SanPham");
            DropForeignKey("dbo.Colors", "SanPham_Masp", "dbo.SanPham");
            DropIndex("dbo.Sizes", new[] { "SanPham_Masp" });
            DropIndex("dbo.Colors", new[] { "SanPham_Masp" });
            DropColumn("dbo.Sizes", "SanPham_Masp");
            DropColumn("dbo.SanPham", "LoaiSP");
            DropColumn("dbo.Colors", "SanPham_Masp");
        }
    }
}
