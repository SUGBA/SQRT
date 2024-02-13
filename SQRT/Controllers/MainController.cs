using Microsoft.AspNetCore.Mvc;

namespace SQRT.Controllers
{
    [Route("Main")]
    public class MainController : Controller
    {
        /// <summary>
        /// Получить Вьюшку
        /// </summary>
        /// <returns></returns>
        [HttpGet("MainView")]
        public IActionResult MainView()
        {
            return View("Main");
        }

        /// <summary>
        /// Установить язык
        /// </summary>
        /// <returns></returns>
        [HttpPost("SetLanguage/{languageId}")]
        public IActionResult SetLanguage(int languageId)
        {
            HttpContext.Session.SetInt32("language", languageId);
            return View();
        }
    }
}
