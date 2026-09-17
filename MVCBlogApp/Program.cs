using Core.Interfaces;
using Infrastructure.Repositories;
using Core.Services;
using Core.Interfaces.DTO;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IBlogRepository>(sp => new BlogRepository(builder.Configuration.GetConnectionString("BlogServerDb")));
builder.Services.AddScoped<IBlogService, BlogService>();

builder.Services.AddScoped<IUserRepository>(sp => new UserRepository(builder.Configuration.GetConnectionString("BlogServerDb")));
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<ILikeRepository>(sp => new LikeRepository(builder.Configuration.GetConnectionString("BlogServerDb")));
builder.Services.AddScoped<ILikeService, LikeService>();

builder.Services.AddScoped<ICommentRepository>(sp => new CommentRepository(builder.Configuration.GetConnectionString("BlogServerDb")));
builder.Services.AddScoped<ICommentService, CommentService>();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Blog}/{action=Index}/{id?}");


app.Run();
