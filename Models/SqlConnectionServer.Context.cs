﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SqlConnectionDatabase : DbContext
    {
        public SqlConnectionDatabase()
            : base("name=SqlConnectionDatabase")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<BacLuong> BacLuongs { get; set; }
        public virtual DbSet<BoPhan> BoPhans { get; set; }
        public virtual DbSet<ChiTietPhieuNhapHang> ChiTietPhieuNhapHangs { get; set; }
        public virtual DbSet<ChiTietPhieuXuatHang> ChiTietPhieuXuatHangs { get; set; }
        public virtual DbSet<ChucVu> ChucVus { get; set; }
        public virtual DbSet<Cong> Congs { get; set; }
        public virtual DbSet<CuaHang> CuaHangs { get; set; }
        public virtual DbSet<DanhMucSanPham> DanhMucSanPhams { get; set; }
        public virtual DbSet<HangHoa> HangHoas { get; set; }
        public virtual DbSet<HDLaoDong> HDLaoDongs { get; set; }
        public virtual DbSet<HoaDonThanhToan> HoaDonThanhToans { get; set; }
        public virtual DbSet<KhachHangThanThiet> KhachHangThanThiets { get; set; }
        public virtual DbSet<Kho> Khoes { get; set; }
        public virtual DbSet<KPI> KPIs { get; set; }
        public virtual DbSet<Luong> Luongs { get; set; }
        public virtual DbSet<NhaCungCap> NhaCungCaps { get; set; }
        public virtual DbSet<NhanVien> NhanViens { get; set; }
        public virtual DbSet<PhieuNhapHang> PhieuNhapHangs { get; set; }
        public virtual DbSet<PhieuTraHang> PhieuTraHangs { get; set; }
        public virtual DbSet<PhieuXuatHang> PhieuXuatHangs { get; set; }
        public virtual DbSet<POS_Machines> POS_Machines { get; set; }
        public virtual DbSet<QLCaNV> QLCaNVs { get; set; }
        public virtual DbSet<Quyen> Quyens { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<ThongKeBanHang> ThongKeBanHangs { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<VaiTro> VaiTroes { get; set; }
    }
}
