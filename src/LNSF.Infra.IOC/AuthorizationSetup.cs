using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace LNSF.Infra.IOC;

public static class AuthorizationSetup
{
	public static IServiceCollection AddAuthorizationConfiguration(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddAuthorization(options =>
		{
			options.AddPolicy("Administrador", policy =>
			{
				policy.RequireRole(["Desenvolvedor", "Administrador"]);
			});

			options.AddPolicy("Assistente Social", policy =>
			{
				policy.RequireRole(["Desenvolvedor", "Administrador", "Assistente Social"]);
			});

			options.AddPolicy("Secretário", policy =>
			{
				policy.RequireRole(["Desenvolvedor", "Administrador", "Secretário"]);
			});

			options.AddPolicy("Base", policy =>
			{
				policy.RequireRole(["Desenvolvedor", "Administrador", "Assistente Social", "Secretário"]);
			});

			options.AddPolicy("User", policy =>
			{
				policy.RequireRole(["Desenvolvedor", "Administrador", "Assistente Social", "Secretário", "Voluntário"]);
			});
		});

		return services;
	}
}
