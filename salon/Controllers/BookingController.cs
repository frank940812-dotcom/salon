using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace YourProjectNamespace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        // 1. 全域預約紀錄暫存
        private static readonly List<object> _globalBookings = new List<object>();

        // 2. 關鍵：直接預設好三位設計師資料！讓後台一打開就有名單
        private static readonly List<object> _globalDesigners = new List<object>()
        {
            new { name = "Andy", title = "總監 - 精修男士/極短髮" },
            new { name = "Bella", title = "資深 - 韓系燙髮/線條染" },
            new { name = "Chris", title = "專業 - 歐美漂染/頭皮理療" }
        };

        // 前端送出預約 (POST: api/booking)
        [HttpPost]
        public IActionResult CreateBooking([FromBody] System.Text.Json.JsonElement data)
        {
            _globalBookings.Add(data);
            return Ok(new { success = true, message = "預約成功！" });
        }

        // 後台撈取預約紀錄 (GET: api/booking)
        [HttpGet]
        public IActionResult GetAllBookings()
        {
            return Ok(_globalBookings);
        }

        // 💡 3. 新增：給設計師後台撈取名冊的 API (GET: api/booking/designers)
        [HttpGet("designers")]
        public IActionResult GetDesigners()
        {
            return Ok(_globalDesigners);
        }

        // 💡 4. 新增：讓後台點擊「儲存人員」時可以寫入的 API (POST: api/booking/designers)
        [HttpPost("designers")]
        public IActionResult AddDesigner([FromBody] System.Text.Json.JsonElement data)
        {
            _globalDesigners.Add(data);
            return Ok(new { success = true, message = "設計師新增成功！" });
        }
    }
}