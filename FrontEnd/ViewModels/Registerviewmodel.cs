using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FrontEnd.Models;
using FrontEnd.Services;
using FrontEnd.Helpers;
using System.Threading.Tasks;

namespace FrontEnd.ViewModels;

public class RegisterViewModel : ViewModelBase
{
    private string _username;
    private string _email;
    private string _password;
    private string _message = "";
    private readonly MainWindowViewModel _mainWindow;

    public string Username
    {
        get => _username;
        set { _username = value; OnPropertyChanged(); }
    }

    public string Email
    {
        get => _email;
        set { _email = value; OnPropertyChanged(); }
    }

    public string Password
    {
        get => _password;
        set { _password = value; OnPropertyChanged(); }
    }

    public string Message
    {
        get => _message;
        set { _message = value; OnPropertyChanged(); }
    }

    public ICommand RegisterCommand { get; }

    public RegisterViewModel(MainWindowViewModel mainWindow)
    {
        _mainWindow = mainWindow;
        RegisterCommand = new RelayCommand(async _ => await ExecuteRegister());
    }

    private async Task ExecuteRegister()
    {
        var service = new AuthService();
        var result = await service.RegisterAsync(new RegisterRequest
        {
            Username = Username,
            Email = Email,
            Password = Password
        });

        Message = result.message;

        if (result.success)
        {
            await Task.Delay(1500);
            _mainWindow.CurrentView = new LoginViewModel(_mainWindow); // Retour au login
        }
    }
}
