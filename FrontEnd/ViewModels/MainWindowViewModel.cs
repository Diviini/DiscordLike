using System;
using System.ComponentModel;

using FrontEnd.Views;

namespace FrontEnd.ViewModels;

public class MainWindowViewModel : INotifyPropertyChanged
{
    private object _currentView = new HomeView();
    private string _test;

    public event PropertyChangedEventHandler? PropertyChanged;

    public string Test
    {
        get => _test;
        set
        {
            _test = value;
            OnPropertyChanged(nameof(Test));
            Console.WriteLine($"title: {_test}");
        }
    }
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
