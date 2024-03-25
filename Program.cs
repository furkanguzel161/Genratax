using Microsoft.EntityFrameworkCore;
using Genratax.Data;
using Genratax.Data.Concrete.EfCore;
using Genratax.Data.Abstrack;
using Genratax.Data.Concrete;
using Microsoft.AspNetCore.Authentication.Cookies;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ForumContext>(optionsForum =>{
    var config = builder.Configuration;
    var connectionString = config.GetConnectionString("forum");
    optionsForum.UseSqlite(connectionString);
});
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => options.LoginPath ="/User/Signin");
builder.Services.AddScoped<IPostRepository,EfPostRepository>();
builder.Services.AddScoped<ICommentRepository,EfCommentRepository>();
builder.Services.AddScoped<IUserRepository,EfUserRepository>();

var app = builder.Build();
SeedData.FillTestData(app);
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseAuthentication();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "post_details",
    pattern: "posts/thread/{url}",
    defaults: new{controller = "Posts", action = "Thread"} );
    
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
