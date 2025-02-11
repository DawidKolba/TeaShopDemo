using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TeaShopDemo.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("TeaShopDemoContextConnection")
    ?? throw new InvalidOperationException("Connection string 'TeaShopDemoContextConnection' not found.");

builder.Services.AddDbContext<TeaShopDemoContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
});

builder.Services.AddControllersWithViews();

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddDefaultUI()
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<TeaShopDemoContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.Cookie.SameSite = SameSiteMode.None;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Events.OnRedirectToLogin = context =>
    {
        if (context.HttpContext.User.IsInRole("Admin"))
        {
            context.RedirectUri = "/admin";
        }
        return Task.CompletedTask;
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
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<NewsletterService>();
builder.Services.AddSingleton<IUserIdProvider, SessionUserIdProvider>();

builder.Services.AddSignalR();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpClient();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();


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


app.MapControllers();
app.MapBlazorHub();
app.MapHub<CartHub>("/cartHub");

//app.MapFallbackToController("BlazorHost", "Home");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
//app.MapFallbackToPage("/_Host");
app.MapRazorPages();
//app.MapFallbackToPage("/app/{*catchall}", "/BlazorHost");
app.MapFallbackToPage("/app/{*catchall}", "/_Host");
//app.MapFallbackToPage("/blazor/{*catchall}", "/_Host");

//app.MapFallbackToController("Blazor", "Home");
//app.MapFallbackToAreaPage("/app/{*path:nonfile}", "/BlazorHost", "Blazor");

app.Run();
