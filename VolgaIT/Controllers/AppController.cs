using Microsoft.AspNetCore.Mvc;
using VolgaIT.Data;
using VolgaIT.Models;
using VolgaIT.Services.Interface;

namespace VolgaIT.Controllers
{
    public class AppController : Controller
    {
        private readonly IRequestAppService _requestService;
        private readonly IAppService _appService;
        private readonly IEventService _eventService;

        public AppController(IAppService appService, IRequestAppService requestService, IEventService eventService)
        {
            _requestService = requestService;
            _appService = appService;
            _eventService = eventService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRequest(string appId, string nameEvent, string bonusInfo)
        {
            try
            {
                App app = await _appService.GetAppByIdAsync(appId);
                Event eventApp = await _eventService.GetEventByNameAsync(nameEvent);
                await _requestService.CreateRequest(appId, eventApp.Id, bonusInfo);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }

        }
    }
}