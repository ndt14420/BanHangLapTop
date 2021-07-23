using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HutechAndYou.Models;
using PagedList;
using PagedList.Mvc;

namespace HutechAndYou.Controllers
{
    public class HomeController : Controller
    {
        dbQLBLaptopDataContext data = new dbQLBLaptopDataContext();
        public ActionResult Index(int ? page)
        {
            int pageSize = 3;
            int pageNum = (page ?? 1);
            var tintuc = from s in data.Tintucs
                         select s;
            return View(tintuc.ToPagedList(pageNum, pageSize));
        }

        //View tin tức
        public ActionResult TinTucAdmin(int ID)
        {
            var Tintuc = from s in data.Tintucs
                          where s.MaTinTuc == ID
                          select s;
            return View(Tintuc.Single());
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        //Liên hệ
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult Contact(FormCollection collection, Lienhe lh, HttpPostedFileBase fileUpload)
        {
            var ten = collection["Name"];
            var email = collection["Email"];
            var noidung = collection["Message"];
            if (string.IsNullOrEmpty(ten))
            {
                ViewData["loi1"] = "nhập tên của bạn";
            }
            else if (string.IsNullOrEmpty(email))
            {
                ViewData["loi2"] = "nhập email của bạn";
            }
            else if (string.IsNullOrEmpty(noidung))
            {
                ViewData["loi3"] = "nhập nội dung bạn muốn nói với chúng tôi";
            }
            else
            {
                lh.TenKH = ten;
                lh.Emai = email;
                lh.NoiDung = noidung;
                data.Lienhes.InsertOnSubmit(lh);
                data.SubmitChanges();
                return RedirectToAction("ContactTC");
            }
            return this.Contact();
        }

        //Liên hệ thành công
        public ActionResult ContactTC()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public ActionResult Guarantee()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult KhuTraiNghiem()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //Sắp xếp giảm dần theo ngày cập nhật:
        public List<SanPham> LaptopMoi(int count)
        {
            return data.SanPhams.OrderByDescending(a => a.NgayCapNhat).Take(count).ToList();
        }

        //Mua hàng
        public ActionResult MuaHang (int? page)
        {
            int pageSize = 6;
            int pageNum = (page ?? 1);
            var laptopmoi = LaptopMoi(100000);
            return View(laptopmoi.ToPagedList(pageNum, pageSize));

        }

        //Tìm kiêm
        public ActionResult Search(string searchTerm,int? page)
        {
            int pageSize = 6;
            int pageNum = (page ?? 1);
            var books = from b in data.SanPhams select b;
            if (!String.IsNullOrEmpty(searchTerm))
            {
                books = data.SanPhams.Where(b => b.Hang.TenHang.Contains(searchTerm));
            }
            if (!String.IsNullOrEmpty(searchTerm))
            {
                books = data.SanPhams.Where(b => b.TenSP.Contains(searchTerm));
            }
            ViewBag.SearchTerm = searchTerm;
            return View(books.ToPagedList(pageNum, pageSize));
        }

        //Chi Tiết
        public ActionResult ChiTiet(int ID)
        {
            var SanPham = from s in data.SanPhams
                          where s.MaSP == ID
                          select s;
            return View(SanPham.Single());
        }


        //Chi Tiết Admin
        public ActionResult ChiTietAdmin(int ID)
        {
            var SanPham = from s in data.SanPhams
                          where s.MaSP == ID
                          select s;
            return View(SanPham.Single());
        }

        //Đăng ký thành công
        public ActionResult DangKyTC()
        {
            return View();
        }

        //Đăng nhập thành công
        public ActionResult DangNhapTC()
        {
            return View();
        }

        //Hãng Sản Xuất:
        public ActionResult HangSanXuat()
        {
            var hangsanxuat = from hangsx in data.Hangs select hangsx;
            return PartialView(hangsanxuat);
        }

        //Sản Phẩm theo hãng:
        public ActionResult SPTheoHang(int ID, int? page)
        {
            int pageSize = 6;
            int pageNum = (page ?? 1);
            var SanPham = from s in data.SanPhams where s.MaHang == ID select s;
            return View(SanPham.ToPagedList(pageNum, pageSize));

        }

        //Gia 0-10tr:
        public ActionResult DEN10(int? page)
        {
            int pageSize = 6;
            int pageNum = (page ?? 1);
            var SanPham = from s in data.SanPhams
                          where s.GiaBan <= 10000000
                          select s;
            return View(SanPham.ToPagedList(pageNum, pageSize));

        }

        //Gia 10-15tr:
        public ActionResult DEN15(int? page)
        {
            int pageSize = 6;
            int pageNum = (page ?? 1);
            var SanPham = from s in data.SanPhams
                          where s.GiaBan > 10000000 && s.GiaBan <= 15000000
                          select s;
            return View(SanPham.ToPagedList(pageNum, pageSize));

        }

        //Gia 15-20tr:
        public ActionResult DEN20(int? page)
        {
            int pageSize = 6;
            int pageNum = (page ?? 1);
            var SanPham = from s in data.SanPhams
                          where s.GiaBan > 15000000 && s.GiaBan <= 20000000
                          select s;
            return View(SanPham.ToPagedList(pageNum, pageSize));

        }

        //Gia 20-25tr:
        public ActionResult DEN25(int? page)
        {
            int pageSize = 6;
            int pageNum = (page ?? 1);
            var SanPham = from s in data.SanPhams
                          where s.GiaBan > 20000000 && s.GiaBan <= 25000000
                          select s;
            return View(SanPham.ToPagedList(pageNum, pageSize));

        }

        //Gia 25-30tr:
        public ActionResult DEN30(int? page)
        {
            int pageSize = 6;
            int pageNum = (page ?? 1);
            var SanPham = from s in data.SanPhams
                          where s.GiaBan > 25000000 && s.GiaBan <= 30000000
                          select s;
            return View(SanPham.ToPagedList(pageNum, pageSize));

        }

        //Gia tren 30tr:
        public ActionResult TREN30(int? page)
        {
            int pageSize = 6;
            int pageNum = (page ?? 1);
            var SanPham = from s in data.SanPhams
                          where s.GiaBan > 30000000
                          select s;
            return View(SanPham.ToPagedList(pageNum, pageSize));

        }
    }
}