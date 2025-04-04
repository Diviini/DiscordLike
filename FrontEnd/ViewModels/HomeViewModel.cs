using FrontEnd.Models;

namespace FrontEnd.ViewModels;

public class HomeViewModel : ViewModelBase
{
  public string Username { get; }
  public string Email { get; }
  public long UserId { get; }

  public string WelcomeMessage => $"Bienvenue Queen {Username} 👑";

  public HomeViewModel(UserInfo user)
  {
    Username = user.Username;
    Email = user.Email;
    UserId = user.UserId;
  }
}

