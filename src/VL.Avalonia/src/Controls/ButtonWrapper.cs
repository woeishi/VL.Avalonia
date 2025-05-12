using Avalonia.Controls;
using System.Reactive;
using VL.Avalonia.Helpers;
using VL.Core.Import;
using VL.Lib.Reactive;
using static VL.Avalonia.Styles;

namespace VL.Avalonia.Controls;

[ProcessNode(Name = "ButtonPrototype")]
public partial class ButtonWrapper : AbstractWrapperBase<Button>
{
    private readonly Button _output = new Button();

    // TODO handle CommandParameter
    private UnitChannelCommand _onClickCommand = new UnitChannelCommand();
    public ButtonWrapper()
    {
        _output.Command = _onClickCommand;
    }

    public void SetContent(object content) =>
        _output.Content = content;

    public void SetCommand(IChannel<Unit> channel) =>
        _onClickCommand.CommandChannel = channel;

    public void SetClickMode(ClickMode mode) =>
        _output.ClickMode = mode;

    [Fragment(IsHidden = true)]
    public object? Content => _output.Content;

    public override Button Output => _output;

    private IAvaloniaStyle? _style;
    public override IAvaloniaStyle? Style
    {
        set
        {
            if (!_style?.Equals(value) ?? _style != value)
            {
                // Need to test ResetStyle
                //_output.ApplyStyling();

                _style = value;
                _style?.ApplyStyle(Output);
            }
        }
    }
}

