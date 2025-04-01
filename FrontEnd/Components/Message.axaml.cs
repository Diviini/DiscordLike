using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace FrontEnd.Components;

public partial class Message : UserControl
{
  public Message()
  {
    InitializeComponent();
    DataContext = this;
  }
  public new string MessageText
  {
    get => GetValue(MessageTextProperty);
    set => SetValue(MessageTextProperty, value);
  }

  public static readonly StyledProperty<string> MessageTextProperty =
      AvaloniaProperty.Register<Message, string>(nameof(MessageText));
}

