using StajTakip.Data.Context;
using StajTakip.Data.Repository;
using StajTakip.Service;
using StajTakip.Service.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(1); // oturum süresi: 1 dakika
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped(sp =>
    new StajTakipDbContext(builder.Configuration.GetConnectionString("DefaultConnection")!));

builder.Services.AddScoped<IStajyerRepository, StajyerRepository>();
builder.Services.AddScoped<IStajyerService, StajyerService>();

builder.Services.AddScoped<IKurumRepository, KurumRepository>();
builder.Services.AddScoped<IKurumService, KurumService>();

builder.Services.AddScoped<IMentorRepository, MentorRepository>();
builder.Services.AddScoped<IMentorService, MentorService>();

builder.Services.AddScoped<IKullaniciRepository, KullaniciRepository>();
builder.Services.AddScoped<IKullaniciService, KullaniciService>();

builder.Services.AddAuthentication("CookieAuth")
    .AddCookie("CookieAuth", options =>
    {
        options.Cookie.Name = "StajTakip.Cookie";
        options.LoginPath = "/Kullanici/Login";
        options.AccessDeniedPath = "/Kullanici/Login";
    });

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();