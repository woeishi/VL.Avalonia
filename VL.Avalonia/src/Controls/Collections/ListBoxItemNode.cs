using Avalonia.Controls;
using VL.Avalonia.Data;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Reactive;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Base wrapper for <see cref="ListBoxItem"/>
    /// </summary>
    [ProcessNode]
    public abstract class ListBoxItemNodeBase<T> : ContentControlNodeBase<T>
        where T : ListBoxItem, new()
    {
        private readonly TwoWayBinding<bool> _isSelectedBinding;

        [Fragment]
        public ListBoxItemNodeBase([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext)
        {
            _isSelectedBinding = new TwoWayBinding<bool>(_output, ListBoxItem.IsSelectedProperty);
        }

        /// <param name="isSelectedChannel">Binds <see cref="ListBoxItem.IsSelected"/> property.</param>
        [Fragment(Order = PinOrder.Action)]
        public void SetIsSelectedChannel(IChannel<bool> isSelectedChannel) =>
            _isSelectedBinding.Bind(isSelectedChannel);

        public override void Dispose()
        {
            _isSelectedBinding.Dispose();
            base.Dispose();
        }
    }

    /// <summary>
    /// Wrapper for <see cref="ListBoxItem"/>
    /// </summary>
    [ProcessNode(Name = "ListBoxItem")]
    public class ListBoxItemNode : ListBoxItemNodeBase<ListBoxItem>
    {
        [Fragment]
        public ListBoxItemNode([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }
    }
}
