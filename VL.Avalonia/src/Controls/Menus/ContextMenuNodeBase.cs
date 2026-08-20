using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Primitives.PopupPositioning;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Controls
{
    [ProcessNode]
    public abstract partial class ContextMenuNodeBase<T> : MenuBaseNode<ContextMenu, T>
    {
        [Fragment]
        public ContextMenuNodeBase([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        /// <summary>Sets the Horizontal offset of the popup in relation to the <see cref="Popup.PlacementTarget"/>.</summary>
        [ImplementProperty(
            typeof(ContextMenu),
            nameof(ContextMenu.HorizontalOffsetProperty),
            Order = PinOrder.Style,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<float> _horizontalOffset;

        /// <summary>Sets the Vertical offset of the popup in relation to the <see cref="Popup.PlacementTarget"/>.</summary>
        [ImplementProperty(
            typeof(ContextMenu),
            nameof(ContextMenu.VerticalOffsetProperty),
            Order = PinOrder.Style,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<float> _verticalOffset;

        /// <summary>Sets the anchor point on the <see cref="Popup.PlacementRect"/> when <see cref="Popup.Placement"/> is <see cref="PlacementMode.AnchorAndGravity"/>.</summary>
        [ImplementProperty(
            typeof(ContextMenu),
            nameof(ContextMenu.PlacementAnchorProperty),
            Order = PinOrder.Style,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<PopupAnchor> _placementAnchor;

        /// <summary>Sets a value describing how the popup position will be adjusted if the unadjusted position would result in the popup being partly constrained.</summary>
        [ImplementProperty(
            typeof(ContextMenu),
            nameof(ContextMenu.PlacementConstraintAdjustmentProperty),
            Order = PinOrder.Style,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<PopupPositionerConstraintAdjustment> _placementConstraintAdjustment;

        /// <summary>Sets a value which defines in what direction the popup should open when <see cref="Popup.Placement"/> is <see cref="PlacementMode.AnchorAndGravity"/>.</summary>
        [ImplementProperty(
            typeof(ContextMenu),
            nameof(ContextMenu.PlacementGravityProperty),
            Order = PinOrder.Style,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<PopupGravity> _placementGravity;

        /// <summary>Sets the desired placement of the popup in relation to the <see cref="Popup.PlacementTarget"/>.</summary>
        [ImplementProperty(
            typeof(ContextMenu),
            nameof(ContextMenu.PlacementProperty),
            Order = PinOrder.Style,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<PlacementMode> _placement;

        /// <summary>Sets the anchor rectangle within the parent that the popup will be placed relative to when <see cref="Popup.Placement"/> is <see cref="PlacementMode.AnchorAndGravity"/>.</summary>
        [ImplementProperty(
            typeof(ContextMenu),
            nameof(ContextMenu.PlacementRectProperty),
            Order = PinOrder.Style,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<Rect?> _placementRect;

        /// <summary>Sets a hint to the window manager that a shadow should be added to the popup.</summary>
        [ImplementProperty(
            typeof(ContextMenu),
            nameof(ContextMenu.WindowManagerAddShadowHintProperty),
            Order = PinOrder.Style,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<bool> _windowManagerAddShadowHint;

        /// <summary>Sets the control that is used to determine the popup's position.</summary>
        [ImplementProperty(
            typeof(ContextMenu),
            nameof(ContextMenu.PlacementTargetProperty),
            Order = PinOrder.Style,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<Control> _placementTarget;

        /// <summary>Gets or sets a delegate handler method that positions the Popup control, when <see cref="Popup.Placement"/> is set to <see cref="PlacementMode.Custom"/>.</summary>
        [ImplementProperty(
            typeof(ContextMenu),
            nameof(ContextMenu.CustomPopupPlacementCallbackProperty),
            Order = PinOrder.Style,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<CustomPopupPlacementCallback> _customPopupPlacementCallback;
    }
}
