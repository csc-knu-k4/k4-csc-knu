using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Text.Json;

namespace OsvitaApp.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "http://192.168.31.195:5134/";

        public ApiService()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri(BaseUrl) };
        }

        public void SetAuthToken(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public void ClearAuthToken()
        {
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        // GET: Отримання даних
        public async Task<(bool IsSuccess, T Data, string ErrorMessage)> GetAsync<T>(string endpoint)
        {
            var response = await _httpClient.GetAsync(endpoint);
            return await HandleResponse<T>(response);
        }

        // GET: Отримання даних
        public async Task<(bool IsSuccess, T Data, string ErrorMessage)> GetAsync<T>(string endpoint, object payload)
        {
            var response = await _httpClient.GetAsync(endpoint);
            return await HandleResponse<T>(response);
        }

        // POST: Створення ресурсу
        public async Task<(bool IsSuccess, T Data, string ErrorMessage)> PostAsync<T>(string endpoint, object payload)
        {
            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(endpoint, content);
            return await HandleResponse<T>(response);
        }

        // PUT: Оновлення ресурсу
        public async Task<(bool IsSuccess, T Data, string ErrorMessage)> PutAsync<T>(string endpoint, object payload)
        {
            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(endpoint, content);
            return await HandleResponse<T>(response);
        }

        // DELETE: Видалення ресурсу
        public async Task<(bool IsSuccess, string ErrorMessage)> DeleteAsync(string endpoint)
        {
            var response = await _httpClient.DeleteAsync(endpoint);

            if(response.IsSuccessStatusCode)
                return (true, string.Empty);

            var error = await response.Content.ReadAsStringAsync();
            return (false, error);
        }

        // Обробка відповіді від сервера
        private async Task<(bool IsSuccess, T Data, string ErrorMessage)> HandleResponse<T>(HttpResponseMessage response)
        {
            if(response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return (true, data, string.Empty);
            }

            var error = await response.Content.ReadAsStringAsync();
            return (false, default, error);
        }
    }

}
