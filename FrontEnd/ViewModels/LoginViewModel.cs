using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FrontEnd.Helpers;
using FrontEnd.Models;
using FrontEnd.Services;

namespace FrontEnd.ViewModels;

public class LoginViewModel : ViewModelBase
{
    private string _username = "";
    public string Username
    {
        get => _username;
        set
        {
            _username = value;
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

    private readonly AuthService _authService = new();

    public LoginViewModel(MainWindowViewModel mainWindow)
    {
        _mainWindow = mainWindow;
        LoginCommand = new RelayCommand(ExecuteLogin, CanLogin);
    }

    private bool CanLogin(object? parameter)
    {
        return !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);
    }

    private async void ExecuteLogin(object? parameter)
    {
        Message = "Connexion en cours...";

        var (success, message, user) = await _authService.LoginAndGetUserInfo(Username, Password);

        if (success && user != null)
        {
            Message = $"Bienvenue Queen {user.Username} 👑";
            await Task.Delay(2500);
            _mainWindow.CurrentView = new HomeViewModel(user);
        }
        else
        {
            Message = message;
        }
    }
}
