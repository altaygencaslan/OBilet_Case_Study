using OBilet.Core.Business.Abstract;
using OBilet.Core.Business.Concrete;
using OBilet.Core.Cache.Abstract;
using OBilet.Core.Cache.Concrete;
using OBilet.Integration.Services.Abstract;
using OBilet.Integration.Services.Concrete;
using OBilet.Web;

var builder = WebApplication.CreateBuilder(args);

//Logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
//Logging

//Dependencies
builder.Services.AddTransient<IApiHelper, ApiHelper>();
builder.Services.AddScoped<IOBiletService, OBiletService>();
builder.Services.AddScoped<IOBiletManager, OBiletManager>();
builder.Services.AddSingleton<IMemoryCacheManager, MemoryCacheManager>();
//Dependencies

//ClientSide data
builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".obilet.casestudy.session";
    options.IdleTimeout = TimeSpan.FromDays(1);
    options.Cookie.IsEssential = true;
});
//ClientSide data

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.UseMiddleware<GlobalExceptionHandling>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
