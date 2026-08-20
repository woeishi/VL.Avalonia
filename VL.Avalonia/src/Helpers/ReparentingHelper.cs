using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using VL.Core;
using StyledElement = Avalonia.StyledElement;

namespace VL.Avalonia.Helpers
{
    /// <summary>
    /// Shared logic for detaching a <see cref="Control"/> from its current visual parent
    /// so it can be re-parented elsewhere, together with a persistent warning message
    /// that automatically disposes itself after a short delay.
    /// </summary>
    public static class ReparentingHelper
    {
        private static readonly TimeSpan MessageLifetime = TimeSpan.FromSeconds(9);

        /// <summary>
        /// Detaches <paramref name="control"/> from its current parent, if any, and raises
        /// a persistent warning message that disposes itself after <see cref="MessageLifetime"/>.
        /// </summary>
        public static void DetachFromParent(NodeContext nodeContext, Control control)
        {
            var parent = control.Parent;
            if (parent is null)
                return;

            switch (parent)
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
                default:
                    return;
            }

            EnqueueReparentWarning(nodeContext, control, parent);
        }

        private static void EnqueueReparentWarning(NodeContext nodeContext, Control control, StyledElement previousParent)
        {
            var message = nodeContext.AddPersistentMessage(
                Lang.MessageSeverity.Warning,
                $"Control '{control}' was removed from its previous parent '{previousParent}' to be re-parented."
            );

            _ = DisposeAfterDelayAsync(message);
        }

        private static async Task DisposeAfterDelayAsync(IDisposable message)
        {
            await Task.Delay(MessageLifetime).ConfigureAwait(false);
            message.Dispose();
        }
    }
}
