using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vFashionWEB.Models;
using PagedList;

namespace vFashionWEB.Controllers
{
    public class SanPhamController : Controller
    {
        // GET: SanPham
        ThoiTrangWeb db = new ThoiTrangWeb(); 
        public ActionResult Index(string currentFilter,string SearchString,  int? page)
        {
            
            var ni = new List<SanPham>();
            if (SearchString != null)
            {
                page = 1;
            }    
            else
            {
                SearchString = currentFilter;
            }
            if (!string.IsNullOrEmpty(SearchString))
            {
       
                ni = db.SanPhams.Where(n => n.Tensp.Contains(SearchString)).ToList();
            }    
            else
            {
                ni = db.SanPhams.ToList();
            }
            if (ni.Count > 0)
            {
                
            }
            else
            {
                ViewBag.Khongcosanpham = "Sản phẩm bạn tìm kiếm hiện không có hoặc đã hết hàng";
            }
            ViewBag.CurrentFilter = SearchString;
            int pageSize = 9;
            int pageNumber = (page ?? 1);
            ni = ni.OrderByDescending(n => n.Masp).ToList();
            return View(ni.ToPagedList(pageNumber, pageSize));




            //if (string.IsNullOrEmpty(SearchString))
            //{
            //    var nic = db.SanPhams/*.Where(n => n.Soluong == null)*/.Take(50).ToList();
            //    return View(nic);
            //}
            //var ni = db.SanPhams.Where(n => n.Tensp.Contains(SearchString)).ToList();
            //return View(ni);




            //if (page == null) page = 1;
            //var sp = db.SanPhams.OrderBy(x => x.Masp);
            //int pageSize = 7; 
            //int pageNumber = (page ?? 1);
            //return View(sp.ToPagedList(pageNumber, pageSize));


            //lấy tất cả sản phẩm trong bảng

            //var sp = vFashionWEB.SanPham.ToList();
            //return View(sp);


        }
        public ActionResult Danhmuc(int id)
        {
            var Danhmucsp = db.SanPhams.Where(n=>n.Mahang == id).ToList();
            return View(Danhmucsp);
        }
        public ActionResult xemchitiet(int Masp = 0)
        {
            var chitiet = db.SanPhams.SingleOrDefault(n => n.Masp == Masp);
            if (chitiet == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(chitiet);
           
        }

        public ActionResult test1()
        {
            return View();
        }
    }
}