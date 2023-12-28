using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vFashionWEB.Models;

namespace vFashionWEB.Areas.NhanVien.Controllers
{
    public class NVSanPhamController : Controller
    {
        ThoiTrangWeb db = new ThoiTrangWeb();
        // GET: NhanVien/NVSanPham
        public ActionResult Index(int? page)
        {
            // 1. Tham số int? dùng để thể hiện null và kiểu int( số nguyên)
            // page có thể có giá trị là null ( rỗng) và kiểu int.

            // 2. Nếu page = null thì đặt lại là 1.
            if (page == null) page = 1;

            // 3. Tạo truy vấn sql, lưu ý phải sắp xếp theo trường nào đó, ví dụ OrderBy
            // theo Masp mới có thể phân trang.
            var sp = db.SanPhams.OrderBy(x => x.Masp);

            // 4. Tạo kích thước trang (pageSize) hay là số sản phẩm hiển thị trên 1 trang
            int pageSize = 9;

            // 4.1 Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            // nếu page = null thì lấy giá trị 1 cho biến pageNumber.
            int pageNumber = (page ?? 1);
            return View(sp.ToPagedList(pageNumber, pageSize));
            
        }
        public ActionResult Create()
        {
            //Để tạo dropdownList bên view
            var hangselected = new SelectList(db.HangSanXuats, "Mahang", "TenHang");
            ViewBag.Mahang = hangselected;
            return View();
        }

        // Tạo sản phẩm mới phương thức POST: Admin/Home/Create
        [HttpPost]
        public ActionResult Create(SanPham sanpham, HttpPostedFileBase uploadhinh)
        {
            //Thêm  sản phẩm mới
            db.SanPhams.Add(sanpham);

            // Lưu lại
            db.SaveChanges();

            if (uploadhinh != null && uploadhinh.ContentLength > 0)
            {
                int id = int.Parse(db.SanPhams.ToList().Last().Masp.ToString());

                string _FileName = "";
                int index = uploadhinh.FileName.IndexOf('.');
                _FileName = "spm" + id.ToString() + "." + uploadhinh.FileName.Substring(index + 1);
                string _path = Path.Combine(Server.MapPath("~/assets/images/"), _FileName);
                uploadhinh.SaveAs(_path);

                SanPham unv = db.SanPhams.FirstOrDefault(x => x.Masp == id);
                unv.Anhbia = _FileName;
                db.SaveChanges();
            }



            // Thành công chuyển đến trang index
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {// Hiển thị dropdownlist
            var sp = db.SanPhams.Find(id);
            var hangselected = new SelectList(db.HangSanXuats, "Mahang", "Tenhang", sp.Mahang);
            ViewBag.Mahang = hangselected;
            // 
            return View(sp);
        }
        // POST: Admin/Home/Edit/5
        [HttpPost]
        public ActionResult Edit(SanPham sanpham, HttpPostedFileBase uploadhinh)
        {
            try
            {
                // Sửa sản phẩm theo mã sản phẩm
                var oldItem = db.SanPhams.Find(sanpham.Masp);
                oldItem.Tensp = sanpham.Tensp;
                oldItem.Giatien = sanpham.Giatien;
                oldItem.Soluong = sanpham.Soluong;
                oldItem.Mota = sanpham.Mota;
                oldItem.Mahang = sanpham.Mahang;
                //oldItem.Size = sanpham.Size;
                if (uploadhinh != null && uploadhinh.ContentLength > 0)
                {
                    int id = sanpham.Masp;
                    string _FileName = "";
                    int index = uploadhinh.FileName.IndexOf('.');
                    _FileName = "ssp" + id.ToString() + "." + uploadhinh.FileName.Substring(index + 1);
                    string _path = Path.Combine(Server.MapPath("~/assets/images/"), _FileName);
                    uploadhinh.SaveAs(_path);
                    oldItem.Anhbia = _FileName;
                }

                // lưu lại
                db.SaveChanges();
                // xong chuyển qua index
                return RedirectToAction("Index", "NVSanPham");
            }
            catch
            {
                return View();
            }
        }
        // Xoá sản phẩm phương thức GET: Admin/Home/Delete/5
        public ActionResult Delete(int id)
        {
            var dt = db.SanPhams.Find(id);
            db.SanPhams.Remove(dt);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //Xoá sản phẩm phương thức POST: Admin/Home/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                //Lấy được thông tin sản phẩm theo ID(mã sản phẩm)
                var dt = db.SanPhams.Find(id);
                // Xoá
                db.SanPhams.Remove(dt);
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