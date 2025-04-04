using System;
using FrontEnd.Models;
using FrontEnd.Services;
using FrontEnd.ViewModels;
using ReactiveUI;

namespace FrontEnd.ViewModels
{
  public class HomeViewModel : ViewModelBase
  {
    public string Username { get; }
    public string Email { get; }
    public long UserId { get; }

    public string WelcomeMessage => $"Bienvenue Queen {Username} 👑";

    public ChatRoomsViewModel ChatRoomsViewModel { get; }
    private ConversationViewModel _selectedConversation;

    public ConversationViewModel SelectedConversation
    {
      get => _selectedConversation;
      set
      {
        if (_selectedConversation != value)
        {
          _selectedConversation = value;
          OnPropertyChanged(nameof(SelectedConversation));
        }
      }
    }

    public HomeViewModel(UserInfo user, ChatService chatService)
    {
      Username = user.Username;
      Email = user.Email;
      UserId = user.UserId;
      Console.WriteLine($"username: {Username}");

      ChatRoomsViewModel = new ChatRoomsViewModel(chatService, Username);
      ChatRoomsViewModel.SelectConversationCommand.Subscribe(conversation =>
      {
        SelectedConversation = new ConversationViewModel(chatService, conversation);
      });
    }
  }
}

