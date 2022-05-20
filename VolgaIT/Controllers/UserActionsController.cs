using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VolgaIT.Data;
using VolgaIT.Models;

namespace VolgaIT.Controllers
{
    [Authorize(Roles = "user")]
    public class UserActionsController : Controller
    {
        private DataContext _db;
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManager;
        private List<App> _apps = new List<App>();
        private List<App> _userApps = new List<App>();

        public UserActionsController(DataContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _db = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _apps = _db.Apps.ToList();
        }

        [HttpGet]
        public async Task<IActionResult> MainPageUser()
        {
            _userApps = await GetUserListAppAsync(); //получем приложения пользователя
            return View(_userApps.Count); //выводим кол-во приложения на глваной странице
        }

        [HttpGet]
        public async Task<IActionResult> ListApp()
        {
            return View(await GetUserListAppAsync()); //выводи список приложений
        }

        #region [AddApp]
        [HttpGet]
        public IActionResult AddApp(string result)
        {
            App app = new App { Id = Guid.NewGuid().ToString() }; //генерируем id для приложения и предлогаем его пользователю по умолчанию
            ViewBag.result = result;
            return View(app);
        }

        [HttpPost]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true, Duration = 0)]
        public async Task<IActionResult> AddApp(App app) //создание приложения
        {
            string result = "";
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User); //получем текущего пользователя
                try
                {
                    UserApps userApps = new UserApps { AppId = app.Id, UserId = user.Id };
                    await _db.Apps.AddAsync(app); //добавлем приложение в бд
                    await _db.UserApps.AddAsync(userApps);
                    await _db.SaveChangesAsync();
                    result = "Приложение добавлено"; //результат создания
                    return RedirectToAction("AddApp", new { result = result });
                }
                catch //если приложение не удалось создать
                {
                    if (_apps.Select(x => x.Name).Contains(app.Name))
                    {
                        ModelState.AddModelError("", "Приложение с таким названием уже существует");
                        return View();
                    }
                    else if (_apps.Select(x => x.Id).Contains(app.Id))
                    {
                        ModelState.AddModelError("", "Приложение с таким Id уже существует");
                        return View();
                    }
                    else if (app.Name == app.Id)
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
        public async Task<IActionResult> UserProfile(IdentityUser user)
        {
            user = await _userManager.GetUserAsync(User); //получаем текущего пользователя
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(IdentityUser user) //редактирование профиля пользователя
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
                IdentityUser userUpdate = await _userManager.FindByIdAsync(user.Id); //получем текущего пользователя
                if(userUpdate.Email == user.Email)
                {
                    return View("UserProfile");
                }
                userUpdate.Email = user.Email; //меняем почту
                userUpdate.UserName = user.Email; //меняем логин
                var result = await _userManager.UpdateAsync(userUpdate); //обновляем пользователя в бд
                if (result.Succeeded)
                {
                    ViewBag.resultSaveProfile = "Успешно сохранено"; //результат сохранения данных о пользователе
                    await _signInManager.RefreshSignInAsync(userUpdate);
                    return RedirectToAction("MainPageUser");
                }
                else
                {
                    ModelState.AddModelError("", "Ошибка сохранения");
                }
            }
            return View("UserProfile");
        }
        #endregion

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<List<App>> GetUserListAppAsync() //получаем список приложения данного пользователя
        {
            IdentityUser user = await _userManager.GetUserAsync(User); //получаем текущего пользователя
            List<UserApps> userApps = new List<UserApps>(await _db.UserApps.Where(x => x.UserId == user.Id).ToListAsync()); //поиск приложений по id
                for (int i = 0; i < userApps.Count; i++)
                {
                    _userApps.Add(_apps.FirstOrDefault(x => x.Id == userApps[i].AppId)); //добавляем приложения с коллекцию
                }
                return _userApps;
            return _userApps;
        }

    }
}
