using Avalonia;
using Stride.Core.Mathematics;
using Point = Avalonia.Point;

namespace VL.Avalonia;

public static class Extensions
{
    public static Vector2 FromPoint(this Point point) => new Vector2(
        (float)point.X,
        (float)point.Y
    );

    public static Point ToPoint(this Vector2 point) => new Point(
        point.X,
        point.Y
    );

    public static RectangleF ToRectangleF(this Rect rect) => new RectangleF(
        (float)rect.X,
        (float)rect.Y,
        (float)rect.Width,
        (float)rect.Height
    );

    public static Rect ToRect(this RectangleF rect) => new Rect(
        rect.X,
        rect.Y,
        rect.Width,
        rect.Height
    );
}
