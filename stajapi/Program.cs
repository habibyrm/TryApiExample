using Microsoft.EntityFrameworkCore;
using stajapi.Entities;
using stajapi.Helpers;
using stajapi.Services;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container.
//builder.Services.Configure<ConnectionString>;
builder.Services.AddScoped<DogumKayitService>();
builder.Services.AddDbContext<DataContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSqlConnection")));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "stajapi v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
