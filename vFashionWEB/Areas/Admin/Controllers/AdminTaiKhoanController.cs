using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vFashionWEB.Models;

namespace vFashionWEB.Areas.Admin.Controllers
{
    public class AdminTaiKhoanController : Controller
    {
        ThoiTrangWeb db = new ThoiTrangWeb();
        // GET: Admin/AdminTaiKhoan
        public ActionResult Index(int? page)
        {
            // 1. Tham số int? dùng để thể hiện null và kiểu int( số nguyên)
            // page có thể có giá trị là null ( rỗng) và kiểu int.

            // 2. Nếu page = null thì đặt lại là 1.
            if (page == null) page = 1;

            // 3. Tạo truy vấn sql, lưu ý phải sắp xếp theo trường nào đó, ví dụ OrderBy
            // theo Masp mới có thể phân trang.
            var sp = db.NguoiDungs.OrderBy(x => x.MaNguoiDung);

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
            var quyenselected = new SelectList(db.PhanQuyens, "IDQuyen", "TenQuyen");
            ViewBag.IDQuyen = quyenselected;
            return View();
        }
        [HttpPost]
        public ActionResult Create(NguoiDung nguoidung, HttpPostedFileBase uploadhinh)
        {
            //Thêm  sản phẩm mới
            db.NguoiDungs.Add(nguoidung);

            // Lưu lại
            db.SaveChanges();

            if (uploadhinh != null && uploadhinh.ContentLength > 0)
            {
                int id = int.Parse(db.NguoiDungs.ToList().Last().MaNguoiDung.ToString());

                string _FileName = "";
                int index = uploadhinh.FileName.IndexOf('.');
                _FileName = "tkm" + id.ToString() + "." + uploadhinh.FileName.Substring(index + 1);
                string _path = Path.Combine(Server.MapPath("~/assets/images/"), _FileName);
                uploadhinh.SaveAs(_path);

                NguoiDung unv = db.NguoiDungs.FirstOrDefault(x => x.MaNguoiDung == id);
                unv.HinhAnh = _FileName;
                db.SaveChanges();
            }



            // Thành công chuyển đến trang index
            return RedirectToAction("Index");
        }


        public ActionResult Edit(int id)
        {
            var nd = db.NguoiDungs.Find(id);
            var quyenselected = new SelectList(db.PhanQuyens, "IDQuyen", "TenQuyen", nd.IDQuyen);
            ViewBag.IDQuyen = quyenselected;

            return View(nd);
        }
        [HttpPost]
        public ActionResult Edit(NguoiDung nguoidung, HttpPostedFileBase uploadhinh)
        {
            try
            {
                // Sửa sản phẩm theo mã sản phẩm
                var oldItem = db.NguoiDungs.Find(nguoidung.MaNguoiDung);
                oldItem.Hoten = nguoidung.Hoten;
                oldItem.Email = nguoidung.Email;
                oldItem.Dienthoai = nguoidung.Dienthoai;
                oldItem.Matkhau = nguoidung.Matkhau;
                oldItem.IDQuyen = nguoidung.IDQuyen;
                oldItem.Diachi= nguoidung.Diachi;
                oldItem.Facebook = nguoidung.Facebook;
                oldItem.Instagram = nguoidung.Instagram;
                oldItem.Twitter = nguoidung.Twitter;
                if (uploadhinh != null && uploadhinh.ContentLength > 0)
                {
                    int id = nguoidung.MaNguoiDung;
                    string _FileName = "";
                    int index = uploadhinh.FileName.IndexOf('.');
                    _FileName = "stk" + id.ToString() + "." + uploadhinh.FileName.Substring(index + 1);
                    string _path = Path.Combine(Server.MapPath("~/assets/images/"), _FileName);
                    uploadhinh.SaveAs(_path);
                    oldItem.HinhAnh = _FileName;
                }

                // lưu lại
                db.SaveChanges();
                // xong chuyển qua index
                return RedirectToAction("Index", "AdminTaiKhoan");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Delete(int id)
        {
            var nd = db.NguoiDungs.Find(id);
            db.NguoiDungs.Remove(nd);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}