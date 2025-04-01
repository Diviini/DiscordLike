using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Linq;
using CommunityToolkit.Mvvm.Input;

namespace FrontEnd.ViewModels;

public class HomeViewModel : INotifyPropertyChanged
{
  private string _message = "";
  private ConversationViewModel? _selectedConversation;

  public ObservableCollection<ConversationViewModel> Conversations { get; } = new();
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

    // Sample conversations
    Conversations.Add(new ConversationViewModel("Conv1"));
    Conversations.Add(new ConversationViewModel("Conv2"));
    Conversations.Add(new ConversationViewModel("Conv3"));

    // Select first conversation by default
    SelectedConversation = Conversations.FirstOrDefault();
  }

  private void ExecuteSendMessage()
  {
    if (SelectedConversation == null) return;

    SelectedConversation.Messages.Add(new MessageViewModel
    {
      MessageText = Message,
      Sender = "Me"
    });

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
      => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
