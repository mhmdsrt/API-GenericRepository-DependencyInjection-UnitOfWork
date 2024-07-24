using MyselfStaj2.Entity;
using MyselfStaj2.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Npgsql;
using MyselfStaj2.Interfaces;
using MyselfStaj2.Classes;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/* 
 "Dependency Injection" sayesinde aþaðýdaki CoordinateService yerine baþka bir service ile çalýþmak istediðimizde 
 Injection sayesinde aþaðýdaki oluþturduðumuz Controller'i, CoordinateService yerine baþka bir service ile çalýþmak isteyebileyeceðimizden dolayý 
Controller'ümüz direkt CoordinateService baðýmlý olmasýn, sadece Çalýþtýrmak istedeðimiz service'i Controller sýnýfýnda constructor olarak parametre
ile isteyip bunu program baþlarken Program.cs de AddScope metodu ile veriyoruz.


Controller'ýnýzýn (CoordinatesController) doðrudan CoordinateService sýnýfýna baðýmlý olmamasýný saðlamak için, 
yalnýzca ICoordinate arayüzüne baðýmlý olmasýný istiyorsunuz. Bu þekilde, controller herhangi bir ICoordinate uygulamasý ile çalýþabilir.
 
 */
builder.Services.AddScoped<ICoordinate, CoordinatePostgreSql>(); //Controller içerisinde constructor'ýn içerisindeki parametreye gönderilir.
/* Bu satýr, ICoordinate arayüzünü implement eden CoordinateService sýnýfýnýn scoped yaþam döngüsünde eklenmesini saðlar. 
Scoped hizmetler, her bir HTTP isteði için bir kez oluþturulur ve ayný HTTP isteði içinde tekrar kullanýlýr.
Yani ICoordinate isteyene CoordinateService sýnýfýný ver.*/
//builder.Services.AddScoped<IEntityService<Coordinate>>();
builder.Services.AddScoped<IEntityService<Coordinate>, EntityService<Coordinate>>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddDbContext<EntityContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("MyConnection"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});
//builder.Services.AddDbContext<EntityContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("MyConnection")));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
