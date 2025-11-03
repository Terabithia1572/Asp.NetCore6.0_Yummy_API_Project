

using YummyApi.WebUI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddHttpClient();

builder.Services.AddSignalR(); // SignalR servisini ekledik
builder.Services.AddHttpClient("openai", client => // OpenAI için özel bir HttpClient ekledik
{
    client.BaseAddress = new Uri("https://api.openai.com/v1/"); // OpenAI API temel adresi
});


builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.MapHub<ChatHub>("/chatHub"); // ChatHub'ý "/chatHub" yoluna eþledik
//app.MapHub<ChatHub>("/chatHub"); // ChatHub'ý "/chatHub" yoluna eþledik

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
