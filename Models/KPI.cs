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
    
    public partial class KPI
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KPI()
        {
            this.Luongs = new HashSet<Luong>();
        }
    
        public int MaKPI { get; set; }
        public int MaNhanVien { get; set; }
        public string LoaiKPI { get; set; }
        public decimal GiaTriKPI { get; set; }
        public System.DateTime NgayDanhGiaKPI { get; set; }
        public string TrangThai { get; set; }
    
        public virtual NhanVien NhanVien { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Luong> Luongs { get; set; }
    }
}