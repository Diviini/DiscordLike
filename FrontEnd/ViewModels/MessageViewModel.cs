using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FrontEnd.ViewModels;

public class MessageViewModel : INotifyPropertyChanged
{
  private string _messageText = "";
  public string Sender { get; set; } = "Unknown";

  public string MessageText
  {
    get => _messageText;
    set
    {
      _messageText = value;
      OnPropertyChanged();
    }
  }

  public event PropertyChangedEventHandler? PropertyChanged;
  protected void OnPropertyChanged([CallerMemberName] string propertyName = null!)
  {
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
  }
}
