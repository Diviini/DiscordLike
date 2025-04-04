using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Threading.Tasks;
using FrontEnd.Models;
using FrontEnd.Services;
using ReactiveUI;

namespace FrontEnd.ViewModels
{
  public class ConversationViewModel : ReactiveObject
  {
    private readonly ChatService _chatService;
    private readonly Conversation _conversation;
    private string _newMessageText;

    public ConversationViewModel(ChatService chatService, Conversation conversation)
    {
      _chatService = chatService;
      _conversation = conversation;
      Messages = new ObservableCollection<Message>(conversation.Messages);
      SendMessageCommand = ReactiveCommand.Create<string>(SendMessage);

      LoadMessages();
    }

    public ObservableCollection<Message> Messages { get; set; }
    public string NewMessageText
    {
      get => _newMessageText;
      set => this.RaiseAndSetIfChanged(ref _newMessageText, value);
    }

    public ReactiveCommand<string, Unit> SendMessageCommand { get; }

    private async void LoadMessages()
    {
      var (success, message, messages) = await _chatService.GetMessagesForConversation(_conversation.Id);
      if (success)
      {
        Messages.Clear();
        foreach (var msg in messages)
        {
          Messages.Add(msg);
        }
      }
      else
      {
        // Handle error
        Console.WriteLine(message);
      }
    }

    private void SendMessage(string content)
    {
      // Logic to send a message
      var newMessage = new Message
      {
        Content = content,
        SenderId = "1",
        SentAt = DateTime.Now
      };
      Messages.Add(newMessage);
      NewMessageText = string.Empty;

      // Send the message to the server
      // _chatService.SendMessage(newMessage);
    }
  }
}
