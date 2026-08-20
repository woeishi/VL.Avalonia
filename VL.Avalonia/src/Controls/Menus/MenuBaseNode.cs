using Avalonia.Controls;
using VL.Avalonia.Data;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Reactive;
using VL.Model;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Base wrapper for <see cref="MenuBase"/>
    /// </summary>
    [ProcessNode]
    public abstract class MenuBaseNode<TControl, TValue>
        : SelectingItemsControlNodeBase<TControl, TValue>,
            IDisposable
        where TControl : MenuBase, new()
    {
        private TwoWayBinding<bool> _isOpenBinding;

        [Fragment]
        public MenuBaseNode([Pin(Visibility = PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext)
        {
            _isOpenBinding = new TwoWayBinding<bool>(_output, MenuBase.IsOpenProperty);
        }

        public void SetIsOpenChannel(
            [Pin(Visibility = PinVisibility.Optional)] IChannel<bool> isOpenChannel
        )
        {
            _isOpenBinding.Bind(isOpenChannel);
        }

        public override void Dispose()
        {
            _isOpenBinding.Dispose();
            base.Dispose();
        }
    }
}
