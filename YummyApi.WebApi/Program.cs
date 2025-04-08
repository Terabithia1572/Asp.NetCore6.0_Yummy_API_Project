using FluentValidation;
using System.Reflection;
using YummyApi.WebApi.Context;
using YummyApi.WebApi.Entities;
using YummyApi.WebApi.ValidationRules;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ApiContext>();

builder.Services.AddScoped<IValidator<Product>, ProductValidator>();
// Add services to the container.
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
//AutoMapper'ý baðýmlýlýk enjeksiyonu(Dependency Injection) ile projeye ekleyerek tüm mapping iþlemlerini otomatik yükler ve her yerde kullanýlabilir hale getirir.
//Bu kod, AutoMapper'ý dependency injection (DI) sistemine ekler ve mevcut projedeki tüm AutoMapper profillerini otomatik olarak yükler. Böylece IMapper nesnesini projede rahatça kullanabiliriz.



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
