using LNSF.Application;
using LNSF.Application.Services;
using LNSF.Application.Validators;
using LNSF.Domain.Entities;
using LNSF.Domain.Repositories;
using LNSF.Infra.Data.Context;
using LNSF.Infra.Data.Repositories;
using LNSF.UI.ViewModels;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

#region AutoMapper

var autoMapperConfig = new MapperConfiguration(configure =>
{
    configure.CreateMap<Account, AccountViewModel>().ReverseMap();

    configure.CreateMap<Room, RoomPostViewModel>().ReverseMap();
    configure.CreateMap<Room, RoomViewModel>().ReverseMap();
    configure.CreateMap<RoomViewModel, RoomPostViewModel>().ReverseMap();
    
    configure.CreateMap<People, PeoplePostViewModel>().ReverseMap();
    configure.CreateMap<People, PeoplePutViewModel>().ReverseMap();
    configure.CreateMap<People, PeopleAddPeopleToRoomViewModel>().ReverseMap();
    configure.CreateMap<People, PeopleViewModel>().ReverseMap();

    configure.CreateMap<EmergencyContact, EmergencyContactPostViewModel>().ReverseMap();
    configure.CreateMap<EmergencyContact, EmergencyContactViewModel>().ReverseMap();

    configure.CreateMap<TourViewModel, TourPostViewModel>().ReverseMap();
    configure.CreateMap<Tour, TourViewModel>().ReverseMap();
    configure.CreateMap<Tour, TourPostViewModel>().ReverseMap();
    configure.CreateMap<Tour, TourPutViewModel>().ReverseMap();
    configure.CreateMap<TourViewModel, TourPutViewModel>().ReverseMap();
});

builder.Services.AddSingleton(autoMapperConfig.CreateMapper());

#endregion

#region DI

IConfiguration configuration = builder.Configuration.GetSection("Jwt");
builder.Services.AddSingleton(configuration);
builder.Services.AddTransient<TokenService>();
builder.Services.AddTransient<AccountService>();

builder.Services.AddTransient<PaginationValidator>();

builder.Services.AddTransient<IToursRepository, ToursRepository>();
builder.Services.AddTransient<TourFilterValidator>();
builder.Services.AddTransient<TourValidator>();
builder.Services.AddTransient<GlobalValidator>();
builder.Services.AddTransient<TourService>();

builder.Services.AddTransient<IRoomsRepository, RoomsRepository>();
builder.Services.AddTransient<RoomValidator>();
builder.Services.AddTransient<RoomFilterValidator>();
builder.Services.AddTransient<RoomService>();

builder.Services.AddTransient<IPeoplesRepository, PeoplesRepository>();
builder.Services.AddTransient<PeopleFilterValidator>();
builder.Services.AddTransient<PeopleValidator>();
builder.Services.AddTransient<PeopleService>();

builder.Services.AddTransient<IEmergencyContactsRepository, EmergencyContactsRepository>();
builder.Services.AddTransient<EmergencyContactValidator>();
builder.Services.AddTransient<EmergencyContactService>();

builder.Services.AddTransient<IAccountRepository, AccountRepository>();

#endregion

builder.Services.AddControllers();

#region Authentication

var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:SecretKey"] ?? 
    throw new InvalidOperationException("JwtConfig: Secret is null"));

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }
).AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateAudience = false,
            ValidateIssuer = false,
        };
    }
);

#endregion

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseInMemoryDatabase("InMemoryDatabaseName");
    // options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

#region Swagger

builder.Services.AddSwaggerGen(setup => 
{
    setup.SwaggerDoc("v1", new() 
    { 
        Title = "LNSF.API",
        Description = "...",
        Version = "v1",
        Contact = new OpenApiContact
        {
            Name = "",
            Email = "",
            Url = new Uri("https://github.com/LovelaceLines/LNSF")
        },
    });

    setup.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme.",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "bearer"
    });

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new string[] {}
        }
    });
});

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || 
    app.Environment.IsProduction() || 
    app.Environment.IsStaging())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
