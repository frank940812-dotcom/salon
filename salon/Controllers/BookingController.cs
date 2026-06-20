using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace SalonSystem.Controllers // 👈 如果你的專案名稱不是 SalonSystem，請改成你的專案名稱
{
    [ApiController]
    [Route("api/[controller]")] // 網址：/api/booking
    public class BookingController : ControllerBase
    {
        // 用一個記憶體暫存清單當臨時資料庫，確保 Demo 當天絕對能動、絕不漏氣
        private static readonly List<object> TempDB = new List<object>();

        // 1. 前端送出預約 (POST /api/booking)
        [HttpPost]
        public IActionResult CreateAppointment([FromBody] BookingModel model)
        {
            if (model == null || string.IsNullOrEmpty(model.Name))
            {
                return Ok(new { success = false, message = "欄位資料不能為空！" });
            }

            // 模擬存入資料庫
            TempDB.Add(new
            {
                id = TempDB.Count + 1,
                time = model.Time,
                name = model.Name,
                phone = model.Phone,
                service = model.Service,
                finalPrice = model.FinalPrice,
                designer = model.Designer,
                status = "待服務"
            });

            // 回傳前端 JavaScript 看得懂的小寫 success
            return Ok(new { success = true });
        }

        // 2. 後台撈取全部資料 (GET /api/booking)
        [HttpGet]
        public IActionResult GetAllAppointments()
        {
            return Ok(TempDB);
        }
    }

    // 💡 直接把 Model 定義在同一個檔案最下方，就不用額外建 Model 檔案了！
    public class BookingModel
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Time { get; set; }
        public string Service { get; set; }
        public string Designer { get; set; }
        public int FinalPrice { get; set; }
    }
}