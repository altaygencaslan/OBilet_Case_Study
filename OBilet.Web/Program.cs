using OBilet.Integration.Services.Abstract;
using OBilet.Integration.Services.Concrete;

var builder = WebApplication.CreateBuilder(args);

//Logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
//Logging

//Dependencies
builder.Services.AddScoped<HttpClient, HttpClient>();
builder.Services.AddScoped<IApiHelper, ApiHelper>();
builder.Services.AddScoped<IOBiletService, OBiletService>();
//Dependencies


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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
