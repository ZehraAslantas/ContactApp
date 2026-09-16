using ContactApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//controller yapýlarýný projede kullanacaðýný söylüyor.

/*Burada(alt satýrda) DI çerçevesi için register kaydý yapcaz.
 ne zaman IContactRepository nesnesi enjekte edilirse o zaman InMemoryContactRepository(nesne) yi newle dedik
eðer veritabaný baðlayacak olsaydýk o zaman InMemoryContactRepository yerine
veritabaný reposunu yazardýk.yani kodlarý deðiþtirmicez*/

builder.Services.AddSingleton<IContactRepository,InMemoryContactRepository>();
//bu yapý controller üzerinde kullanýlacak


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    /*tarayýcýya yazýlan adres þablonu.
    Hangi C# sýnýfýnýn (Controller),
    hangi metodunun (Action) çalýþtýrýlacaðýný seçer tarayýcýya
    yazarsýn orasý çalýþýr.Bu yapýya KONFÝGÜRASYON yapýsý denir*/
    .WithStaticAssets();


app.Run();
