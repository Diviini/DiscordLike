using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FrontEnd.Models
{
  public class Conversation
  {
    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("chat_room_id")]
    public int Conversation_id { get; set; }

    [JsonPropertyName("messages")]
    public List<Message> Messages { get; set; }
  }
}
