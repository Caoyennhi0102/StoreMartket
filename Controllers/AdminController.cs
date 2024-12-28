using StoreMartket.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Services.Description;

namespace StoreMartket.Controllers
{
    // Đánh dấu phân quyền đăng nhập Admin
    // [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {


        private readonly SqlConnectionDatabase _sqlConnectionDatabase;

        public AdminController()
        {
            _sqlConnectionDatabase = new SqlConnectionDatabase();
        }

        /*
        protected override void OnActionExecuting(ActionExecutingContext actionExecutingContext)
        {
            if (Session["UserName"] == null)
            {
                actionExecutingContext.Result = RedirectToAction("Login", "Login");
            }
            base.OnActionExecuting(actionExecutingContext);
        }*/
        public ActionResult Dashboard()
        {
            bool IsSessionActive = (Session["UserName"] != null);
            ViewBag.IsSessionActive = IsSessionActive;
            return View();
        }
        public ActionResult LogOut(User user)
        {
            FormsAuthentication.SignOut();
            user.TGDangXuat = DateTime.Now;
            _sqlConnectionDatabase.SaveChanges();
            Session.Clear();
            return RedirectToAction("Login", "Admin");
        }
        private int GenerateCuaHangId()
        {
            // Lấy mã cửa hàng cao nhất hiện tại
            var maxID = _sqlConnectionDatabase.CuaHangs.Select(CH => CH.MaCuaHang).DefaultIfEmpty(0).Max();

            // Tăng mã lên 1
            return maxID + 1;
        }

        public string GetFormattedCuaHangId()
        {
            int NewStoreID = GenerateCuaHangId();
            return "CH" + NewStoreID.ToString("D4");
        }

        // Phương thức kiểm tra địa  chỉ 
        public bool CheckAddress(string address)
        {
            if (string.IsNullOrEmpty(address))
            {
                // Địa chỉ không được để trống
                return false;
            }
            if (address.Length < 10)
            {
                // Địa chỉ quá ngắn
                return false;
            }
            string allowedCharsPattern = @"^[a-zA-Z0-9\s,.-]+$";
            if (!System.Text.RegularExpressions.Regex.IsMatch(address, allowedCharsPattern))
            {
                // Địa chỉ chứa ký tự không hợp lệ
                return false;
            }
            return true;

        }
        // Phương thức kiểm tra địa chỉ Email 
        public bool CheckEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return false;
            }
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            if (!System.Text.RegularExpressions.Regex.IsMatch(email, emailPattern))
            {
                return false;
            }
            return true;
        }
        public bool IsPhoneNumberValid(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
            {
                return false;
            }
            if (phoneNumber.Length < 10)
            {
                return false;
            }
            string pattern = @"^\d{10}$";
            return Regex.IsMatch(pattern, phoneNumber);
        }

        public bool CheckTaxCode(string taxCode)
        {
            if (string.IsNullOrEmpty(taxCode))
            {
                return false;
            }
            string pattern = @"^\d{10}$|^\d{14}$";
            return Regex.IsMatch(taxCode, pattern);
        }
        public string GenerateStoreEmail(string tenCH, int maCH)
        {
            string emailPrefix = tenCH.Trim().ToLower().Replace(" ", ".");
            string email = $"{emailPrefix}.{maCH}@company.com";
            return email;
        }
        [HttpGet]
        public JsonResult GetStoreList()
        {
            var stores = _sqlConnectionDatabase.CuaHangs.ToList();
            return Json(stores, JsonRequestBehavior.AllowGet);
        }
        // GET: Admin
        [HttpGet]
        public ActionResult AddStore()
        {

            return View();
        }
        [HttpPost]
        public ActionResult AddStore(string tenCH, string diaChi, string dienThoai, string email, string mst, int chTruong)
        {
            // Kiểm tra tên cửa hàng có để trống hay không 
            if (string.IsNullOrEmpty(tenCH))
            {
                return Json(new { success = false, message = "Tên cửa hàng không được để trống" });

            }
            if (!CheckAddress(diaChi))
            {
                return Json(new { success = false, message = "Địa chỉ không hợp lệ. Vui lòng kiểm tra lại." });
            }
            if (!CheckAddress(email))
            {
                return Json(new { success = false, message = "Địa chỉ email không hợp lệ. Vui lòng kiểm tra lại." });
            }
            if (!IsPhoneNumberValid(dienThoai))
            {
                return Json(new { success = false, message = "Số điện thoại không hợp lệ.Vui lòng kiểm tra lại." });
            }

            if (!CheckAddress(mst))
            {
                return Json(new { success = false, message = "Mã số thuế không hợp lệ .Vui lòng kiểm tra lại." });
            }
            var CHTruong = _sqlConnectionDatabase.NhanViens.FirstOrDefault(nv => nv.MaNhanVien == chTruong);
            if (CHTruong == null)
            {
                return Json(new { success = false, message = "Mã nhân viên trưởng cửa hàng không hợp lệ." });
            }

            int maCH = GenerateCuaHangId();
            int soluongNhanVien = _sqlConnectionDatabase.NhanViens.Count(nv => nv.MaCuaHang == maCH);
            // Truyền dữ liệu qua ViewData
            ViewData["SLNV"] = soluongNhanVien;
            string generatedEmail = GenerateStoreEmail(tenCH, maCH);
            var newStore = new CuaHang
            {
                MaCuaHang = maCH,
                TenCH = tenCH,
                DiaChi = diaChi,
                DienThoai = dienThoai,
                Email = generatedEmail,
                MST = mst,
                CHTruong = chTruong,
                SLNV = soluongNhanVien
            };
            _sqlConnectionDatabase.CuaHangs.Add(newStore);
            _sqlConnectionDatabase.SaveChanges();

            return Json(new { success = true, message = "Thêm cửa hàng thành công", maCH = maCH });
        }
        [HttpGet]
        public ActionResult UpdateStore()
        {
            return View();
        }
        [HttpPost]
        public JsonResult SearchStore(string maCH)
        {
            try
            {
                if (string.IsNullOrEmpty(maCH) || !int.TryParse(maCH, out var storeId))
                {
                    return Json(new { success = false, message = "Mã cửa hàng không hợp lệ." });
                }


                var store = _sqlConnectionDatabase.CuaHangs.FirstOrDefault(CH => CH.MaCuaHang == storeId);
                if (store == null)
                {
                    return Json(new { success = false, message = "Cửa hàng không tồn tại" });
                }
                // Trả về thông tin cửa hàng dưới dạng JSON
                return Json(new
                {
                    success = true,
                    store = new
                    {
                        store.MaCuaHang,
                        store.TenCH,
                        store.DiaChi,
                        store.DienThoai,
                        store.Email,
                        store.MST,
                        store.CHTruong,
                        store.SLNV
                    }
                });
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = $"Có lỗi xảy ra trong quá trình cập nhật. Vui lòng liên hệ với quản trị viên.{ex.Message}" });
            }
        }

        [HttpPost]
        public ActionResult UpdateStore(int? maCH, string tenCH, string diaChi, string dienThoai, string email, string mst, int? chTruong)
        {
            try
            {
                if (maCH == null)
                {
                    Console.WriteLine("Giá trị maCH: " + maCH);
                    return Json(new { success = false, message = "Mã cửa hàng không hợp lệ." });
                }

                if (_sqlConnectionDatabase == null || _sqlConnectionDatabase.CuaHangs == null)
                {
                    return Json(new { success = false, message = "Không thể truy cập dữ liệu cửa hàng. Vui lòng kiểm tra lại." });
                }
                var store = _sqlConnectionDatabase.CuaHangs.FirstOrDefault(CH => CH.MaCuaHang == maCH);
                if (store == null)
                {
                    return Json(new { success = false, message = "Cửa hàng không tồn tại" });
                }
                if (string.IsNullOrEmpty(tenCH))
                {
                    return Json(new { success = false, message = "Tên cửa hàng không được để trống" });

                }
                if (!CheckAddress(diaChi))
                {
                    return Json(new { success = false, message = "Địa chỉ không hợp lệ. Vui lòng kiểm tra lại." });
                }
                if (!CheckAddress(email))
                {
                    return Json(new { success = false, message = "Địa chỉ email không hợp lệ. Vui lòng kiểm tra lại." });
                }
                if (!IsPhoneNumberValid(dienThoai))
                {
                    return Json(new { success = false, message = "Số điện thoại không hợp lệ.Vui lòng kiểm tra lại." });
                }

                if (!CheckAddress(mst))
                {
                    return Json(new { success = false, message = "Mã số thuế không hợp lệ .Vui lòng kiểm tra lại." });
                }

                if (chTruong == null || !_sqlConnectionDatabase.NhanViens.Any(nv => nv.MaNhanVien == chTruong.Value))
                {
                    return Json(new { success = false, message = "Mã nhân viên trưởng cửa hàng không hợp lệ." });
                }
                int soluongNhanVien = _sqlConnectionDatabase.NhanViens.Count(nv => nv.MaCuaHang == maCH);
                // Truyền dữ liệu qua ViewData
                ViewData["SLNV"] = soluongNhanVien;
                store.TenCH = tenCH;
                store.DiaChi = diaChi;
                store.DienThoai = dienThoai;
                store.Email = email;
                store.MST = mst;
                store.CHTruong = (int)chTruong;
                store.SLNV = soluongNhanVien;
                _sqlConnectionDatabase.SaveChanges();

                return Json(new { success = true, message = "Cập nhật cửa hàng thành công" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Có lỗi xảy ra trong quá trình cật nhật{ex.Message}" });
            }


        }

        [HttpGet]
        public ActionResult DeleteStore()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DeleteStore(int? maCH)
        {
            try
            {
                if (maCH == null)
                {
                    return Json(new { success = false, message = "Mã cửa hàng không hợp lệ." });
                }
                if (_sqlConnectionDatabase == null || _sqlConnectionDatabase.CuaHangs == null)
                {
                    return Json(new { success = false, message = "Không thể truy cập dữ liệu cửa hàng. Vui lòng kiểm tra lại." });
                }
                var store = _sqlConnectionDatabase.CuaHangs.Find(maCH);
                if (store != null)
                {
                    var deleteStore = _sqlConnectionDatabase.BoPhans.Where(u => u.MaCuaHang == maCH);


                    if (deleteStore.Any())
                    {

                        _sqlConnectionDatabase.BoPhans.RemoveRange(deleteStore);
                    }
                    var deleteStoreNV = _sqlConnectionDatabase.NhanViens.Where(u => u.MaCuaHang == maCH);
                    if (deleteStoreNV.Any())
                    {
                        _sqlConnectionDatabase.NhanViens.RemoveRange(deleteStoreNV);
                    }

                    _sqlConnectionDatabase.CuaHangs.Remove(store);
                    _sqlConnectionDatabase.SaveChanges();


                }
                return Json(new { success = false, message = "Cửa hàng không tồn tại" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Có lỗi xảy ra trong quá trình xóa {ex.Message}" });
            }
        }
     
        public ActionResult GetStore()
        {
            try
            {
                var getStore = _sqlConnectionDatabase.CuaHangs.Select(Ch => new { Ch.MaCuaHang, Ch.TenCH }).ToList();
                return Json(getStore, JsonRequestBehavior.AllowGet);
            }catch(Exception ex)
            {
                return Json(new { success = false, message = $"Có lỗi xảy ra trong quá trình gọi danh sách cửa hàng{ex.Message}" });
            }
        }
        public string CreateCodeDepartments(string TenBP)
        {
            if (string.IsNullOrEmpty(TenBP))
            {
                return "Tên bộ phận không được để trống.";

            }
            string[] from = TenBP.Trim().Split(new char[] {' '},  StringSplitOptions.RemoveEmptyEntries);

            string Firstcharacter = string.Concat(from.Select(t => t.ToString().ToUpper()));

            return $"BP_{Firstcharacter}";

        }
        [HttpGet]
        public ActionResult CreateDepartments()
        {
            var store = _sqlConnectionDatabase.CuaHangs.Select(ch =>  new {ch.MaCuaHang, ch.TenCH}).ToList();
            ViewBag.Store = new SelectList(store, "MaCuaHang", "TenCH");
            return View();
        }
        [HttpPost]
        public ActionResult CreateDepartments(string TenBP, int? maCH)
        {
            try
            {
                if(string.IsNullOrEmpty(TenBP) || maCH == null)
                {
                    return Json(new { success = false, message = "Tên bộ phận và mã cửa hàng không được để trống." });

                }
                string MaBP = CreateCodeDepartments(TenBP);
                var department = new BoPhan
                {
                    MaBoPhan = MaBP,
                    TenBoPhan = TenBP,
                    MaCuaHang =(int) maCH,
                };
                _sqlConnectionDatabase.BoPhans.Add(department);
                _sqlConnectionDatabase.SaveChanges();
                return Json(new { success = true, message = "Thêm bộ phận thành công!" });
            }
            catch(Exception ex)
            {
                return Json(new { success = false, message = $"Có lỗi xảy ra trong quá trình thêm bộ phận{ex.Message}" });
            }
        }
        [HttpGet]
        public ActionResult GetDepartments()
        {
            try
            {
                var departments = _sqlConnectionDatabase.BoPhans.Select(
                    bp => new
                    {
                        bp.MaBoPhan,
                        bp.TenBoPhan,
                        bp.MaCuaHang
                    })
                    .ToList();
                return Json(departments, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(new { success = false, message = $"Có lỗi xảy ra khi tải danh sách bộ phận: {ex.Message}" });
            }
        }




    }
}