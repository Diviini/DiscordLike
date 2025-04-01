using System.Collections.ObjectModel;

namespace FrontEnd.ViewModels;

public class ConversationViewModel
{
  public string Title { get; }
  public ObservableCollection<MessageViewModel> Messages { get; } = new();

  public ConversationViewModel(string title)
  {
    Title = title;
  }
}
