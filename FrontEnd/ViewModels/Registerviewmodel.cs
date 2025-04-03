// using System.Windows.Input;
// using FrontEnd.Helpers;
// using FrontEnd.Models;
// using FrontEnd.Services;
// using System.Threading.Tasks;


// namespace FrontEnd.ViewModels;

// public class RegisterViewModel : ViewModelBase
// {
//     private readonly MainWindowViewModel _mainWindow;
//     private readonly AuthService _authService = new();

//     public string Username { get; set; } = "";
//     public string Email { get; set; } = "";
//     public string Password { get; set; } = "";
//     public string Message { get; set; } = "";

//     public ICommand RegisterCommand { get; }
//     public ICommand NavigateToLoginCommand { get; }

//     public RegisterViewModel(MainWindowViewModel mainWindow)
//     {
//         _mainWindow = mainWindow;
//         RegisterCommand = new RelayCommand(async _ => await RegisterAsync());
//         NavigateToLoginCommand = new RelayCommand(_ => _mainWindow.CurrentView = new LoginViewModel(_mainWindow));
//     }

//     private async Task RegisterAsync()
//     {
//         var request = new RegisterRequest
//         {
//             Username = Username,
//             Email = Email,
//             Password = Password
//         };

//         var (success, msg) = await _authService.RegisterAsync(request);
//         Message = msg;

//         if (success)
//         {
//             await Task.Delay(2000);
//             _mainWindow.CurrentView = new LoginViewModel(_mainWindow);
//         }
//     }
// }

// using System;
// using System.Windows.Input;
// using System.Threading.Tasks;
// using FrontEnd.Helpers;
// using FrontEnd.Models;
// using FrontEnd.Services;

// namespace FrontEnd.ViewModels;

// public class RegisterViewModel : ViewModelBase
// {
//     private readonly MainWindowViewModel _mainWindow;
//     private readonly AuthService _authService = new();

//     public string Username { get; set; } = "";
//     public string Email { get; set; } = "";
//     public string Password { get; set; } = "";
//     public string Message { get; set; } = "";

//     public ICommand RegisterCommand { get; }
//     public ICommand NavigateToLoginCommand { get; }

//     public RegisterViewModel(MainWindowViewModel mainWindow)
//     {
//         _mainWindow = mainWindow;

//         RegisterCommand = new RelayCommand(async _ => await RegisterAsync());
//         NavigateToLoginCommand = new RelayCommand(_ =>
//         {
//             Console.WriteLine("🔙 Retour vers la LoginView");
//             _mainWindow.CurrentView = new LoginViewModel(_mainWindow);
//         });
//     }

//     private async Task RegisterAsync()
//     {
//         if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
//         {
//             Message = "Veuillez remplir tous les champs ❌";
//             return;
//         }

//         var request = new RegisterRequest
//         {
//             Username = Username,
//             Email = Email,
//             Password = Password
//         };

//         var (success, msg) = await _authService.RegisterAsync(request);
//         Message = msg;

//         if (success)
//         {
//             Console.WriteLine("✅ Inscription réussie");
//             await Task.Delay(2000);
//             _mainWindow.CurrentView = new LoginViewModel(_mainWindow);
//         }
//     }
// }

// using System;
// using System.Windows.Input;
// using System.Threading.Tasks;
// using FrontEnd.Helpers;
// using FrontEnd.Models;
// using FrontEnd.Services;

// namespace FrontEnd.ViewModels;

// public class RegisterViewModel : ViewModelBase
// {
//     private readonly MainWindowViewModel _mainWindow;
//     private readonly AuthService _authService = new();

//     public string Username { get; set; } = "";
//     public string Email { get; set; } = "";
//     public string Password { get; set; } = "";
//     public string Message { get; set; } = "";

//     public ICommand RegisterCommand { get; }
//     public ICommand NavigateToLoginCommand { get; }

//     public RegisterViewModel(MainWindowViewModel mainWindow)
//     {
//         _mainWindow = mainWindow;

//         RegisterCommand = new RelayCommand(async _ => await RegisterAsync());
//         NavigateToLoginCommand = new RelayCommand(_ =>
//         {
//             Console.WriteLine("🔙 Retour vers LoginView");
//             _mainWindow.CurrentView = new LoginViewModel(_mainWindow);
//         });
//     }

//     private async Task RegisterAsync()
//     {
//         Console.WriteLine("🟢 RegisterCommand déclenché");

//         if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
//         {
//             Message = "❌ Remplis tous les champs Queen !";
//             return;
//         }

//         var request = new RegisterRequest
//         {
//             Username = Username,
//             Email = Email,
//             Password = Password
//         };

//         var (success, msg) = await _authService.RegisterAsync(request);
//         Console.WriteLine($"🔁 Réponse du serveur → {success}, {msg}");

//         Message = msg;

//         if (success)
// {
//     Message = "✅ Compte créé avec succès ma Queen !";

// }
// else
// {
//     Message = msg; // ❌ email déjà utilisé, etc.
// }

//     }
// }

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