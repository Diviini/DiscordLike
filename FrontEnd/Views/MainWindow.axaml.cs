using Avalonia.Controls;
using FrontEnd.ViewModels;

namespace FrontEnd
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }
    }
}
