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
