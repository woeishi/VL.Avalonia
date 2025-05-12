using Avalonia.Controls;
using VL.Core.Import;
using VL.Lib.Collections;
using static VL.Avalonia.Styles;

namespace VL.Avalonia.Controls;

[ProcessNode(Name = "StackPanelPrototype")]
public partial class StackPanelWrapper : AbstractWrapperBase<StackPanel>
{
    private readonly StackPanel _output = new StackPanel();
    public StackPanelWrapper()
    {
    }

    private Spread<Control> _children;

    public override StackPanel Output => _output;

    public void SetChildren(Spread<Control> children)
    {
        if (_children != children)
        {
            _children = children;
            _output.Children.Clear();
            foreach (var child in _children)
            {
                if (child is Control control)
                {
                    _output.Children.Add(control);
                }
            }
        }
    }

    private IAvaloniaStyle? _style;
    public override IAvaloniaStyle? Style
    {
        set
        {
            if (!_style?.Equals(value) ?? _style != value)
            {
                // Need to test ResetStyle
                //_output.ApplyStyling();

                _style = value;
                _style?.ApplyStyle(Output);
            }
        }
    }
}
