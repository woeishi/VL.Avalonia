using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Layout;
using Avalonia.Media;
using VL.Avalonia.Attributes;
using VL.Avalonia.Data;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Collections;
using VL.Lib.Reactive;
using VL.Model;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Base wrapper for <see cref="ComboBox"/>
    /// </summary>
    [ProcessNode]
    public abstract partial class ComboBoxNodeBase<T> : SelectingItemsControlNodeBase<ComboBox, T>, IDisposable
    {
        private TwoWayBinding<bool, bool> _isDropDownOpenBinding;

        [Fragment]
        public ComboBoxNodeBase([Pin(Visibility = PinVisibility.Hidden)] NodeContext nodeContext)
            : base(nodeContext)
        {
            _isDropDownOpenBinding = new TwoWayBinding<bool, bool>(
                _output,
                ComboBox.IsDropDownOpenProperty
            );
        }

        /// <param name="isDropDownOpenChannel">
        /// Whether the dropdown list is currently open and visible
        /// </param>
        [Fragment(Order = PinOrder.Action)]
        public virtual void SetIsDropDownOpenChannel(
            [Pin(Visibility = PinVisibility.Optional)] IChannel<bool> isDropDownOpenChannel
        ) => _isDropDownOpenBinding.Bind(isDropDownOpenChannel);

        /// <summary>Sets the maximum height of the dropdown list when opened.</summary>
        [ImplementProperty(
            typeof(ComboBox),
            nameof(ComboBox.MaxDropDownHeightProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<float> _maxDropDownHeight;

        /// <summary>Sets the template used to display the selected item in the closed ComboBox.</summary>
        [ImplementProperty(
            typeof(ComboBox),
            nameof(ComboBox.SelectionBoxItemTemplateProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<IDataTemplate> _selectionBoxItemTemplate;

        /// <summary>Sets the text displayed when no item is selected.</summary>
        [ImplementProperty(
            typeof(ComboBox),
            nameof(ComboBox.PlaceholderTextProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<string> _placeholderText;

        /// <summary>Sets the brush used to render the placeholder text.</summary>
        [ImplementProperty(
            typeof(ComboBox),
            nameof(ComboBox.PlaceholderForegroundProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<IBrush> _placeholderForeground;

        /// <summary>Sets the horizontal alignment of the selected item content within the ComboBox.</summary>
        [ImplementProperty(
            typeof(ComboBox),
            nameof(ComboBox.HorizontalContentAlignmentProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<HorizontalAlignment> _horizontalContentAlignment;

        /// <summary>Sets the vertical alignment of the selected item content within the ComboBox.</summary>
        [ImplementProperty(
            typeof(ComboBox),
            nameof(ComboBox.VerticalContentAlignmentProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<VerticalAlignment> _verticalContentAlignment;

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public override void Dispose()
        {
            _isDropDownOpenBinding?.Dispose();
            base.Dispose();
        }
    }

    /// <summary>
    /// The <c>ComboBox</c> is a selection control that combines a text box with a drop-down list, allowing users to either select from a list of predefined options or enter their own value. It displays the selected item in a compact form and shows the full list of options when activated. The ComboBox is ideal for scenarios where screen space is limited but many options need to be available.
    /// <br/><br/><see href="https://docs.avaloniaui.net/docs/reference/controls/combobox">ComboBox</see>
    /// </summary>
    [ProcessNode(Name = "ComboBox")]
    public class ComboBoxNode : ComboBoxNodeBase<object>
    {
        [Fragment]
        public ComboBoxNode([Pin(Visibility = PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        [Fragment(Order = PinOrder.Main)]
        public override void SetItems(
            [Pin(PinGroupKind = PinGroupKind.Collection, PinGroupDefaultCount = 1)]
                Spread<object?> items
        )
        {
            base.SetItems(items);
        }
    }

    /// <inheritdoc cref="ComboBoxNode"/>
    [ProcessNode(Name = "ComboBox (Spectral)")]
    public class ComboBoxSpectralNode : ComboBoxNodeBase<object>
    {
        [Fragment]
        public ComboBoxSpectralNode([Pin(Visibility = PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        [Fragment(Order = PinOrder.Main)]
        public override void SetItems(Spread<object?> items)
        {
            base.SetItems(items);
        }
    }

    /// <summary>
    /// Generic wrapper for <see cref="ComboBox"/>
    /// </summary>
    [ProcessNode(Name = "ComboBox (Advanced)")]
    public class ComboBoxNode<T> : ComboBoxNodeBase<T>
    {
        [Fragment]
        public ComboBoxNode([Pin(Visibility = PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        [Fragment(Order = PinOrder.Main)]
        public override void SetItems(
            [Pin(PinGroupKind = PinGroupKind.Collection, PinGroupDefaultCount = 1)]
                Spread<T?> items
        )
        {
            base.SetItems(items);
        }
    }

    /// <inheritdoc cref="ComboBoxNode{T}"/>
    [ProcessNode(Name = "ComboBox (Advanced Spectral)")]
    public class ComboBoxSpectralNode<T> : ComboBoxNodeBase<T>
    {
        [Fragment]
        public ComboBoxSpectralNode([Pin(Visibility = PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        [Fragment(Order = PinOrder.Main)]
        public override void SetItems(Spread<T?> items)
        {
            base.SetItems(items);
        }
    }

    /// <inheritdoc cref="ComboBoxNode{T}"/>
    [ProcessNode(Name = "ComboBox (Advanced Reactive)")]
    public class ComboBoxReactiveNode<T> : ComboBoxNodeBase<T>
    {
        [Fragment]
        public ComboBoxReactiveNode([Pin(Visibility = PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        [Fragment(Order = PinOrder.Main)]
        public override void SetItemsSource(IChannel<IReadOnlyList<T>> itemsSource)
        {
            base.SetItemsSource(itemsSource);
        }
    }
}
