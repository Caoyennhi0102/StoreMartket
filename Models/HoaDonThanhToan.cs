//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StoreMartket.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class HoaDonThanhToan
    {
        public string MaHoaDon { get; set; }
        public string LoaiKH { get; set; }
        public string MaKhachHang { get; set; }
        public int MaNhanVien { get; set; }
        public System.DateTime NgayThanhToan { get; set; }
        public decimal TongTien { get; set; }
        public string PhuongThucThanhToan { get; set; }
        public string TrangThai { get; set; }
    
        public virtual KhachHangThanThiet KhachHangThanThiet { get; set; }
        public virtual NhanVien NhanVien { get; set; }
    }
}
