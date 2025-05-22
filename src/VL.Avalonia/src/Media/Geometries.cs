using Avalonia.Media;
using Stride.Core.Mathematics;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Collections;

namespace VL.Avalonia.Media;

/// <summary>
/// Avalonia.Media Geometry seems to be the atoms for higher order nodes like Line, Rectangle
/// Seems properties are not reactive
/// </summary>


[ProcessNode(Name = "StreamGeometry")]
public partial class StreamGeometryWrapper
{
    [ImplementOutput]
    private StreamGeometry _output = new StreamGeometry();

    private Optional<string> _data;
    public void SetData(Optional<string> data)
    {
        if (_data != data)
        {
            _data = data;
            if (_data.HasValue)
            {
                _output = StreamGeometry.Parse(_data.Value);
            }
        }
    }
}

[ProcessNode(Name = "RectangleGeometry")]
public partial class RectangleGeometryWrapper
{
    [ImplementOutput]
    private RectangleGeometry _output = new RectangleGeometry();

    private Optional<RectangleF> _rect;
    public void SetRect(Optional<RectangleF> rect)
    {
        if (_rect != rect)
        {
            _rect = rect;
            _output.SetValue(RectangleGeometry.RectProperty, rect.HasValue ? rect.Value.ToRect() : null);
        }
    }
}

[ProcessNode(Name = "PolylineGeometry")]
public partial class PolylineGeometryWrapper
{
    [ImplementOutput]
    private PolylineGeometry _output = new PolylineGeometry();

    private Spread<Vector2>? _points;
    public void SetPoints(Spread<Vector2>? points)
    {
        if (_points != points)
        {
            _points = points;
            _output.SetValue(PolylineGeometry.PointsProperty, points.Select(p => p.ToPoint()).ToList());
        }
    }

    private Optional<bool> _isFilled;
    public void SetFilled(Optional<bool> isFilled)
    {
        if (_isFilled != isFilled)
        {
            _isFilled = isFilled;
            _output.SetValue(PolylineGeometry.IsFilledProperty, isFilled.Value);
        }
    }

}

// TODO : Implement PathGeometryWrapper 
// Involves implementation of segments
// [ProcessNode(Name = "PathGeometry")]
// public partial class PathGeometryWrapper {}

[ProcessNode(Name = "LineGeometry")]
public partial class LineGeometryWrapper
{
    [ImplementOutput]
    private LineGeometry _output = new LineGeometry();

    private Optional<Vector2> _startPoint;
    public void SetStartPoint(Optional<Vector2> startPoint)
    {
        if (_startPoint != startPoint)
        {
            _startPoint = startPoint;
            _output.SetValue(LineGeometry.StartPointProperty, startPoint.HasValue ? startPoint.Value.ToPoint() : null);
        }
    }
    private Optional<Vector2> _endPoint;
    public void SetEndPoint(Optional<Vector2> endPoint)
    {
        if (_endPoint != endPoint)
        {
            _endPoint = endPoint;
            _output.SetValue(LineGeometry.EndPointProperty, endPoint.HasValue ? endPoint.Value.ToPoint() : null);
        }
    }
}

[ProcessNode(Name = "EllipseGeometry")]
public partial class EllipseGeometryWrapper
{
    [ImplementOutput]
    private EllipseGeometry _output = new EllipseGeometry();

    // Seems not affect anything, prolly to use with canvas
    //private Optional<Vector2> _center;
    //public void SetCenter(Optional<Vector2> center)
    //{
    //    if (_center != center)
    //    {
    //        _center = center;
    //        _output.SetValue(EllipseGeometry.CenterProperty, center.HasValue ? center.Value.ToPoint() : null);
    //    }
    //}

    private Optional<Vector2> _radius;
    public void SetRadius(Optional<Vector2> radius)
    {
        if (_radius != radius)
        {
            _radius = radius;
            _output.SetValue(EllipseGeometry.RadiusXProperty, radius.HasValue ? (double)radius.Value.X : null);
            _output.SetValue(EllipseGeometry.RadiusYProperty, radius.HasValue ? (double)radius.Value.Y : null);
        }
    }
}

