using AvaloniaControls = Avalonia.Controls;
using Avalonia.Themes.Fluent;

namespace VL.Avalonia.Controls;

[ProcessNode]
public class Window : AvaloniaControls.Window, IDisposable
{
    protected override Type StyleKeyOverride => typeof(AvaloniaControls.Window);

    public Window() : base()
    {
        this.Styles.Add(new FluentTheme());
        this.Show();
    }
    public void Dispose()
    {
        this.Close();
        this.Content = null;
    }

    public new object? Content
    {
        get => base.Content;
        set { if (value != base.Content) base.Content = value; }
    }
}
