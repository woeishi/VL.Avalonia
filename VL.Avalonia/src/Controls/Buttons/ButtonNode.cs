using System.Reactive;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using VL.Avalonia.Attributes;
using VL.Avalonia.Data;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Reactive;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Base wrapper for <see cref="Button"/>
    /// </summary>
    [ProcessNode]
    public abstract partial class ButtonNodeBase<T> : ContentControlNodeBase<T>, IDisposable
        where T : Button, new()
    {
        private readonly UnitCommandBinding _commandBinding;

        [Fragment]
        public ButtonNodeBase([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext)
        {
            _commandBinding = new UnitCommandBinding(_output, Button.CommandProperty);
        }

        /// <param name="commandChannel">Binds button <see cref="ICommand"/> to <see cref="IChannel{T}"/> <see cref="Unit"/></param>
        [Fragment(Order = PinOrder.Action)]
        public void SetCommandChannel(IChannel<Unit> commandChannel) =>
            _commandBinding.Bind(commandChannel);

        // TODO:
        ///// <summary>Sets a parameter to be passed to the <see cref="Button.Command"/>.</summary>
        //[ImplementProperty(
        //    typeof(Button),
        //    nameof(Button.CommandParameterProperty),
        //    Order = PinOrder.Action,
        //    PinVisibility = Model.PinVisibility.Optional
        //)]
        //private Optional<object> _commandParameter;

        /// <summary>Sets an <see cref="KeyGesture"/> associated with this control.</summary>
        [ImplementProperty(
            typeof(Button),
            nameof(Button.HotKeyProperty),
            Order = PinOrder.Action,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<KeyGesture> _hotKey;

        /// <summary>Sets a value indicating how the <see cref="Button"/> should react to clicks.</summary>
        [ImplementProperty(
            typeof(Button),
            nameof(Button.ClickModeProperty),
            Order = PinOrder.Style,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<ClickMode> _clickMode;

        /// <summary>Sets a value indicating whether the button is the default button for the window.</summary>
        [ImplementProperty(
            typeof(Button),
            nameof(Button.IsDefaultProperty),
            Order = PinOrder.Action,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<bool> _isDefault;

        /// <summary>Sets sets a value indicating whether the button is the Cancel button for the window.</summary>
        [ImplementProperty(
            typeof(Button),
            nameof(Button.IsCancelProperty),
            Order = PinOrder.Action,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<bool> _isCancel;

        /// <summary>Sets the Flyout that should be shown with this button.</summary>
        [ImplementProperty(
            typeof(Button),
            nameof(Button.FlyoutProperty),
            Order = PinOrder.DataTemplate,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<FlyoutBase> _flyout;

        public override void Dispose()
        {
            _commandBinding.Dispose();
            base.Dispose();
        }
    }

    /// <summary>
    /// Wrapper for <see cref="Button"/>
    /// </summary>
    [ProcessNode(Name = "Button")]
    public class ButtonNode : ButtonNodeBase<Button>
    {
        [Fragment]
        public ButtonNode([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }
    }
}
