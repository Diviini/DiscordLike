using System;
using FrontEnd.Models;
using FrontEnd.Services;
using ReactiveUI;

namespace FrontEnd.ViewModels
{
  public class HomeViewModel : ViewModelBase
  {
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
      ChatRoomsViewModel = new ChatRoomsViewModel(chatService);
      ChatRoomsViewModel.SelectConversationCommand.Subscribe(conversation =>
      {
        SelectedConversation = new ConversationViewModel(chatService, conversation);
      });
    }
  }
}
