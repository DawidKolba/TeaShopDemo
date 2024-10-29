using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using TeaShopDemo.Services;


var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("TeaShopDemoContextConnection")
    ?? throw new InvalidOperationException("Connection string 'TeaShopDemoContextConnection' not found.");


builder.Services.AddDbContext<TeaShopDemoContext>(options => options.UseSqlite(connectionString));

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
});

builder.Services.AddControllersWithViews();

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddDefaultUI()
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<TeaShopDemoContext>();

//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("RequireAdministratorRole",
//        policy => policy.RequireRole("Administrator"));
//});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.Cookie.SameSite = SameSiteMode.None;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Events.OnRedirectToLogin = async context =>
    {
        if (context.HttpContext.User.IsInRole("Admin"))
        {
            context.RedirectUri = "/admin"; 
        }
        await Task.CompletedTask;
    };
});

builder.Services.AddSession(options =>
{
    options.Cookie.Name = "TeaShopDemo.session";
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.SameSite = SameSiteMode.None;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<CartService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddSingleton<IUserIdProvider, SessionUserIdProvider>();

builder.Services.AddSignalR();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpClient();


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseCookiePolicy();
app.UseSession();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();

//app.MapFallbackToPage("/_Host");
app.MapFallbackToController("Index", "Home");

app.MapHub<CartHub>("/cartHub");


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "admin",
    pattern: "{controller=Admin}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
