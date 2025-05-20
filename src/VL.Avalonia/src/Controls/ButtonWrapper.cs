using Avalonia.Controls;
using System.Reactive;
using VL.Avalonia.Attributes;
using VL.Avalonia.Helpers;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Reactive;
using static VL.Avalonia.Styles;

namespace VL.Avalonia.Controls;

[ProcessNode(Name = "Button")]
public partial class ButtonWrapper
{
    [ImplementOutput]
    private readonly Button _output = new Button();

    [ImplementStyle]
    private Optional<IAvaloniaStyle> _style;

    //[ImplementContent]
    //private Optional<object?> _content;

    private Optional<object?> _content;
    [Fragment(Order = -2)]
    public void SetContent(Optional<object?> content)
    {
        if (_content != content)
        {
            {
                _content = content;

                if (content.HasValue)
                {
                    // Should trigger PropertyChanged
                    _output.SetValue(Button.ContentProperty, _content.Value);
                }

            }
        }
    }


    public ButtonWrapper()
    {
        _output.Command = _onClickCommand;


        _output.PropertyChanged += (s, a) =>
        {
            // if this called when content on button set it works
            Console.WriteLine($"Property changed: {a.ToString()}");
        };
    }

    private UnitChannelCommand _onClickCommand = new UnitChannelCommand();

    public void SetCommand(IChannel<Unit> channel) =>
        _onClickCommand.CommandChannel = channel;

    public void SetClickMode(ClickMode mode) =>
        _output.ClickMode = mode;

}

