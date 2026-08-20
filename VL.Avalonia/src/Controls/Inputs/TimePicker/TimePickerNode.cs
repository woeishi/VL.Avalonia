using Avalonia.Controls;
using VL.Avalonia.Attributes;
using VL.Avalonia.Data;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Reactive;
using VL.Model;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Base wrapper for <see cref="TimePicker"/>
    /// </summary>
    [ProcessNode]
    public abstract partial class TimePickerNodeBase<T> : ControlNodeBase<T>, IDisposable
        where T : TimePicker, new()
    {
        private TwoWayBinding<TimeSpan?, TimeSpan?> _selectedTimeBinding;

        [Fragment]
        public TimePickerNodeBase([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext)
            : base(nodeContext)
        {
            _selectedTimeBinding = new TwoWayBinding<TimeSpan?, TimeSpan?>(
                _output,
                TimePicker.SelectedTimeProperty,
                x => x,
                y => y
            );
        }

        /// <param name="selectedTimeChannel">
        /// Selected Time for the picker, can be null
        /// </param>
        [Fragment(Order = PinOrder.Action)]
        public void SetSelectedTimeChannel(IChannel<TimeSpan?> selectedTimeChannel) =>
            _selectedTimeBinding.Bind(selectedTimeChannel);

        /// <summary>Sets the minute increment in the picker.</summary>
        [ImplementProperty(
            typeof(TimePicker),
            nameof(TimePicker.MinuteIncrementProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<int> _minuteIncrement;

        /// <summary>Sets the second increment in the picker.</summary>
        [ImplementProperty(
            typeof(TimePicker),
            nameof(TimePicker.SecondIncrementProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<int> _secondIncrement;

        /// <summary>Sets the clock identifier, either 12HourClock or 24HourClock.</summary>
        [ImplementProperty(
            typeof(TimePicker),
            nameof(TimePicker.ClockIdentifierProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<string> _clockIdentifier;

        /// <summary>Sets the use seconds switch, either true or false.</summary>
        [ImplementProperty(
            typeof(TimePicker),
            nameof(TimePicker.UseSecondsProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<bool> _useSeconds;

        public override void Dispose()
        {
            _selectedTimeBinding?.Dispose();
            base.Dispose();
        }
    }

    /// <summary>
    /// Wrapper for <see cref="TimePicker"/>
    /// </summary>
    [ProcessNode(Name = "TimePicker")]
    public class TimePickerNode : TimePickerNodeBase<TimePicker>
    {
        [Fragment]
        public TimePickerNode([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }
    }
}
