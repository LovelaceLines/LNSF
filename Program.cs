using LNSF.Application;
using LNSF.Application.Services;
using LNSF.Application.Validators;
using LNSF.Domain.Entities;
using LNSF.Domain.Repositories;
using LNSF.Infra.Data.Context;
using LNSF.Infra.Data.Repositories;
using LNSF.Api.ViewModels;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using LNSF.Domain.Filters;
using Serilog;
using Serilog.Formatting.Json;
using LNSF.Application.Interfaces;
using LNSF.Domain.Exceptions;
using LNSF.Infra.Data.Migrations;

var builder = WebApplication.CreateBuilder(args);

#region Logger

Log.Logger = new LoggerConfiguration() 
    .Enrich.FromLogContext()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Logging.AddSerilog();

#endregion

#region AutoMapper

var autoMapperConfig = new MapperConfiguration(configure =>
{
    configure.CreateMap<AuthenticationToken, AuthenticationTokenViewModel>().ReverseMap();

    configure.CreateMap<Account, AccountLoginViewModel>().ReverseMap();
    configure.CreateMap<Account, AccountViewModel>().ReverseMap();
    configure.CreateMap<Account, AccountPostViewModel>().ReverseMap();
    configure.CreateMap<Account, AccountPutViewModel>().ReverseMap();
    configure.CreateMap<AccountViewModel, AccountFilter>().ReverseMap();

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

    configure.CreateMap<Hospital, HospitalViewModel>().ReverseMap();
    configure.CreateMap<Hospital, HospitalPostViewModel>().ReverseMap();
});

builder.Services.AddSingleton(autoMapperConfig.CreateMapper());

#endregion

#region DI

IConfiguration configuration = builder.Configuration.GetSection("Jwt");
builder.Services.AddSingleton(configuration);

builder.Services.AddTransient<IAuthenticationTokenService, AuthenticationTokenService>();
builder.Services.AddTransient<AuthenticationTokenValidator>();

builder.Services.AddTransient<IAccountService, AccountService>();
builder.Services.AddTransient<AccountValidator>();
builder.Services.AddTransient<PasswordValidator>();
builder.Services.AddTransient<IAccountRepository, AccountRepository>();

builder.Services.AddTransient<PaginationValidator>();

builder.Services.AddTransient<IToursRepository, ToursRepository>();
builder.Services.AddTransient<TourFilterValidator>();
builder.Services.AddTransient<TourValidator>();
builder.Services.AddTransient<GlobalValidator>();
builder.Services.AddTransient<ITourService, TourService>();

builder.Services.AddTransient<IRoomsRepository, RoomsRepository>();
builder.Services.AddTransient<RoomValidator>();
builder.Services.AddTransient<RoomFilterValidator>();
builder.Services.AddTransient<IRoomService, RoomService>();

builder.Services.AddTransient<IPeoplesRepository, PeoplesRepository>();
builder.Services.AddTransient<PeopleFilterValidator>();
builder.Services.AddTransient<PeopleValidator>();
builder.Services.AddTransient<IPeopleService, PeopleService>();

builder.Services.AddTransient<IEmergencyContactsRepository, EmergencyContactsRepository>();
builder.Services.AddTransient<EmergencyContactValidator>();
builder.Services.AddTransient<IEmergencyContactService, EmergencyContactService>();

builder.Services.AddTransient<IHospitalRepository, HospitalRepository>();
builder.Services.AddTransient<IHospitalService, HospitalService>();

builder.Services.AddTransient<IPatientsRepository, PatientsRepository>();
builder.Services.AddTransient<IPatientService, PatientService>();

#endregion

#region CORS

var cors = builder.Configuration.GetSection("Cors");
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: cors["PolicyName"] ?? throw new AppException("Cors: PolicyName is null!"), builder =>
    {
        builder.WithOrigins(cors["AllowedOrigins"] ?? throw new AppException("Cors: WithOrigins is null!"))
            //.WithMethods(cors["AllowedMethods"] ?? throw new AppException("Cors: WithMethods is null!"))
            .AllowAnyMethod()
            //.WithHeaders(cors["AllowedHeaders"] ?? throw new AppException("Cors: WithHeaders is null!"))
            .AllowAnyHeader()
            .DisallowCredentials();
    });
});

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
    // options.UseInMemoryDatabase("InMemoryDatabaseName");
    // options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionMSSQL"));
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnectionSQLite"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

#region Swagger

builder.Services.AddSwaggerGen(setup => 
{
    setup.SwaggerDoc("v1", new() 
    { 
        Title = "LNSF.API - Lar Nossa Senhora de Fátima",
        Version = "v1",
        Description = "O sistema LNSF foi desenvolvido com um propósito fundamental: aprimorar a eficiência das operações realizadas pelos servidores dedicados ao Lar Nossa Senhora de Fátima. Este sistema visa a melhoria do processo de gestão dos pacientes e quartos, além de automatizar tarefas complexas, anteriormente realizadas de forma manual, como a emissão de relatórios essenciais para os servidores e entidades superiores.",
        Contact = new OpenApiContact
        {
            Name = "Lovelace Lines",
            Email = "lovelacelines@gmail.com",
            Url = new Uri("https://github.com/LovelaceLines/")
        },
        License = new OpenApiLicense
        {
            Name = "MIT",
            Url = new Uri("https://opensource.org/license/mit/"),
        }
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
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

#endregion

var app = builder.Build();

var helperMigration = new HelperMigration(app.Services);

app.UseExceptionHandler("/api/Error");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || 
    app.Environment.IsProduction() || 
    app.Environment.IsStaging())
{
    // app.UseDeveloperExceptionPage();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection(); 

app.UseCors(cors["PolicyName"] ?? throw new AppException("Cors: PolicyName is null!"));

// app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
