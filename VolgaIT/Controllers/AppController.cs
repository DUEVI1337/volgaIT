using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VolgaIT.Data;
using VolgaIT.Models;

namespace VolgaIT.Controllers
{
    public class AppController : Controller
    {
        private DataContext _db;
        private List<Event> _eventList;

        public AppController(DataContext context)
        {
            _db = context;
            _eventList = _db.Events.ToList();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRequest(string id, string nameEvent, string bonusInfo)
        {
            try
            {
                App app = await _db.Apps.FindAsync(id);
                RequestUser requestUser = new RequestUser { AppId = app.Id,  EventId = _eventList.FirstOrDefault(x => x.Name == nameEvent).Id, BonusInfo = bonusInfo};
                await _db.RequestUsers.AddAsync(requestUser);
                await _db.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }

        }
    }
}