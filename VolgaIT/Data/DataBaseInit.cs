using Microsoft.AspNetCore.Identity;
using VolgaIT.Models;

namespace VolgaIT.Data
{
    public class DataBaseInit
    {
        public static async Task InitDataBase(RoleManager<IdentityRole> roleManager, DataContext context)
        {
            if(!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole("user"));
            }
            if(!context.Events.Any())
            {
                List<Event> events = new List<Event>
                {
                    new Event{ Name = "View"},
                    new Event{ Name = "SignIn"},
                    new Event{ Name = "Register"},
                    new Event{ Name = "Click"},
                };
                await context.Events.AddRangeAsync(events);
                await context.SaveChangesAsync();
            }
        }
    }
}