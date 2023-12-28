using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using vFashionWEB.Models;

namespace vFashionWEB.Areas.NhanVien.Controllers
{
    public class NVKhachHangController : Controller
    {
        ThoiTrangWeb db = new ThoiTrangWeb();
        // GET: NhanVien/NVKhachHang
        public ActionResult Index()
        {
            //var kh = db.NguoiDungs.Where(n => n.IDQuyen == 2).Take(50).ToList();

            //return PartialView();

            var kh = db.NguoiDungs.Where(n => n.IDQuyen == 2);

            return View(kh.ToList());
        }
        public ActionResult CTKhachHang(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // Khai báo một người dùng theo mã
            NguoiDung nguoidung = db.NguoiDungs.Find(id);
            if (nguoidung == null)
            {
                return HttpNotFound();
            }
            // trả về trang chi tiết người dùng
            return View(nguoidung);


        }
        public ActionResult Edit(int id)
        {
            var nd = db.NguoiDungs.Find(id);
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
                oldItem.Diachi = nguoidung.Diachi;
                oldItem.Facebook = nguoidung.Facebook;
                oldItem.Instagram = nguoidung.Instagram;
                oldItem.Twitter = nguoidung.Twitter;
                //oldItem.IDQuyen = nguoidung.IDQuyen;
                if (uploadhinh != null && uploadhinh.ContentLength > 0)
                {
                    int id = nguoidung.MaNguoiDung;
                    string _FileName = "";
                    int index = uploadhinh.FileName.IndexOf('.');
                    _FileName = "skh" + id.ToString() + "." + uploadhinh.FileName.Substring(index + 1);
                    string _path = Path.Combine(Server.MapPath("~/assets/images/"), _FileName);
                    uploadhinh.SaveAs(_path);
                    oldItem.HinhAnh = _FileName;
                }

                // lưu lại
                db.SaveChanges();
                // xong chuyển qua index
                return RedirectToAction("Index", "NVKhachHang");
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