using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace YourProjectNamespace.Controllers // 💡 這裡請維持你原本的命名空間
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        // 💡 關鍵：建立一個靜態的記憶體清單，只要 Render 沒有重啟，所有人的資料都會同步存在這！
        private static readonly List<object> _globalBookings = new List<object>();

        // 1. 這是前端網頁送出預約的 API (POST: api/booking)
        [HttpPost]
        public IActionResult CreateBooking([FromBody] System.Text.Json.JsonElement data)
        {
            try
            {
                // 將收到的預約資料塞進雲端全域清單中
                _globalBookings.Add(data);

                // 回傳成功給前端網頁
                return Ok(new { success = true, message = "預約成功！資料已同步至雲端管理後台。" });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        // 2. 這是你的設計師後台撈資料的 API (GET: api/booking)
        // 💡 你的後台管理網頁只要用 fetch(API_URL, { method: 'GET' }) 呼叫這一條，就能撈到同步更新的全部資料！
        [HttpGet]
        public IActionResult GetAllBookings()
        {
            return Ok(_globalBookings);
        }
    }
}