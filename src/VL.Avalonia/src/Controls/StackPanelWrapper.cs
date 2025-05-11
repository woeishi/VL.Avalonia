using Avalonia.Controls;
using VL.Avalonia.Styles;
using VL.Core.Import;
using VL.Lib.Collections;

namespace VL.Avalonia.Controls;

[ProcessNode(Name = "StackPanelTest")]
public partial class StackPanelWrapper
{
    private StackPanel _stackPanel = new StackPanel();
    public StackPanelWrapper()
    {
    }

    private IAvaloniaStyle? _style;
    public IAvaloniaStyle? Style
    {
        set
        {
            if (!_style?.Equals(value) ?? _style != value)
            {
                _style = value;
                _style?.ApplyStyle(_stackPanel);
            }
        }
    }

    private Spread<object> _children;
    public void SetChildren(Spread<object> children)
    {
        if (_children != children)
        {
            _children = children;
            _stackPanel.Children.Clear();
            foreach (var child in _children)
            {
                if (child is Control control)
                {
                    _stackPanel.Children.Add(control);
                }
            }
        }
    }
    public StackPanel Output => _stackPanel;
}
