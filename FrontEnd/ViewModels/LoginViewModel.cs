using System;
using System.Windows.Input;
using System.Threading.Tasks;
using FrontEnd.Helpers;
using FrontEnd.Models;
using FrontEnd.Services;

namespace FrontEnd.ViewModels;

public class LoginViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _mainWindow;
    private readonly AuthService _authService = new();
    //

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

    public ICommand LoginCommand { get; }
    public ICommand NavigateToRegisterCommand { get; }

    public LoginViewModel(MainWindowViewModel mainWindow)
    {
        _mainWindow = mainWindow;
        LoginCommand = new RelayCommand(async _ => await ExecuteLoginAsync(), CanLogin);
        NavigateToRegisterCommand = new RelayCommand(_ =>
        {
            Console.WriteLine("🔁 Navigation vers la RegisterView");
            _mainWindow.CurrentView = new RegisterViewModel(_mainWindow);
        });
    }

    private bool CanLogin(object? parameter)
    {
        return !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);
    }

    private async Task ExecuteLoginAsync()
    {
        var (success, msg, user) = await _authService.LoginAndGetUserInfo(Username, Password);
        Message = msg;

        if (success && user is not null)
        {
            await Task.Delay(1000);
            var chatService = new ChatService();
            _mainWindow.CurrentView = new HomeViewModel(user, chatService);
        }
    }
}

