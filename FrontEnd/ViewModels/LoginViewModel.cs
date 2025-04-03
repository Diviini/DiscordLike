// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using System.Windows.Input;
// using FrontEnd.Helpers;
// using FrontEnd.Models;
// using FrontEnd.Services;



// namespace FrontEnd.ViewModels;

// public class LoginViewModel : ViewModelBase
// {
//     private string _email = "";
//     public string Email
//     {
//         get => _email;
//         set
//         {
//             _email = value;
//             OnPropertyChanged();
//             (LoginCommand as RelayCommand)?.RaiseCanExecuteChanged();
//         }
//     }

//     private string _password = "";
//     public string Password
//     {
//         get => _password;
//         set
//         {
//             _password = value;
//             OnPropertyChanged();
//             (LoginCommand as RelayCommand)?.RaiseCanExecuteChanged();
//         }
//     }

//     private string _message = "";
//     public string Message
//     {
//         get => _message;
//         set
//         {
//             _message = value;
//             OnPropertyChanged();
//         }
//     }

//     private readonly MainWindowViewModel _mainWindow;
//     public ICommand LoginCommand { get; }

//     private readonly List<UserLogin> fakeUsers = new()
//     {
//         new UserLogin { Email = "ines@mail.com", Password = "123456" },
//         new UserLogin { Email = "test@mail.com", Password = "abcdef" }
//     };

//     public LoginViewModel(MainWindowViewModel mainWindow)
//     {
//         _mainWindow = mainWindow;
//         LoginCommand = new RelayCommand(ExecuteLogin, CanLogin);
//     }

//     private bool CanLogin(object? parameter)
//     {
//         return !string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Password);
//     }

//     private async void ExecuteLogin(object? parameter)
//     {
//         var isMatch = fakeUsers.Any(u => u.Email == Email && u.Password == Password);

//         if (isMatch)
//         {
//             Message = $"Bienvenue Queen {Email} 👑 (fake login réussi)";
//             await Task.Delay(3000);
//             _mainWindow.CurrentView = new HomeViewModel();
//         }
//         else
//         {
//             Message = "Identifiants incorrects ❌";
//         }
//     }
// }


// using System;
// using System.Text;
// using System.Threading.Tasks;
// using System.Windows.Input;
// using FrontEnd.Helpers;
// using FrontEnd.Models;
// using FrontEnd.Services;

// namespace FrontEnd.ViewModels;

// public class LoginViewModel : ViewModelBase
// {
//     private string _username= "";
//     public string Username
//     {
//         get => _username;
//         set
//         {
//             _username= value;
//             OnPropertyChanged();
//             (LoginCommand as RelayCommand)?.RaiseCanExecuteChanged();
//         }
//     }

//     private string _password = "";
//     public string Password
//     {
//         get => _password;
//         set
//         {
//             _password = value;
//             OnPropertyChanged();
//             (LoginCommand as RelayCommand)?.RaiseCanExecuteChanged();
//         }
//     }

//     private string _message = "";
//     public string Message
//     {
//         get => _message;
//         set
//         {
//             _message = value;
//             OnPropertyChanged();
//         }
//     }

//     private readonly MainWindowViewModel _mainWindow;
//     public ICommand LoginCommand { get; }

//     private readonly AuthService _authService = new();

//     public LoginViewModel(MainWindowViewModel mainWindow)
//     {
//         _mainWindow = mainWindow;
//         LoginCommand = new RelayCommand(ExecuteLogin, CanLogin);
//     }

//     private bool CanLogin(object? parameter)
//     {
//         return !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);
//     }

//     private async void ExecuteLogin(object? parameter)
//     {
//         Message = "Connexion en cours...";

//         var (success, message, user) = await _authService.LoginAndGetUserInfo(Username, Password);

//         if (success && user != null)
//         {
//             Message = $"Bienvenue Queen {user.Username} 👑";
//             await Task.Delay(2500);
//             _mainWindow.CurrentView = new HomeViewModel(user);
//         }
//         else
//         {
//             Message = message;
//         }
//     }
// }

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
            _mainWindow.CurrentView = new HomeViewModel(user);
        }
    }
}

