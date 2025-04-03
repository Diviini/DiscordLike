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
            var response = await _httpClient.PostAsJsonAsync("/auth/register", user);
            return response.IsSuccessStatusCode;
        }

        // Method to login.
        public async Task<LoginResponse?> LoginAsync(UserLogin login)
        {
            // Adjust the endpoint as needed.
            var response = await _httpClient.PostAsJsonAsync("/auth/login", login);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<LoginResponse>();
            }
            return null;
        }

    }
}
