using Avalonia.Controls;
using Avalonia.Controls.Templates;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Collections;
using VL.Lib.Reactive;
using static VL.Avalonia.Styles;

namespace VL.Avalonia.Controls
{
    [ProcessNode(Name = "ListBox")]
    public partial class ListBoxWrapper<T>
    {
        [ImplementOutput]
        private readonly ListBox _output = new ListBox();

        [ImplementStyle]
        private Optional<IAvaloniaStyle> _style;

        private Spread<T?> _items;
        private IChannel<Spread<T?>> _itemsChannel = ChannelHelpers.CreateChannelOfType<Spread<T?>>();

        [Fragment(Order = -1)]
        public void SetItems(Spread<T?> items)
        {
            if (_items != items)
            {
                _items = items;
                _itemsChannel.OnNext(items);
            }
        }

        private Optional<IDataTemplate> _itemTemplate;
        public void SetDataTemplate(Optional<IDataTemplate> itemTemplate)
        {
            if (_itemTemplate != itemTemplate)
            {
                _itemTemplate = itemTemplate;
                _output.SetValue(ListBox.ItemTemplateProperty, itemTemplate.Value);
            }
        }




        public ListBoxWrapper()
        {
            _output.Bind(ListBox.ItemsSourceProperty, _itemsChannel);


        }
    }


    /// <summary>
    /// First attempt, using regular <see href="https://docs.avaloniaui.net/docs/concepts/templates/implement-idatatemplate">IDataTemplate</see>
    /// </summary>
    [ProcessNode(Name = "DataTemplate", HasStateOutput = true)]
    public partial class DataTemplateWrapper : IDataTemplate
    {
        private Func<object?, Control?>? _template;
        public void SetTemplate(Func<object?, Control?>? template)
        {
            // NEEDS CACHING
            if (template != _template)
            {
                _template = template;
            }
        }

        [Fragment(IsHidden = true)]
        public Control? Build(object? param) => _template?.Invoke(param);

        [Fragment(IsHidden = true)]
        public bool Match(object? data)
        {
            return data is not null;
        }
    }

    /// <summary>
    /// Attempt using <see href="https://docs.avaloniaui.net/docs/concepts/templates/creating-data-templates-in-code">FuncDataTemplate</see>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [ProcessNode(Name = "FuncDataTemplate")]
    public partial class DataTemplateWrapper<T>
    {
        [ImplementOutput]
        private FuncDataTemplate<T> _output;

        private Func<T, bool> _match = (T value) => value is T;

        // Not sure how to implement this, not on create
        // so it's cached internally. Likely the only
        // way is custom region...
        public DataTemplateWrapper(Func<T, Control> build)
        {
            _output = new FuncDataTemplate<T>(_match, build);
        }
    }
}
