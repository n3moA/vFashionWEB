using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vFashionWEB.Models;

namespace vFashionWEB.Areas.NhanVien.Controllers
{
    public class NVHomeController : Controller
    {
        ThoiTrangWeb db = new ThoiTrangWeb();
        // GET: NhanVien/NVHome
        public ActionResult Index()
        {
            ViewBag.CountSanPham = db.SanPhams.Count();
            ViewBag.CountDonHang = db.DonHangs.Count();
            ViewBag.TongBanRa = TongSoLuongBanRa();
            ViewBag.TongTonKho = Thongketonkho();
            ViewBag.TongDoanhThu = Thongkedoanhthu();
            ViewBag.TongDoanhThuThang12 = Thongkedoanhthuthang(12, 2022);
            ViewBag.TongSoLuongThang12 = Thongkesoluongbanthang(12, 2022);
            ViewBag.TongDoanhThuThang11 = Thongkedoanhthuthang(11, 2022);
            ViewBag.TongSoLuongThang11 = Thongkesoluongbanthang(11, 2022);
            ViewBag.TongDoanhThuThang10 = Thongkedoanhthuthang(10, 2022);
            ViewBag.TongSoLuongThang10 = Thongkesoluongbanthang(10, 2022);
            ViewBag.TongDoanhThuThang9 = Thongkedoanhthuthang(9, 2022);
            ViewBag.TongSoLuongThang9 = Thongkesoluongbanthang(9, 2022);
            ViewBag.TongDoanhThuThang8 = Thongkedoanhthuthang(8, 2022);
            ViewBag.TongSoLuongThang8 = Thongkesoluongbanthang(8, 2022);
            ViewBag.TongDoanhThuThang7 = Thongkedoanhthuthang(7, 2022);
            ViewBag.TongSoLuongThang7 = Thongkesoluongbanthang(7, 2022);
            ViewBag.TongDoanhThuThang6 = Thongkedoanhthuthang(6, 2022);
            ViewBag.TongSoLuongThang6 = Thongkesoluongbanthang(6, 2022);
            ViewBag.TongDoanhThuThang5 = Thongkedoanhthuthang(5, 2022);
            ViewBag.TongSoLuongThang5 = Thongkesoluongbanthang(5, 2022);
            ViewBag.TongDoanhThuThang4 = Thongkedoanhthuthang(4, 2022);
            ViewBag.TongSoLuongThang4 = Thongkesoluongbanthang(4, 2022);
            ViewBag.TongDoanhThuThang3 = Thongkedoanhthuthang(3, 2022);
            ViewBag.TongSoLuongThang3 = Thongkesoluongbanthang(3, 2022);
            ViewBag.TongDoanhThuThang2 = Thongkedoanhthuthang(2, 2022);
            ViewBag.TongSoLuongThang2 = Thongkesoluongbanthang(2, 2022);
            ViewBag.TongDoanhThuThang1 = Thongkedoanhthuthang(1, 2022);
            ViewBag.TongSoLuongThang1 = Thongkesoluongbanthang(1, 2022);
            return View();
        }
        public decimal Thongkedoanhthu()
        {
            decimal TongDoanhThu = db.CTDonHangs.Sum(n => n.Soluong * n.Dongia).Value;
            return TongDoanhThu;
        }

        public decimal Thongkesoluongbanthang(int Thang, int Nam)
        {
            var lstCTDDH = db.DonHangs.Where(n => n.Ngaydat.Value.Month == Thang && n.Ngaydat.Value.Year == Nam);
            decimal TongSoLuongThang = 0;
            foreach (var item in lstCTDDH)
            {
                TongSoLuongThang += decimal.Parse(item.CTDonHangs.Sum(n => n.Soluong).Value.ToString());

            }
            return TongSoLuongThang;
        }

        public decimal Thongkedoanhthuthang(int Thang, int Nam)
        {
            var lstDDH = db.DonHangs.Where(n => n.Ngaydat.Value.Month == Thang && n.Ngaydat.Value.Year == Nam);
            decimal TongTienThang = 0;
            foreach (var item in lstDDH)
            {
                TongTienThang += decimal.Parse(item.CTDonHangs.Sum(n => n.Soluong * n.Dongia).Value.ToString());

            }
            return TongTienThang;

        }
        public decimal TongSoLuongBanRa()
        {
            decimal TongBanRa = db.CTDonHangs.Sum(n => n.Soluong).Value;
            return TongBanRa;
        }

        public decimal Thongketonkho()
        {
            decimal TongTonKho = (db.SanPhams.Sum(n => n.Kho).Value - db.CTDonHangs.Sum(n => n.Soluong).Value);
            return TongTonKho;
        }
    }
}