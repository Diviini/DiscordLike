using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FrontEnd.Models;

namespace FrontEnd.ViewModels
{
  public class ConversationViewModel : INotifyPropertyChanged
  {
    private ObservableCollection<MessageViewModel> _messages;
    private string _title;

    public ConversationViewModel(string title)
    {
      _title = title;
      Messages = new ObservableCollection<MessageViewModel>();
    }

    public ObservableCollection<MessageViewModel> Messages
    {
      get => _messages;
      set
      {
        _messages = value;
        OnPropertyChanged();
      }
    }

    public string Title
    {
      get => _title;
      set
      {
        _title = value;
        OnPropertyChanged();
      }
    }

    public void LoadFakeData(List<Conversation> chatRooms)
    {
      Messages.Clear();
      foreach (var chatRoom in chatRooms)
      {
        foreach (var message in chatRoom.Messages)
        {
          Messages.Add(new MessageViewModel
          {
            MessageText = message.MessageText,
            Sender = message.Sender,
            Date = message.Date
          });
        }
      }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null!)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
