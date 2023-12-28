using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using vFashionWEB.Models;

namespace vFashionWEB.Areas.Admin.Controllers
{
    public class AdminNhanVienController : Controller
    {
        ThoiTrangWeb db = new ThoiTrangWeb();
        // GET: Admin/AdminNhanVien

        public ActionResult Index()
        {
            var kh = db.NguoiDungs.Where(n => n.IDQuyen == 3);

            return View(kh.ToList());
            
        }

        public ActionResult CTNhanVien(int? id)
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
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //NguoiDung nguoidung = db.NguoiDungs.Find(id);
            //if (nguoidung == null)
            //{
            //    return HttpNotFound();
            //}
            //ViewBag.IDQuyen = new SelectList(db.PhanQuyens, "IDQuyen", "TenQuyen", nguoidung.IDQuyen);
            //return View(nguoidung);

            var nd = db.NguoiDungs.Find(id);
            //var hangselected = new SelectList(db.PhanQuyens, "IDQuyen", "TenQuyen", nd.IDQuyen);
            //ViewBag.Mahang = hangselected;
            // 
            return View(nd);

        }
        [HttpPost]
        //public ActionResult Edit([Bind(Include = "MaNguoiDung,Hoten,Email,Dienthoai,Matkhau,/*IDQuyen*/")] NguoiDung nguoidung)
        public ActionResult Edit(NguoiDung nguoidung, HttpPostedFileBase uploadhinh)
        {
            //if (ModelState.IsValid)
            //{
            //    db.Entry(nguoidung).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //ViewBag.IDQuyen = new SelectList(db.PhanQuyens, "IDQuyen", "TenQuyen", nguoidung.IDQuyen);
            //return View(nguoidung);
            try
            {
                // Sửa sản phẩm theo mã sản phẩm
                var oldItem = db.NguoiDungs.Find(nguoidung.MaNguoiDung);
                oldItem.Hoten = nguoidung.Hoten;
                oldItem.Email = nguoidung.Email;
                oldItem.Dienthoai = nguoidung.Dienthoai;
                oldItem.Matkhau = nguoidung.Matkhau;
                oldItem.Diachi= nguoidung.Diachi;
                oldItem.Facebook = nguoidung.Facebook;
                oldItem.Instagram = nguoidung.Instagram;
                oldItem.Twitter = nguoidung.Twitter;
                //oldItem.IDQuyen = nguoidung.IDQuyen;
                if (uploadhinh != null && uploadhinh.ContentLength > 0)
                {
                    int id = nguoidung.MaNguoiDung;
                    string _FileName = "";
                    int index = uploadhinh.FileName.IndexOf('.');
                    _FileName = "snv" + id.ToString() + "." + uploadhinh.FileName.Substring(index + 1);
                    string _path = Path.Combine(Server.MapPath("~/assets/images/"), _FileName);
                    uploadhinh.SaveAs(_path);
                    oldItem.HinhAnh = _FileName;
                }

                // lưu lại
                db.SaveChanges();
                // xong chuyển qua index
                return RedirectToAction("Index", "AdminNhanVien");
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