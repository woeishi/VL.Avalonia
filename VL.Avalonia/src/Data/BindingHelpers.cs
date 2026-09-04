using Avalonia;
using Avalonia.Data;
using System.Globalization;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using VL.Core.Import;
using VL.Lib.Reactive;

namespace VL.Avalonia.Data
{
    /// <summary>
    /// Stateless helpers binding a VL <see cref="IChannel{T}"/> to an Avalonia property.
    /// </summary>
    public static class BindingHelpers
    {
        /// <summary>
        /// Binds <paramref name="channel"/> to <paramref name="property"/> on <paramref name="target"/>.
        /// </summary>
        /// <param name="target">The object owning the property. A <c>null</c> target is a no-op.</param>
        /// <param name="property">The property to bind. A <c>null</c> property is a no-op.</param>
        /// <param name="channel">The channel driving/receiving the value. A <c>null</c> channel is a no-op.</param>
        /// <param name="mode">
        /// The binding direction. <see cref="BindingMode.Default"/> resolves to the property's own
        /// <see cref="AvaloniaPropertyMetadata.DefaultBindingMode"/>.
        /// </param>
        /// <param name="toProperty">
        /// Converts a channel value into a property value. When <c>null</c> a best-effort conversion is used.
        /// </param>
        /// <param name="toValue">
        /// Converts a property value into a channel value. When <c>null</c> a best-effort conversion is used.
        /// </param>
        /// <param name="initialValueHandling">Which side wins before the binding is established.</param>
        /// <param name="priority">The priority the property value is written with.</param>
        /// <returns>A disposable detaching the binding. Never <c>null</c>.</returns>
        [Name("Bind (Stateless Advanced)")]
        public static IDisposable Bind<TValue, TProperty>(
            AvaloniaObject? target,
            AvaloniaProperty? property,
            IChannel<TValue>? channel,
            BindingMode mode = BindingMode.OneWay,
            Func<TValue?, TProperty?>? toProperty = null,
            Func<TProperty?, TValue?>? toValue = null,
            SupprotedBindingInitialValueHandling initialValueHandling =
                SupprotedBindingInitialValueHandling.ChannelToControl,
            BindingPriority priority = BindingPriority.LocalValue
        )
        {
            if (target is null || property is null || channel is null)
                return Disposable.Empty;

            if (mode == BindingMode.Default)
                mode = property.GetMetadata(target).DefaultBindingMode;

            // Guards against the channel -> property -> channel echo in two-way mode.
            var isBusy = false;

            object? ToPropertyObject(TValue? value)
            {
                try
                {
                    return ConvertToProperty(value, toProperty);
                }
                catch (Exception e)
                {
                    return new BindingNotification(e, BindingErrorType.Error);
                }
            }

            void PushToChannel(object? propertyValue)
            {
                if (isBusy)
                    return;

                isBusy = true;
                try
                {
                    channel.EnsureValue(ConvertToValue(propertyValue, toValue));
                }
                catch (Exception e)
                    when (e
                            is InvalidCastException
                                or FormatException
                                or OverflowException
                                or ArgumentException
                    )
                {
                    // A failing reverse conversion must not tear down the binding.
                }
                finally
                {
                    isBusy = false;
                }
            }

            void PushToProperty()
            {
                isBusy = true;
                try
                {
                    target.SetValue(property, ToPropertyObject(channel.Value), priority);
                }
                finally
                {
                    isBusy = false;
                }
            }

            if (mode == BindingMode.OneTime)
            {
                // The channel always carries a current value, so there is nothing to wait for.
                if (initialValueHandling is SupprotedBindingInitialValueHandling.ControlToChannel)
                    PushToChannel(target.GetValue(property));
                else
                    PushToProperty();

                return Disposable.Empty;
            }

            if (mode == BindingMode.OneWayToSource)
            {
                // GetObservable pushes the current property value on subscribe, which covers the
                // initial control -> channel handoff.
                return target.GetObservable(property).Subscribe(PushToChannel);
            }

            // Initial value handling. Applied before subscribing so it cannot be mistaken for a change.
            // ChannelToControl needs no explicit write - the StartWith below already pushes the
            // current channel value as the binding's first emission.
            if (initialValueHandling is SupprotedBindingInitialValueHandling.ControlToChannel)
                PushToChannel(target.GetValue(property));

            // A channel is a plain subject - it replays nothing on subscribe - so the current value
            // is prepended explicitly, otherwise the property would keep its old value until the
            // channel next changes. DistinctUntilChanged keeps a channel that re-publishes an
            // unchanged value every frame from writing the property every frame.
            var source = channel
                .StartWith(channel.Value)
                .DistinctUntilChanged()
                .Select(ToPropertyObject);

            var toTarget = target.Bind(property, source, priority);

            if (mode != BindingMode.TwoWay)
                return toTarget;

            // Avalonia has no subject-based Bind overload: the reverse direction is a separate
            // subscription, mirroring what BindingOperations.Apply does internally.
            return new CompositeDisposable(2)
            {
                toTarget,
                target.GetObservable(property).Subscribe(PushToChannel),
            };
        }

        /// <summary>
        /// Binds <paramref name="channel"/> to <paramref name="property"/> on <paramref name="target"/>
        /// for the common case where the property value type and the channel value type are the same.
        /// </summary>
        /// <param name="target">The object owning the property. A <c>null</c> target is a no-op.</param>
        /// <param name="property">The property to bind. A <c>null</c> property is a no-op.</param>
        /// <param name="channel">The channel driving/receiving the value. A <c>null</c> channel is a no-op.</param>
        /// <param name="mode">
        /// The binding direction. <see cref="BindingMode.Default"/> resolves to the property's own
        /// <see cref="AvaloniaPropertyMetadata.DefaultBindingMode"/>.
        /// </param>
        /// <param name="initialValueHandling">Which side wins before the binding is established.</param>
        /// <param name="priority">The priority the property value is written with.</param>
        /// <returns>A disposable detaching the binding. Never <c>null</c>.</returns>
        [Name("Bind (Stateless Simple)")]
        public static IDisposable Bind<TValue>(
            AvaloniaObject? target,
            AvaloniaProperty? property,
            IChannel<TValue>? channel,
            BindingMode mode = BindingMode.OneWay,
            SupprotedBindingInitialValueHandling initialValueHandling =
                SupprotedBindingInitialValueHandling.ChannelToControl,
            BindingPriority priority = BindingPriority.LocalValue
        ) =>
            Bind<TValue, TValue>(
                target,
                property,
                channel,
                mode,
                toProperty: null,
                toValue: null,
                initialValueHandling,
                priority
            );

        /// <summary>
        /// Binds <paramref name="channel"/> to the property named <paramref name="propertyName"/> on
        /// <paramref name="target"/>, optionally converting between the channel value type and the
        /// property value type.
        /// </summary>
        /// <param name="target">The object owning the property. A <c>null</c> target is a no-op.</param>
        /// <param name="propertyName">
        /// The name of the property to bind, resolved against the target's type. A name that is not
        /// registered is a no-op.
        /// </param>
        /// <param name="channel">The channel driving/receiving the value. A <c>null</c> channel is a no-op.</param>
        /// <param name="mode">
        /// The binding direction. <see cref="BindingMode.Default"/> resolves to the property's own
        /// <see cref="AvaloniaPropertyMetadata.DefaultBindingMode"/>.
        /// </param>
        /// <param name="toProperty">
        /// Converts a channel value into a property value. When <c>null</c> a best-effort conversion is used.
        /// </param>
        /// <param name="toValue">
        /// Converts a property value into a channel value. When <c>null</c> a best-effort conversion is used.
        /// </param>
        /// <param name="initialValueHandling">Which side wins before the binding is established.</param>
        /// <param name="priority">The priority the property value is written with.</param>
        /// <returns>A disposable detaching the binding. Never <c>null</c>.</returns>
        [Name("Bind (Stateless Name Advanced)")]
        public static IDisposable Bind<TValue, TProperty>(
            AvaloniaObject? target,
            string? propertyName,
            IChannel<TValue>? channel,
            BindingMode mode = BindingMode.OneWay,
            Func<TValue?, TProperty?>? toProperty = null,
            Func<TProperty?, TValue?>? toValue = null,
            SupprotedBindingInitialValueHandling initialValueHandling =
                SupprotedBindingInitialValueHandling.ChannelToControl,
            BindingPriority priority = BindingPriority.LocalValue
        ) =>
            Bind(
                target,
                FindProperty(target, propertyName),
                channel,
                mode,
                toProperty,
                toValue,
                initialValueHandling,
                priority
            );

        /// <summary>
        /// Binds <paramref name="channel"/> to the property named <paramref name="propertyName"/> on
        /// <paramref name="target"/> for the common case where the property value type and the channel
        /// value type are the same.
        /// </summary>
        /// <param name="target">The object owning the property. A <c>null</c> target is a no-op.</param>
        /// <param name="propertyName">
        /// The name of the property to bind, resolved against the target's type. A name that is not
        /// registered is a no-op.
        /// </param>
        /// <param name="channel">The channel driving/receiving the value. A <c>null</c> channel is a no-op.</param>
        /// <param name="mode">
        /// The binding direction. <see cref="BindingMode.Default"/> resolves to the property's own
        /// <see cref="AvaloniaPropertyMetadata.DefaultBindingMode"/>.
        /// </param>
        /// <param name="initialValueHandling">Which side wins before the binding is established.</param>
        /// <param name="priority">The priority the property value is written with.</param>
        /// <returns>A disposable detaching the binding. Never <c>null</c>.</returns>
        [Name("Bind (Stateless Name)")]
        public static IDisposable Bind<TValue>(
            AvaloniaObject? target,
            string? propertyName,
            IChannel<TValue>? channel,
            BindingMode mode = BindingMode.OneWay,
            SupprotedBindingInitialValueHandling initialValueHandling =
                SupprotedBindingInitialValueHandling.ChannelToControl,
            BindingPriority priority = BindingPriority.LocalValue
        ) =>
            Bind<TValue, TValue>(
                target,
                FindProperty(target, propertyName),
                channel,
                mode,
                toProperty: null,
                toValue: null,
                initialValueHandling,
                priority
            );

        /// <summary>
        /// Resolves a registered Avalonia property by name against the target's type.
        /// </summary>
        internal static AvaloniaProperty? FindProperty(AvaloniaObject? target, string? propertyName)
        {
            if (target is null || string.IsNullOrEmpty(propertyName))
                return null;

            return AvaloniaPropertyRegistry.Instance.FindRegistered(target.GetType(), propertyName);
        }

        /// <summary>
        /// Converts a channel value into a property value, using <paramref name="toProperty"/> when given.
        /// </summary>
        internal static object? ConvertToProperty<TValue, TProperty>(
            TValue? value,
            Func<TValue?, TProperty?>? toProperty
        )
        {
            if (toProperty is not null)
                return toProperty(value);

            if (typeof(TValue) == typeof(TProperty))
                return value;

            return ChangeType<TProperty>(value);
        }

        /// <summary>
        /// Converts a property value into a channel value, using <paramref name="toValue"/> when given.
        /// </summary>
        internal static TValue? ConvertToValue<TValue, TProperty>(
            object? value,
            Func<TProperty?, TValue?>? toValue
        )
        {
            value = BindingNotification.ExtractValue(value);

            if (value == AvaloniaProperty.UnsetValue)
                value = null;

            if (toValue is not null)
                return toValue(ChangeType<TProperty>(value));

            return ChangeType<TValue>(value);
        }

        /// <summary>
        /// Converts a value to <typeparamref name="TTarget"/> without throwing. Returns the default
        /// value when no conversion is possible - supply a converter for those cases.
        /// </summary>
        internal static TTarget? ChangeType<TTarget>(object? value)
        {
            if (value is null)
                return default;

            if (value is TTarget target)
                return target;

            try
            {
                var targetType = Nullable.GetUnderlyingType(typeof(TTarget)) ?? typeof(TTarget);

                return (TTarget?)Convert.ChangeType(value, targetType, CultureInfo.InvariantCulture);
            }
            catch (Exception e)
                when (e
                        is InvalidCastException
                            or FormatException
                            or OverflowException
                            or ArgumentException
                )
            {
                return default;
            }
        }
    }
}
