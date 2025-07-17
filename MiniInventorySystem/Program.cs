using Microsoft.AspNetCore.DataProtection;
using MiniInventorySystem.Interface;
using MiniInventorySystem.Repository;

var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.AddControllersWithViews();
//builder.Services.AddDataProtection()
//    .PersistKeysToFileSystem(new DirectoryInfo(@"/app/keys"))
//    .SetApplicationName("MiniInventorySystem");
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ISaleRepository, SaleRepository>();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Userlogin}/{action=Login}/{id?}");

app.Run();
