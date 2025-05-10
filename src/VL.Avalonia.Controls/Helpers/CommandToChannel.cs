using System.Reactive;
using System.Windows.Input;
using VL.Lib.Reactive;

namespace VL.Avalonia.Controls.Helpers;

public class UnitChannelCommand : ICommand
{
    public IChannel<Unit> CommandChannel { get; set; }

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        CommandChannel.OnNext(new Unit());
    }
}


//public class AbstractChannelCommand<T> : ICommand
//{
//    private IChannel<T>? _commandChannel;

//    public event EventHandler? CanExecuteChanged;

//    public IChannel<T>? CommandChannel
//    {
//        get => _commandChannel;
//        set
//        {
//            if (value != _commandChannel)
//                _commandChannel = value;
//        }
//    }

//    public bool CanExecute(object? parameter)
//    {
//        return true;
//    }

//    public void Execute(object? parameter)
//    {
//        CommandChannel?.OnNext(default);
//    }
//}

