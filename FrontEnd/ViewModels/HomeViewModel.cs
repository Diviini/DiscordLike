using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Linq;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.Input;
using FrontEnd.Models;

namespace FrontEnd.ViewModels;

public class HomeViewModel : INotifyPropertyChanged
{
  private string _test = "Default Title";
  private string _message = "";
  private ConversationViewModel? _selectedConversation;
  public ObservableCollection<ConversationViewModel> Conversations { get; } = new();

  public string Test
  {
    get => _test;
    set
    {
      _test = value;
      OnPropertyChanged(nameof(Test));
      Console.WriteLine($"title: {_test}");
    }
  }

  public ConversationViewModel? SelectedConversation
  {
    get => _selectedConversation;
    set
    {
      _selectedConversation = value;
      OnPropertyChanged();
      OnPropertyChanged(nameof(Messages)); // Update displayed messages
    }
  }

  public ObservableCollection<MessageViewModel> Messages => SelectedConversation?.Messages ?? new ObservableCollection<MessageViewModel>();

  public string Message
  {
    get => _message;
    set
    {
      _message = value;
      OnPropertyChanged();
      OnPropertyChanged(nameof(CanSendMessage));
    }
  }

  public bool CanSendMessage => !string.IsNullOrWhiteSpace(Message) && SelectedConversation != null;

  public ICommand SendMessageCommand { get; }
  public ICommand CreateConversationCommand { get; }
  public ICommand SelectConversationCommand { get; }

  public HomeViewModel()
  {
    SendMessageCommand = new RelayCommand(ExecuteSendMessage);
    CreateConversationCommand = new RelayCommand(ExecuteCreateConversation);
    SelectConversationCommand = new RelayCommand<ConversationViewModel>(ExecuteSelectConversation);
  }

  public void LoadData(List<Conversation> conversations)
  {
    foreach (var conversation in conversations)
    {
      Console.WriteLine($"conversations: {conversation.Title}");
      var chatRoom = new ConversationViewModel(conversation.Title);
      chatRoom.LoadFakeData(new List<Conversation> { conversation });
      Conversations.Add(chatRoom);
    }

    // Select the first conversation by default
    SelectedConversation = Conversations.FirstOrDefault();
    Console.WriteLine($"Selected conversation: {SelectedConversation?.Title}");
    OnPropertyChanged(nameof(Messages));
  }

  private void ExecuteSendMessage()
  {
    if (SelectedConversation == null) return;

    SelectedConversation.Messages.Add(new MessageViewModel
    {
      MessageText = Message,
      Sender = "Me",
      Date = DateTime.Now
    });

    Console.WriteLine($"conv: {Message}");

    Message = "";
    OnPropertyChanged(nameof(Messages));
  }

  private void ExecuteCreateConversation()
  {
    var newConv = new ConversationViewModel($"Conv{Conversations.Count + 1}");
    Conversations.Add(newConv);
    SelectedConversation = newConv;
  }
  private void ExecuteSelectConversation(ConversationViewModel? conversation)
  {
    if (conversation != null)
    {
      SelectedConversation = conversation;
    }
  }

  public event PropertyChangedEventHandler? PropertyChanged;
  protected void OnPropertyChanged([CallerMemberName] string propertyName = null!)
  {
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
  }
}
