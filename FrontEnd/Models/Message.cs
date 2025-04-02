using System;
using System.Text.Json.Serialization;

namespace FrontEnd.Models;

public class Message
{
  [JsonPropertyName("content")]
  public string MessageText { get; set; }

  [JsonPropertyName("date")]
  public DateTime Date { get; set; }

  [JsonPropertyName("sender_id")]
  public int SenderId { get; set; }

  [JsonPropertyName("username")]
  public string Sender { get; set; }
}
