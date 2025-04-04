using System;
using System.Text.Json.Serialization;

namespace FrontEnd.Models
{
  public class Message
  {
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("content")]
    public string Content { get; set; }

    [JsonPropertyName("senderId")]
    public string SenderId { get; set; }

    [JsonPropertyName("sentAt")]
    public DateTime SentAt { get; set; }
  }
}
