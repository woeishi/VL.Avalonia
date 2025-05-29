using Avalonia.Controls;
using Avalonia.Controls.Templates;
using System.Reactive;
using VL.Avalonia.Attributes;
using VL.Avalonia.Helpers;
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

    [ImplementClasses]
    protected Optional<string> _classes;

    protected IChannel<Spread<object?>> _itemsChannel = ChannelHelpers.CreateChannelOfType<Spread<object?>>();
    protected Spread<object?> _items = Spread<object?>.Empty;
    [Fragment(Order = -5)]
    public virtual void SetItems(Spread<object?> items)
    {
        if (_items != items)
        {
            _items = items;
            _itemsChannel.OnNext(items);
        }
    }

    protected Optional<IDataTemplate> _itemTemplate;
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

    [ImplementIsEnabled<Menu>]
    protected Optional<bool> _isEnabled;
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

    [ImplementClasses]
    protected Optional<string> _classes;

    protected IChannel<Spread<object?>> _itemsChannel = ChannelHelpers.CreateChannelOfType<Spread<object?>>();
    protected Spread<object?> _items = Spread<object?>.Empty;
    [Fragment(Order = -5)]
    public virtual void SetItems(Spread<object?> items)
    {
        if (_items != items)
        {
            _items = items;
            _itemsChannel.OnNext(items);
        }
    }

    protected ChannelCommand<Unit> _command = new((s, a) => new Unit());
    protected Optional<IChannel<Unit>> _commandChannel;
    public void SetCommandChannel(Optional<IChannel<Unit>> commandChannel)
    {
        if (_commandChannel != commandChannel)
        {
            _commandChannel = commandChannel;
            _output.Command = _command;

            if (_commandChannel.HasValue)
            {
                _command.Channel = commandChannel.Value;
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

    [ImplementIsEnabled<MenuItem>]
    protected Optional<bool> _isEnabled;

}

[ProcessNode(Name = "MenuItem")]
public partial class MenuItemWrapper : MenuItemSpectralWrapper
{
    [Fragment(Order = -5)]
    public override void SetItems([Pin(PinGroupKind = Model.PinGroupKind.Collection, PinGroupDefaultCount = 1)] Spread<object?> items) =>
        base.SetItems(items);
}

