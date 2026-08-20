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
    /// Base wrapper for <see cref="SplitButton"/>
    /// </summary>
    [ProcessNode]
    public abstract partial class SplitButtonNodeBase<T> : ContentControlNodeBase<T>, IDisposable
        where T : SplitButton, new()
    {
        private readonly UnitCommandBinding _commandBinding;

        [Fragment]
        public SplitButtonNodeBase([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext)
        {
            _commandBinding = new UnitCommandBinding(_output, SplitButton.CommandProperty);
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

        /// <summary>Sets  the <see cref="FlyoutBase"/> that is shown when the secondary part is pressed.</summary>
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
    /// Wrapper for <see cref="SplitButton"/>
    /// </summary>
    [ProcessNode(Name = "SplitButton")]
    public class SplitButtonNode : SplitButtonNodeBase<SplitButton>
    {
        [Fragment]
        public SplitButtonNode([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }
    }
}
