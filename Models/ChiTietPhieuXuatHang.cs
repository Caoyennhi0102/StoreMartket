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
    
    public partial class ChiTietPhieuXuatHang
    {
        public string MaPhieuXuat { get; set; }
        public string MaHangHoa { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public decimal ThanhTien { get; set; }
    
        public virtual HangHoa HangHoa { get; set; }
        public virtual PhieuXuatHang PhieuXuatHang { get; set; }
    }
}
