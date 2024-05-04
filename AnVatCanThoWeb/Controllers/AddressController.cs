using AnVatCanTho.DataAccess.Data;
using AnVatCanTho.Models;
using Microsoft.AspNetCore.Mvc;

namespace AnVatCanThoWeb.Controllers
{
    public class AddressController : Controller
    {
        private readonly ApplicationDbContext _db;

        public AddressController(ApplicationDbContext db)
        {
            _db = db;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}



        [HttpPost]
        public ActionResult AddAddress(Address address)
        {
            bool flag = false;
            // Tạo quận huyện mới
            var foundDistrict = _db.Districts.FirstOrDefault(x => x.Name == address.DistrictName);
            if (foundDistrict is null)
            {
                _db.Add(new District()
                {
                    Name = address.DistrictName
                });
                flag = true;
            }

            // Tạo phường xã mới

            var foundWard = _db.Wards.FirstOrDefault(x => x.DistrictName == address.DistrictName && x.Name == address.WardName);

            if (foundWard is null)
            {
                _db.Add(new Ward()
                {
                    Name = address.WardName,
                    DistrictName = address.DistrictName
                });
                flag = true;
            }

            // Tạo địa chỉ mới
            var foundAddress = _db.Addresses.FirstOrDefault(x => 
                x.CustomerId == address.CustomerId &&
                x.WardName == address.WardName &&
                x.DistrictName == address.DistrictName &&
                x.NoAndStreet == address.NoAndStreet);

            if (foundAddress is null)
            {
                _db.Add(new Address()
                {
                    WardName = address.WardName,
                    DistrictName = address.DistrictName,
                    CustomerId = address.CustomerId,
                    NoAndStreet = address.NoAndStreet,
                });
                flag = true;
            }


            if( flag  == true)
            {
                _db.SaveChanges();
                TempData["Success"] = "Tạo địa chỉ mới thành công!";
            }
            else
            {
                TempData["Failed"] = "Địa chỉ đã tồn tại. Vui lòng nhập địa chỉ khác!";
            }

            return RedirectToAction("Order", "Product");
        }
    }
}
