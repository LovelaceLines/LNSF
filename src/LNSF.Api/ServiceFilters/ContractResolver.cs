using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Reflection;

namespace LNSF.API.ServiceFilters;

public class SerializationContract
{
	public required string Role { get; set; }
	public string[] PropsSkipSerialization { get; set; } = [];
}

public class ContractResolver(SerializationContract[] serializationContracts, IHttpContextAccessor httpContextAccessor) : DefaultContractResolver
{
	protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
	{
		var userRoles = httpContextAccessor.HttpContext?.Items["CurrentUserRoles"] as string[] ?? [];

		var property = base.CreateProperty(member, memberSerialization);

		foreach (var role in userRoles)
		{
			var contract = serializationContracts.FirstOrDefault(x => x.Role == role);

			if (contract == null) continue;

			if (contract.Role == role && contract.PropsSkipSerialization.Contains(property.PropertyName))
				property.ShouldSerialize = instance => false;
		}

		return property;
	}
}
