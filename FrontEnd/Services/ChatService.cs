using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FrontEnd.Models;
using RestSharp;
using Newtonsoft.Json;
using System.Text;

namespace FrontEnd.Services
{
  public class ChatService
  {
    private readonly RestClient _client;

    public string basicAuth = Convert.ToBase64String(Encoding.UTF8.GetBytes($"ayline:mdp"));

    public ChatService()
    {
      var options = new RestClientOptions("http://localhost:8080")
      {
        CookieContainer = new CookieContainer() // Keep session cookies
      };

      _client = new RestClient(options);
    }

    public async Task<(bool success, string message, List<Conversation>? chats)> GetAllChats()
    {
      var chatRequest = new RestRequest("/chat", Method.Get);
      chatRequest.AddHeader("Authorization", $"Basic {basicAuth}");

      var chatResponse = await _client.ExecuteAsync(chatRequest);


      if (!chatResponse.IsSuccessful)
        return (false, $"Erreur serveur ({(int)chatResponse.StatusCode})", null);

      var chats = JsonConvert.DeserializeObject<List<Conversation>>(chatResponse.Content ?? "");
      Console.WriteLine($"chats: {chats}");

      return (true, "Chats récupérés avec succès ✅", chats);
    }

    public async Task<(bool success, string message, List<Message>? messages)> GetMessagesForConversation(int conversationId)
    {
      var messagesRequest = new RestRequest($"/chat/{conversationId}/message", Method.Get);
      messagesRequest.AddHeader("Authorization", $"Basic {basicAuth}");
      var messagesResponse = await _client.ExecuteAsync(messagesRequest);

      if (!messagesResponse.IsSuccessful)
        return (false, $"Erreur serveur ({(int)messagesResponse.StatusCode})", null);

      var messages = JsonConvert.DeserializeObject<List<Message>>(messagesResponse.Content ?? "");
      Console.WriteLine($"messages: {messages}");

      return (true, "Messages récupérés avec succès ✅", messages);
    }
  }
}
