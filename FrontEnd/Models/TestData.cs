namespace FrontEnd.Models;
using System.Text.Json.Serialization;

public class TestData
{
  [JsonPropertyName("title")]
  public string Title { get; set; }
}
