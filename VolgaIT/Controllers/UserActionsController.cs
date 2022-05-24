using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VolgaIT.Models;
using VolgaIT.Models.ViewModels;
using VolgaIT.Services.Interface;

namespace VolgaIT.Controllers
{
    [Authorize(Roles = "user")]
    public class UserActionsController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAppService _appService;
        private readonly IUserAppsService _userAppsService;

        public UserActionsController(IUserService userService, IUserAppsService userAppsService, IAppService appService)
        {
            _userService = userService;
            _userAppsService = userAppsService;
            _appService = appService;
         }

        [HttpGet]
        public async Task<IActionResult> MainPageUser()
        {
            var user = await _userService.GetUserAsync();
            return View(user.UsersApps.Count());
        }

        [HttpGet]
        public async Task<IActionResult> ListApp()
        {
            var user = await _userService.GetUserAsync();
            return View(user.UsersApps.ToList());
        }

        #region [AddApp]
        [HttpGet]
        public IActionResult AddUserApp(string result)
        {
            var model = new AddAppViewModel { AppId = Guid.NewGuid().ToString() };
            ViewBag.result = result;
            return View(model);
        }

        [HttpPost]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true, Duration = 0)]
        public async Task<IActionResult> AddUserApp(AddAppViewModel model)
        {
            string result = "";
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userService.GetUserAsync();
                    await _appService.AddAppAsync(model);
                    await _userAppsService.AddUserAppAsync(model.AppId, user.Id);
                    result = "Приложение добавлено";
                }
                catch
                {
                    var allApps = await _appService.GetAllAppsAsync();
                    if (allApps.Select(x => x.Name).Contains(model.AppName))
                    {
                        ModelState.AddModelError("", "Приложение с таким названием уже существует");
                        return View();
                    }
                    else if (allApps.Select(x => x.Id).Contains(model.AppId))
                    {
                        ModelState.AddModelError("", "Приложение с таким Id уже существует");
                        return View();
                    }
                    else if (model.AppName == model.AppId)
                    {
                        ModelState.AddModelError("", "Название приложения должно отличаться от его Id");
                        return View();
                    }
                    else
                    {
                        ModelState.AddModelError("", "Ошибка создания");
                        return View();
                    }
                }
            }
            return View(model: result);
        }

        #endregion

        #region [UserProfile]
        [HttpGet]
        public async Task<IActionResult> UserProfile(User user)
        {
            user = await _userService.GetUserAsync();
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(User user)
        {
            if (user.Email == null)
            {
                ModelState.AddModelError("", "Поле 'Почта' не может быть пустым");
            }
            else if (!user.Email.Contains('@'))
            {
                ModelState.AddModelError("", "Неккоректная электронная почта");
            }
            else if (ModelState.IsValid)
            {
                var result = await _userService.EditProfileAsync(user);
                if (result)
                {
                    ViewBag.resultSaveProfile = "Успешно сохранено";
                    return RedirectToAction("MainPageUser");
                }
                ModelState.AddModelError("", "Ошибка сохранения");
            }
            return View("UserProfile");
        }
        #endregion

    }
}
