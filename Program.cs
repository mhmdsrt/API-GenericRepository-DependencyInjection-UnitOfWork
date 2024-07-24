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
 "Dependency Injection" sayesinde a�a��daki CoordinateService yerine ba�ka bir service ile �al��mak istedi�imizde 
 Injection sayesinde a�a��daki olu�turdu�umuz Controller'i, CoordinateService yerine ba�ka bir service ile �al��mak isteyebileyece�imizden dolay� 
Controller'�m�z direkt CoordinateService ba��ml� olmas�n, sadece �al��t�rmak istede�imiz service'i Controller s�n�f�nda constructor olarak parametre
ile isteyip bunu program ba�larken Program.cs de AddScope metodu ile veriyoruz.


Controller'�n�z�n (CoordinatesController) do�rudan CoordinateService s�n�f�na ba��ml� olmamas�n� sa�lamak i�in, 
yaln�zca ICoordinate aray�z�ne ba��ml� olmas�n� istiyorsunuz. Bu �ekilde, controller herhangi bir ICoordinate uygulamas� ile �al��abilir.
 
 */
builder.Services.AddScoped<ICoordinate, CoordinatePostgreSql>(); //Controller i�erisinde constructor'�n i�erisindeki parametreye g�nderilir.
/* Bu sat�r, ICoordinate aray�z�n� implement eden CoordinateService s�n�f�n�n scoped ya�am d�ng�s�nde eklenmesini sa�lar. 
Scoped hizmetler, her bir HTTP iste�i i�in bir kez olu�turulur ve ayn� HTTP iste�i i�inde tekrar kullan�l�r.
Yani ICoordinate isteyene CoordinateService s�n�f�n� ver.*/
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
