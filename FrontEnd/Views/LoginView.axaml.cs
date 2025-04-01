using Avalonia.Controls;
using FrontEnd.ViewModels;

namespace FrontEnd.Views;

public partial class LoginView : UserControl
{
    public LoginView()
    {
        InitializeComponent();
        DataContext = new LoginViewModel();
    }
}
