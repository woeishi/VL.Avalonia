using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Media.Transformation;
using Stride.Core.Mathematics;
using VL.Avalonia.Animation;
using VL.Avalonia.Attributes;
using VL.Avalonia.Styles;
using VL.Core;
using VL.Core.Import;
using VL.Model;
using Matrix = Stride.Core.Mathematics.Matrix;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Base wrapper for <see cref="Animatable"/>
    /// </summary>
    [ProcessNode]
    public abstract class AnimatableNodeBase<T> : AvaloniaObjectNodeBase<T>
        where T : Animatable, new()
    {
        private IReadOnlyList<IAvaloniaTransition>? _transitions;

        /// <param name="transitions">Sets the property transitions for the control.</param>
        [Fragment(Order = PinOrder.Animatable)]
        public void SetTransitions(
            [Pin(Visibility = Model.PinVisibility.Optional)]
                IReadOnlyList<IAvaloniaTransition> transitions
        )
        {
            if (ReferenceEquals(_transitions, transitions))
                return;

            _transitions = transitions;

            if (_transitions != null)
            {
                var tc = new Transitions();
                foreach (var builder in _transitions)
                {
                    if (builder.TryBuildTransition(Output, out var transition))
                    {
                        tc.Add(transition);
                    }
                }

                Output.SetCurrentValue(Animatable.TransitionsProperty, tc);
            }
            else
            {
                Output.ClearValue(Animatable.TransitionsProperty);
            }
        }
    }

    /// <summary>
    /// Base wrapper for <see cref="StyledElement"/>
    /// </summary>
    [ProcessNode]
    public abstract partial class StyledElementNodeBase<T> : AnimatableNodeBase<T>
        where T : StyledElement, new()
    {
        private Optional<IAvaloniaStyle> _style;

        /// <param name="style">Applies style setters to control</param>
        [Fragment(Order = PinOrder.Setters)]
        public void SetStyle(Optional<IAvaloniaStyle> style)
        {
            if (_style == style)
                return;

            _style = style;

            if (style.HasValue)
            {
                Output.TryUpdateStyles(style.Value);
            }
            else
            {
                Output.Styles.Clear();
            }
        }

        private Optional<string> _classes;

        /// <param name="classes">Applies classes to control</param>
        [Fragment(Order = PinOrder.Style)]
        public void SetClasses(
            [Pin(Visibility = Model.PinVisibility.Optional)] Optional<string> classes
        )
        {
            if (_classes == classes)
                return;

            _classes = classes;

            if (classes.HasValue)
            {
                Output.TryUpdateClasses(classes.Value);
            }
            else
            {
                Output.Classes.Clear();
            }
        }

        /// <summary>Sets the name of the styled element.</summary>
        [ImplementProperty(
            typeof(StyledElement),
            nameof(StyledElement.NameProperty),
            Order = PinOrder.Style,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<string> _name;
    }

    /// <summary>
    /// Base wrapper for <see cref="Visual"/>
    /// </summary>
    [ProcessNode]
    public abstract partial class VisualNodeBase<T> : StyledElementNodeBase<T>
        where T : Visual, new()
    {
        /// <summary>Sets a value indicating whether this control is visible.</summary>
        [ImplementProperty(
            typeof(Visual),
            nameof(Visual.IsVisibleProperty),
            Order = PinOrder.Visual,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<bool> _isVisible;

        /// <summary>Sets the effect applied to this control.</summary>
        [ImplementProperty(
            typeof(Visual),
            nameof(Visual.EffectProperty),
            Order = PinOrder.Visual,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<IEffect> _effect;

        private Optional<Matrix> _renderTransform;

        /// <param name="renderTransform">Sets the render transform for the control.</param>
        [Fragment(Order = PinOrder.Visual)]
        public void SetRenderTransform(
            [Pin(Visibility = Model.PinVisibility.Optional)] Optional<Matrix> renderTransform
        )
        {
            if (_renderTransform == renderTransform)
                return;

            _renderTransform = renderTransform;

            if (renderTransform.HasValue)
            {
                renderTransform.Value.Decompose(
                    out Vector3 scale,
                    out Matrix rotation,
                    out Vector3 translation
                );
                rotation.Decompose(out float yaw, out float pitch, out float roll);

                var builder = TransformOperations.CreateBuilder(3);

                builder.AppendScale(scale.X, scale.Y);
                var angleDegrees = MathUtil.RadiansToDegrees(roll);
                builder.AppendRotate(angleDegrees);
                builder.AppendTranslate(translation.X, translation.Y);

                _output.SetValue(Visual.RenderTransformProperty, builder.Build());
            }
            else
            {
                _output.ClearValue(Visual.RenderTransformProperty);
            }
        }
    }

    /// <summary>
    /// Base wrapper for <see cref="Layoutable"/>
    /// </summary>
    [ProcessNode]
    public abstract partial class LayoutableNodeBase<T> : VisualNodeBase<T>
        where T : Layoutable, new()
    {
        /// <summary>Sets the margin around the element.</summary>
        [ImplementProperty(
            typeof(Layoutable),
            nameof(Layoutable.MarginProperty),
            Order = PinOrder.Layoutable,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<Thickness> _margin;

        /// <summary>Sets the horizontal alignment of the layoutable element.</summary>
        [ImplementProperty(
            typeof(Layoutable),
            nameof(Layoutable.HorizontalAlignmentProperty),
            Order = PinOrder.Layoutable,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<HorizontalAlignment> _horizontalAlignment;

        /// <summary>Sets the vertical alignment of the layoutable element.</summary>
        [ImplementProperty(
            typeof(Layoutable),
            nameof(Layoutable.VerticalAlignmentProperty),
            Order = PinOrder.Layoutable,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<VerticalAlignment> _verticalAlignment;
    }

    /// <summary>
    /// Base wrapper for <see cref="Interactive"/>
    /// </summary>
    [ProcessNode]
    public abstract class InteractiveNodeBase<T> : LayoutableNodeBase<T>
        where T : Interactive, new()
    { }

    /// <summary>
    /// Base wrapper for <see cref="InputElement"/>
    /// </summary>
    [ProcessNode]
    public abstract partial class InputElementNodeBase<T> : InteractiveNodeBase<T>
        where T : InputElement, new()
    {
        /// <summary>Sets a value indicating whether this input element can receive focus.</summary>
        [ImplementProperty(
            typeof(InputElement),
            nameof(InputElement.FocusableProperty),
            Order = PinOrder.InputElement,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<bool> _focusable;

        /// <summary>Sets a value indicating whether this input element is enabled.</summary>
        [ImplementProperty(
            typeof(InputElement),
            nameof(InputElement.IsEnabledProperty),
            Order = PinOrder.InputElement,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<bool> _isEnabled;
    }

    /// <summary>
    /// Base wrapper for <see cref="Control"/>
    /// </summary>
    [ProcessNode]
    public abstract partial class ControlNodeBase<T> : InputElementNodeBase<T>
        where T : Control, new()
    {

        protected readonly NodeContext NodeContext;
        public ControlNodeBase([Pin(Visibility = PinVisibility.Hidden)] NodeContext nodeContext)
        {
            NodeContext = nodeContext;
        }

        /// <summary>Sets the focus adorner for the control.</summary>
        [ImplementProperty(
            typeof(Control),
            nameof(Control.FocusAdornerProperty),
            PinVisibility = Model.PinVisibility.Optional,
            Order = PinOrder.Control
        )]
        private Optional<ITemplate<Control>> _focusAdorner;

        /// <summary>Sets the context menu for the control.</summary>
        [ImplementProperty(
            typeof(Control),
            nameof(Control.ContextMenuProperty),
            PinVisibility = Model.PinVisibility.Optional,
            Order = PinOrder.Control
        )]
        private Optional<ContextMenu> _contextMenu;

        /// <summary>Sets the context flyout for the control.</summary>
        [ImplementProperty(
            typeof(Control),
            nameof(Control.ContextFlyoutProperty),
            PinVisibility = Model.PinVisibility.Optional,
            Order = PinOrder.Control
        )]
        private Optional<FlyoutBase> _contextFlyout;
    }
}
