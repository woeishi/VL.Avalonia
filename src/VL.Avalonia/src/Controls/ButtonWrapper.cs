using Avalonia.Controls;
using System.Reactive;
using VL.Avalonia.Helpers;
using VL.Core.Import;
using VL.Lib.Reactive;

namespace VL.Avalonia.Controls;

[ProcessNode(Name = "ButtonTest")]
public class ButtonWrapper
{
    private Button _button = new Button();
    private UnitChannelCommand _onClickCommand = new UnitChannelCommand();
    public ButtonWrapper()
    {
        _button.Command = _onClickCommand;
    }
    public void SetContent(object content) =>
        _button.Content = content;

    public void SetCommand(IChannel<Unit> channel) =>
        _onClickCommand.CommandChannel = channel;

    public void SetClickMode(ClickMode mode) =>
        _button.ClickMode = mode;

    public Button Output => _button;

    [Fragment(IsHidden = true)]
    public object? Content => _button.Content;
}
