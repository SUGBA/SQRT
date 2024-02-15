using Microsoft.AspNetCore.Mvc;
using SQRT.Models;
using SQRT.Services;

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
        /// Сервис по рассчету корня
        /// </summary>
        private readonly ISqrtWorker sqrtWorker;

        public MainController(ISqrtWorker sqrtWorker)
        {
            this.sqrtWorker = sqrtWorker;
        }

        /// <summary>
        /// Получить Вьюшку
        /// </summary>
        /// <returns></returns>
        [HttpGet("MainView")]
        public IActionResult MainView()
        {
            var res = new MainDto();
            var languageId = HttpContext.Session.GetInt32("language");
            var fileName = GetPathToPageByLanguageId(languageId);
            return View(fileName, res);
        }

        private string GetPathToPageByLanguageId(int? languageId)
        {
            var language = languageId == null ? LanguageEnum.Ru : (LanguageEnum)languageId;
            switch (language)
            {
                case LanguageEnum.Ru:
                    return RU_FILE_NAME;
                case LanguageEnum.En:
                    return EN_FILE_NAME;
                case LanguageEnum.Ch:
                    return CH_FILE_NAME;
                case LanguageEnum.Es:
                    return ES_FILE_NAME;
                default:
                    return RU_FILE_NAME;
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

        /// <summary>
        /// Установить язык
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Calculate(MainDto model)
        {
            //var res = new MainDto();
            model.Result = sqrtWorker.CalculateSquareRoot(model.Value, model.DigitNumber);
            var languageId = HttpContext.Session.GetInt32("language");
            var fileName = GetPathToPageByLanguageId(languageId);
            return View(fileName, model);
        }
    }
}
