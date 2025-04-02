namespace FrontEnd.Models;

public class LoginResponse
{
    public string Token { get; set; } = "";
    public string Username { get; set; } = "";
    // Include additional properties returned by your backend if needed.
}
