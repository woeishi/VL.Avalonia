using Avalonia;
using Avalonia.Media;
using Stride.Core.Mathematics;
using AvaloniaColort = Avalonia.Media.Color;
using Point = Avalonia.Point;

namespace VL.Avalonia;

public static class Extensions
{
    public static Vector2 FromPoint(this Point point) =>
        new Vector2(
            (float)point.X,
            (float)point.Y
        );

    public static Point ToPoint(this Vector2 point) =>
        new Point(
            point.X,
            point.Y
        );

    public static RectangleF FromRect(this Rect rect) =>
        new RectangleF(
            (float)rect.X,
            (float)rect.Y,
            (float)rect.Width,
            (float)rect.Height
        );

    public static Rect ToRect(this RectangleF rect) =>
        new Rect(
            rect.X,
            rect.Y,
            rect.Width,
            rect.Height
        );

    public static Color4 FromColor(this AvaloniaColort color) =>
        new Color4(
            color.A,
            color.R,
            color.G,
            color.B
        );

    // TODO: FIX
    // public static AvaloniaColort ToColor(this Color4 color)
    // {
    //     var bytecolor = color.ToColor();
    //     return new AvaloniaColort(bytecolor.R, bytecolor.G, bytecolor.B, bytecolor.A);
    // }

    // public static SolidColorBrush ToBrush(this Color4 color) => new SolidColorBrush(color.ToColor(), color.A);
}
