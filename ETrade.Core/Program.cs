using ETrade.DAL.Abstract;
using ETrade.DAL.Concrete;
using ETrade.DAL.Context;
using ETrade.Entity.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//dependency injectionla ilgili kodlar addController ve builder.Build aras�na yaz�l�r genelde
//Dependency Injection tan�mlamalar� addScopedla olur.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ETradeDbContext>();
builder.Services.AddScoped<ICategoryDAL,CategoryDAL>();
builder.Services.AddScoped<IProductDAL,ProductDAL>();
builder.Services.AddScoped<IOrderDAL,OrderDAL>();
builder.Services.AddScoped<IOrderlineDAL,OrderlineDAL>();

//AddAuthentication.AddGoogle
builder.Services.AddAuthentication().AddGoogle(options => {
    options.ClientId = "1034829810646-caf5msofnp83v3ikrinlvgap6b8sm65g.apps.googleusercontent.com";
    options.ClientSecret = "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"; // Google Apiden ald�k ad�mlar readMe -de yaz�yor.
});//appsettingsde yazd�klar�m�zdan sonra bunu yazmam�z laz�m.

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
    options.LoginPath="/Account/SignIn"; // giri� yap�lmad�ysa 
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

//AddSession
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//UseSession
app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//Identity ��lemi
app.UseAuthentication();//giri� kontr�l�
app.UseAuthorization();//yetkilendirme kontrol�


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
}); //useEndpoint k�sm� yazarsan �nce ya da sonra yazman�n bir �nemi yok.

app.Run();
