using Microsoft.AspNetCore.Mvc;
using SQRT.Models;

namespace SQRT.Controllers
{
    [Route("Main")]
    public class MainController : Controller
    {
        /// <summary>
        /// Название страницы на русском
        /// </summary>
        private const string RU_FILE_NAME = "RuMain";
        /// <summary>
        /// Название страницы на английском
        /// </summary>
        private const string EN_FILE_NAME = "EnMain";
        /// <summary>
        /// Название странцы на китайском
        /// </summary>
        private const string CH_FILE_NAME = "ChMain";
        /// <summary>
        /// Название странцы на китайском
        /// </summary>
        private const string ES_FILE_NAME = "EsMain";

        /// <summary>
        /// Получить Вьюшку
        /// </summary>
        /// <returns></returns>
        [HttpGet("MainView")]
        public IActionResult MainView()
        {
            var languageId = HttpContext.Session.GetInt32("language");
            var language = languageId == null ? LanguageEnum.Ru : (LanguageEnum)languageId;
            switch (language)
            {
                case LanguageEnum.Ru:
                    return View(RU_FILE_NAME);
                case LanguageEnum.En:
                    return View(EN_FILE_NAME);
                case LanguageEnum.Ch:
                    return View(CH_FILE_NAME);
                case LanguageEnum.Es:
                    return View(ES_FILE_NAME);
                default:
                    return View(RU_FILE_NAME);
            }
        }

        /// <summary>
        /// Установить язык
        /// </summary>
        /// <returns></returns>
        [HttpGet("SetLanguage/{languageId}")]
        public IActionResult SetLanguage(int languageId)
        {
            HttpContext.Session.SetInt32("language", languageId);
            return RedirectToAction("MainView");
        }
    }
}
