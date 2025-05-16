using Avalonia.Controls;
using System.Reactive;
using VL.Avalonia.Attributes;
using VL.Avalonia.Helpers;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Reactive;
using static VL.Avalonia.Styles;

namespace VL.Avalonia.Controls;

[ProcessNode(Name = "ButtonPrototype")]
public partial class ButtonWrapper
{
    [ImplementOutput]
    private readonly Button _output = new Button();

    [ImplementStyle]
    private Optional<IAvaloniaStyle> _style;

    [ImplementContent]
    private Optional<object?> _content;


    public ButtonWrapper()
    {
        _output.Command = _onClickCommand;
    }

    private UnitChannelCommand _onClickCommand = new UnitChannelCommand();

    public void SetCommand(IChannel<Unit> channel) =>
        _onClickCommand.CommandChannel = channel;

    public void SetClickMode(ClickMode mode) =>
        _output.ClickMode = mode;

}

