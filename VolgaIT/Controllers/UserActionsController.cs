using Microsoft.AspNetCore.Authorization;
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
        private readonly IAccountService _accountService;
        private readonly IUserAppsService _userAppsService;

        public UserActionsController(IUserService userService, IUserAppsService userAppsService, IAppService appService, IAccountService accountService)
        {
            _userService = userService;
            _userAppsService = userAppsService;
            _appService = appService;
            _accountService = accountService;
         }

        [HttpGet]
        public async Task<IActionResult> MainPageUser()
        {
            var user = await _userService.GetUserAsync();
            if(user==null)
            {
                await _accountService.LogoutAsync();
                return RedirectToAction("Index", "Home");
            }
            return View(user.UsersApps.Count());
        }

        [HttpGet]
        public async Task<IActionResult> ListApp()
        {
            var user = await _userService.GetUserAsync();
            var userApps = await _appService.GetAppRangeAsync(user.UsersApps.Select(x=>x.AppsId).ToList());
            return View(userApps);
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
                    return RedirectToAction("AddUserApp", new { result = result});
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
            return View();
        }

        #endregion

        #region [UserProfile]
        [HttpGet]
        public async Task<IActionResult> UserProfile(UpdateProfileViewModel model)
        {
            User user = await _userService.GetUserAsync();
            model.IdUser = user.Id;
            model.NewEmail = user.Email;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(UpdateProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.EditProfileAsync(model);
                if (result)
                {
                    return RedirectToAction("MainPageUser");
                }
                ModelState.AddModelError("", "Ошибка сохранения");
            }
            ModelState.AddModelError("", "Ошибка сохранения");
            return View("UserProfile");
        }
        #endregion

    }
}
