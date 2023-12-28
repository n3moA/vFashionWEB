namespace vFashionWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Something1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Colors",
                c => new
                    {
                        MaMau = c.Int(nullable: false, identity: true),
                        TenMau = c.String(),
                    })
                .PrimaryKey(t => t.MaMau);
            
            //CreateTable(
            //    "dbo.CTDonHang",
            //    c => new
            //        {
            //            Madon = c.Int(nullable: false),
            //            Masp = c.Int(nullable: false),
            //            Soluong = c.Int(),
            //            Dongia = c.Decimal(precision: 18, scale: 0),
            //            Thanhtien = c.Decimal(precision: 18, scale: 0),
            //        })
            //    .PrimaryKey(t => new { t.Madon, t.Masp })
            //    .ForeignKey("dbo.DonHang", t => t.Madon)
            //    .ForeignKey("dbo.SanPham", t => t.Masp)
            //    .Index(t => t.Madon)
            //    .Index(t => t.Masp);
            
            //CreateTable(
            //    "dbo.DonHang",
            //    c => new
            //        {
            //            Madon = c.Int(nullable: false, identity: true),
            //            Ngaydat = c.DateTime(),
            //            Ngaygiao = c.DateTime(),
            //            Tinhtrang = c.Int(),
            //            Tinhtrangthanhtoan = c.Int(),
            //            MaNguoidung = c.Int(),
            //            ShipName = c.String(maxLength: 50),
            //            ShipTel = c.String(maxLength: 10),
            //            ShipEmail = c.String(maxLength: 50),
            //            ShipAddress = c.String(maxLength: 100),
            //            Status = c.String(maxLength: 100),
            //            Tongthanhtien = c.Decimal(precision: 18, scale: 2),
            //            ThanhCong = c.Boolean(),
            //            ThanhToan_MaThanhToan = c.Int(),
            //            TinhTrang1_MaTT = c.Int(),
            //        })
            //    .PrimaryKey(t => t.Madon)
            //    .ForeignKey("dbo.NguoiDung", t => t.MaNguoidung)
            //    .ForeignKey("dbo.ThanhToan", t => t.ThanhToan_MaThanhToan)
            //    .ForeignKey("dbo.TinhTrang", t => t.TinhTrang1_MaTT)
            //    .Index(t => t.MaNguoidung)
            //    .Index(t => t.ThanhToan_MaThanhToan)
            //    .Index(t => t.TinhTrang1_MaTT);
            
            //CreateTable(
            //    "dbo.NguoiDung",
            //    c => new
            //        {
            //            MaNguoiDung = c.Int(nullable: false, identity: true),
            //            Hoten = c.String(maxLength: 50),
            //            Email = c.String(maxLength: 50),
            //            Dienthoai = c.String(maxLength: 10, fixedLength: true),
            //            Matkhau = c.String(maxLength: 50, unicode: false),
            //            IDQuyen = c.Int(),
            //            Diachi = c.String(maxLength: 100),
            //            HinhAnh = c.String(maxLength: 50),
            //            Facebook = c.String(maxLength: 200),
            //            Instagram = c.String(maxLength: 200),
            //            Twitter = c.String(maxLength: 200),
            //        })
            //    .PrimaryKey(t => t.MaNguoiDung)
            //    .ForeignKey("dbo.PhanQuyen", t => t.IDQuyen)
            //    .Index(t => t.IDQuyen);
            
            //CreateTable(
            //    "dbo.PhanQuyen",
            //    c => new
            //        {
            //            IDQuyen = c.Int(nullable: false),
            //            TenQuyen = c.String(maxLength: 20),
            //        })
            //    .PrimaryKey(t => t.IDQuyen);
            
            //CreateTable(
            //    "dbo.ThanhToan",
            //    c => new
            //        {
            //            MaThanhToan = c.Int(nullable: false),
            //            LoaiThanhToan = c.String(maxLength: 50),
            //        })
            //    .PrimaryKey(t => t.MaThanhToan);
            
            //CreateTable(
            //    "dbo.TinhTrang",
            //    c => new
            //        {
            //            MaTT = c.Int(nullable: false),
            //            LoaiTT = c.String(maxLength: 50),
            //        })
            //    .PrimaryKey(t => t.MaTT);
            
            //CreateTable(
            //    "dbo.SanPham",
            //    c => new
            //        {
            //            Masp = c.Int(nullable: false, identity: true),
            //            Tensp = c.String(maxLength: 50),
            //            Giatien = c.Decimal(precision: 18, scale: 0),
            //            Size = c.Int(),
            //            Mota = c.String(storeType: "ntext"),
            //            Anhbia = c.String(maxLength: 50),
            //            Mahang = c.Int(),
            //            Soluong = c.Int(),
            //            Kho = c.Int(),
            //        })
            //    .PrimaryKey(t => t.Masp)
            //    .ForeignKey("dbo.HangSanXuat", t => t.Mahang)
            //    .Index(t => t.Mahang);
            
            //CreateTable(
            //    "dbo.HangSanXuat",
            //    c => new
            //        {
            //            Mahang = c.Int(nullable: false, identity: true),
            //            TenHang = c.String(maxLength: 10, fixedLength: true),
            //            ChiTietHang = c.String(maxLength: 200),
            //        })
            //    .PrimaryKey(t => t.Mahang);
            
            CreateTable(
                "dbo.LoaiSPs",
                c => new
                    {
                        MaLoai = c.Int(nullable: false, identity: true),
                        TenLoai = c.String(),
                    })
                .PrimaryKey(t => t.MaLoai);
            
            CreateTable(
                "dbo.Sizes",
                c => new
                    {
                        MaSize = c.Int(nullable: false, identity: true),
                        TenSize = c.String(),
                    })
                .PrimaryKey(t => t.MaSize);
            
            //CreateTable(
            //    "dbo.sysdiagrams",
            //    c => new
            //        {
            //            diagram_id = c.Int(nullable: false, identity: true),
            //            name = c.String(nullable: false, maxLength: 128),
            //            principal_id = c.Int(nullable: false),
            //            version = c.Int(),
            //            definition = c.Binary(),
            //        })
            //    .PrimaryKey(t => t.diagram_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SanPham", "Mahang", "dbo.HangSanXuat");
            //DropForeignKey("dbo.CTDonHang", "Masp", "dbo.SanPham");
            DropForeignKey("dbo.DonHang", "TinhTrang1_MaTT", "dbo.TinhTrang");
            DropForeignKey("dbo.DonHang", "ThanhToan_MaThanhToan", "dbo.ThanhToan");
            DropForeignKey("dbo.NguoiDung", "IDQuyen", "dbo.PhanQuyen");
            DropForeignKey("dbo.DonHang", "MaNguoidung", "dbo.NguoiDung");
            //DropForeignKey("dbo.CTDonHang", "Madon", "dbo.DonHang");
            DropIndex("dbo.SanPham", new[] { "Mahang" });
            DropIndex("dbo.NguoiDung", new[] { "IDQuyen" });
            DropIndex("dbo.DonHang", new[] { "TinhTrang1_MaTT" });
            DropIndex("dbo.DonHang", new[] { "ThanhToan_MaThanhToan" });
            DropIndex("dbo.DonHang", new[] { "MaNguoidung" });
            //DropIndex("dbo.CTDonHang", new[] { "Masp" });
            //DropIndex("dbo.CTDonHang", new[] { "Madon" });
            DropTable("dbo.sysdiagrams");
            DropTable("dbo.Sizes");
            DropTable("dbo.LoaiSPs");
            DropTable("dbo.HangSanXuat");
            DropTable("dbo.SanPham");
            DropTable("dbo.TinhTrang");
            DropTable("dbo.ThanhToan");
            DropTable("dbo.PhanQuyen");
            DropTable("dbo.NguoiDung");
            DropTable("dbo.DonHang");
            //DropTable("dbo.CTDonHang");
            DropTable("dbo.Colors");
        }
    }
}
