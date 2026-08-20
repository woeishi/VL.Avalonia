using Avalonia.Controls;
using Avalonia.Media;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Controls;

/// <summary>
/// The <c>SelectableTextBlock</c> block is a label for the display of text that allows selecting and copying of text. It can display multiple lines, and features full control over the font used.
/// </summary>
[ProcessNode(Name = "SelectableTextBlock")]
public partial class SelectableTextBlockWrapper : TextBlockWrapperBase<SelectableTextBlock>
{
    [Fragment]
    public SelectableTextBlockWrapper([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

    #region Selection Properties (Core Functionality)
    /* TODO:
    /// <param name="selectionStart">
    /// Character index for the beginning of the current selection
    /// </param>
    [ImplementProperty("SelectableTextBlock.SelectionStartProperty")]
    protected Optional<int> _selectionStart;

    /// <param name="selectionEnd">
    /// Character index for the end of the current selection
    /// </param>
    [ImplementProperty("SelectableTextBlock.SelectionEndProperty")]
    protected Optional<int> _selectionEnd;
    */
    #endregion

    #region Selection Appearance Properties

    /// <param name="selectionBrush">
    /// Brush that highlights selected text background
    /// </param>
    [ImplementProperty(
        "SelectableTextBlock.SelectionBrushProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    protected Optional<IBrush> _selectionBrush;

    /// <param name="selectionForegroundBrush">
    /// Brush used for the foreground color of selected text
    /// </param>
    [ImplementProperty(
        "SelectableTextBlock.SelectionForegroundBrushProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    protected Optional<IBrush> _selectionForegroundBrush;

    #endregion
}
