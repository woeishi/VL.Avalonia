using Avalonia.Controls.Primitives;
using VL.Avalonia.Attributes;
using VL.Avalonia.Data;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Reactive;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Base wrapper for <see cref="ToggleButton"/>
    /// </summary>
    [ProcessNode]
    public abstract partial class ToggleButtonNodeBase<T> : ButtonNodeBase<T>, IDisposable
        where T : ToggleButton, new()
    {
        private readonly TwoWayBinding<bool, bool?> _isCheckedBinding;

        [Fragment]
        public ToggleButtonNodeBase([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext)
        {
            _isCheckedBinding = new TwoWayBinding<bool, bool?>(
                _output,
                ToggleButton.IsCheckedProperty,
                x => x,
                y => y ?? false
            );
        }

        /// <param name="isCheckedChannel">Binds <see cref="ToggleButton.IsChecked"/> property.</param>
        [Fragment(Order = PinOrder.Action)]
        public void SetIsCheckedChannel(IChannel<bool> isCheckedChannel) =>
            _isCheckedBinding.Bind(isCheckedChannel);

        /// <summary>Sets a value that indicates whether the control supports three states. </summary>
        [ImplementProperty(
            typeof(ToggleButton),
            nameof(ToggleButton.IsThreeStateProperty),
            Order = PinOrder.Style,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<bool> _isThreeState;

        public override void Dispose()
        {
            _isCheckedBinding.Dispose();
            base.Dispose();
        }
    }

    /// <summary>
    /// Wrapper for <see cref="ToggleButton"/>
    /// </summary>
    [ProcessNode(Name = "ToggleButton")]
    public partial class ToggleButtonNode : ToggleButtonNodeBase<ToggleButton>
    {
        [Fragment]
        public ToggleButtonNode([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }
    }
}
