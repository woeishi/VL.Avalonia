using Avalonia.Controls;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Collections;
using static VL.Avalonia.Styles;

namespace VL.Avalonia.Controls;

[ProcessNode(Name = "Grid")]
public partial class GridWrapper
{
    [ImplementOutput]
    private readonly Grid _output = new Grid();

    [ImplementStyle]
    private Optional<IAvaloniaStyle> _style;

    [ImplementChildren]
    private Spread<Control> _children;

    private Spread<ColumnDefinition?> _columnDefinitions = Spread<ColumnDefinition?>.Empty;
    public void SetColumnDefinitions(Spread<ColumnDefinition?> columnDefinitions)
    {
        if (_columnDefinitions != columnDefinitions)
        {
            _columnDefinitions = columnDefinitions;

            var cd = new ColumnDefinitions();
            foreach (var columnDefinition in _columnDefinitions)
            {
                if (columnDefinition != null)
                {
                    cd.Add(columnDefinition);
                }
            }
            _output.ColumnDefinitions = cd;
        }
    }

    private Spread<RowDefinition?> _rowDefinitions = Spread<RowDefinition?>.Empty;
    public void SetRowDefinitions(Spread<RowDefinition?> rowDefinitions)
    {
        if (_rowDefinitions != rowDefinitions)
        {
            _rowDefinitions = rowDefinitions;
            var rd = new RowDefinitions();
            foreach (var rowDefinition in _rowDefinitions)
            {
                if (rowDefinition != null)
                {
                    rd.Add(rowDefinition);
                }
            }
            _output.RowDefinitions = rd;
        }
    }
}

[ProcessNode(Name = "ColumnDefinition")]
public partial class ColumnDefinitionWrapper
{
    [ImplementOutput]
    private readonly ColumnDefinition _output = new ColumnDefinition();

    private float _gridUnitWidth = 1.0f;
    public void SetValue(float value = 1.0f)
    {
        if (_gridUnitWidth != value)
        {
            _gridUnitWidth = value;
            _output.SetValue(ColumnDefinition.WidthProperty, new GridLength(_gridUnitWidth, _gridUnitType));
        }
    }

    private GridUnitType _gridUnitType = GridUnitType.Star;
    public void SetGridUnitType(GridUnitType gridUnitType = GridUnitType.Star)
    {
        if (_gridUnitType != gridUnitType)
        {
            _gridUnitType = gridUnitType;
            _output.SetValue(ColumnDefinition.WidthProperty, new GridLength(_gridUnitWidth, _gridUnitType));
        }
    }

    private Optional<float> _maxWidth;
    public void SetMaxWidth(Optional<float> maxWidth)
    {
        if (_maxWidth != maxWidth)
        {
            _maxWidth = maxWidth;
            _output.MaxWidth = _maxWidth.HasValue ? _maxWidth.Value : double.PositiveInfinity;
        }
    }

    private Optional<float> _minWidth;
    public void SetMinWidth(Optional<float> minWidth)
    {
        if (_minWidth != minWidth)
        {
            _minWidth = minWidth;
            _output.MinWidth = _minWidth.HasValue ? _minWidth.Value : 0.0f;
        }
    }
}

[ProcessNode(Name = "RowDefinition")]
public partial class RowDefinitionWrapper
{
    [ImplementOutput]
    private readonly RowDefinition _output = new RowDefinition();

    private float _gridUnitHeight = 1.0f;
    public void SetValue(float value = 1.0f)
    {
        if (_gridUnitHeight != value)
        {
            _gridUnitHeight = value;
            _output.SetValue(RowDefinition.HeightProperty, new GridLength(_gridUnitHeight, _gridUnitType));
        }
    }
    private GridUnitType _gridUnitType = GridUnitType.Star;
    public void SetGridUnitType(GridUnitType gridUnitType = GridUnitType.Star)
    {
        if (_gridUnitType != gridUnitType)
        {
            _gridUnitType = gridUnitType;
            _output.SetValue(RowDefinition.HeightProperty, new GridLength(_gridUnitHeight, _gridUnitType));
        }
    }
    private Optional<float> _maxHeight;
    public void SetMaxHeight(Optional<float> maxHeight)
    {
        if (_maxHeight != maxHeight)
        {
            _maxHeight = maxHeight;
            _output.MaxHeight = _maxHeight.HasValue ? _maxHeight.Value : double.PositiveInfinity;
        }
    }
    private Optional<float> _minHeight;
    public void SetMinHeight(Optional<float> minHeight)
    {
        if (_minHeight != minHeight)
        {
            _minHeight = minHeight;
            _output.MinHeight = _minHeight.HasValue ? _minHeight.Value : 0.0f;
        }
    }
}


[ProcessNode(Name = "GridProperty")]
public partial class GridProperty
{
    private Optional<Control> _input;
    public void SetInput(Optional<Control> input)
    {
        if (_input != input)
        {
            _input = input;

            if (_input.HasValue)
            {
                UpdateSetters();
            }
        }
    }
    public Control? Output => _input.Value;

    private Optional<int> _column;
    public void SetColumn(Optional<int> column)
    {
        if (_column != column)
        {
            _column = column;

            if (_input.HasValue)
            {
                UpdateSetters();
            }
        }
    }

    private Optional<int> _columnSpan;
    public void SetColumnSpan(Optional<int> columnSpan)
    {
        if (_columnSpan != columnSpan)
        {
            _columnSpan = columnSpan;
            if (_input.HasValue)
            {
                UpdateSetters();
            }
        }
    }

    private Optional<int> _row;
    public void SetRow(Optional<int> row)
    {
        if (_row != row)
        {
            _row = row;

            if (_input.HasValue)
            {
                UpdateSetters();
            }
        }
    }

    private Optional<int> _rowSpan;
    public void SetRowSpan(Optional<int> rowSpan)
    {
        if (_rowSpan != rowSpan)
        {
            _rowSpan = rowSpan;
            if (_input.HasValue)
            {
                UpdateSetters();
            }
        }
    }

    private void UpdateSetters()
    {
        Grid.SetRowSpan(_input.Value, _rowSpan.HasValue ? _rowSpan.Value : 1);
        Grid.SetRow(_input.Value, _row.HasValue ? _row.Value : 0);
        Grid.SetColumn(_input.Value, _column.HasValue ? _column.Value : 0);
        Grid.SetColumnSpan(_input.Value, _columnSpan.HasValue ? _columnSpan.Value : 1);
    }
}
