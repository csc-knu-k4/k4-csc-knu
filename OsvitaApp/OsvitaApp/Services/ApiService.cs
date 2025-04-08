using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class ApiService
{
    public static readonly string BaseUrl = "http://192.168.31.195:5134";
    private readonly HttpClient _httpClient = new HttpClient();
    private string _authToken;
    private const string JsonMimeType = "application/json";

    public void SetAuthToken(string token)
    {
        _authToken = token;
        _httpClient.DefaultRequestHeaders.Authorization = string.IsNullOrEmpty(_authToken)
            ? null
            : new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _authToken);
    }

    public void ClearAuthToken()
    {
        SetAuthToken(null);
    }

    public async Task<(bool IsSuccess, T Data, string ErrorMessage)> GetAsync<T>(string endpoint)
        => await SendRequestAsync<T>(HttpMethod.Get, endpoint);

    public async Task<(bool IsSuccess, T Data, string ErrorMessage)> GetAsync<T>(string endpoint, object payload)
    {
        var queryString = payload != null ? SerializePayloadToQueryString(payload) : string.Empty;
        var fullEndpoint = string.IsNullOrEmpty(queryString) ? endpoint : $"{endpoint}?{queryString}";
        return await SendRequestAsync<T>(HttpMethod.Get, endpoint);
    }

    public async Task<(bool IsSuccess, T Data, string ErrorMessage)> PostAsync<T>(string endpoint, object payload)
        => await SendRequestAsync<T>(HttpMethod.Post, endpoint, payload);

    public async Task<(bool IsSuccess, T Data, string ErrorMessage)> PutAsync<T>(string endpoint, object payload)
        => await SendRequestAsync<T>(HttpMethod.Put, endpoint, payload);

    public async Task<(bool IsSuccess, string ErrorMessage)> DeleteAsync(string endpoint)
    {
        var result = await SendRequestAsync<object>(HttpMethod.Delete, endpoint);
        return (result.IsSuccess, result.ErrorMessage);
    }

    private async Task<(bool IsSuccess, T Data, string ErrorMessage)> SendRequestAsync<T>(
        HttpMethod method,
        string endpoint,
        object payload = null)
    {
        try
        {
            var fullUrl = new Uri(new Uri(BaseUrl), endpoint); // Додаємо BaseUrl до ендпоінту
            var request = CreateHttpRequest(method, fullUrl.ToString(), payload);

            var response = await _httpClient.SendAsync(request);
            return await HandleResponse<T>(response);
        }
        catch (Exception ex)
        {
            return (false, default, $"Exception: {ex.Message}");
        }
    }

    private HttpRequestMessage CreateHttpRequest(HttpMethod method, string url, object payload)
    {
        var request = new HttpRequestMessage(method, url);
        if (payload != null)
        {
            var json = JsonSerializer.Serialize(payload);
            request.Content = new StringContent(json, Encoding.UTF8, JsonMimeType);
        }

        return request;
    }

    private static string SerializePayloadToQueryString(object payload)
    {
        var properties = payload.GetType().GetProperties();
        return string.Join("&", properties.Select(prop =>
        {
            var name = Uri.EscapeDataString(prop.Name);
            var value = Uri.EscapeDataString(prop.GetValue(payload)?.ToString() ?? string.Empty);
            return $"{name}={value}";
        }));
    }

    public async Task<(bool IsSuccess, T Data, string ErrorMessage)> HandleResponse<T>(HttpResponseMessage response)
    {
        if (response == null)
        {
            return (false, default, "No response received");
        }

        if (response.IsSuccessStatusCode)
        {
            try
            {
                var content = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return (true, data, null);
            }
            catch (Exception ex)
            {
                return (false, default, $"Failed to parse response: {ex.Message}");
            }
        }
        else
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            return (false, default, errorMessage);
        }
    }
}