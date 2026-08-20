using System.Reactive.Linq;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using VL.Avalonia.Attributes;
using VL.Avalonia.Data;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Collections;
using VL.Lib.Reactive;
using VL.Model;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Base wrapper for <see cref="AutoCompleteBox"/>
    /// </summary>
    [ProcessNode(FragmentSelection = FragmentSelection.Explicit)]
    public abstract partial class AutoCompleteBoxNodeBase<TControl, TValue>
        : ControlNodeBase<TControl>,
            IDisposable
        where TControl : AutoCompleteBox, new()
    {
        private TwoWayBinding<string, string> _textBinding;
        private TwoWayBinding<TValue?, object?> _selectedItemBinding;

        private IChannel<IReadOnlyList<TValue>>? _itemsSource;
        private IDisposable? _itemSourceBinding;
        private ISpread? _items;

        [Fragment]
        public AutoCompleteBoxNodeBase([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext)
            : base(nodeContext)
        {
            _textBinding = new TwoWayBinding<string, string>(_output, AutoCompleteBox.TextProperty);
            _selectedItemBinding = new TwoWayBinding<TValue?, object?>(
                _output,
                AutoCompleteBox.SelectedItemProperty,
                x => (object?)x,
                x => (TValue?)x
            );
        }

        /// <param name="textChannel">The text content of the TextBox</param>
        [Fragment(Order = PinOrder.Action)]
        public void SetTextChannel(
            [Pin(Visibility = PinVisibility.Visible)] IChannel<string>? textChannel
        ) => _textBinding.Bind(textChannel);

        /// <param name="items">The collection of items that provides the data for the dropdown suggestions</param>
        public virtual void SetItems(Spread<TValue?> items)
        {
            if (ReferenceEquals(_items, items))
                return;

            _itemSourceBinding?.Dispose();
            _items = items;
            _itemsSource = null;

            if (_items != null)
            {
                _output.SetValue(AutoCompleteBox.ItemsSourceProperty, items);
            }
            else
            {
                _output.ClearValue(AutoCompleteBox.ItemsSourceProperty);
            }
        }

        /// <param name="itemsSource">The collection of items that provides the data for the dropdown suggestions</param>
        public virtual void SetItemsSource(IChannel<IReadOnlyList<TValue>> itemsSource)
        {
            if (ReferenceEquals(_itemsSource, itemsSource))
                return;

            _itemSourceBinding?.Dispose();
            _itemsSource = itemsSource;
            _items = null;

            if (_itemsSource != null)
            {
                _itemSourceBinding = _output.Bind(
                    AutoCompleteBox.ItemsSourceProperty,
                    itemsSource.StartWith(itemsSource.Value)
                );
            }
            else
            {
                _output.ClearValue(AutoCompleteBox.ItemsSourceProperty);
            }
        }

        /// <param name="itemChannel">The currently selected item from the dropdown</param>
        [Fragment(Order = PinOrder.Action)]
        public void SetSelectedItemChannel(
            [Pin(Visibility = PinVisibility.Visible)] IChannel<TValue?>? itemChannel
        ) => _selectedItemBinding.Bind(itemChannel);

        /// <summary>Sets the filtering mode used to determine which items match the typed text.</summary>
        [ImplementProperty(
            typeof(AutoCompleteBox),
            nameof(AutoCompleteBox.FilterModeProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<AutoCompleteFilterMode> _filterMode;

        /// <summary>Sets the minimum number of characters that must be typed before suggestions appear.</summary>
        [ImplementProperty(
            typeof(AutoCompleteBox),
            nameof(AutoCompleteBox.MinimumPrefixLengthProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<int> _minimumPrefixLength;

        /// <summary>Sets the delay before showing suggestions after typing stops.</summary>
        [ImplementProperty(
            typeof(AutoCompleteBox),
            nameof(AutoCompleteBox.MinimumPopulateDelayProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<TimeSpan> _minimumPopulateDelay;

        /// <summary>Sets the custom filter function for advanced filtering scenarios.</summary>
        [ImplementProperty(
            typeof(AutoCompleteBox),
            nameof(AutoCompleteBox.ItemFilterProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<AutoCompleteFilterPredicate<object>> _itemFilter;

        /// <summary>Sets the string-based filter function for text filtering.</summary>
        [ImplementProperty(
            typeof(AutoCompleteBox),
            nameof(AutoCompleteBox.TextFilterProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<AutoCompleteFilterPredicate<string>> _textFilter;

        /// <summary>Sets whether the dropdown is currently open.</summary>
        [ImplementProperty(
            typeof(AutoCompleteBox),
            nameof(AutoCompleteBox.IsDropDownOpenProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<bool> _isDropDownOpen;

        /// <summary>Sets the maximum height of the dropdown when open.</summary>
        [ImplementProperty(
            typeof(AutoCompleteBox),
            nameof(AutoCompleteBox.MaxDropDownHeightProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<float> _maxDropDownHeight;

        /// <summary>Sets whether text completion is enabled (auto-completes text as you type).</summary>
        [ImplementProperty(
            typeof(AutoCompleteBox),
            nameof(AutoCompleteBox.IsTextCompletionEnabledProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<bool> _isTextCompletionEnabled;

        /// <summary>Sets the template used to display items in the dropdown.</summary>
        [ImplementProperty(
            typeof(AutoCompleteBox),
            nameof(AutoCompleteBox.ItemTemplateProperty),
            Order = PinOrder.Style
        )]
        private Optional<IDataTemplate> _itemTemplate;

        /// <summary>Sets the placeholder text shown when the text box is empty.</summary>
        [ImplementProperty(
            typeof(AutoCompleteBox),
            nameof(AutoCompleteBox.WatermarkProperty),
            Order = PinOrder.Style
        )]
        private Optional<string> _watermark;

        /// <summary>Sets the custom selector for how selected items modify the text.</summary>
        [ImplementProperty(
            typeof(AutoCompleteBox),
            nameof(AutoCompleteBox.ItemSelectorProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<AutoCompleteSelector<object>> _itemSelector;

        /// <summary>Sets the string-based selector for text modification.</summary>
        [ImplementProperty(
            typeof(AutoCompleteBox),
            nameof(AutoCompleteBox.TextSelectorProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<AutoCompleteSelector<string>> _textSelector;

        /// <summary>Sets the async function for populating suggestions from external sources.</summary>
        [ImplementProperty(
            typeof(AutoCompleteBox),
            nameof(AutoCompleteBox.AsyncPopulatorProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<
            Func<string, CancellationToken, Task<IEnumerable<object>>>
        > _asyncPopulator;

        public override void Dispose()
        {
            _textBinding?.Dispose();
            _selectedItemBinding?.Dispose();

            _itemsSource = null;
            _itemSourceBinding?.Dispose();
            _items = null;

            base.Dispose();
        }
    }

    /// <summary>
    /// Wrapper for <see cref="AutoCompleteBox"/>
    /// </summary>
    [ProcessNode(Name = "AutoCompleteBox")]
    public class AutoCompleteBoxNode : AutoCompleteBoxNodeBase<AutoCompleteBox, object>
    {
        [Fragment]
        public AutoCompleteBoxNode([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        [Fragment(Order = PinOrder.Main)]
        public override void SetItems(
            [Pin(PinGroupKind = PinGroupKind.Collection, PinGroupDefaultCount = 1)]
                Spread<object?> items
        )
        {
            base.SetItems(items);
        }
    }

    /// <inheritdoc cref="AutoCompleteBoxNode"/>
    [ProcessNode(Name = "AutoCompleteBox (Spectral)")]
    public class AutoCompleteBoxSpectralNode : AutoCompleteBoxNodeBase<AutoCompleteBox, object>
    {
        [Fragment]
        public AutoCompleteBoxSpectralNode([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        [Fragment(Order = PinOrder.Main)]
        public override void SetItems(Spread<object?> items)
        {
            base.SetItems(items);
        }
    }

    /// <summary>
    /// Generic wrapper for <see cref="AutoCompleteBox"/>
    /// </summary>
    [ProcessNode(Name = "AutoCompleteBox (Advanced)")]
    public class AutoCompleteBoxNode<T> : AutoCompleteBoxNodeBase<AutoCompleteBox, T>
    {
        [Fragment]
        public AutoCompleteBoxNode([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        [Fragment(Order = PinOrder.Main)]
        public override void SetItems(
            [Pin(PinGroupKind = PinGroupKind.Collection, PinGroupDefaultCount = 1)] Spread<T?> items
        )
        {
            base.SetItems(items);
        }
    }

    /// <inheritdoc cref="AutoCompleteBoxNode{T}"/>
    [ProcessNode(Name = "AutoCompleteBox (Advanced Spectral)")]
    public class AutoCompleteBoxSpectralNode<T> : AutoCompleteBoxNodeBase<AutoCompleteBox, T>
    {
        [Fragment]
        public AutoCompleteBoxSpectralNode([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        [Fragment(Order = PinOrder.Main)]
        public override void SetItems(Spread<T?> items)
        {
            base.SetItems(items);
        }
    }

    /// <inheritdoc cref="AutoCompleteBoxNode{T}"/>
    [ProcessNode(Name = "AutoCompleteBox (Advanced Reactive)")]
    public class AutoCompleteBoxReactiveNode<T> : AutoCompleteBoxNodeBase<AutoCompleteBox, T>
    {
        [Fragment]
        public AutoCompleteBoxReactiveNode([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        [Fragment(Order = PinOrder.Main)]
        public override void SetItemsSource(IChannel<IReadOnlyList<T>> itemsSource)
        {
            base.SetItemsSource(itemsSource);
        }
    }
}
