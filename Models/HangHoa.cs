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
    
    public partial class HangHoa
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HangHoa()
        {
            this.ChiTietPhieuNhapHangs = new HashSet<ChiTietPhieuNhapHang>();
            this.ChiTietPhieuXuatHangs = new HashSet<ChiTietPhieuXuatHang>();
            this.ThongKeBanHangs = new HashSet<ThongKeBanHang>();
            this.Khoes = new HashSet<Kho>();
        }
    
        public string MaHangHoa { get; set; }
        public string TenHangHoa { get; set; }
        public string MaDanhMuc { get; set; }
        public string MaNhaCungCap { get; set; }
        public decimal DonGia { get; set; }
        public int SoLuong { get; set; }
        public string TrangThai { get; set; }
        public System.DateTime NgayNhap { get; set; }
        public System.DateTime NgaySX { get; set; }
        public System.DateTime NgayHaoHut { get; set; }
        public string GhiChu { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietPhieuNhapHang> ChiTietPhieuNhapHangs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietPhieuXuatHang> ChiTietPhieuXuatHangs { get; set; }
        public virtual DanhMucSanPham DanhMucSanPham { get; set; }
        public virtual NhaCungCap NhaCungCap { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ThongKeBanHang> ThongKeBanHangs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Kho> Khoes { get; set; }
    }
}
