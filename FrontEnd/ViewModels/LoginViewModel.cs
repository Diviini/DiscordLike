using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using FrontEnd.Helpers;

namespace FrontEnd.ViewModels;

public class LoginViewModel : INotifyPropertyChanged
{
    private string _email = "";
    private string _password = "";
    private string _message = "";

    public event PropertyChangedEventHandler? PropertyChanged;

    public string Email
    {
        get => _email;
        set { _email = value; OnPropertyChanged(); OnPropertyChanged(nameof(CanLogin)); }
    }

    public string Password
    {
        get => _password;
        set { _password = value; OnPropertyChanged(); OnPropertyChanged(nameof(CanLogin)); }
    }

    public string Message
    {
        get => _message;
        set { _message = value; OnPropertyChanged(); }
    }

    public bool CanLogin => !string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Password);

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null!)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
