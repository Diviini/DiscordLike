using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Threading.Tasks;
using FrontEnd.Models;
using FrontEnd.Services;
using ReactiveUI;

namespace FrontEnd.ViewModels
{
  public class ChatRoomsViewModel : ReactiveObject
  {
    private readonly ChatService _chatService;
    public string Username { get; }

    public ChatRoomsViewModel(ChatService chatService, string username)
    {
      Username = username;
      _chatService = chatService;
      Conversations = new ObservableCollection<Conversation>();
      CreateConversationCommand = ReactiveCommand.Create(CreateConversation);
      SelectConversationCommand = ReactiveCommand.Create<Conversation, Conversation>(conversation => conversation);

      LoadConversations();
    }

    public ObservableCollection<Conversation> Conversations { get; set; }
    public ReactiveCommand<Unit, Unit> CreateConversationCommand { get; }
    public ReactiveCommand<Conversation, Conversation> SelectConversationCommand { get; }

    private async void LoadConversations()
    {
      var (success, message, chats) = await _chatService.GetAllChats();
      if (success)
      {
        Conversations.Clear();
        foreach (var chat in chats)
        {
          Conversations.Add(chat);
        }
      }
      else
      {
        // Handle error
        Console.WriteLine(message);
      }
    }

    private void CreateConversation()
    {
      // Logic to create a new conversation
    }
  }
}
