using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FrontEnd.Models
{
  public class Conversation
  {
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("createdByUsername")]
    public string CreatedByUsername { get; set; }

    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; }

    // This property will be used to store messages when retrieved
    [JsonPropertyName("messages")]
    public List<Message> Messages { get; set; } = new List<Message>();
  }
}
