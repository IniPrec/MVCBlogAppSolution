using Core.Interfaces;
using Infrastructure.Repositories;
using Core.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IBlogRepository>(sp => new BlogRepository(builder.Configuration.GetConnectionString("BlogServerDb")));
builder.Services.AddScoped<IBlogService, BlogService>();

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseStaticFiles();
app.UseRouting();
app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Blog}/{action=Index}/{id?}");


app.Run();
