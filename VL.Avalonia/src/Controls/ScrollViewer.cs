using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Stride.Core.Mathematics;
using VL.Avalonia.Attributes;
using VL.Avalonia.Data;
using VL.Avalonia.Extensions;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Reactive;
using VL.Model;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Base wrapper for <see cref="ScrollViewer"/>
    /// </summary>
    [ProcessNode]
    public abstract partial class ScrollViewerNodeBase<T> : ContentControlNodeBase<T>, IDisposable
        where T : ScrollViewer, new()
    {
        private TwoWayBinding<Vector2, Vector> _offsetBinding;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScrollViewerNodeBase{T}"/> class.
        /// </summary>
        [Fragment]
        public ScrollViewerNodeBase([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext)
        {
            _offsetBinding = new TwoWayBinding<Vector2, Vector>(
                _output,
                ScrollViewer.OffsetProperty,
                x => x.ToVector(),
                x => x.FromVector()
            );
        }

        /// <param name="offsetChannel">
        /// The current scroll offset as a Vector (horizontal, vertical).
        /// </param>
        [Fragment(Order = PinOrder.Action)]
        public virtual void SetOffsetChannel(IChannel<Vector2> offsetChannel) =>
            _offsetBinding.Bind(offsetChannel);

        /// <summary>Sets the horizontal scrollbar visibility.</summary>
        [ImplementProperty(
            typeof(ScrollViewer),
            nameof(ScrollViewer.HorizontalScrollBarVisibilityProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<ScrollBarVisibility> _horizontalScrollBarVisibility;

        /// <summary>Sets the vertical scrollbar visibility.</summary>
        [ImplementProperty(
            typeof(ScrollViewer),
            nameof(ScrollViewer.VerticalScrollBarVisibilityProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<ScrollBarVisibility> _verticalScrollBarVisibility;

        /// <summary>Sets a value that determines whether the ScrollViewer uses a bring-into-view scroll behavior when an item in the view gets focus.</summary>
        [ImplementProperty(
            typeof(ScrollViewer),
            nameof(ScrollViewer.BringIntoViewOnFocusChangeProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<bool> _bringIntoViewOnFocusChange;

        /// <summary>Sets a value that indicates whether scrollbars can hide itself when user is not interacting with it.</summary>
        [ImplementProperty(
            typeof(ScrollViewer),
            nameof(ScrollViewer.AllowAutoHideProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<bool> _allowAutoHide;

        /// <summary>Sets if scroll chaining is enabled.</summary>
        [ImplementProperty(
            typeof(ScrollViewer),
            nameof(ScrollViewer.IsScrollChainingEnabledProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<bool> _isScrollChainingEnabled;

        /// <summary>Sets whether scroll gestures should include inertia in their behavior and value.</summary>
        [ImplementProperty(
            typeof(ScrollViewer),
            nameof(ScrollViewer.IsScrollInertiaEnabledProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<bool> _isScrollInertiaEnabled;

        /// <summary>Sets whether dragging of Thumb elements should update the ScrollViewer only when the user releases the mouse.</summary>
        [ImplementProperty(
            typeof(ScrollViewer),
            nameof(ScrollViewer.IsDeferredScrollingEnabledProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<bool> _isDeferredScrollingEnabled;

        /// <summary>Sets how scroll gesture reacts to the snap points along the horizontal axis.</summary>
        [ImplementProperty(
            typeof(ScrollViewer),
            nameof(ScrollViewer.HorizontalSnapPointsTypeProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<SnapPointsType> _horizontalSnapPointsType;

        /// <summary>Sets how scroll gesture reacts to the snap points along the vertical axis.</summary>
        [ImplementProperty(
            typeof(ScrollViewer),
            nameof(ScrollViewer.VerticalSnapPointsTypeProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<SnapPointsType> _verticalSnapPointsType;

        /// <summary>Sets how the existing snap points are horizontally aligned versus the initial viewport.</summary>
        [ImplementProperty(
            typeof(ScrollViewer),
            nameof(ScrollViewer.HorizontalSnapPointsAlignmentProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<SnapPointsAlignment> _horizontalSnapPointsAlignment;

        /// <summary>Sets how the existing snap points are vertically aligned versus the initial viewport.</summary>
        [ImplementProperty(
            typeof(ScrollViewer),
            nameof(ScrollViewer.VerticalSnapPointsAlignmentProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<SnapPointsAlignment> _verticalSnapPointsAlignment;

        public override void Dispose()
        {
            _offsetBinding?.Dispose();
            base.Dispose();
        }
    }

    /// <summary>
    /// Wrapper for <see cref="ScrollViewer"/>
    /// </summary>
    [ProcessNode(Name = "ScrollViewer")]
    public class ScrollViewerNode : ScrollViewerNodeBase<ScrollViewer>
    {
        [Fragment]
        public ScrollViewerNode([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }
    }
}
