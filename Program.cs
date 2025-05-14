using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UDAS.Data;
using UDAS.Models;
using UDAS.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Veri Tabanı Bağlantısı
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));



builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options=>
{
    options.Cookie.Name = "NetCoreMvc.Auth";
    options.LoginPath = "/";
    options.AccessDeniedPath = "/";
});


// Repository'i bağımlılık enjeksiyonu (DI) konteynerine scoped olarak kaydetme !!!!!
builder.Services.AddScoped<CourseScheduleRepository>();
builder.Services.AddScoped<ClassroomDisplayRepository>();
builder.Services.AddScoped<ExamScheduleRepository>();


// // ---------------------------------- Session Ayarı -------------------------------------
// builder.Services.AddSession(options =>
// {
//     options.IdleTimeout = TimeSpan.FromMinutes(30);
//     options.Cookie.HttpOnly = true;
//     options.Cookie.IsEssential = true;
//     options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
// });


// Add services to the container.
builder.Services.AddControllersWithViews();

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

// // ------------------------- Session Kullanımı -----------------------------
// app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
