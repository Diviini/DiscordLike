using System.ComponentModel;
using FrontEnd.Views;

namespace FrontEnd.ViewModels;

public class MainWindowViewModel : INotifyPropertyChanged
{
    private object _currentView;

    public event PropertyChangedEventHandler? PropertyChanged;

    public object CurrentView
    {
        get => _currentView;
        private set
        {
            _currentView = value;
            OnPropertyChanged(nameof(CurrentView));
        }
    }

    public MainWindowViewModel()
    {
        UpdateView();
    }

    private void UpdateView()
    {
        CurrentView = new HomeView();
        // CurrentView = new LoginView();
    }

    protected void OnPropertyChanged(string propertyName)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
