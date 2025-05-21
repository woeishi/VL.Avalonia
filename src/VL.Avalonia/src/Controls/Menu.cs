using Avalonia.Controls;
using Avalonia.Controls.Templates;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Collections;
using VL.Lib.Reactive;
using static VL.Avalonia.Styles;

namespace VL.Avalonia.Controls;

[ProcessNode(Name = "Menu (Spectral)")]
public partial class MenuSpectralWrapper
{
    [ImplementOutput]
    protected readonly Menu _output = new Menu();

    [ImplementStyle]
    protected Optional<IAvaloniaStyle> _style;

    protected IChannel<Spread<ItemsControl?>> _itemsChannel = ChannelHelpers.CreateChannelOfType<Spread<ItemsControl?>>();
    protected Spread<ItemsControl?> _items = Spread<ItemsControl?>.Empty;
    public virtual void SetItems(Spread<ItemsControl?> items)
    {
        if (_items != items)
        {
            _items = items;
            _itemsChannel.OnNext(_items);
        }
    }

    private Optional<IDataTemplate> _itemTemplate;
    public void SetDataTemplate(Optional<IDataTemplate> itemTemplate)
    {
        if (_itemTemplate != itemTemplate)
        {
            _itemTemplate = itemTemplate;
            _output.SetValue(Menu.ItemTemplateProperty, itemTemplate.Value);
        }
    }

    public MenuSpectralWrapper()
    {
        _output.Bind(Menu.ItemsSourceProperty, _itemsChannel);
    }
}

[ProcessNode(Name = "MenuItem")]
public partial class MenuItemSpectralWrapper
{
    [ImplementOutput]
    protected readonly MenuItem _output = new MenuItem();

    [ImplementStyle]
    protected Optional<IAvaloniaStyle> _style;

    protected IChannel<Spread<ItemsControl?>> _itemsChannel = ChannelHelpers.CreateChannelOfType<Spread<ItemsControl?>>();
    protected Spread<ItemsControl?> _items = Spread<ItemsControl?>.Empty;
    public virtual void SetItems(Spread<ItemsControl?> items)
    {
        if (_items != items)
        {
            _items = items;
            _itemsChannel.OnNext(_items);
        }
    }

    protected Optional<IDataTemplate> _itemTemplate;
    public void SetDataTemplate(Optional<IDataTemplate> itemTemplate)
    {
        if (_itemTemplate != itemTemplate)
        {
            _itemTemplate = itemTemplate;
            _output.SetValue(MenuItem.ItemTemplateProperty, itemTemplate.Value);
        }
    }

    protected Optional<object?> _header;
    public void SetHeader(Optional<object?> header)
    {
        if (_header != header)
        {
            _header = header;
            _output.SetValue(MenuItem.HeaderProperty, header.Value);
        }
    }

    public MenuItemSpectralWrapper()
    {
        _output.Bind(MenuItem.ItemsSourceProperty, _itemsChannel);
    }
}

