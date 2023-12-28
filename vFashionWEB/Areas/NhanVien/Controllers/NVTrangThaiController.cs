using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vFashionWEB.Models;

namespace vFashionWEB.Areas.NhanVien.Controllers
{
    public class NVTrangThaiController : Controller
    {
        ThoiTrangWeb db = new ThoiTrangWeb();
        // GET: NhanVien/NVTrangThai
        public ActionResult Index(int? page)
        {
            if (page == null) page = 1;
            var dh = db.DonHangs.OrderBy(x => x.Madon);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(dh.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult test(int? page)
        {
            if (page == null) page = 1;
            var dh = db.DonHangs.OrderBy(x => x.Madon);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(dh.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult XacNhanDonHang(int id)
        {
            try
            {
                DonHang order = db.DonHangs.Find(id);
                order.Tinhtrang = 2;
                order.Ngaygiao = DateTime.Now.AddDays(3);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }

        }
        public ActionResult GiaoHang(int ID)
        {
            DonHang order = db.DonHangs.Find(ID);
            order.Tinhtrang = 3;
            order.Ngaygiao = DateTime.Now.AddDays(3);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult HoanThanh(int ID)
        {
            DonHang order = db.DonHangs.Find(ID);
            order.Tinhtrang = 4;
            order.Ngaygiao = DateTime.Now.AddDays(3);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}