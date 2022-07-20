using GymExam.Data;
using GymExam.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<ApplicationContext>();

builder.Services.AddDbContext<GymContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("GymConnection")));




var app = builder.Build();

using var serviceScope = app.Services.CreateScope();
var dbContext = serviceScope.ServiceProvider.GetRequiredService<GymContext>();
DbInit.Initialize(dbContext);

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
