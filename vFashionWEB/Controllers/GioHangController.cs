using mvcDangNhap.common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using vFashionWEB.Models;

namespace vFashionWEB.Controllers
{
    public class GioHangController : Controller
    {
        
        ThoiTrangWeb db = new ThoiTrangWeb();
        // GET: GioHang

        //Lấy giỏ hàng 
        public List<GioHang> LayGioHang()
        {
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang == null)
            {
                //Nếu giỏ hàng chưa tồn tại thì mình tiến hành khởi tao list giỏ hàng (sessionGioHang)
                lstGioHang = new List<GioHang>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }
        //Thêm giỏ hàng
        public ActionResult ThemGioHang(int iMasp, string strURL)
        {
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.Masp == iMasp);
            if (sp == null)
            {
                Response.StatusCode = 404;
                ViewBag.Fail = "Chưa có sản phẩm nào";
                return null;
                
            }
            //Lấy ra session giỏ hàng
            List<GioHang> lstGioHang = LayGioHang();
            //Kiểm tra sp này đã tồn tại trong session[giohang] chưa
            GioHang sanpham = lstGioHang.Find(n => n.iMasp == iMasp);
            if (sanpham == null)
            {
                sanpham = new GioHang(iMasp);
                //Add sản phẩm mới thêm vào list
                lstGioHang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.iSoLuong++;
                return Redirect(strURL);
            }
        }
        //Cập nhật giỏ hàng 
        public ActionResult CapNhatGioHang(int iMaSP, FormCollection f)
        {
            //Kiểm tra masp
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.Masp == iMaSP);
            //Nếu get sai masp thì sẽ trả về trang lỗi 404
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //Lấy giỏ hàng ra từ session
            List<GioHang> lstGioHang = LayGioHang();
            //Kiểm tra sp có tồn tại trong session["GioHang"]
            GioHang sanpham = lstGioHang.SingleOrDefault(n => n.iMasp == iMaSP);
            //Nếu mà tồn tại thì chúng ta cho sửa số lượng
            if (sanpham != null)
            {
                sanpham.iSoLuong = int.Parse(f["txtSoLuong"].ToString());

            }
            return RedirectToAction("GioHang");
        }
        //Xóa giỏ hàng
        public ActionResult XoaGioHang(int iMaSP)
        {
            //Kiểm tra masp
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.Masp == iMaSP);
            //Nếu get sai masp thì sẽ trả về trang lỗi 404
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //Lấy giỏ hàng ra từ session
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sanpham = lstGioHang.SingleOrDefault(n => n.iMasp == iMaSP);
            //Nếu mà tồn tại thì chúng ta cho sửa số lượng
            if (sanpham != null)
            {
                lstGioHang.RemoveAll(n => n.iMasp == iMaSP);

            }
            if (lstGioHang.Count == 0)
            {
                return RedirectToAction("Index", "SanPham");
            }
            return RedirectToAction("GioHang");
        }
        
        //Tính tổng số lượng và tổng tiền
        //Tính tổng số lượng
        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                iTongSoLuong = lstGioHang.Sum(n => n.iSoLuong);
            }
            return iTongSoLuong;
        }
        //Tính tổng thành tiền
        private double TongTien()
        {
            double dTongTien = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                dTongTien = lstGioHang.Sum(n => n.ThanhTien);
            }
            return dTongTien;
        }
        //Xây dựng trang giỏ hàng
        public ActionResult GioHang()
        {
            List<GioHang> lstGioHang = LayGioHang();
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "SanPham");
            }
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(lstGioHang);
        }
        
        //Xây dựng 1 view cho người dùng chỉnh sửa giỏ hàng
        public ActionResult SuaGioHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "SanPham");
            }
            List<GioHang> lstGioHang = LayGioHang();
            return View(lstGioHang);

        }
        #region // Mới hoàn thiện
        //Xây dựng chức năng đặt hàng
        [HttpPost]
        public ActionResult DatHang(string shipName, string shipTel, string shipEmail, string shipAddress, string payment="")
        {
            //Kiểm tra đăng đăng nhập
            if (Session["use"] == null || Session["use"].ToString() == "")
            {
                return RedirectToAction("Login", "NguoiDung");
            }
            //Kiểm tra giỏ hàng
            if (Session["GioHang"] == null)
            {
                RedirectToAction("Index", "Home");
            }
            //Thêm đơn hàng
            DonHang ddh = new DonHang();
            NguoiDung kh = (NguoiDung)Session["use"];
            List<GioHang> gh = LayGioHang();
            ddh.MaNguoidung = kh.MaNguoiDung;
            ddh.Ngaydat = DateTime.Now;
            ddh.Tinhtrang = 1;
            ddh.Tinhtrangthanhtoan = 1;
            ddh.ThanhCong = false;
            ddh.ShipName = shipName;
            ddh.ShipTel = shipTel;
            ddh.ShipEmail = shipEmail;
            ddh.ShipAddress = shipAddress;
            ddh.Tongthanhtien = (decimal)TongTien();
            Console.WriteLine(ddh);
            db.DonHangs.Add(ddh);
            db.SaveChanges();
            int o = db.DonHangs.OrderByDescending(n => n.Madon).FirstOrDefault().Madon;
            Session["Madon"] = o;

            //Thêm chi tiết đơn hàng
            foreach (var item in gh)
            {
                CTDonHang ctDH = new CTDonHang();
                decimal thanhtien = item.iSoLuong * (decimal)item.dDonGia;
                
                ctDH.Madon = ddh.Madon;
                ctDH.Masp = item.iMasp;
                ctDH.Soluong = item.iSoLuong;
                ctDH.Dongia = (decimal)item.dDonGia;
                ctDH.Thanhtien = (decimal)thanhtien;
                
                db.CTDonHangs.Add(ctDH);
                
            }
            string content = System.IO.File.ReadAllText(Server.MapPath("~/content/template/donhangmoi.html"));

            content = content.Replace("{{CustomerName}}", shipName);
            content = content.Replace("{{Phone}}", shipTel);
            content = content.Replace("{{Email}}", shipEmail);
            content = content.Replace("{{Address}}", shipAddress);
            content = content.Replace("{{Total}}", TongTien().ToString("N0"));
            //content = content.Replace("{{Total}}", tongThanhtien);
            var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

            // Để Gmail cho phép SmtpClient kết nối đến server SMTP của nó với xác thực 
            //là tài khoản gmail của bạn, bạn cần thiết lập tài khoản email của bạn như sau:
            //Vào địa chỉ https://myaccount.google.com/security  Ở menu trái chọn mục Bảo mật, sau đó tại mục Quyền truy cập 
            //của ứng dụng kém an toàn phải ở chế độ bật
            //  Đồng thời tài khoản Gmail cũng cần bật IMAP
            //Truy cập địa chỉ https://mail.google.com/mail/#settings/fwdandpop

            new MailHelper().SendMail(shipEmail, "Đơn hàng mới từ Quang Vinh Sneaker", content);
            new MailHelper().SendMail(toEmail, "Đơn hàng mới từ Quang Vinh Sneaker", content);
            db.SaveChanges();
            if (payment == "paypal")
            {
                return RedirectToAction("PaymentWithPaypal", "ThanhToan");
            }
            else if (payment == "momo")
            {
                return RedirectToAction("PaymentWithMomo", "ThanhToan");
            }
            return RedirectToAction("Index", "Donhangs");
        }


        #endregion
        public ActionResult KTGioHang(int iMaSP)
        {
            
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sanpham = lstGioHang.SingleOrDefault(n => n.iMasp == iMaSP);
            //Nếu mà tồn tại thì chúng ta cho sửa số lượng
            if (sanpham == null)
            {
                ViewBag.Fail = "Không có gì trong giỏ hàng";
                return View("Giohang");
            }
            return View("Giohang");
        }



        //[HttpPost]
        //public ActionResult Send(FormCollection form)
        //{
        //    int idUser = Int32.Parse(form["id_user"]);
        //    string email = form["email"];
        //    var list = scoreRepository.getListScoreById(idUser);
        //    string html = "";
        //    for (int i = 0; i < list.Count; i++)
        //    {
        //        html += "<tr>" +
        //            "<td>" + (i + 1) + "</td>" +
        //            "<td>" + list[i].Subject.name + "</td>" +
        //            "<td>" + list[i].point + "</td>" +
        //            "<td>" + list[i].point2 + "</td>" +
        //            "</tr>";
        //    }
        //    string content = System.IO.File.ReadAllText(Server.MapPath("~/Content/html/TemplateEmail.cshtml"));
        //    content = content.Replace("{{body}}", html);
        //    sendMail(email, content, idUser);
        //    return RedirectToAction("Index", new { msg = "1" });
        //}



        //public void sendMail(string email, string body, int id_user)
        //{
        //    var user = userRepository.getUserById(id_user);
        //    var formEmailAddress = ConfigurationManager.AppSettings["FormEmailAddress"].ToString();
        //    var formEmailDisplayName = ConfigurationManager.AppSettings["FormEmailDisplayName"].ToString();
        //    var formEmailPassword = ConfigurationManager.AppSettings["FormEmailPassword"].ToString();
        //    var smtpHost = ConfigurationManager.AppSettings["SMTPHost"].ToString();
        //    var smtpPort = ConfigurationManager.AppSettings["SMTPPost"].ToString();
        //    bool enableSsl = bool.Parse(ConfigurationManager.AppSettings["EnabledSSL"].ToString());
        //    MailMessage message = new MailMessage(new MailAddress(formEmailAddress, formEmailDisplayName), new MailAddress(email));
        //    message.Subject = "Bảng điểm của sinh viên " + user.fullname;
        //    message.IsBodyHtml = true;
        //    message.Body = body;
        //    var client = new SmtpClient();
        //    client.Credentials = new NetworkCredential(formEmailAddress, formEmailPassword);
        //    client.Host = smtpHost;
        //    client.EnableSsl = enableSsl;
        //    client.Port = !string.IsNullOrEmpty(smtpPort) ? Convert.ToInt32(smtpPort) : 0;
        //    client.Send(message);
        //}
    }





}