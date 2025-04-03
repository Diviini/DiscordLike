using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.ComponentModel;

using FrontEnd.Views;

namespace FrontEnd.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
	private ViewModelBase _currentView;

	public ViewModelBase CurrentView
	{
		get => _currentView;
		set => SetProperty(ref _currentView, value);
	}

	public MainWindowViewModel()
	{
		CurrentView = new LoginViewModel(this); // Vue de démarrage
	}

	public void NavigateToHome()
	{
		CurrentView = new HomeViewModel(); // Navigation vers la HomeViewModel
	}
}

// public class MainWindowViewModel : ViewModelBase
// {
//     private object _currentView = new HomeView();
//     public event PropertyChangedEventHandler? PropertyChanged;

//     private string _test;

//     public string Test
//     {
//         get => _test;
//         set
//         {
//             _test = value;
//             OnPropertyChanged(nameof(Test));
//             Console.WriteLine($"title: {_test}");
//         }
//     }
//     public object CurrentView
//     {
//         get => _currentView;
//         private set
//         {
//             _currentView = value;
//             OnPropertyChanged(nameof(CurrentView));
//         }
//     }

//     public MainWindowViewModel()
//     {
//         UpdateView();
//     }

//     private void UpdateView()
//     {
//         CurrentView = new HomeView();
//         // CurrentView = new LoginView();
//     }

//     protected void OnPropertyChanged(string propertyName)
//         => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
// }
