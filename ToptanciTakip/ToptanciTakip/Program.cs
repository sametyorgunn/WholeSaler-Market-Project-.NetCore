using BusinessLayer;
using DataAccessLayer.Contexts;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddServiceRouting();
builder.Services.AddDbContext<Context>();
builder.Services.AddControllersWithViews();
builder.Services.AddMvc();
builder.Services.AddSession();
builder.Services.AddAuthentication();

builder.Services.AddIdentity<AppUser, AppRole>(x =>
{
    x.Password.RequireLowercase = false;
    x.Password.RequireNonAlphanumeric = false;
    x.Password.RequireDigit = false;
    x.Password.RequireUppercase = false;
    x.Password.RequiredUniqueChars = 0;


})
        .AddEntityFrameworkStores<Context>().AddDefaultTokenProviders();

builder.Services.AddAuthentication(
              CookieAuthenticationDefaults.AuthenticationScheme)
              .AddCookie(x =>
              {
                  x.LoginPath = "/Admin/Login/Index";
              });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "Market",
    pattern:"{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "Yonetim",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "wholesaler",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");








app.Run();
