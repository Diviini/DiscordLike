using Avalonia.Controls;
using FrontEnd.ViewModels;

namespace FrontEnd.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
    }
}