using System.Reactive;
using System.Windows.Input;
using VL.Lib.Reactive;

namespace VL.Avalonia.Helpers;

public class UnitChannelCommand : ICommand
{
    public IChannel<Unit>? CommandChannel { get; set; }

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        CommandChannel?.OnNext(new Unit());
    }
}

public class TChannelCommand<T> : ICommand
{
    public IChannel<T>? CommandChannel { get; set; }

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter)
    {
        throw new NotImplementedException();
    }

    public void Execute(object? parameter)
    {
        CommandChannel?.OnNext((T)parameter);
    }
}