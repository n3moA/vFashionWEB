using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vFashionWEB.Models;

namespace vFashionWEB.Areas.Admin.Controllers
{
    public class AdminHangSanXuatController : Controller
    {
        ThoiTrangWeb db = new ThoiTrangWeb();
        // GET: Admin/AdminHangSanXuat
        public ActionResult Index(int? page)
        {
            
            if (page == null) page = 1;

            
            var ha = db.HangSanXuats.OrderBy(x => x.Mahang);

            
            int pageSize = 9;

            
            int pageNumber = (page ?? 1);
            return View(ha.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(HangSanXuat hang)
        {

            db.HangSanXuats.Add(hang);

            // Lưu lại
            db.SaveChanges();

            return RedirectToAction("Index");

        }
        public ActionResult Delete(int id)
        {
            var hang = db.HangSanXuats.Find(id);
            db.HangSanXuats.Remove(hang);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {// Hiển thị dropdownlist
            var hang = db.HangSanXuats.Find(id);
            // 
            return View(hang);
        }
        [HttpPost]
        public ActionResult Edit(HangSanXuat hang)
        {
            try
            {
                var oldItem = db.HangSanXuats.Find(hang.Mahang);
                oldItem.TenHang = hang.TenHang;
                oldItem.ChiTietHang = hang.ChiTietHang;
                db.SaveChanges();
                // xong chuyển qua index
                return RedirectToAction("Index", "AdminHangSanXuat");
            }
            catch
            {
                return View();
            }
        }

            //Xoá sản phẩm phương thức POST: Admin/Home/Delete/5
            [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                //Lấy được thông tin sản phẩm theo ID(mã sản phẩm)
                var hang = db.HangSanXuats.Find(id);
                // Xoá
                db.HangSanXuats.Remove(hang);
                // Lưu lại
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}