using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using System.Reactive;
using System.Reactive.Linq;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Collections;
using VL.Lib.Reactive;
using VL.Model;
using static VL.Avalonia.Styles;

namespace VL.Avalonia.Controls;

[ProcessNode(Name = "Menu (Spectral)")]
public partial class MenuSpectralWrapper
{
    [ImplementOutput]
    protected readonly Menu _output = new Menu();

    [ImplementStyle]
    protected Optional<IAvaloniaStyle> _style;

    protected IChannel<Spread<object?>> _itemsChannel = ChannelHelpers.CreateChannelOfType<Spread<object?>>();
    protected Spread<object?> _items = Spread<object?>.Empty;
    [Fragment(Order = -5)]
    public virtual void SetItems(Spread<object?> items)
    {
        if (_items != items)
        {
            _items = items;
            _itemsChannel.OnNext(_items);
        }
    }

    protected Optional<IChannel<Unit>> _commandChannel;
    public void SetCommandChannel([Pin(Visibility = PinVisibility.Optional)] Optional<IChannel<Unit>> commandChannel)
    {
        if (_commandChannel != commandChannel)
        {
            _commandChannel = commandChannel;
            if (_commandChannel.HasValue)
            {
                _output.Bind(MenuItem.CommandProperty, _commandChannel.Value as IObservable<object?>);
                _output.GetObservable(MenuItem.CommandProperty)
                    .Subscribe(value => _commandChannel.Value.OnNext(new Unit()));
            }
        }
    }

    private Optional<IDataTemplate> _itemTemplate;
    public void SetDataTemplate([Pin(Visibility = Model.PinVisibility.Optional)] Optional<IDataTemplate> itemTemplate)
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

[ProcessNode(Name = "Menu")]
public partial class MenuWrapper : MenuSpectralWrapper
{
    [Fragment(Order = -5)]
    public override void SetItems([Pin(PinGroupKind = Model.PinGroupKind.Collection, PinGroupDefaultCount = 1)] Spread<object?> items) =>
        base.SetItems(items);
}


[ProcessNode(Name = "MenuItem (Spectral)")]
public partial class MenuItemSpectralWrapper
{
    [ImplementOutput]
    protected readonly MenuItem _output = new MenuItem();

    [ImplementStyle]
    protected Optional<IAvaloniaStyle> _style;

    protected IChannel<Spread<object?>> _itemsChannel = ChannelHelpers.CreateChannelOfType<Spread<object?>>();
    protected Spread<object?> _items = Spread<object?>.Empty;
    [Fragment(Order = -5)]
    public virtual void SetItems(Spread<object?> items)
    {
        if (_items != items)
        {
            _items = items;
            _itemsChannel.OnNext(_items);
        }
    }

    protected Optional<IChannel<Unit>> _commandChannel;
    public void SetCommandChannel(Optional<IChannel<Unit>> commandChannel)
    {
        if (_commandChannel != commandChannel)
        {
            _commandChannel = commandChannel;
            if (_commandChannel.HasValue)
            {
                _output.Bind(MenuItem.CommandProperty, (IObservable<object?>)_commandChannel.Value);
                _output.GetObservable(MenuItem.CommandProperty)
                    .Subscribe(value => _commandChannel.Value.OnNext(new Unit()));
            }
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

    protected Optional<object?> _icon;
    public void SetIcon(Optional<object?> icon)
    {
        if (_icon != icon)
        {
            _icon = icon;
            _output.SetValue(MenuItem.IconProperty, icon.Value);
        }
    }




    protected Optional<IDataTemplate> _itemTemplate;
    [Fragment(IsHidden = true)]
    public void SetDataTemplate([Pin(Visibility = Model.PinVisibility.Optional)] Optional<IDataTemplate> itemTemplate)
    {
        if (_itemTemplate != itemTemplate)
        {
            _itemTemplate = itemTemplate;
            _output.SetValue(MenuItem.ItemTemplateProperty, itemTemplate.Value);
        }
    }

    public MenuItemSpectralWrapper()
    {
        _output.Bind(MenuItem.ItemsSourceProperty, _itemsChannel);
    }
}

[ProcessNode(Name = "MenuItem")]
public partial class MenuItemWrapper : MenuItemSpectralWrapper
{
    [Fragment(Order = -5)]
    public override void SetItems([Pin(PinGroupKind = Model.PinGroupKind.Collection, PinGroupDefaultCount = 1)] Spread<object?> items) =>
        base.SetItems(items);
}

