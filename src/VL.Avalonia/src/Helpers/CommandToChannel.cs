using System.Reactive;
using System.Windows.Input;
using VL.Lib.Reactive;

namespace VL.Avalonia.Helpers;

public class ChannelCommand<T> : ICommand
{
    public IChannel<T>? Channel { get; set; }
    public Func<ChannelCommand<T>, object?, T> OnExecute { get; set; }
    public Func<ChannelCommand<T>, object?, bool>? OnCanExecute { get; set; }

    public ChannelCommand(Func<ChannelCommand<T>, object?, T> onExecute, Func<ChannelCommand<T>, object?, bool>? onCanExecute = null)
    {
        OnExecute = onExecute;
        OnCanExecute = onCanExecute;
    }

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter) => OnCanExecute?.Invoke(this, parameter) ?? true;

    public void Execute(object? parameter) =>
        Channel?.OnNext(OnExecute.Invoke(this, parameter));
}

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