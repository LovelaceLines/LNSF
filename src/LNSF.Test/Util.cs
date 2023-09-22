using System.Net.Http.Json;
using Newtonsoft.Json;

namespace LNSF.Test;

public class Util
{
    public const string BaseUrl = "http://localhost:5206/api/";
    public async Task<int> GetQuantity(HttpClient client)
    {
        var response = await client.GetAsync("quantity");
        var quantity = int.Parse(await response.Content.ReadAsStringAsync());

        return quantity;
    }

    public string ConvertObjectToQueryString(object obj)
    {
        var json = JsonConvert.SerializeObject(obj);
        var encodedJson = Uri.EscapeDataString(json);

        return $"?{encodedJson}";
    }
}
