using ETrade.DAL.Abstract;
using ETrade.DAL.Concrete;
using ETrade.DAL.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//dependency injectionla ilgili kodlar addController ve builder.Build arasýna yazýlýr genelde
//Dependency Injection tanýmlamalarý addScopedla olur.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ETradeDbContext>();
builder.Services.AddScoped<ICategoryDAL,CategoryDAL>();
builder.Services.AddScoped<IProductDAL,ProductDAL>();
builder.Services.AddScoped<IOrderDAL,OrderDAL>();
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
