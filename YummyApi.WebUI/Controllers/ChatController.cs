using Microsoft.AspNetCore.Mvc;

namespace YummyApi.WebUI.Controllers
{
    public class ChatController : Controller
    {
        public IActionResult SendChatWithAI()
        {
            return View();
        }
    }
}
