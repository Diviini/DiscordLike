using System;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using FrontEnd.ViewModels;

namespace FrontEnd
{
    public class ViewLocator : IDataTemplate
    {
        public Control? Build(object? data)
        {
            if (data == null)
                return null;

            // Obtenir le nom complet du ViewModel, par exemple "FrontEnd.ViewModels.LoginViewModel"
            var viewModelType = data.GetType();
            // Remplacer "ViewModels" par "Views" et "ViewModel" par "View"
            var viewName = viewModelType.FullName!
                .Replace("ViewModels", "Views")
                .Replace("ViewModel", "View", StringComparison.Ordinal);

            // Chercher le type dans tous les assemblages chargés
            Type? viewType = AppDomain.CurrentDomain.GetAssemblies()
                .Select(asm => asm.GetType(viewName))
                .FirstOrDefault(t => t != null);

            if (viewType != null)
            {
                if (Activator.CreateInstance(viewType) is Control view)
                {
                    view.DataContext = data;
                    return view;
                }
            }

            return new TextBlock { Text = $"View not found for {viewName}" };
        }

        public bool Match(object? data)
        {
            return data is ViewModelBase;
        }
    }
}
