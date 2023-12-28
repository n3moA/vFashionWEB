using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vFashionWEB.Models;
using System.IO;
using System.Web.Hosting;
using System.Net;
using System.Data.Entity;

namespace vFashionWEB.Controllers
{
    public class NguoiDungController : Controller
    {
        ThoiTrangWeb db = new ThoiTrangWeb();
        // GET: NguoiDung
        public ActionResult Dangky()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Dangky(NguoiDung nguoidung)
        {
            try
            {
                
                // Thêm người dùng  mới

                db.NguoiDungs.Add(nguoidung);
                
                // Lưu lại vào cơ sở dữ liệu
                db.SaveChanges();
                // Nếu dữ liệu đúng thì trả về trang đăng nhập
                if (ModelState.IsValid)
                {
                    return RedirectToAction("Confirm", "NguoiDung");
                }
                return View("Dangky");

            }
            catch
            {
                return View();
            }
        }


        // ĐĂNG KÝ PHƯƠNG THỨC POST

        //public ActionResult Index()
        //{
        //    return View();
        //}
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection userlog)
        {
            string userMail = userlog["userMail"].ToString();
            string password = userlog["password"].ToString();
            
            var islogin = db.NguoiDungs.SingleOrDefault(x => x.Email.Equals(userMail) && x.Matkhau.Equals(password));
            
            if (islogin != null)
            {
                if(islogin.IDQuyen == 3)
                {
                    Session["use"] = islogin;
                    return RedirectToAction("Index", "NhanVien/NVHome");
                }    
                if (userMail == "Admin@gmail.com" || islogin.IDQuyen==1) 
                {
                    Session["use"] = islogin;
                    return RedirectToAction("Index", "Admin/AdminHome");
                }
                else
                {
                    Session["use"] = islogin;
                    return RedirectToAction("Index", "SanPham");
                }
            }
            else
            {
                ViewBag.Fail = "Tài khoản hoặc mật khẩu không chính xác!";
                return View("Login");
            }

        }

        //public JsonResult SaveData(NguoiDung model)
        //{
        //    model.XacNhan = false;
        //    db.NguoiDungs.Add(model);
        //    db.SaveChanges();
        //    BuildEmailTemplate(model.MaNguoiDung);
        //    return Json("Thanhcong",JsonRequestBehavior.AllowGet);

        //}
        //public void BuildEmailTemplate(int regID)
        //{
        //    string body = System.IO.File.ReadAllText(HostingEnvironment.MapPath());
        //}

        public ActionResult Confirm()
        {
            return View();
        }

        public ActionResult ThanhCong()
        {
            return View();
        }
        public ActionResult DangXuat()
        {
            Session["use"] = null;
            return RedirectToAction("Login", "NguoiDung");
        }
        public ActionResult HoSo()
        {
            return View();
        }

        public ActionResult SuaHoSo(int id)
        {
            var nd = db.NguoiDungs.Find(id);
            return View(nd);
        }
        [HttpPost]
        public ActionResult SuaHoSo(NguoiDung nguoidung, HttpPostedFileBase uploadhinh)
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
                    _FileName = "shs" + id.ToString() + "." + uploadhinh.FileName.Substring(index + 1);
                    string _path = Path.Combine(Server.MapPath("~/assets/images/"), _FileName);
                    uploadhinh.SaveAs(_path);
                    oldItem.HinhAnh = _FileName;
                }

                // lưu lại
                db.SaveChanges();
                // xong chuyển qua index
                return RedirectToAction("Login", "NguoiDung");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Chat()
        {
            return View();
        }
    }

}