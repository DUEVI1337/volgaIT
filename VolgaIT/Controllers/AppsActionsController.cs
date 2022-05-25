using Microsoft.AspNetCore.Mvc;
using VolgaIT.Models;
using VolgaIT.Models.ViewModels;
using VolgaIT.Services.Interface;

namespace VolgaIT.Controllers
{
    public class AppsActionsController : Controller
    {
        private IAppService _appService;
        private IEventService _eventService;

        public AppsActionsController(IAppService appService, IEventService eventService)
        {
            _appService = appService;
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<IActionResult> Settings(string appId)
        {
            App app = await _appService.GetAppByIdAsync(appId);
            var model = new UpdateAppViewModel()
            {
                AppId = app.Id,
                AppName = app.Name,
                AddDateCreate = app.DateCreate.ToString()
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> StatisticsApp(string appId)
        {
            App app = await _appService.GetAppByIdAsync(appId);
            StatisticsAppViewModel model = new StatisticsAppViewModel
            {
                App = app,
                Events = await _eventService.GetAllEventAsync(),
                RequestUsers = app.RequestsUsers.ToList()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteApp(string appId)
        {
            await _appService.RemoveAppByIdAsync(appId);
            return RedirectToAction("ListApp", "UserActions");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateApp(UpdateAppViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _appService.UpdateAppAsync(model);
                    ViewBag.resultUpdateApp = "Успешно сохранено";
                }
                catch
                {
                    ModelState.AddModelError("", "Ошибка сохранения");
                }
            }
            return View("Settings", model);
        }


        [HttpGet]
        public async Task<IActionResult> TimeFilterRequestApp(string timeFilter, string appId)
        {
            App app = await _appService.GetAppByIdAsync(appId);
            StatisticsAppViewModel model = new StatisticsAppViewModel
            {
                App = app,
                Events = await _eventService.GetAllEventAsync(),
                RequestUsers = app.RequestsUsers.ToList()
            };
            return PartialView("PartialTableStat", _appService.TimeFilterRequestApp(timeFilter, model));
        }
    }
}