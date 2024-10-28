using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;

using LNSF.API.ServiceFilters;
using LNSF.Application.Interfaces;
using LNSF.Application.Services;
using LNSF.Domain.Repositories;
using LNSF.Infra.Data.Repositories;
using LNSF.Application.Validators;
using LNSF.Infra.Data.Configurations;
using LNSF.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LNSF.Infra.IOC;

public static class DependencyInjectionSetup
{
	public static IServiceCollection AddDependencyInjectionConfiguration(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddSingleton(configuration);

		services.AddTransient<IAuthService, AuthService>();

		services.AddTransient<IChainRepository, ChainRepository>();

		services.AddTransient<IEmergencyContactRepository, EmergencyContactRepository>();
		services.AddTransient<IEmergencyContactService, EmergencyContactService>();
		services.AddTransient<EmergencyContactValidator>();

		services.AddTransient<IEscortRepository, EscortRepository>();
		services.AddTransient<IEscortService, EscortService>();

		services.AddTransient<IFamilyGroupProfileRepository, FamilyGroupProfileRepository>();
		services.AddTransient<IFamilyGroupProfileService, FamilyGroupProfileService>();
		services.AddTransient<FamilyGroupProfileValidator>();

		services.AddTransient<IHospitalRepository, HospitalRepository>();
		services.AddTransient<IHospitalService, HospitalService>();
		services.AddTransient<HospitalValidator>();

		services.AddTransient<IHostingRepository, HostingRepository>();
		services.AddTransient<IHostingService, HostingService>();
		services.AddTransient<HostingValidator>();

		services.AddTransient<IHostingEscortRepository, HostingEscortRepository>();
		services.AddTransient<IHostingEscortService, HostingEscortService>();

		services.AddTransient<IPatientRepository, PatientRepository>();
		services.AddTransient<IPatientService, PatientService>();

		services.AddTransient<IPatientTreatmentRepository, PatientTreatmentRepository>();
		services.AddTransient<IPatientTreatmentService, PatientTreatmentService>();

		services.AddTransient<IPeopleRoomHostingRepository, PeopleRoomHostingRepository>();
		services.AddTransient<IPeopleRoomHostingService, PeopleRoomHostingService>();

		services.AddTransient<IPeopleRepository, PeopleRepository>();
		services.AddTransient<IPeopleService, PeopleService>();
		services.AddTransient<PeopleValidator>();

		services.AddTransient<IRoleRepository, RoleRepository>();
		services.AddTransient<IRoleService, RoleService>();
		services.AddTransient<RoleValidator>();

		services.AddTransient<IRoomRepository, RoomRepository>();
		services.AddTransient<IRoomService, RoomService>();
		services.AddTransient<RoomValidator>();

		services.AddTransient<IServiceRecordRepository, ServiceRecordRepository>();
		services.AddTransient<IServiceRecordService, ServiceRecordService>();

		services.AddTransient<ITourRepository, TourRepository>();
		services.AddTransient<ITourService, TourService>();
		services.AddTransient<TourValidator>();

		services.AddTransient<ITreatmentRepository, TreatmentRepository>();
		services.AddTransient<ITreatmentService, TreatmentService>();
		services.AddTransient<TreatmentValidator>();

		services.AddTransient<IUserRepository, UserRepository>();
		services.AddTransient<IUserService, UserService>();
		services.AddTransient<UserValidator>();
		services.AddTransient<PasswordValidator>();

		services.AddTransient<IUserRoleRepository, UserRoleRepository>();
		services.AddTransient<IUserRoleService, UserRoleService>();

		services.AddTransient<AuthAndUserExtractionFilter>();
		// BaseConfiguration
		services.AddTransient<IEntityTypeConfiguration<EmergencyContact>, EmergencyContactsConfiguration>();
		services.AddTransient<GlobalValidator>();

		return services;
	}
}
