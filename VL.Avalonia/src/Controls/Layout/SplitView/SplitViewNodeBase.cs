using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Media;
using VL.Avalonia.Attributes;
using VL.Avalonia.Data;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Reactive;
using VL.Model;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Base wrapper for <see cref="SplitView"/>
    /// </summary>
    [ProcessNode]
    public abstract partial class SplitViewNodeBase<T> : ContentControlNodeBase<T>, IDisposable
        where T : SplitView, new()
    {
        private TwoWayBinding<bool> _isPaneOpenBinding;

        [Fragment]
        public SplitViewNodeBase([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext)
        {
            _isPaneOpenBinding = new TwoWayBinding<bool>(_output, SplitView.IsPaneOpenProperty);
        }

        /// <param name="isPaneOpenChannel">Binds is pane open property.</param>
        [Fragment(Order = PinOrder.Action)]
        public void SetIsPaneOpenChannel(IChannel<bool> isPaneOpenChannel) =>
            _isPaneOpenBinding.Bind(isPaneOpenChannel);

        /// <summary>Sets the content of the pane</summary>
        [ImplementProperty(
            typeof(SplitView),
            nameof(SplitView.PaneProperty),
            Order = PinOrder.Main
        )]
        private Optional<object> _pane;

        /// <summary>Sets the length of the pane when in <see cref="SplitViewDisplayMode.CompactOverlay"/> or <see cref="SplitViewDisplayMode.CompactInline"/> mode</summary>
        [ImplementProperty(
            typeof(SplitView),
            nameof(SplitView.CompactPaneLengthProperty),
            Order = PinOrder.Layoutable,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<float> _compactPaneLength;

        /// <summary>Sets the <see cref="SplitViewDisplayMode"/> for the SplitView</summary>
        [ImplementProperty(
            typeof(SplitView),
            nameof(SplitView.DisplayModeProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<SplitViewDisplayMode> _displayMode;

        /// <summary>Sets the length of the pane when open</summary>
        [ImplementProperty(
            typeof(SplitView),
            nameof(SplitView.OpenPaneLengthProperty),
            Order = PinOrder.Layoutable,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<float> _openPaneLength;

        /// <summary>Sets the <see cref="SplitView.PaneBackground"/> for the SplitView</summary>
        [ImplementProperty(
            typeof(SplitView),
            nameof(SplitView.PaneBackgroundProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<IBrush> _paneBackground;

        /// <summary>Sets the <see cref="SplitViewPanePlacement"/> for the SplitView</summary>
        [ImplementProperty(
            typeof(SplitView),
            nameof(SplitView.PanePlacementProperty),
            Order = PinOrder.Layoutable,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<SplitViewPanePlacement> _panePlacement;

        /// <summary>Sets the <see cref="SplitView.PaneTemplate"/> for the SplitView</summary>
        [ImplementProperty(
            typeof(SplitView),
            nameof(SplitView.PaneTemplateProperty),
            Order = PinOrder.DataTemplate,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<IDataTemplate> _paneTemplate;

        /// <summary>Sets the <see cref="SplitView.UseLightDismissOverlayMode"/> for the SplitView</summary>
        [ImplementProperty(
            typeof(SplitView),
            nameof(SplitView.UseLightDismissOverlayModeProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<bool> _useLightDismissOverlayMode;

        public override void Dispose()
        {
            _isPaneOpenBinding?.Dispose();
            base.Dispose();
        }
    }
}
