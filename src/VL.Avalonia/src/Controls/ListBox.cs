using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using System.Reactive.Linq;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Collections;
using VL.Lib.Reactive;
using static VL.Avalonia.Styles;


namespace VL.Avalonia.Controls;

/// <summary>
/// ListBox node, using <see href="https://docs.avaloniaui.net/docs/reference/controls/listbox">ListBox</see>.
/// WARNING: Multiselect not  implemented yet.
/// </summary>
/// <typeparam name="T"></typeparam>
[ProcessNode(Name = "ListBox")]
public partial class ListBoxWrapper<T>
{
    [ImplementOutput]
    protected readonly ListBox _output = new ListBox();

    [ImplementStyle]
    protected Optional<IAvaloniaStyle> _style;

    [ImplementClasses]
    protected Optional<string> _classes;

    protected Spread<T?> _items;
    protected IChannel<Spread<T?>> _itemsChannel = ChannelHelpers.CreateChannelOfType<Spread<T?>>();
    [Fragment(Order = -1)]
    public void SetItems(Spread<T?> items)
    {
        if (_items != items)
        {
            _items = items;
            _itemsChannel.OnNext(items);
        }
    }

    protected Optional<IDataTemplate> _itemTemplate;
    public void SetDataTemplate(Optional<IDataTemplate> itemTemplate)
    {
        if (_itemTemplate != itemTemplate)
        {
            _itemTemplate = itemTemplate;
            _output.SetValue(ListBox.ItemTemplateProperty, itemTemplate.Value);
        }
    }


    /// <summary>
    /// Two way binding
    /// </summary>
    protected Optional<IChannel<int>> _selectedIndexChannel;
    public void SetSelectedIndexChannel(Optional<IChannel<int>> selectedIndexChannel)
    {
        if (_selectedIndexChannel != selectedIndexChannel)
        {
            _selectedIndexChannel = selectedIndexChannel;
            if (_selectedIndexChannel.HasValue && _selectedIndexChannel.Value != null)
            {
                _output.Bind(ListBox.SelectedIndexProperty, _selectedIndexChannel.Value);
                _output.GetObservable(ListBox.SelectedIndexProperty)
                    .Subscribe(value => _selectedIndexChannel.Value.OnNext(value));
            }
        }
    }

    protected Optional<IChannel<T>> _selectedItemChannel;
    public void SetSelectedItemChannel(Optional<IChannel<T>> selectedItemChannel)
    {
        if (_selectedItemChannel != selectedItemChannel)
        {
            _selectedItemChannel = selectedItemChannel;
            if (_selectedItemChannel.HasValue && _selectedItemChannel.Value != null)
            {
                _output.Bind(ListBox.SelectedItemProperty, _selectedItemChannel.Value as IObservable<object?>);
                _output.GetObservable(ListBox.SelectedItemProperty)
                    .Subscribe(value => _selectedItemChannel.Value.OnNext((T?)value));
            }
        }
    }


    // TODO: Figure out how to implement this, currently not working
    // Seems deserves MultiSelectListBox
    //Optional<IChannel<IList<T>?>> _selectedItemsChannel;
    //public void SetSelectedItemsChannel(Optional<IChannel<IList<T>?>> selectedItemsChannel)
    //{
    //    if (_selectedItemsChannel != selectedItemsChannel)
    //    {
    //        _selectedItemsChannel = selectedItemsChannel;
    //        if (_selectedItemsChannel.HasValue && _selectedItemsChannel.Value != null)
    //        {
    //            _output.Bind(ListBox.SelectedItemsProperty, _selectedItemsChannel.Value);
    //            _output.GetObservable(ListBox.SelectedItemsProperty)
    //                .Subscribe(value =>
    //                {
    //                    if (value is List<T> list)
    //                    {
    //                        _selectedItemsChannel.Value.OnNext(list.ToSpread());
    //                    }
    //                });
    //        }
    //    }
    //}

    // TODO: Selection 
    // An ISelectionModel object with various methods to track
    // multiple selected items. This is is optimized for a large items collection.



    [ImplementOptional<ListBox>(nameof(ListBox.SelectionModeProperty))]
    private Optional<SelectionMode> _selectionMode;



    // TODO: Seems it's better to have attached properties this a separate node
    private Optional<ScrollBarVisibility> _horizontalScrollbarVisibility;
    public void SetHorizontalScrollbarVisibility(Optional<ScrollBarVisibility> horizontalScrollbarVisibility)
    {
        if (_horizontalScrollbarVisibility != horizontalScrollbarVisibility)
        {
            _horizontalScrollbarVisibility = horizontalScrollbarVisibility;
            ScrollViewer.SetHorizontalScrollBarVisibility(_output, _horizontalScrollbarVisibility.Value);
        }
    }

    private Optional<ScrollBarVisibility> _verticalScrollbarVisibility;
    public void SetVerticalScrollbarVisibility(Optional<ScrollBarVisibility> verticalScrollbarVisibility)
    {
        if (_verticalScrollbarVisibility != verticalScrollbarVisibility)
        {
            _verticalScrollbarVisibility = verticalScrollbarVisibility;
            ScrollViewer.SetVerticalScrollBarVisibility(_output, _verticalScrollbarVisibility.Value);
        }
    }

    public ListBoxWrapper()
    {
        _output.Bind(ListBox.ItemsSourceProperty, _itemsChannel);
    }
}




