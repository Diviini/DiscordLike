// using Avalonia.Controls;
// using FrontEnd.ViewModels;

// namespace FrontEnd.Views;

// public partial class LoginView : UserControl
// {
//     public LoginView()
//     {
//         InitializeComponent();
//         DataContext = new LoginViewModel();
//     }
// }

// using Avalonia.Controls;
// using Avalonia.Markup.Xaml;

// namespace FrontEnd.Views
// {
//     public partial class LoginView : UserControl
//     {
//         public LoginView()
//         {
//             InitializeComponent();
//         }

//         private void InitializeComponent()
//         {
//             AvaloniaXamlLoader.Load(this);
//         }

//         private void PasswordInput_OnTextChanged(object sender, Avalonia.Interactivity.RoutedEventArgs e)
//         {
//             // Handle password changed event
//         }
//     }
// }
using Avalonia.Controls;
using Avalonia.Interactivity;
using FrontEnd.ViewModels;

namespace FrontEnd.Views;

public partial class LoginView : UserControl
{
    public LoginView()
    {
        InitializeComponent(); // ← Ça appelle la méthode auto-générée
    }

    private void PasswordInput_OnTextChanged(object? sender, RoutedEventArgs e)
    {
        if (DataContext is LoginViewModel vm && sender is TextBox textBox)
        {
            vm.Password = textBox.Text;
        }
    }
}
