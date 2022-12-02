using FinalCIS174.Models;
using FinalCIS174.Models.UserLogin;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRouting(options => options.LowercaseUrls = true);
//Session state/cookies
builder.Services.AddMemoryCache();
builder.Services.AddSession();
// Add services to the container.
builder.Services.AddControllersWithViews().AddNewtonsoftJson();
//DBContext
builder.Services.AddDbContext<DNDContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DNDContext")));
builder.Services.AddIdentity<User, IdentityRole>(options => {
    //Password must be 8 characters long with at least one lowercase letter,
    //one uppercase letter, one number, and one special character
}).AddEntityFrameworkStores<DNDContext>().AddDefaultTokenProviders();
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

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
