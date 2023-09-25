using System.Net;
using System.Net.Http.Json;
using System.Reflection;
using System.Web;
using Newtonsoft.Json;

namespace LNSF.Test;

public class Global
{
    public const string BaseUrl = "http://localhost:5206/api/";
    public readonly HttpClient _peopleClient = new() { BaseAddress = new Uri($"{BaseUrl}People/") };
    public readonly HttpClient _roomClient = new() { BaseAddress = new Uri($"{BaseUrl}Room/") };

    public async Task<T> Get<T>(HttpClient client, string queryString = "") where T : class
    {
        var response = await client.GetAsync(queryString);
        var content = await response.Content.ReadAsStringAsync();
        T? value = JsonConvert.DeserializeObject<T>(content);

        return value ?? throw new Exception("Value is null");
    }    
    public async Task<int> GetQuantity(HttpClient client)
    {
        var response = await client.GetAsync("quantity");
        var quantity = int.Parse(await response.Content.ReadAsStringAsync());

        return quantity;
    }

    public async Task<T> Post<T>(HttpClient client, T value) where T : class
    {
        var postResponse = await client.PostAsync("", JsonContent.Create(value));
        T? content = await postResponse.Content.ReadFromJsonAsync<T>();

        return content ?? throw new Exception("Value is null");        
    }

    public async Task Post<T>(HttpClient client, List<T> values) where T : class
    {
        var postTasks = values.Select(value => client.PostAsync("", JsonContent.Create(value))).ToList();
        await Task.WhenAll(postTasks);
    }

    public string ConvertObjectToQueryString(object obj)
    {
        var properties = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

        var keyValuePairs = new List<string>();

        foreach (var property in properties)
        {
            var value = property.GetValue(obj);

            if (value != null)
            {
                var key = property.Name;
                var encodedValue = Uri.EscapeDataString(value.ToString());
                keyValuePairs.Add($"{key}={encodedValue}");
            }
        }

        var queryString = string.Join("&", keyValuePairs);
        return $"?{queryString}";
    }
}
