using Avalonia.Controls.Primitives;
using VL.Avalonia.Attributes;
using VL.Avalonia.Data;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Reactive;
using VL.Model;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Base wrapper for <see cref="SelectingItemsControl">SelectingItemsControl</see>
    /// </summary>
    [ProcessNode]
    public abstract partial class SelectingItemsControlNodeBase<TControl, TValue>
        : ItemsControlNodeBase<TControl, TValue>,
            IDisposable
        where TControl : SelectingItemsControl, new()
    {
        private readonly TwoWayBinding<TValue?, object?> _selectedItemBinding;

        [Fragment]
        public SelectingItemsControlNodeBase([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext)
        {
            _selectedItemBinding = new TwoWayBinding<TValue?, object?>(
                _output,
                SelectingItemsControl.SelectedItemProperty,
                x => (object?)x,
                y => (TValue?)y
            );
        }

        /// <param name="selectedItemChannel">Binds <see cref="SelectingItemsControl.SelectedItem"/> property.</param>
        [Fragment(Order = PinOrder.Action)]
        public void SetSelectedItemChannel(
            [Pin(Visibility = PinVisibility.Optional)] IChannel<TValue?> selectedItemChannel
        ) => _selectedItemBinding.Bind(selectedItemChannel);

        /// <summary>Sets a value indicating whether to automatically scroll to newly selected items.</summary>
        [ImplementProperty(
            typeof(SelectingItemsControl),
            nameof(SelectingItemsControl.AutoScrollToSelectedItemProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<bool> _autoScrollToSelectedItem;

        /// <summary> Sets a value which indicates whether to wrap around when the first or last item is reached.</summary>
        [ImplementProperty(
            typeof(SelectingItemsControl),
            nameof(SelectingItemsControl.WrapSelectionProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<bool> _wrapSelection;

        /// <summary>Sets a value that specifies whether a user can jump to a value by typing.</summary>
        [ImplementProperty(
            typeof(SelectingItemsControl),
            nameof(SelectingItemsControl.IsTextSearchEnabledProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<bool> _isTextSearchEnabled;

        public override void Dispose()
        {
            _selectedItemBinding.Dispose();
            base.Dispose();
        }
    }
}
