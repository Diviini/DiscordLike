using System;
using System.ComponentModel;

using FrontEnd.Views;

namespace FrontEnd.ViewModels;

public class MainWindowViewModel : INotifyPropertyChanged
{
	private object _currentView;
	private string _test;
	private HomeViewModel _homeViewModel;

	public event PropertyChangedEventHandler? PropertyChanged;

	public string Test
	{
		get => _test;
		set
		{
			_test = value;
			OnPropertyChanged(nameof(Test));
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

	public void SetHomeViewModel(HomeViewModel homeViewModel)
	{
		_homeViewModel = homeViewModel;
		UpdateView();
	}

	private void UpdateView()
	{
		var homeView = new HomeView();
		if (_homeViewModel != null)
		{
			homeView.DataContext = _homeViewModel;
		}
		CurrentView = homeView;
	}

	protected void OnPropertyChanged(string propertyName)
			=> PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
