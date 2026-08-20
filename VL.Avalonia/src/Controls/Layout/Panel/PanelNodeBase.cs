using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Media;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Collections;
using VL.Model;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Base wrapper for <see cref="Panel"/>
    /// </summary>
    [ProcessNode(FragmentSelection = FragmentSelection.Explicit)]
    public abstract partial class PanelNodeBase<T> : ControlNodeBase<T>, IDisposable
        where T : Panel, new()
    {
        private IReadOnlyList<Control>? _children;

        [Fragment]
        public PanelNodeBase([Pin(Visibility = PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        /// <param name="children">Set children of <see cref="Panel"/></param>
        public virtual void SetChildren(IReadOnlyList<Control> children)
        {
            if (ReferenceEquals(_children, children))
                return;

            _children = children;
            var collection = _output.Children;

            collection.Clear();

            foreach (var child in _children)
            {
                if (child is Control control)
                {
                    DetachFromParent(control);
                    collection.Add(control);
                }
            }
        }

        /// <summary>
        /// Removes the control from its current parent so it can be re-parented.
        /// </summary>
        private static void DetachFromParent(Control control)
        {
            switch (control.Parent)
            {
                case Panel panel:
                    panel.Children.Remove(control);
                    break;
                case ContentControl contentControl when ReferenceEquals(contentControl.Content, control):
                    contentControl.Content = null;
                    break;
                case ContentPresenter contentPresenter when ReferenceEquals(contentPresenter.Content, control):
                    contentPresenter.Content = null;
                    break;
                case Decorator decorator when ReferenceEquals(decorator.Child, control):
                    decorator.Child = null;
                    break;
                case ItemsControl itemsControl:
                    itemsControl.Items.Remove(control);
                    break;
            }


        }



        /// <param name="children"><inheritdoc cref="SetChildren(IReadOnlyList{Control})"/></param>
        public virtual void SetChildren(Spread<Control> children) =>
            SetChildren((IReadOnlyList<Control>)children);

        /// <summary>Sets Panel background brush.</summary>
        [ImplementProperty(
            typeof(Panel),
            nameof(Panel.BackgroundProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<IBrush> _background;

        public override void Dispose()
        {
            _children = null;
            base.Dispose();
        }
    }
}
