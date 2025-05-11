using Avalonia.Controls;
using System.Reactive;
using VL.Avalonia.Helpers;
using VL.Avalonia.Styles;
using VL.Core.Import;
using VL.Lib.Reactive;

namespace VL.Avalonia.Controls;

[ProcessNode(Name = "ButtonTest")]
public class ButtonWrapper
{
    private Button _control = new Button();

    [Pin(Name = "Output")]
    public Button Control => _control;

    private UnitChannelCommand _onClickCommand = new UnitChannelCommand();
    public ButtonWrapper()
    {
        _control.Command = _onClickCommand;
    }



    private IAvaloniaStyle? _style;
    public IAvaloniaStyle? Style
    {
        set
        {
            if (!_style?.Equals(value) ?? _style != value)
            {
                _style = value;
                _style?.ApplyStyle(_control);
            }
        }
    }

    public void SetContent(object content) =>
        _control.Content = content;

    public void SetCommand(IChannel<Unit> channel) =>
        _onClickCommand.CommandChannel = channel;

    public void SetClickMode(ClickMode mode) =>
        _control.ClickMode = mode;



    [Fragment(IsHidden = true)]
    public object? Content => _control.Content;
}

