using Newtonsoft.Json;
using LNSF.Domain.Filters;

namespace LNSF.API.ServiceFilters;

public class JsonSerializer<T>
{
	public List<T> Serialize(ContractResolver contractResolver, List<T> values)
	{
		JsonSerializerSettings Settings = new()
		{
			ContractResolver = contractResolver
		};

		var json = JsonConvert.SerializeObject(values, Settings);
		var deserializedItems = JsonConvert.DeserializeObject<List<T>>(json);

		if (deserializedItems != null)
			values = deserializedItems;

		return values;
	}

	public QueryResult<T> Serialize(ContractResolver contractResolver, QueryResult<T> values)
	{
		values.Items = this.Serialize(contractResolver, values.Items);
		return values;
	}
}
