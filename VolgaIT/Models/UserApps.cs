using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VolgaIT.Models
{
    [Keyless]
    public class UserApps
    {
        public string UserId { get; set; }
        public string AppId { get; set; }
    }
}