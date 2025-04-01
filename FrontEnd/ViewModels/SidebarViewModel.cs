using System.Collections.ObjectModel;

namespace FrontEnd.ViewModels
{
  public class SidebarViewModel
  {
    public ObservableCollection<string> Conversations { get; } = new()
        {
            "Conv1", "Conv2", "Conv3"
        };
  }
}
