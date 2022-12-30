//Import Models, services and plugins to use
using BackEndAPI.Models;
using Microsoft.EntityFrameworkCore;
using BackEndAPI.Services.Contract;
using BackEndAPI.Services.Implementation;
using BackEndAPI.Utilities;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Conection to DBSupermarket
builder.Services.AddDbContext<DBSupermarketContext>(options =>
{

    options.UseSqlServer(builder.Configuration.GetConnectionString("sqlConection"));

});

//Services
builder.Services.AddScoped<IProductServices, ProductService>();

//Mapping
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

//Configuration CORS
builder.Services.AddCors(options => {
    options.AddPolicy("PolicityToApp", app =>
    {
        app.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Use CORS
app.UseCors("PolicityToApp");

app.UseAuthorization();

app.MapControllers();

app.Run();
