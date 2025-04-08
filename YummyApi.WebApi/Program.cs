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
//AutoMapper'� ba��ml�l�k enjeksiyonu(Dependency Injection) ile projeye ekleyerek t�m mapping i�lemlerini otomatik y�kler ve her yerde kullan�labilir hale getirir.
//Bu kod, AutoMapper'� dependency injection (DI) sistemine ekler ve mevcut projedeki t�m AutoMapper profillerini otomatik olarak y�kler. B�ylece IMapper nesnesini projede rahat�a kullanabiliriz.



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
