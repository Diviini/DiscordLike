// using System.Net.Http;
// using System.Text;
// using System.Text.Json;
// using System.Threading.Tasks;
// using FrontEnd.Models;
// using System;

// namespace FrontEnd.Services;

// public class ApiService
// {
//     private readonly HttpClient _httpClient = new();

//     public async Task<bool> LoginAsync(UserLogin login)
//     {
//         var json = JsonSerializer.Serialize(login);
//         var content = new StringContent(json, Encoding.UTF8, "application/json");

//         var response = await _httpClient.PostAsync("http://localhost:8080/api/login", content);
//         return response.IsSuccessStatusCode;
//     }
// }

using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FrontEnd.Models;

namespace FrontEnd.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService()
        {
            // Set the base address to your Spring Boot backend.
            _httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:8080") };
        }

        // Method to register a new user.
        public async Task<bool> RegisterUserAsync(User user)
        {
            // Adjust the endpoint as needed.
            var response = await _httpClient.PostAsJsonAsync("/api/register", user);
            return response.IsSuccessStatusCode;
        }

        // Method to login.
        public async Task<LoginResponse?> LoginAsync(UserLogin login)
        {
            // Adjust the endpoint as needed.
            var response = await _httpClient.PostAsJsonAsync("/api/login", login);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<LoginResponse>();
            }
            return null;
        }
    }
}
