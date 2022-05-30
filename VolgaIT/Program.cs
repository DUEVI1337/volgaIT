using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VolgaIT.Data;
using VolgaIT.Data.Repository;
using VolgaIT.Data.Repository.Interface;
using VolgaIT.Data.Repository0;
using VolgaIT.Models;
using VolgaIT.Services;
using VolgaIT.Services.Interface;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ConnectionString");

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DataContext>(opt =>
{
    opt.UseNpgsql(connectionString);
});
builder.Services.AddIdentity<User, IdentityRole>(opt=>
{
    opt.User.RequireUniqueEmail = true;
    opt.Password.RequireDigit = false;
    opt.Password.RequiredLength = 3;
    opt.Password.RequireUppercase = false;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequireLowercase = false;
}).AddEntityFrameworkStores<DataContext>().AddTokenProvider<DataProtectorTokenProvider<User>>(TokenOptions.DefaultProvider);
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IAccountService, AccountService>()
                .AddScoped<IPasswordService, PasswordService>()

                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IUserService, UserService>()

                .AddScoped<IAppRepository, AppRepository>()
                .AddScoped<IAppService, AppService>()

                .AddScoped<IUserAppsRepository, UserAppsRepository>()
                .AddScoped<IUserAppsService, UserAppsService>()

                .AddScoped<IEventRepository, EventRepository>()
                .AddScoped<IEventService, EventService>()

                .AddScoped<IRequestRepository, RequestRepository>()
                .AddScoped<IRequestAppService, RequestAppService>()
                
                .AddTransient<IEmailService, EmailService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var context = scope.ServiceProvider.GetRequiredService<DataContext>();
    await DataBaseInit.InitDataBase(roleManager, context);
}

app.Run();
