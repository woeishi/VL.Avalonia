using Avalonia.Controls;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Reactive;
using static VL.Avalonia.Styles;

namespace VL.Avalonia.Controls;

[ProcessNode(Name = "SplitView")]
public partial class SplitViewWrapper
{
    [ImplementOutput]
    protected readonly SplitView _output = new SplitView();

    [ImplementStyle]
    protected Optional<IAvaloniaStyle> _style;

    [ImplementContent]
    private Optional<object?> _content;

    private Optional<object?> _pane;
    /// <summary>
    /// Sets the content of the pane in the SplitView.
    /// </summary>
    /// <param name="pane">An optional object representing the new content for the pane. Can be <see langword="null"/> to clear the pane.</param>
    public void SetPane(Optional<object?> pane)
    {
        if (_pane != pane)
        {
            _pane = pane;
            _output.SetValue(SplitView.PaneProperty, pane.Value);
        }
    }

    private Optional<SplitViewPanePlacement> _panePlacement;
    public void SetPanePlacement(Optional<SplitViewPanePlacement> panePlacement)
    {
        if (_panePlacement != panePlacement)
        {
            _panePlacement = panePlacement;
            _output.SetValue(SplitView.PanePlacementProperty, panePlacement.Value);
        }
    }


    private IChannel<bool>? _isPaneOpen;
    public void SetIsPaneOpen(IChannel<bool>? isPaneOpen)
    {
        if (_isPaneOpen != isPaneOpen)
        {
            _isPaneOpen = isPaneOpen;

            if (_isPaneOpen != null)
            {
                _output.Bind(SplitView.IsPaneOpenProperty, _isPaneOpen);
            }
            else
            {
                _output.ClearValue(SplitView.IsPaneOpenProperty);
            }
        }
    }

    private Optional<SplitViewDisplayMode> _displayMode;
    public void SetDisplayMode(Optional<SplitViewDisplayMode> displayMode)
    {
        if (_displayMode != displayMode)
        {
            _displayMode = displayMode;
            _output.SetValue(SplitView.DisplayModeProperty, displayMode.Value);
        }
    }

    private Optional<float> _openPaneLength;
    public void SetOpenPaneLength(Optional<float> openPaneLength)
    {
        if (_openPaneLength != openPaneLength)
        {
            _openPaneLength = openPaneLength;
            _output.SetValue(SplitView.OpenPaneLengthProperty, openPaneLength.Value);
        }
    }

    private Optional<float> _compactPaneLength;
    public void SetCompactPanelLength(Optional<float> compactPanelLength)
    {
        if (_compactPaneLength != compactPanelLength)
        {
            _compactPaneLength = compactPanelLength;
            _output.SetValue(SplitView.CompactPaneLengthProperty, compactPanelLength.Value);
        }
    }
}

