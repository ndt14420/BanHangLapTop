using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HutechAndYou.Models
{
    public class GioHang
    { 
            dbQLBLaptopDataContext data = new dbQLBLaptopDataContext();
            public int iMaSP { set; get; }
            public string sTenSP { set; get; }
            public string sAnhMH { set; get; }
            public string sCPU { set; get; }
            public string sLoaiCPU { set; get; }
            public string sRam { set; get; }
            public string sManHinh { set; get; }
            public string sCard { set; get; }
            public string sOCung { set; get; }
            public string sNoiDung { set; get; }
            public Double dDonGia { set; get; }
            public int iSoLuong { set; get; }
            public Double dThanhTien
            {
                get { return iSoLuong * dDonGia; }
            }
            public GioHang(int MaSP)
            {
               iMaSP = MaSP;
               SanPham SanPham = data.SanPhams.Single(n => n.MaSP == iMaSP);
               sTenSP = SanPham.TenSP;
               sAnhMH = SanPham.AnhMH;
               sOCung = SanPham.OCung;
               sManHinh = SanPham.ManHinh;
               sRam = SanPham.Ram;
               sCPU = SanPham.CPU;
               sLoaiCPU = SanPham.LoaiCpu;
               sNoiDung = SanPham.NoiDung;
               sCard = SanPham.Crad;
               dDonGia = double.Parse(SanPham.GiaBan.ToString());
               iSoLuong = 1;
            }       
    }
}