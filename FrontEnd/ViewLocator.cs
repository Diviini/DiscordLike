// using System;
// using Avalonia.Controls;
// using Avalonia.Controls.Templates;
// using FrontEnd.ViewModels;

// namespace FrontEnd;

// public class ViewLocator : IDataTemplate
// {

//     public Control? Build(object? param)
//     {
//         if (param is null)
//             return null;
        
//         var name = param.GetType().FullName!.Replace("ViewModels","Views")
//         .Replace("ViewModel", "View", StringComparison.Ordinal);
//         var type = Type.GetType(name);

//         if (type != null)
//         {
//             return (Control)Activator.CreateInstance(type)!;
//         }
        
//         return new TextBlock { Text = "Not Found: " + name };
//     }

//     public bool Match(object? data)
//     {
//         return data is ViewModelBase;
//     }
// }
// using Avalonia.Controls;
// using Avalonia.Controls.Templates;
// using FrontEnd.ViewModels;
// using System.Linq;
// using System;

// namespace FrontEnd;

// public class ViewLocator : IDataTemplate
// {
//     public Control? Build(object? data)
//     {
//         if (data is null)
//             return null;

//         // Récupère le nom complet du ViewModel, par exemple "FrontEnd.ViewModels.LoginViewModel"
//         var vmFullName = data.GetType().FullName;
//         // Remplace "ViewModels" par "Views", puis "ViewModel" par "View"
//         var viewName = vmFullName!.Replace("ViewModels", "Views")
//                                    .Replace("ViewModel", "View", StringComparison.Ordinal);

//         // Essaye de récupérer le type de la vue
//         var type = Type.GetType(viewName);
//         if (type != null && Activator.CreateInstance(type) is Control control)
//         {
//             control.DataContext = data;
//             return control;
//         }
//         // Si la vue n'est pas trouvée, retourne un TextBlock d'erreur
//         return new TextBlock { Text = "Not Found: " + viewName, Foreground = Avalonia.Media.Brushes.Red };
//     }

//     public bool Match(object? data)
//     {
//         return data is ViewModelBase;
//     }
// }
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
