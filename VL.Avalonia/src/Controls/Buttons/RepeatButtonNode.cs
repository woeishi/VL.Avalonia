using Avalonia.Controls;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Base wrapper for <see cref="RepeatButton"/>
    /// </summary>
    [ProcessNode]
    public abstract partial class RepeatButtonNodeBase<T> : ButtonNodeBase<T>
        where T : RepeatButton, new()
    {
        [Fragment]
        public RepeatButtonNodeBase([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        /// <summary>Sets the amount of time, in milliseconds, of repeating clicks.</summary>
        [ImplementProperty(
            typeof(RepeatButton),
            nameof(RepeatButton.IntervalProperty),
            Order = PinOrder.Style,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<int> _interval;

        /// <summary>Sets the amount of time, in milliseconds, to wait before repeating begins.</summary>
        [ImplementProperty(
            typeof(RepeatButton),
            nameof(RepeatButton.DelayProperty),
            Order = PinOrder.Style,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<int> _delay;
    }

    /// <summary>
    /// Wrapper for <see cref="RepeatButton"/>
    /// </summary>
    [ProcessNode(Name = "RepeatButton")]
    public class RepeatButtonNode : RepeatButtonNodeBase<RepeatButton>
    {
        [Fragment]
        public RepeatButtonNode([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }
    }
}
