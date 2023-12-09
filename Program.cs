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
using Serilog;
using Serilog.Formatting.Json;
using LNSF.Application.Interfaces;
using LNSF.Domain.Exceptions;
using LNSF.Infra.Data.Migrations;
using System.Reflection;
using LNSF.Infra.Data;
using Microsoft.AspNetCore.Identity;
using System.Net;

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
    configure.CreateMap<IdentityUser, UserViewModel>().ReverseMap();
    configure.CreateMap<IdentityUser, UserPostViewModel>().ReverseMap();
    configure.CreateMap<IdentityRole, RoleViewModel>().ReverseMap();
    configure.CreateMap<IdentityRole, RolePostViewModel>().ReverseMap();

    configure.CreateMap<Room, RoomPostViewModel>().ReverseMap();
    configure.CreateMap<Room, RoomViewModel>().ReverseMap();
    configure.CreateMap<RoomViewModel, RoomPostViewModel>().ReverseMap();
    
    configure.CreateMap<People, PeoplePostViewModel>().ReverseMap();
    configure.CreateMap<People, PeoplePutViewModel>().ReverseMap();
    configure.CreateMap<People, PeopleViewModel>().ReverseMap();

    configure.CreateMap<PeopleRoom, PeopleRoomViewModel>().ReverseMap();

    configure.CreateMap<EmergencyContact, EmergencyContactPostViewModel>().ReverseMap();
    configure.CreateMap<EmergencyContact, EmergencyContactViewModel>().ReverseMap();

    configure.CreateMap<TourViewModel, TourPostViewModel>().ReverseMap();
    configure.CreateMap<Tour, TourViewModel>().ReverseMap();
    configure.CreateMap<Tour, TourPostViewModel>().ReverseMap();
    configure.CreateMap<Tour, TourPutViewModel>().ReverseMap();
    configure.CreateMap<TourViewModel, TourPutViewModel>().ReverseMap();

    configure.CreateMap<Hospital, HospitalViewModel>().ReverseMap();
    configure.CreateMap<Hospital, HospitalPostViewModel>().ReverseMap();

    configure.CreateMap<Patient, PatientPostViewModel>().ReverseMap();
    configure.CreateMap<Patient, PatientViewModel>().ReverseMap();

    configure.CreateMap<Escort, EscortPostViewModel>().ReverseMap();
    configure.CreateMap<Escort, EscortViewModel>().ReverseMap();

    configure.CreateMap<Treatment, TreatmentViewModel>().ReverseMap();
    configure.CreateMap<Treatment, TreatmentPostViewModel>().ReverseMap();

    configure.CreateMap<Hosting, HostingViewModel>().ReverseMap();
    configure.CreateMap<Hosting, HostingPostViewModel>().ReverseMap();
});

builder.Services.AddSingleton(autoMapperConfig.CreateMapper());

#endregion

#region DI

IConfiguration configuration = builder.Configuration.GetSection("Jwt");
builder.Services.AddSingleton(configuration);

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<UserValidator>();
builder.Services.AddTransient<IdentityUserRole<string>>();
builder.Services.AddTransient<SignInManager<IdentityUser>>();

builder.Services.AddTransient<IRoleService, RoleService>();
builder.Services.AddTransient<IRoleRepository, RoleRepository>();
builder.Services.AddTransient<RoleValidator>();

builder.Services.AddTransient<IUserRoleRepository, UserRoleRepository>();
builder.Services.AddTransient<IUserRoleService, UserRoleService>();

builder.Services.AddTransient<IAuthenticationTokenService, AuthenticationTokenService>();

builder.Services.AddTransient<PasswordValidator>();

builder.Services.AddTransient<IGlobalRepository, GlobalRepository>();

builder.Services.AddTransient<ITourRepository, ToursRepository>();
builder.Services.AddTransient<TourValidator>();
builder.Services.AddTransient<GlobalValidator>();
builder.Services.AddTransient<ITourService, TourService>();

builder.Services.AddTransient<IRoomRepository, RoomsRepository>();
builder.Services.AddTransient<RoomValidator>();
builder.Services.AddTransient<IRoomService, RoomService>();

builder.Services.AddTransient<IPeopleRepository, PeopleRepository>();
builder.Services.AddTransient<PeopleValidator>();
builder.Services.AddTransient<IPeopleService, PeopleService>();

builder.Services.AddTransient<IPeopleRoomRepository, PeopleRoomRepository>();
builder.Services.AddTransient<IPeopleRoomService, PeopleRoomService>();

builder.Services.AddTransient<IEmergencyContactRepository, EmergencyContactRepository>();
builder.Services.AddTransient<EmergencyContactValidator>();
builder.Services.AddTransient<IEmergencyContactService, EmergencyContactService>();

builder.Services.AddTransient<IHospitalRepository, HospitalRepository>();
builder.Services.AddTransient<IHospitalService, HospitalService>();
builder.Services.AddTransient<HospitalValidator>();

builder.Services.AddTransient<IPatientRepository, PatientRepository>();
builder.Services.AddTransient<IPatientService, PatientService>();

builder.Services.AddTransient<IEscortRepository, EscortRepository>();
builder.Services.AddTransient<IEscortService, EscortService>();

builder.Services.AddTransient<ITreatmentRepository, TreatmentRepository>();
builder.Services.AddTransient<ITreatmentService, TreatmentService>();
builder.Services.AddTransient<TreatmentValidator>();

builder.Services.AddTransient<IHostingService, HostingService>();
builder.Services.AddTransient<IHostingRepository, HostingRepository>();
builder.Services.AddTransient<HostingValidator>();

builder.Services.AddTransient<IPatientTreatmentRepository, PatientTreatmentRepository>();

builder.Services.AddTransient<IHostingEscortRepository, HostingEscortRepository>();

builder.Services.AddTransient<IReportService, ReportService>();

#endregion

#region CORS

var cors = builder.Configuration.GetSection("Cors");
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: cors["PolicyName"] ?? throw new AppException("Cors: PolicyName is null!", HttpStatusCode.InternalServerError), builder =>
    {
        builder.WithOrigins(cors["AllowedOrigins"] ?? throw new AppException("Cors: WithOrigins is null!", HttpStatusCode.InternalServerError))
            //.WithMethods(cors["AllowedMethods"] ?? throw new AppException("Cors: WithMethods is null!"))
            .AllowAnyMethod()
            //.WithHeaders(cors["AllowedHeaders"] ?? throw new AppException("Cors: WithHeaders is null!"))
            .AllowAnyHeader()
            .DisallowCredentials();
    });
});

#endregion

#region Context

builder.Services.AddDbContext<AppDbContext>(options =>
{
    // options.UseInMemoryDatabase("InMemoryDatabaseName");
    // options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionMSSQL"));
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnectionSQLite"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

#endregion

#region Authentication

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    }
).AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"] ?? throw new AppException("JwtConfig: Issuer is null", HttpStatusCode.InternalServerError),
            ValidAudience = builder.Configuration["Jwt:Audience"] ?? throw new AppException("JwtConfig: Audience is null", HttpStatusCode.InternalServerError),
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"] ?? throw new AppException("JwtConfig: Secret is null", HttpStatusCode.InternalServerError))),
        };
    }
);

#endregion

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

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    setup.IncludeXmlComments(xmlPath);

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

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

var helperMigration = new HelperMigration(app.Services);

app.UseExceptionHandler("/api/Error");

if (app.Environment.IsDevelopment() || 
    app.Environment.IsProduction() || 
    app.Environment.IsStaging())
{
    // app.UseDeveloperExceptionPage();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseCors(cors["PolicyName"] ?? throw new AppException("Cors: PolicyName is null!", HttpStatusCode.InternalServerError));

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
