using Avalonia.Controls;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;
using VL.Model;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Wrapper for <see cref="ButtonSpinner"/>
    /// </summary>
    [ProcessNode(Name = "ButtonSpinner")]
    public partial class ButtonSpinnerNode<T> : SpinnerNodeBase<T>
        where T : ButtonSpinner, new()
    {
        [Fragment]
        public ButtonSpinnerNode([Pin(Visibility = PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        /// <summary>Sets a value indicating whether the <see cref="ButtonSpinner"/> should allow to spin.</summary>
        [ImplementProperty(
            typeof(ButtonSpinner),
            nameof(ButtonSpinner.AllowSpinProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<bool> _allowSpin;

        /// <summary>Sets a value indicating whether the spin buttons should be shown.</summary>
        [ImplementProperty(
            typeof(ButtonSpinner),
            nameof(ButtonSpinner.ShowButtonSpinnerProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<bool> _showButtonSpinner;

        /// <summary>Sets a current location of the <see cref="ButtonSpinner"/>.</summary>
        [ImplementProperty(
            typeof(ButtonSpinner),
            nameof(ButtonSpinner.ButtonSpinnerLocationProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<Location> _buttonSpinnerLocation;
    }
}
