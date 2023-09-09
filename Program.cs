using LNSF.Application;
using LNSF.Application.Services;
using LNSF.Application.Services.Validators;
using LNSF.Application.Validators;
using LNSF.Domain;
using LNSF.Domain.Repositories;
using LNSF.Infra;
using LNSF.Infra.Data.Context;
using LNSF.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<PaginationValidator>();

builder.Services.AddTransient<ITourRepository, ToursRepository>();
builder.Services.AddTransient<TourOutputValidator>();
builder.Services.AddTransient<TourInputValidator>();
builder.Services.AddTransient<GlobalValidator>();
builder.Services.AddTransient<TourService>();

builder.Services.AddTransient<IRoomRepository, RoomRepository>();
builder.Services.AddTransient<RoomValidator>();
builder.Services.AddTransient<RoomService>();

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    // options.UseInMemoryDatabase("InMemoryDatabaseName");
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

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
