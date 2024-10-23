using AutoMapper;
using LNSF.Domain.DTOs;
using LNSF.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace LNSF.Infra.IOC;

public static class AutoMapperSetup
{
	public static IServiceCollection AddAutoMapperConfiguration(this IServiceCollection services)
	{
		var autoMapperConfig = new MapperConfiguration(cfg =>
		{
			cfg.CreateMap<User, UserDTO>().ReverseMap();
			cfg.CreateMap<Patient, PatientDTO>().ReverseMap();
			cfg.CreateMap<People, PeopleDTO>().ReverseMap();
			cfg.CreateMap<Hosting, HostingDTO>().ReverseMap();
		});

		services.AddSingleton(autoMapperConfig.CreateMapper());

		return services;
	}
}
