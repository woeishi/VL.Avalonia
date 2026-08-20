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
    /// Base wrapper for <see cref="DatePicker"/>
    /// </summary>
    [ProcessNode]
    public abstract partial class DatePickerNodeBase<T> : ControlNodeBase<T>, IDisposable
        where T : DatePicker, new()
    {
        private TwoWayBinding<DateTime?, DateTimeOffset?> _selectedDateBinding;

        [Fragment]
        public DatePickerNodeBase([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext)
            : base(nodeContext)
        {
            _selectedDateBinding = new TwoWayBinding<DateTime?, DateTimeOffset?>(
                _output,
                DatePicker.SelectedDateProperty,
                (dt) => dt == null ? null : DateTime.SpecifyKind(dt.Value, DateTimeKind.Local),
                dts => dts?.DateTime
            );
        }

        /// <param name="selectedDateChannel">
        /// Selected Date for the picker, can be null
        /// </param>
        [Fragment(Order = PinOrder.Action)]
        public void SetSelectedDateChannel(IChannel<DateTime?> selectedDateChannel) =>
            _selectedDateBinding.Bind(selectedDateChannel);

        /// <summary>Sets the format string for the day part of the date.</summary>
        [ImplementProperty(
            typeof(DatePicker),
            nameof(DatePicker.DayFormatProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<string> _dayFormat;

        /// <summary>Sets if the day column is visible.</summary>
        [ImplementProperty(
            typeof(DatePicker),
            nameof(DatePicker.DayVisibleProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<bool> _dayVisible;

        /// <summary>Sets the format string for the month part of the date.</summary>
        [ImplementProperty(
            typeof(DatePicker),
            nameof(DatePicker.MonthFormatProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<string> _monthFormat;

        /// <summary>Sets if the month column is visible.</summary>
        [ImplementProperty(
            typeof(DatePicker),
            nameof(DatePicker.MonthVisibleProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<bool> _monthVisible;

        /// <summary>Sets the format string for the year part of the date.</summary>
        [ImplementProperty(
            typeof(DatePicker),
            nameof(DatePicker.YearFormatProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<string> _yearFormat;

        /// <summary>Sets if the year column is visible.</summary>
        [ImplementProperty(
            typeof(DatePicker),
            nameof(DatePicker.YearVisibleProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<bool> _yearVisible;

        private Optional<DateTime> _minYear;

        /// <param name="minYear">
        /// Sets the minimum year for the picker.
        /// </param>
        [Fragment(Order = PinOrder.Style)]
        public void SetMinYear(
            [Pin(Visibility = PinVisibility.Optional)] Optional<DateTime> minYear
        )
        {
            if (_minYear != minYear)
            {
                _minYear = minYear;

                if (minYear.HasValue)
                {
                    _output.SetValue(
                        DatePicker.MinYearProperty,
                        DateTime.SpecifyKind(minYear.Value, DateTimeKind.Local)
                    );
                }
                else
                {
                    _output.ClearValue(DatePicker.MinYearProperty);
                }
            }
        }

        private Optional<DateTime> _maxYear;

        /// <param name="maxYear">
        /// Sets the maximum year for the picker.
        /// </param>
        [Fragment(Order = PinOrder.Style)]
        public void SetMaxYear(
            [Pin(Visibility = PinVisibility.Optional)] Optional<DateTime> maxYear
        )
        {
            if (_maxYear != maxYear)
            {
                _maxYear = maxYear;

                if (maxYear.HasValue)
                {
                    _output.SetValue(
                        DatePicker.MaxYearProperty,
                        DateTime.SpecifyKind(maxYear.Value, DateTimeKind.Local)
                    );
                }
                else
                {
                    _output.ClearValue(DatePicker.MaxYearProperty);
                }
            }
        }

        public override void Dispose()
        {
            _selectedDateBinding?.Dispose();
            base.Dispose();
        }
    }

    /// <summary>
    /// Wrapper for <see cref="DatePicker"/>.
    /// </summary>
    [ProcessNode(Name = "DatePicker")]
    public class DatePickerNode : DatePickerNodeBase<DatePicker>
    {
        [Fragment]
        public DatePickerNode([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }
    }
}
