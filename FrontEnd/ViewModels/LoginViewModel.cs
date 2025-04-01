using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Front_end.Helpers;

namespace Front_end.ViewModels;

public class LoginViewModel : INotifyPropertyChanged
{
    private string _email = "";
    private string _password = "";
    private string _message = "";

    public string Email
    {
        get => _email;
        set
        {
            _email = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(CanLogin));
        }
    }

    public string Password
    {
        get => _password;
        set
        {
            _password = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(CanLogin));
        }
    }

    public string Message
    {
        get => _message;
        set { _message = value; OnPropertyChanged(); }
    }

    public ICommand LoginCommand { get; }

    public bool CanLogin => !string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Password);

    public LoginViewModel()
    {
        LoginCommand = new RelayCommand(ExecuteLogin);
    }

    private void ExecuteLogin(object? obj)
    {
        if (!CanLogin)
        {
            Message = "Remplis tous les champs queen 😅";
            return;
        }

        Message = $"Bienvenue {Email} 👑";
        // 👉 ici on pourra ajouter la navigation vers une autre vue
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
