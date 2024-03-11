
using Data.SqlServer;
using Server.WebUtilities;
using Services;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureSwagger();


//// Project specific.
builder.Services.AddSqlServerDataContext(builder.Configuration);
builder.Services.RegisterProjectServices(builder.Configuration);


// Personalized web utilities.
builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    builder.Services.AddCors();
    app.UseCors(p =>
    {
        p.AllowAnyHeader();
        p.AllowAnyMethod();
        p.AllowCredentials();
        p.WithOrigins("http://localhost:4200");
    });
    ;
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
app.MapControllers();

app.Run();
