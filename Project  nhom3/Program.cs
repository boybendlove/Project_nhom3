using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Project__nhom3.Data;
using System;

var builder = WebApplication.CreateBuilder(args);

// Cấu hình Configuration
builder.Configuration.AddJsonFile("appsettings.json");

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("AirlineDbContext");
builder.Services.AddDbContext<AirlineDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(p => p.AddPolicy("MyCors", buid =>
{ //buid.WithOrigins("https://localhost:3000/";
    buid.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();
}));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var dbContext = serviceProvider.GetRequiredService<AirlineDbContext>();

    try
    {
        dbContext.Database.EnsureCreated(); // Kết nối và tạo cơ sở dữ liệu nếu chưa tồn tại
        Console.WriteLine("Kết nối cơ sở dữ liệu thành công!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Lỗi khi kết nối cơ sở dữ liệu: {ex.Message}");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("MyCors");
app.UseAuthorization();

app.MapControllers();

app.Run();
