namespace FrontEnd.Models;

public class AuthResponse
{
    // public string Token { get; set; }  // Si ton backend renvoie un JWT
    public string Message { get; set; }
    public bool Success { get; set; }
}
