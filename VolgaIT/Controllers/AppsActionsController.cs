using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VolgaIT.Data;
using VolgaIT.Models;

namespace VolgaIT.Controllers
{
    public class AppsActionsController : Controller
    {
        private DataContext _db;
        private UserManager<IdentityUser> _userManager;

        public AppsActionsController(DataContext context, UserManager<IdentityUser> userManager)
        {
            _db = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Settings(string id)
        {
            App app = await _db.Apps.FindAsync(id);
            return View(app);
        }

        [HttpGet]
        public async Task<IActionResult> StatisticsApp(string id)
        {
            StatisticsAppViewModel statModel = await GetStatisticsApp(id);
            return View(statModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteApp(App app)
        {
            App appNew = await _db.Apps.FindAsync(app.Id);
            List<RequestUser> requests = await _db.RequestUsers.Where(x => x.AppId == appNew.Id).ToListAsync();
            UserApps userApps = await _db.UserApps.FirstOrDefaultAsync(x => x.AppId == appNew.Id);
            _db.Apps.Remove(appNew);
            _db.RequestUsers.RemoveRange(requests);
            _db.UserApps.Remove(userApps);
            await _db.SaveChangesAsync();
            return RedirectToAction("ListApp", "UserActions");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateApp(App app)
        {
            //App updateApp = new App { Id = app.Id, Name = app.Name, DateCreate = app.DateCreate };
            if (ModelState.IsValid)
            {
                try
                {
                    App appUpdate = await _db.Apps.FindAsync(app.Id);
                    appUpdate.Name = app.Name;
                    _db.Apps.Update(appUpdate);
                    await _db.SaveChangesAsync();
                    ViewBag.resultUpdateApp = "Успешно сохранено";
                }
                catch
                {
                    ModelState.AddModelError("", "Ошибка сохранения");
                }
            }
            return View("Settings", app);
        }


        [HttpGet]
        public async Task<IActionResult> TimeFilter(string timeFilter, string id)
        {
            StatisticsAppViewModel app = await GetStatisticsApp(id);
            switch (timeFilter)
            {
                case "week":
                    DateTime rqstUserWeek = DateTime.Now.AddDays(-7);
                    app.RequestUsers = app.RequestUsers.Where(x => x.CreatedDate > rqstUserWeek).ToList();
                    break;

                case "month":
                    DateTime rqstUserMonth = DateTime.Now.AddDays(-31);
                    app.RequestUsers = app.RequestUsers.Where(x => x.CreatedDate > rqstUserMonth).ToList();
                    break;

                case "year":
                    DateTime rqstUserYear = DateTime.Now.AddDays(-365);
                    app.RequestUsers = app.RequestUsers.Where(x => x.CreatedDate > rqstUserYear).ToList();
                    break;
            }
            return PartialView("PartialTableStat", app);
        }


        public async Task<StatisticsAppViewModel> GetStatisticsApp(string id)
        {
            App thisApp = await _db.Apps.FindAsync(id);
            StatisticsAppViewModel app = new StatisticsAppViewModel
            {
                App = thisApp,
                Events = await _db.Events.ToListAsync(),
                RequestUsers = await _db.RequestUsers.Where(x => x.AppId == thisApp.Id).ToListAsync()
            };
            return app;
        }
    }
}