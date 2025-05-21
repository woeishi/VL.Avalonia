using Avalonia.Controls;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Collections;
using static VL.Avalonia.Styles;

namespace VL.Avalonia.Controls;

// reference: https://docs.avaloniaui.net/docs/reference/controls/dockpanel

[ProcessNode(Name = "DockPanel (Spectral)")]
public partial class DockPanelSpectralWrapper
{
    [ImplementOutput]
    protected readonly DockPanel _output = new DockPanel();

    [ImplementStyle]
    protected Optional<IAvaloniaStyle> _style;

    [ImplementChildren]
    protected Spread<Control>? _children;
}

[ProcessNode(Name = "DockPanel")]
public partial class DockPanelWrapper : DockPanelSpectralWrapper
{
    [ImplementChildren(IsPinGroup = true)]
    protected Spread<Control>? _children;
}

[ProcessNode(Name = "DockProperty")]
public class DockProperty
{
    private Optional<Control> _input;
    public void SetInput(Optional<Control> input)
    {
        if (_input != input)
        {
            _input = input;

            UpdateSetters();
        }
    }
    public Control? Output => _input.Value;

    private Optional<Dock> _dock;
    public void SetDock(Optional<Dock> dock)
    {
        if (_dock != dock)
        {
            _dock = dock;

            UpdateSetters();
        }
    }

    private void UpdateSetters()
    {
        if (_input.HasValue)
        {
            _input.Value.SetValue(DockPanel.DockProperty, _dock.Value);
        }
    }
}