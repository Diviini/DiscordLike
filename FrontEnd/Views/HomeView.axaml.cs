using Avalonia.Controls;
using FrontEnd.ViewModels;

namespace FrontEnd.Views;

public partial class HomeView : UserControl
{
  public HomeView()
  {
    InitializeComponent();
    // DataContext = new HomeViewModel();
  }
}
