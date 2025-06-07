using Avalonia.Controls;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Collections;
using static VL.Avalonia.Styles;

namespace VL.Avalonia.Controls;

[ProcessNode(Name = "RelativePanel (Spectral)")]
public partial class RelativePanelWrapperSpectral
{
    [ImplementOutput]
    protected readonly RelativePanel _output = new RelativePanel();

    [ImplementStyle]
    protected Optional<IAvaloniaStyle> _style;

    [ImplementClasses]
    protected Optional<string> _classes;

    [ImplementChildren]
    protected Spread<Control?> _children;

    [ImplementProperty("RelativePanel.NameProperty", PinVisibility = Model.PinVisibility.Hidden)]
    protected Optional<string> _name;
}

[ProcessNode(Name = "RelativePanel")]
public partial class RelativePanelWrapper : RelativePanelWrapperSpectral
{
    [ImplementChildren(IsPinGroup = true)]
    protected Spread<Control?> _children;
}

[ProcessNode(Name = "RelativePanelProperty")]
public partial class RelativePanelProperty
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

    private Optional<bool> _alignTopWithPanel;
    /// <summary>
    /// Boolean. Align the top edge of the child control with the top edge of the panel.
    /// </summary>
    /// <param name="alignTopWithPanel"></param>
    public void SetAlignTopWithPanel(Optional<bool> alignTopWithPanel)
    {
        if (_alignTopWithPanel != alignTopWithPanel)
        {
            _alignTopWithPanel = alignTopWithPanel;

            UpdateSetters();

        }
    }

    private Optional<bool> _alignBottomWithPanel;
    /// <summary>
    /// Boolean. Attached to a child control to align the bottom edge of the child control with the bottom edge of the panel.
    /// </summary>
    /// <param name="alignBottomWithPanel"></param>
    public void SetAlignBottomWithPanel(Optional<bool> alignBottomWithPanel)
    {
        if (_alignBottomWithPanel != alignBottomWithPanel)
        {
            _alignBottomWithPanel = alignBottomWithPanel;

            UpdateSetters();
        }
    }

    private Optional<bool> _alignLeftWithPanel;
    /// <summary>
    /// Boolean. Attached to a child control to align the left edge of the child control with the left edge of the panel.
    /// </summary>
    /// <param name="alignLeftWithPanel"></param>
    public void SetAlignLeftWithPanel(Optional<bool> alignLeftWithPanel)
    {
        if (_alignLeftWithPanel != alignLeftWithPanel)
        {
            _alignLeftWithPanel = alignLeftWithPanel;

            UpdateSetters();
        }
    }

    private Optional<bool> _alignRightWithPanel;
    /// <summary>
    /// Boolean. Attached to a child control to align the right edge of the child control with the right edge of the panel.
    /// </summary>
    public void SetAlignRightWithPanel(Optional<bool> alignRightWithPanel)
    {
        if (_alignRightWithPanel != alignRightWithPanel)
        {
            _alignRightWithPanel = alignRightWithPanel;

            UpdateSetters();
        }
    }

    private Optional<bool> _alignHorizontalCenterWithPanel;
    /// <summary>
    /// Boolean. Attached to a child control to align the horizontal center of the child control with the horizontal center of the panel.
    /// </summary>
    public void SetAlignHorizontalCenterWithPanel(Optional<bool> alignHorizontalCenterWithPanel)
    {
        if (_alignHorizontalCenterWithPanel != alignHorizontalCenterWithPanel)
        {
            _alignHorizontalCenterWithPanel = alignHorizontalCenterWithPanel;

            UpdateSetters();
        }
    }

    private Optional<bool> _alignVerticalCenterWithPanel;
    /// <summary>
    /// Boolean. Attached to a child control to align the vertical center of the child control with the vertical center of the panel.
    /// </summary>
    public void SetAlignVerticalCenterWithPanel(Optional<bool> alignVerticalCenterWithPanel)
    {
        if (_alignVerticalCenterWithPanel != alignVerticalCenterWithPanel)
        {
            _alignVerticalCenterWithPanel = alignVerticalCenterWithPanel;

            UpdateSetters();
        }
    }

    // TODO: https://docs.avaloniaui.net/docs/reference/controls/relativepanel

    private void UpdateSetters()
    {
        if (_alignTopWithPanel.HasValue)
        {
            _input.Value.SetValue(RelativePanel.AlignTopWithPanelProperty, _alignTopWithPanel.Value);
        }
        else
        {
            _input.Value.ClearValue(RelativePanel.AlignTopWithPanelProperty);
        }

        if (_alignBottomWithPanel.HasValue)
        {
            _input.Value.SetValue(RelativePanel.AlignBottomWithPanelProperty, _alignBottomWithPanel.Value);
        }
        else
        {
            _input.Value.ClearValue(RelativePanel.AlignBottomWithPanelProperty);
        }

        if (_alignLeftWithPanel.HasValue)
        {
            _input.Value.SetValue(RelativePanel.AlignLeftWithPanelProperty, _alignLeftWithPanel.Value);
        }
        else
        {
            _input.Value.ClearValue(RelativePanel.AlignLeftWithPanelProperty);
        }

        if (_alignRightWithPanel.HasValue)
        {
            _input.Value.SetValue(RelativePanel.AlignRightWithPanelProperty, _alignRightWithPanel.Value);
        }
        else
        {
            _input.Value.ClearValue(RelativePanel.AlignRightWithPanelProperty);
        }

        if (_alignHorizontalCenterWithPanel.HasValue)
        {
            _input.Value.SetValue(RelativePanel.AlignHorizontalCenterWithPanelProperty, _alignHorizontalCenterWithPanel.Value);
        }
        else
        {
            _input.Value.ClearValue(RelativePanel.AlignHorizontalCenterWithPanelProperty);
        }

        if (_alignVerticalCenterWithPanel.HasValue)
        {
            _input.Value.SetValue(RelativePanel.AlignHorizontalCenterWithPanelProperty, _alignVerticalCenterWithPanel.Value);
        }
        else
        {
            _input.Value.ClearValue(RelativePanel.AlignHorizontalCenterWithPanelProperty);
        }
    }
}
