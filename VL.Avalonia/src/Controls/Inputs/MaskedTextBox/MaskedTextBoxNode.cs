using System.Globalization;
using Avalonia.Controls;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;
using VL.Model;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Base wrapper for <see cref="MaskedTextBox"/>
    /// </summary>
    [ProcessNode]
    public abstract partial class MaskedTextBoxNodeBase<T> : TextBoxNodeBase<T>
        where T : MaskedTextBox, new()
    {
        [Fragment]
        public MaskedTextBoxNodeBase([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        /// <summary>Sets the input mask pattern that defines allowed input format (e.g., "000-000-0000" for phone numbers).</summary>
        [ImplementProperty(
            typeof(MaskedTextBox),
            nameof(MaskedTextBox.MaskProperty),
            Order = PinOrder.Style
        )]
        private Optional<string> _mask;

        /// <summary>Sets the character used to represent the absence of user input (default: '_').</summary>
        [ImplementProperty(
            typeof(MaskedTextBox),
            nameof(MaskedTextBox.PromptCharProperty),
            Order = PinOrder.Style
        )]
        private Optional<char> _promptChar;

        /// <summary>Sets whether prompt characters are hidden when the control loses focus.</summary>
        [ImplementProperty(
            typeof(MaskedTextBox),
            nameof(MaskedTextBox.HidePromptOnLeaveProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<bool> _hidePromptOnLeave;

        /// <summary>Sets whether selected characters should be reset when the prompt character is pressed.</summary>
        [ImplementProperty(
            typeof(MaskedTextBox),
            nameof(MaskedTextBox.ResetOnPromptProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<bool> _resetOnPrompt;

        /// <summary>Sets whether selected characters should be reset when the space character is pressed.</summary>
        [ImplementProperty(
            typeof(MaskedTextBox),
            nameof(MaskedTextBox.ResetOnSpaceProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<bool> _resetOnSpace;

        /// <summary>Sets whether the masked text box is restricted to accept only ASCII characters.</summary>
        [ImplementProperty(
            typeof(MaskedTextBox),
            nameof(MaskedTextBox.AsciiOnlyProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<bool> _asciiOnly;

        /// <summary>Sets the culture information used for character validation and formatting.</summary>
        [ImplementProperty(
            typeof(MaskedTextBox),
            nameof(MaskedTextBox.CultureProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<CultureInfo> _culture;
    }

    /// <summary>
    /// The <see href="https://docs.avaloniaui.net/docs/reference/controls/maskedtextbox">MaskedTextBox</see> presents an area for typed (keyboard) input, but where the format and characters permitted can be constrained by a mask pattern formed from special characters.
    /// </summary>
    [ProcessNode(Name = "MaskedTextBox")]
    public class MaskedTextBoxNode : MaskedTextBoxNodeBase<MaskedTextBox>
    {
        [Fragment]
        public MaskedTextBoxNode([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }
    }
}
