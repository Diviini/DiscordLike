using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using FrontEnd.ViewModels;
using FrontEnd.Models;
using System.IO;
using System.Text.Json;
using System.Linq;
using System.Collections.Generic;
using System;

namespace FrontEnd
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                DisableAvaloniaDataAnnotationValidation();

                // Load JSON data
                string jsonData = File.ReadAllText("conv.json");
                var conversations = JsonSerializer.Deserialize<List<Conversation>>(jsonData);

                // Initialize the HomeViewModel with the loaded data
                var homeViewModel = new HomeViewModel();
                homeViewModel.LoadData(conversations);

                // Load JSON data from test.json
                string testJsonData = File.ReadAllText("test.json");
                var testData = JsonSerializer.Deserialize<TestData>(testJsonData);
                homeViewModel.Test = testData?.Title;
                Console.WriteLine($"TestData loaded: {testData?.Title}");

                // Set the DataContext of MainWindow to MainWindowViewModel
                var mainWindowViewModel = new MainWindowViewModel();
                mainWindowViewModel.CurrentView = homeViewModel;

                desktop.MainWindow = new MainWindow
                {
                    DataContext = mainWindowViewModel
                };
            }

            base.OnFrameworkInitializationCompleted();
        }


        private void DisableAvaloniaDataAnnotationValidation()
        {
            // Get an array of plugins to remove
            var dataValidationPluginsToRemove =
                BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

            // Remove each entry found
            foreach (var plugin in dataValidationPluginsToRemove)
            {
                BindingPlugins.DataValidators.Remove(plugin);
            }
        }
    }
}
