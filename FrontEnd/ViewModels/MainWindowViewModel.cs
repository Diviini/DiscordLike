using FrontEnd.ViewModels;

namespace FrontEnd.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private ViewModelBase _currentView;
    public ViewModelBase CurrentView
    {
        get => _currentView;
        set
        {
            _currentView = value;
            OnPropertyChanged();
        }
    }

    public MainWindowViewModel()
    {
        CurrentView = new LoginViewModel(this); // ✅ start with login
    }
}

// public ViewModelBase CurrentView
// {
// 	get => _currentView;
// 	set => SetProperty(ref _currentView, value);
// }
