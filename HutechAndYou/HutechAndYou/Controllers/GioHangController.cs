using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using System.Web.Mvc;
using HutechAndYou.Models;

namespace HutechAndYou.Controllers
{
    public class GioHangController : Controller
    {
        dbQLBLaptopDataContext data = new dbQLBLaptopDataContext();
        //Giỏ Hàng      
        public List<GioHang> LayGioHang()
        {
            List<GioHang> lGioHang = Session["GioHang"] as List<GioHang>;
            if (lGioHang == null)
            {
                lGioHang = new List<GioHang>();
                Session["GioHang"] = lGioHang;
            }
            return lGioHang;
        }
        public ActionResult ThemGioHang(int iMaSP, string strURL)
        {
            List<GioHang> lGioHang = LayGioHang();
            GioHang sanpham = lGioHang.Find(n => n.iMaSP == iMaSP);
            if (sanpham == null)
            {
                sanpham = new GioHang(iMaSP);
                lGioHang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.iSoLuong++;
                return Redirect(strURL);
            }

        }
        //Tính Số Lượng
        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<GioHang> lGioHang = Session["GioHang"] as List<GioHang>;
            if (lGioHang != null)
            {
                iTongSoLuong = lGioHang.Sum(n => n.iSoLuong);
            }
            return iTongSoLuong;
        }
        //Tính Tổng Số Tiền
        private double TongTien()
        {
            double iTongTien = 0;
            List<GioHang> lGioHang = Session["GioHang"] as List<GioHang>;
            if (lGioHang != null)
            {
                iTongTien = lGioHang.Sum(n => n.dThanhTien);
            }
            return iTongTien;
        }
        //Số dư
        private Double DuTien()
        {
            Double DT;
            KhachHang kh = (KhachHang)Session["TaiKhoan"];
            KhachHang kh2 = data.KhachHangs.SingleOrDefault(n => n.IDKH == kh.IDKH);
            DT = double.Parse(kh2.SoTienCo.ToString());
            return dSoDu;

        }
        public Double dSoDu { set; get; }
        private double SoDu()
        {
            double iTongTien = 0;
            double iSoDu = 0;
            List<GioHang> lGioHang = Session["GioHang"] as List<GioHang>;
            if (lGioHang != null)
            {
                iTongTien = lGioHang.Sum(n => n.dThanhTien);
                KhachHang kh = (KhachHang)Session["TaiKhoan"];
                KhachHang kh2 = data.KhachHangs.SingleOrDefault(n => n.IDKH == kh.IDKH);
                dSoDu = double.Parse(kh2.SoTienCo.ToString());
                iSoDu = dSoDu - iTongTien;
            }
            return iSoDu;
        }

        //Giỏ hàng
        public ActionResult GioHangCuaBan()
        {
            List<GioHang> lGioHang = LayGioHang();
            if (lGioHang.Count == 0)
            {
                return RedirectToAction("MuaHang","Home");
            }
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(lGioHang);
        }

        //Hiện thị trên thanh công cụ
        public ActionResult GiohangPartial()
        {
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return PartialView();
        }

        //Xóa giỏ hàng
        public ActionResult XoaGioHang(int iMaSP)
        {
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sanpham = lstGioHang.SingleOrDefault(n => n.iMaSP == iMaSP);
            if (sanpham != null)
            {
                lstGioHang.RemoveAll(n => n.iMaSP == iMaSP);
                return RedirectToAction("GioHangCuaBan");
            }
            if (lstGioHang.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("GioHangCuaBan");
        }

        //Cập nhật số lượng
        public ActionResult CapNhatSoLuong (int iMaSP, FormCollection f)
        {
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sanpham = lstGioHang.SingleOrDefault(n => n.iMaSP == iMaSP);
            if(sanpham != null)
            {
                sanpham.iSoLuong = int.Parse(f["cnsoluong"].ToString());
            }
            return RedirectToAction("GioHangCuaBan");
        }

        //Xóa tất cả
        public ActionResult XoaHetGioHang()
        {
            List<GioHang> lstGioHang = LayGioHang();
            lstGioHang.Clear();
            return RedirectToAction("Index", "Home");
        }

        //Đặt hàng
        [HttpGet]
        public ActionResult DatHang()
        {
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("Index", "Login");
            }
            if (Session["Giohang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<GioHang> lstGioHang = LayGioHang();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            ViewBag.SoDu = SoDu();
            ViewBag.DT = DuTien();
            return View(lstGioHang);
        }

        //Xác nhận đặt hàng
        [HttpPost]
        public ActionResult DatHang(FormCollection collection)
        {
            DonDatHang ddh = new DonDatHang();
            KhachHang kh = (KhachHang)Session["TaiKhoan"];
            KhachHang kh2 = data.KhachHangs.SingleOrDefault(n => n.IDKH == kh.IDKH);
            List<GioHang> gh = LayGioHang();
            ddh.IDKH = kh.IDKH;
            ddh.NgayDat = DateTime.Now;
            var ngaygiao = String.Format("{0:MM/dd/yyyy}", collection["NgayGiao"]);
            double sd = double.Parse(SoDu().ToString());
            if(sd>=0)
            {
                if (String.IsNullOrEmpty(ngaygiao))
                {
                    ViewData["Loi"] = "thời gian nhận mong muốn?";
                }

                else
                {
                    ddh.NgayGiao = DateTime.Parse(ngaygiao);
                    ddh.TinhTrangGiaoHang = "false";
                    ddh.DaThanhToan = int.Parse(TongTien().ToString()); ;
                    data.DonDatHangs.InsertOnSubmit(ddh);
                    data.SubmitChanges();
                    //them chi tiet gio hang
                    foreach (var laptop in gh)
                    {
                        ChiTietDDH ctdh = new ChiTietDDH();
                        ctdh.MaDH = ddh.MaDH;
                        ctdh.MaSP = laptop.iMaSP;
                        ctdh.Soluong = laptop.iSoLuong;
                        ctdh.Dongia = (decimal)laptop.dDonGia;
                        kh2.SoTienCo = int.Parse(SoDu().ToString());
                        UpdateModel(kh2);
                        data.ChiTietDDHs.InsertOnSubmit(ctdh);
                    }
                    data.SubmitChanges();

                    Session["Giohang"] = null;
                    return RedirectToAction("XacNhanDonHang", "Giohang");
                }
            }
            else
            {
                ViewData["Loi"] = "tài khoản quý khách không đủ?";
            }
            return this.DatHang();
        }
        public ActionResult XacNhanDonHang()
        {
            return View();
        }

        //Quản Lý đơn hàng
        public ActionResult QuanLyDonHang(int? page)
        {
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("Index", "Login");
            }
            KhachHang kh = (KhachHang)Session["TaiKhoan"];
            int ID1 = kh.IDKH;
            int pageSize = 10;
            int pageNum = (page ?? 1);
            var donhang = from s in data.DonDatHangs
                          where s.IDKH == ID1
                          select s;
            return View(donhang.ToPagedList(pageNum, pageSize));
        }

        //Chi tiết đơn hàng
        public ActionResult ChiTietDonHang(int ID, int? page)
        {
            int pageSize = 10;
            int pageNum = (page ?? 1);
            var CTDDH = from s in data.ChiTietDDHs
                        where s.MaDH == ID
                        select s;
            return View(CTDDH.ToPagedList(pageNum, pageSize));
        }

        // Xóa sản phẩm
        [HttpGet]
        public ActionResult XoaSanPham(int id)
        {
            DonDatHang ddh = data.DonDatHangs.SingleOrDefault(n => n.MaDH == id);
            ChiTietDDH ctdh = data.ChiTietDDHs.SingleOrDefault(n => n.MaDH == id);
            ViewBag.ddh = ddh.MaDH;
            ViewBag.ctdh = ctdh.MaDH;
            if (ddh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(ddh);
        }
        [HttpPost, ActionName("xoasanpham")]
        public ActionResult XNXoaSanPham(int id)
        {
            DonDatHang ddh = data.DonDatHangs.SingleOrDefault(n => n.MaDH == id);
            if(ddh.TinhTrangGiaoHang=="false")
            {
                ChiTietDDH ctdh = data.ChiTietDDHs.SingleOrDefault(n => n.MaDH == id);
                KhachHang kh = (KhachHang)Session["TaiKhoan"];
                KhachHang kh2 = data.KhachHangs.SingleOrDefault(n => n.IDKH == kh.IDKH);
                double ngu = double.Parse(kh2.SoTienCo.ToString());
                double ngu2 = double.Parse(ddh.DaThanhToan.ToString());
                double ngu3 = ngu + ngu2;
                ViewBag.ddh = ddh.MaDH;
                ViewBag.ctdh = ctdh.MaDH;
                if (ddh == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                kh2.SoTienCo = int.Parse(ngu3.ToString());
                UpdateModel(kh2);
                data.ChiTietDDHs.DeleteOnSubmit(ctdh);
                data.DonDatHangs.DeleteOnSubmit(ddh);
                data.SubmitChanges();
            }
            else
            {
                return RedirectToAction("XoaSanPhamLoi", "GioHang", routeValues: new { id = ddh.MaDH });
            }
            return RedirectToAction("QuanLyDonHang", "GioHang");
        }
        public ActionResult XoaSanPhamLoi(int id)
        {
            DonDatHang ddh = data.DonDatHangs.SingleOrDefault(n => n.MaDH == id);
            ChiTietDDH ctdh = data.ChiTietDDHs.SingleOrDefault(n => n.MaDH == id);
            ViewBag.ddh = ddh.MaDH;
            ViewBag.ctdh = ctdh.MaDH;
            if (ddh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(ddh);
        }

        // chitiettaikhoan
        [HttpGet]
        public ActionResult ChiTietTaiKhoan()
        {
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("Index", "Login");              
            }
            KhachHang kh = (KhachHang)Session["TaiKhoan"];
            KhachHang kh2 = data.KhachHangs.SingleOrDefault(n => n.IDKH == kh.IDKH);
            ViewBag.dn = kh.HoTen;
            double sd = double.Parse(kh2.SoTienCo.ToString());
            ViewBag.dc2 = kh2.DiaChi;
            ViewBag.ht = kh2.HoTen;
            ViewBag.DT = int.Parse(sd.ToString());
            ViewBag.em = kh2.Email;
            ViewBag.sdt = kh2.DienThoai;
            return View();
        }

        //Xác nhận đặt hàng
        [HttpPost]
        public ActionResult ChiTietTaiKhoan(FormCollection collection)
        {
            KhachHang kh = (KhachHang)Session["TaiKhoan"];
            KhachHang kh2 = data.KhachHangs.SingleOrDefault(n => n.IDKH == kh.IDKH);
            var ht = collection["hoten"];
            var dc = collection["diachi"];
            if (string.IsNullOrEmpty(ht))
            {
                ViewData["loi"] = "mời nhập họ tên";
            }
            if (string.IsNullOrEmpty(dc))
            {
                ViewData["loi"] = "mời nhập dia chi";
            }
            else
            {
                if (dc.Length!=0 && ht.Length!=0)
                {
                    kh2.DiaChi = dc;
                    kh2.HoTen = ht;
                    UpdateModel(kh2);
                }
                data.SubmitChanges();
            }
            return this.ChiTietTaiKhoan();
        }

        public ActionResult Hientaikhoan()
        {
            if (Session["TaiKhoan"] != null)
            {
                KhachHang kh = (KhachHang)Session["TaiKhoan"];
                KhachHang kh2 = data.KhachHangs.SingleOrDefault(n => n.IDKH == kh.IDKH);
                ViewBag.dn = kh2.HoTen;
            }
            else
            {
                ViewBag.dn = "Đăng nhập";
            }
            return PartialView();
        }
    }
}