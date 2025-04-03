using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FrontEnd.Models;
using RestSharp;
using Newtonsoft.Json;

namespace FrontEnd.Services;

public class AuthService
{
    private readonly RestClient _client;

    public AuthService()
    {
        var options = new RestClientOptions("http://localhost:8080")
        {
            CookieContainer = new CookieContainer() // ✅ on garde les cookies de session
        };

        _client = new RestClient(options);
    }

    public async Task<(bool success, string message, UserInfo? user)> LoginAndGetUserInfo(string username, string password)
    {
        string basicAuth = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));

        // 🔐 1. POST /auth/login
        var loginRequest = new RestRequest("/auth/login", Method.Post);
        loginRequest.AddHeader("Authorization", $"Basic {basicAuth}");

        var loginResponse = await _client.ExecuteAsync(loginRequest);

        if (loginResponse.StatusCode == HttpStatusCode.Unauthorized)
            return (false, "❌ Identifiants incorrects", null);

        if (!loginResponse.IsSuccessful)
            return (false, $"Erreur serveur ({(int)loginResponse.StatusCode})", null);

        // 🙋 2. GET /users/me
        var userRequest = new RestRequest("/users/me", Method.Get);
        userRequest.AddHeader("Authorization", $"Basic {basicAuth}"); // toujours utile pour certains backends

        var userResponse = await _client.ExecuteAsync(userRequest);

        if (!userResponse.IsSuccessful)
            return (false, $"Erreur lors de la récupération du profil ({(int)userResponse.StatusCode})", null);

        var userInfo = JsonConvert.DeserializeObject<UserInfo>(userResponse.Content ?? "");

        return (true, "Connexion réussie ✅", userInfo);
    }
}
