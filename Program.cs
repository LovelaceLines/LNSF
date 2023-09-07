using LNSF.Application.Services;
using LNSF.Application.Validators;
using LNSF.Domain.Repositories;
using LNSF.Infra.Data.Context;
using LNSF.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<IToorRepository, ToorRepository>();
builder.Services.AddTransient<ToorOutputValidator>();
builder.Services.AddTransient<ToorInputValidator>();
builder.Services.AddTransient<ToorService>();

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
