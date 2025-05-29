using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Styling;
using System.Reactive;
using VL.Avalonia.Attributes;
using VL.Avalonia.Helpers;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Collections;
using VL.Lib.Reactive;
using static VL.Avalonia.Styles;

//https://docs.avaloniaui.net/docs/reference/styles/style-selector-syntax

namespace VL.Avalonia.Controls.Test;

public interface IStyler
{
    public Style BuildStyle(StyledElement owner, Style style);


    public static StylerBackground TestStylerBackground(IStyler? input, IBrush? brush) =>
        new StylerBackground(input, brush);

    public static StylerForeground TestStylerForeground(IStyler? input, IBrush? brush) =>
        new StylerForeground(input, brush);


    public static StylerSelectorStyle TestStylerSelectorStyle(IStyler? input, Selector? selector, IStyler? selectorStyle) =>
        new StylerSelectorStyle(input, selector, selectorStyle);

    public static StylerSelectorReflection TestStylerSelectorReflection(IStyler? input, string selectorString, IStyler? selectorStyle) =>
        new StylerSelectorReflection(input, selectorString, selectorStyle);
}

public record struct StylerBackground(IStyler? Input, IBrush? Brush) : IStyler
{
    public Style BuildStyle(StyledElement owner, Style style)
    {
        style.TryAddSetter(owner, "Background", Brush);

        Input?.BuildStyle(owner, style);

        return style;
    }
}

public record struct StylerForeground(IStyler? Input, IBrush? Brush) : IStyler
{
    public Style BuildStyle(StyledElement owner, Style style)
    {
        style.TryAddSetter(owner, "Foreground", Brush);

        Input?.BuildStyle(owner, style);

        return style;
    }
}

public record struct StylerSelectorStyle(IStyler? Input, Selector? Selector, IStyler? SelectorStyle) : IStyler
{
    public Style BuildStyle(StyledElement owner, Style style)
    {
        if (SelectorStyle != null)
        {
            var selectorStyle = SelectorStyle.BuildStyle(owner, new Style() { Selector = Selector });

            style.Add(selectorStyle);

        }
        Input?.BuildStyle(owner, style);
        return style;

    }
}

public record struct StylerSelectorReflection(IStyler? Input, string SelectorString, IStyler? SelectorStyle) : IStyler
{
    public Style BuildStyle(StyledElement owner, Style style)
    {
        if (SelectorStyle != null)
        {
            var selectorStyle = SelectorStyle.BuildStyle(owner, new Style() { Selector = SelectorHelper.ParseSelector(SelectorString) });

            style.Add(selectorStyle);

        }
        Input?.BuildStyle(owner, style);
        return style;
    }
}


[ProcessNode(Name = "ButtonProto")]
public partial class ButtonWrapper2
{
    [ImplementOutput]
    private readonly Button _output = new Button();

    private Optional<IAvaloniaStyle> _style;
    public void SetStyle(Optional<IAvaloniaStyle> style)
    {
        if (_style != style)
        {
            _style = style;

            _output.TryUpdateStyles(style.Value);
        }
    }

    [ImplementContent]
    private Optional<object?> _content;

    private Optional<Style> _styleProto;
    public void SetStyleProto(Optional<Style> styleProto)
    {
        if (_styleProto != styleProto)
        {
            _styleProto = styleProto;
            _output.Styles.Add(styleProto.Value);
        }
    }

    private Optional<string> _classes;
    public void SetClasses(Optional<string> classes)
    {
        if (_classes != classes)
        {
            _classes = classes;
            _output.TryUpdateClasses(classes.Value);
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

    private Optional<ClickMode> _clickMode;
    public void SetClickMode(Optional<ClickMode> clickMode)
    {
        if (_clickMode != clickMode)
        {
            _clickMode = clickMode;
            _output.SetValue(Button.ClickModeProperty, clickMode.Value);
        }
    }

    [ImplementIsEnabled<Button>]
    protected Optional<bool> _isEnabled;
}

[ProcessNode(Name = "StyleProto")]
public partial class StyleWrapper<T> where T : StyledElement
{
    [ImplementOutput]
    private Style _output = new Style();

    private Optional<string> _selectorString;
    public void SetSelector(Optional<string> selectorString)
    {
        if (_selectorString != selectorString)
        {
            _selectorString = selectorString;

            if (selectorString.HasValue)
            {
                _output.Selector = SelectorHelper.ParseSelector(selectorString.Value);
            }
            else
            {
                _output.Selector = null;
            }
        }
    }

    private Spread<SetterBase?> _setters;
    public void SetSetters(Spread<SetterBase?> setters)
    {
        if (_setters != setters)
        {
            _setters = setters;

            _output.Setters.Clear();
            _setters.ForEach(x =>
            {
                if (x != null)
                {
                    _output.Setters.Add(x);
                }
            });
        }
    }
}

[ProcessNode(Name = "SetterProto")]
public partial class SetterWrapper<T> where T : StyledElement
{
    [ImplementOutput]
    private Setter _output = new Setter();

    private Optional<string> _property;
    private Optional<object> _value;

    public void SetProperty(Optional<string> property)
    {
        if (_property != property)
        {
            _property = property;
            // _output.Property = AvaloniaPropertyRegistry.Instance.GetRegistered(typeof(T)).FirstOrDefault(p => p.Name == property.Value);

            Invalidate();
        }
    }

    public void SetValue(Optional<object> value)
    {
        if (_value != value)
        {
            _value = value;
            //_output.Value = value.Value;

            Invalidate();
        }
    }

    private void Invalidate()
    {
        if (_property.HasValue && _value.HasValue)
        {
            var prop = AvaloniaPropertyRegistry.Instance.GetRegistered(typeof(T)).FirstOrDefault(p => p.Name == _property.Value);

            _output = new Setter(prop, _value.Value);
        }
    }
}