using ETrade.DAL.Abstract;
using ETrade.DAL.Concrete;
using ETrade.DAL.Context;
using ETrade.Entity.Models.Identity;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//dependency injectionla ilgili kodlar addController ve builder.Build arasýna yazýlýr genelde
//Dependency Injection tanýmlamalarý addScopedla olur.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ETradeDbContext>();
builder.Services.AddScoped<ICategoryDAL,CategoryDAL>();
builder.Services.AddScoped<IProductDAL,ProductDAL>();
builder.Services.AddScoped<IOrderDAL,OrderDAL>();

//AddIdentity 
builder.Services.AddIdentity<User,Role>(options =>
{
    //lockout -> kilitleme 
    options.Lockout.MaxFailedAccessAttempts = 3;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
    //password 
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    options.Password.RequiredLength=1;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;  
}).AddEntityFrameworkStores<ETradeDbContext>().AddDefaultTokenProviders();


//Cookie
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath="/Account/SignIn"; // giriþ yapýlmadýysa 
    options.AccessDeniedPath="/";
    options.SlidingExpiration = true;
    options.ExpireTimeSpan =TimeSpan.FromMinutes(15);
    options.Cookie = new CookieBuilder
    {
        HttpOnly= false,
        SameSite =SameSiteMode.Lax,
        SecurePolicy =CookieSecurePolicy.Always
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//Identity Ýþlemi
app.UseAuthentication();//giriþ kontrölü
app.UseAuthorization();//yetkilendirme kontrolü

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
