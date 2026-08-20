using System.Reactive;
using System.Reactive.Linq;
using Avalonia.Controls;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Reactive;
using VL.Model;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Wrapper for <see cref="Spinner"/> control.
    /// </summary>
    [ProcessNode]
    public partial class SpinnerNodeBase<T> : ContentControlNodeBase<T>, IDisposable
        where T : Spinner, new()
    {
        private IChannel<Unit>? _increaseChannel;
        private IChannel<Unit>? _decreaseChannel;

        private readonly IDisposable _subscription;

        [Fragment]
        public SpinnerNodeBase([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext)
        {
            _subscription = Observable
                .FromEventPattern<SpinEventArgs>(h => _output.Spin += h, h => _output.Spin -= h)
                .Subscribe(eventPattern =>
                {
                    var args = eventPattern.EventArgs;
                    if (args.Direction == SpinDirection.Increase)
                    {
                        _increaseChannel?.OnNext(Unit.Default);
                    }
                    else if (args.Direction == SpinDirection.Decrease)
                    {
                        _decreaseChannel?.OnNext(Unit.Default);
                    }
                });
        }

        /// <param name="increaseChannel">Bind increase click</param>
        [Fragment(Order = PinOrder.Action)]
        public void SetIncreaseChannel(IChannel<Unit> increaseChannel)
        {
            if (ReferenceEquals(_increaseChannel, increaseChannel))
                return;

            _increaseChannel = increaseChannel;
        }

        /// <param name="decreaseChannel">Bind decrease click</param>
        [Fragment(Order = PinOrder.Action)]
        public void SetDecreaseChannel(IChannel<Unit> decreaseChannel)
        {
            if (ReferenceEquals(_decreaseChannel, decreaseChannel))
                return;

            _decreaseChannel = decreaseChannel;
        }

        /// <summary>Sets <see cref="ValidSpinDirections"/> allowed for this control.</summary>
        [ImplementProperty(
            typeof(Spinner),
            nameof(Spinner.ValidSpinDirectionProperty),
            Order = PinOrder.Action,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<ValidSpinDirections> _validSpinDirections;

        public override void Dispose()
        {
            _subscription.Dispose();
            _increaseChannel = null;
            _decreaseChannel = null;
            base.Dispose();
        }
    }
}
