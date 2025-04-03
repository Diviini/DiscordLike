using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace FrontEnd.Converters;

public class MessageToColorConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var msg = value as string;

        if (string.IsNullOrWhiteSpace(msg))
            return Brushes.Transparent;

        return msg.StartsWith("✅") ? Brushes.LightGreen :
               msg.StartsWith("❌") ? Brushes.OrangeRed :
               Brushes.White;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
