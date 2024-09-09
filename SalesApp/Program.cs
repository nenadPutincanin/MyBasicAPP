using Microsoft.EntityFrameworkCore;
using SalesApp.Data;
using SalesApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<FieldSalesContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SalesConnection"));
});

builder.Services.AddDefaultIdentity<IdentityUser>().AddDefaultTokenProviders()    /*(options => options.SignIn.RequireConfirmedAccount = true)*/
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<UserContext>();

builder.Services.AddDbContext<IdentitySalesDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SalesIdentity"));
});

builder.Services.AddDbContext<UserContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("RealIdentity"));
});






var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
