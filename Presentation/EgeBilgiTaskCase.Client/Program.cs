using EgeBilgiTaskCase.Client.Extensions.StartupExtensions;
using EgeBilgiTaskCase.Client.HelperServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.JsonOptionsStartupExtension();
builder.Services.AddHttpContextAccessor();
builder.Services.AuthOptionsStartupExtension();
builder.Services.IISOptionsStartupExtension();
builder.Services.HttpClientOptionsStartupExtension(builder.Configuration);
builder.Services.AddScoped<AAAService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseDeveloperExceptionPage();

app.UseStaticFiles();

app.UseRouting();
//app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
