using Avalonia.Controls;
using Avalonia.Media;
using VL.Avalonia.Attributes;
using VL.Avalonia.Data;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Reactive;
using VL.Model;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Base wrapper for <see cref="TextBox"/>
    /// </summary>
    [ProcessNode]
    public abstract partial class TextBoxNodeBase<T> : ControlNodeBase<T>, IDisposable
        where T : TextBox, new()
    {
        private TwoWayBinding<string, string> _textBinding;

        [Fragment]
        public TextBoxNodeBase([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext)
            : base(nodeContext)
        {
            _textBinding = new TwoWayBinding<string, string>(_output, TextBox.TextProperty);
        }

        /// <param name="textChannel">
        /// The text content of the TextBox
        /// </param>
        [Fragment(Order = PinOrder.Action)]
        public void SetTextChannel(IChannel<string>? textChannel) => _textBinding.Bind(textChannel);

        /*
        // Selection and Navigation Properties
        // This should be handled via channel, so not going to include
        /// <param name="_caretIndex">
        /// Current position of the text cursor/caret
        /// </param>
        [ImplementProperty("TextBox.CaretIndexProperty")]
        private Optional<int> _caretIndex;

        /// <param name="_selectionStart">
        /// Starting position of selected text
        /// </param>
        [ImplementProperty("TextBox.SelectionStartProperty")]
        private Optional<int> _selectionStart;

        /// <param name="_selectionEnd">
        /// Ending position of selected text
        /// </param>
        [ImplementProperty("TextBox.SelectionEndProperty")]
        private Optional<int> _selectionEnd;
        */

        /// <summary>Sets the placeholder text displayed when TextBox is empty.</summary>
        [ImplementProperty(
            typeof(TextBox),
            nameof(TextBox.WatermarkProperty),
            Order = PinOrder.Style
        )]
        private Optional<string> _watermark;

        /// <summary>Sets whether watermark floats above text when typing.</summary>
        [ImplementProperty(
            typeof(TextBox),
            nameof(TextBox.UseFloatingWatermarkProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<bool> _useFloatingWatermark;

        /// <summary>Sets whether the TextBox is read-only (cannot be edited).</summary>
        [ImplementProperty(
            typeof(TextBox),
            nameof(TextBox.IsReadOnlyProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<bool> _isReadOnly;

        /// <summary>Sets whether the TextBox allows and displays newline characters.</summary>
        [ImplementProperty(
            typeof(TextBox),
            nameof(TextBox.AcceptsReturnProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<bool> _acceptsReturn;

        /// <summary>Sets whether the TextBox allows and displays tab characters.</summary>
        [ImplementProperty(
            typeof(TextBox),
            nameof(TextBox.AcceptsTabProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<bool> _acceptsTab;

        /// <summary>Sets the character used for password masking (e.g., '*').</summary>
        [ImplementProperty(
            typeof(TextBox),
            nameof(TextBox.PasswordCharProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<char> _passwordChar;

        /// <summary>Sets whether password text should be revealed temporarily.</summary>
        [ImplementProperty(
            typeof(TextBox),
            nameof(TextBox.RevealPasswordProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<bool> _revealPassword;

        /// <summary>Sets the maximum number of characters that can be entered.</summary>
        [ImplementProperty(
            typeof(TextBox),
            nameof(TextBox.MaxLengthProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<int> _maxLength;

        /// <summary>Sets how text wraps (NoWrap, Wrap, WrapWithOverflow).</summary>
        [ImplementProperty(
            typeof(TextBox),
            nameof(TextBox.TextWrappingProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<TextWrapping> _textWrapping;

        /// <summary>Sets the maximum number of visible lines for sizing.</summary>
        [ImplementProperty(
            typeof(TextBox),
            nameof(TextBox.MaxLinesProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<int> _maxLines;

        /// <summary>Sets the minimum number of visible lines for sizing.</summary>
        [ImplementProperty(
            typeof(TextBox),
            nameof(TextBox.MinLinesProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<int> _minLines;

        /// <summary>Sets the characters inserted when Enter is pressed.</summary>
        [ImplementProperty(
            typeof(TextBox),
            nameof(TextBox.NewLineProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<string> _newLine;

        /// <summary>Sets whether undo/redo functionality is enabled.</summary>
        [ImplementProperty(
            typeof(TextBox),
            nameof(TextBox.IsUndoEnabledProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<bool> _isUndoEnabled;

        /// <summary>Sets the maximum number of items in the undo stack.</summary>
        [ImplementProperty(
            typeof(TextBox),
            nameof(TextBox.UndoLimitProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<int> _undoLimit;

        /// <summary>Sets the brush used to highlight selected text background.</summary>
        [ImplementProperty(
            typeof(TextBox),
            nameof(TextBox.SelectionBrushProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<IBrush> _selectionBrush;

        /// <summary>Sets the brush used for selected text foreground color.</summary>
        [ImplementProperty(
            typeof(TextBox),
            nameof(TextBox.SelectionForegroundBrushProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<IBrush> _selectionForegroundBrush;

        /// <summary>Sets the brush used for the text caret/cursor.</summary>
        [ImplementProperty(
            typeof(TextBox),
            nameof(TextBox.CaretBrushProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<IBrush> _caretBrush;

        /// <summary>Sets how fast the caret blinks (default 500ms).</summary>
        [ImplementProperty(
            typeof(TextBox),
            nameof(TextBox.CaretBlinkIntervalProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<TimeSpan> _caretBlinkInterval;

        /// <summary>Sets custom content positioned on the left side of text.</summary>
        [ImplementProperty(
            typeof(TextBox),
            nameof(TextBox.InnerLeftContentProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<object> _innerLeftContent;

        /// <summary>Sets custom content positioned on the right side of text.</summary>
        [ImplementProperty(
            typeof(TextBox),
            nameof(TextBox.InnerRightContentProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<object> _innerRightContent;

        /// <summary>Sets whether selection is highlighted when not focused.</summary>
        [ImplementProperty(
            typeof(TextBox),
            nameof(TextBox.IsInactiveSelectionHighlightEnabledProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<bool> _isInactiveSelectionHighlightEnabled;

        /// <summary>Sets whether selection is cleared when focus is lost.</summary>
        [ImplementProperty(
            typeof(TextBox),
            nameof(TextBox.ClearSelectionOnLostFocusProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<bool> _clearSelectionOnLostFocus;

        public override void Dispose()
        {
            _textBinding?.Dispose();
            base.Dispose();
        }
    }

    /// <summary>
    /// The <b>TextBox</b> presents an area for typed (keyboard) input. It can be for a single or multiple lines of input.
    /// </summary>
    [ProcessNode(Name = "TextBox")]
    public class TextBoxNode : TextBoxNodeBase<TextBox>
    {
        [Fragment]
        public TextBoxNode([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }
    }
}
