using System.Collections.ObjectModel;

namespace FrontEnd.ViewModels
{
  public class SidebarViewModel
  {
    public ObservableCollection<string> Conversations { get; } = new();

    public SidebarViewModel()
    {
      // Initialize with a default conversation title
      Conversations.Add("Conv1");
    }

    public void AddConversation(string title)
    {
      Conversations.Add(title);
    }
  }
}
