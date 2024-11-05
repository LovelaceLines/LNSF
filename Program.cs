using LNSF.Domain.Exceptions;
using LNSF.Infra.IOC;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLoggerConfiguration(builder.Configuration, builder.Logging);
builder.Services.AddAutoMapperConfiguration();
builder.Services.AddDependencyInjectionConfiguration(builder.Configuration);
builder.Services.AddDbContextConfiguration(builder.Configuration);
builder.Services.AddAuthenticationConfiguration(builder.Configuration);
builder.Services.AddAuthorizationConfiguration(builder.Configuration);
builder.Services.AddCorsConfiguration(builder.Configuration);
builder.Services.AddSwaggerConfiguration();

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.AddDatabaseAutoMigrationConfiguration();

app.UseExceptionHandler("/api/Error");

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(builder.Configuration["Cors:PolicyName"] ??
	throw new AppException("Cors:PolicyName not found in appsettings.json!", HttpStatusCode.InternalServerError));

app.UseAuthorization();

app.MapControllers();

app.Run();
