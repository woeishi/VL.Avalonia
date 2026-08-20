using Avalonia.Controls;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Collections;
using VL.Model;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Base wrapper for <see cref="Grid"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [ProcessNode]
    public abstract partial class GridNodeBase<T> : PanelNodeBase<T>
        where T : Grid, new()
    {
        [Fragment]
        public GridNodeBase([Pin(Visibility = VL.Model.PinVisibility.Hidden)] NodeContext nodeContext) : base(nodeContext) { }

        private IReadOnlyList<ColumnDefinition> _columnDefinitions = Spread<ColumnDefinition>.Empty;
        private IReadOnlyList<RowDefinition> _rowDefinitions = Spread<RowDefinition>.Empty;

        /// <param name="columnDefinitions">Set <see cref="Grid"/> column definitions.</param>
        [Fragment(Order = PinOrder.Action)]
        public void SetColumnDefinitions(IReadOnlyList<ColumnDefinition> columnDefinitions)
        {
            if (ReferenceEquals(_columnDefinitions, columnDefinitions))
                return;

            _columnDefinitions = columnDefinitions;
            var definitions = new ColumnDefinitions();

            if (_columnDefinitions is not null)
            {
                foreach (var definition in _columnDefinitions)
                {
                    if (definition is ColumnDefinition)
                        definitions.Add(definition);
                }
            }

            _output.ColumnDefinitions = definitions;
        }

        /// <param name="rowDefinitions">Set <see cref="Grid"/> row definitions.</param>
        [Fragment(Order = PinOrder.Action)]
        public void SetRowDefinitions(IReadOnlyList<RowDefinition> rowDefinitions)
        {
            if (ReferenceEquals(_rowDefinitions, rowDefinitions))
                return;

            _rowDefinitions = rowDefinitions;
            var definitions = new RowDefinitions();

            if (_rowDefinitions is not null)
            {
                foreach (var definition in _rowDefinitions)
                {
                    if (definition is RowDefinition)
                        definitions.Add(definition);
                }
            }

            _output.RowDefinitions = definitions;
        }

        /// <summary>Sets show grid lines helper.</summary>
        [ImplementProperty(
            typeof(Grid),
            nameof(Grid.ShowGridLinesProperty),
            Order = PinOrder.Control,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<bool> _showGridLines;
    }
}
