// using FrontEnd.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using FrontEnd.Helpers;
using FrontEnd.Models;
using FrontEnd.Services;



namespace FrontEnd.ViewModels;

public class LoginViewModel : ViewModelBase
{
    private string _email = "";
    public string Email
    {
        get => _email;
        set
        {
            _email = value;
            OnPropertyChanged();
            (LoginCommand as RelayCommand)?.RaiseCanExecuteChanged();
        }
    }

    private string _password = "";
    public string Password
    {
        get => _password;
        set
        {
            _password = value;
            OnPropertyChanged();
            (LoginCommand as RelayCommand)?.RaiseCanExecuteChanged();
        }
    }

    private string _message = "";
    public string Message
    {
        get => _message;
        set
        {
            _message = value;
            OnPropertyChanged();
        }
    }

    private readonly MainWindowViewModel _mainWindow;
    public ICommand LoginCommand { get; }

    private readonly List<UserLogin> fakeUsers = new()
    {
        new UserLogin { Email = "ines@mail.com", Password = "123456" },
        new UserLogin { Email = "test@mail.com", Password = "abcdef" }
    };

    public LoginViewModel(MainWindowViewModel mainWindow)
    {
        _mainWindow = mainWindow;
        LoginCommand = new RelayCommand(ExecuteLogin, CanLogin);
    }

    private bool CanLogin(object? parameter)
    {
        return !string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Password);
    }

    private async void ExecuteLogin(object? parameter)
    {
        var isMatch = fakeUsers.Any(u => u.Email == Email && u.Password == Password);

        if (isMatch)
        {
            Message = $"Bienvenue Queen {Email} 👑 (fake login réussi)";
            await Task.Delay(3000);
            _mainWindow.CurrentView = new HomeViewModel();
        }
        else
        {
            Message = "Identifiants incorrects ❌";
        }
    }
}
