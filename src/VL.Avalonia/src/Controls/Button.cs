﻿using Avalonia.Controls;
using Avalonia.Styling;
using System.Reactive;
using VL.Avalonia.Attributes;
using VL.Avalonia.Helpers;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Reactive;
using static VL.Avalonia.Styles;

namespace VL.Avalonia.Controls;

/// <summary>
/// The button is a control that reacts to pointer actions (and has some keyboard equivalents). It presents visual feedback in the form of a depressed state when the pointer is down.
/// </summary>
[ProcessNode(Name = "Button")]
public partial class ButtonWrapper
{
    [ImplementOutput]
    protected readonly Button _output = new Button();

    [ImplementStyle]
    protected Optional<IAvaloniaStyle> _style;

    [ImplementClasses]
    protected Optional<string> _classes;

    [ImplementContent]
    protected Optional<object?> _content;

    protected Optional<Style> _styleProto;
    public void SetStyleProto(Optional<Style> styleProto)
    {
        if (_styleProto != styleProto)
        {
            _styleProto = styleProto;
            _output.Styles.Add(styleProto.Value);
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

    protected Optional<ClickMode> _clickMode;
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
