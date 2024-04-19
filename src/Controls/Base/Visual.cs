using AvaloniaBase = Avalonia;
using AvaloniaMedia = Avalonia.Media;

namespace VL.Avalonia.Controls;

public static class Visual
{
    public static T? Bounds<T>(T? control, out AvaloniaBase.Rect bounds) where T : AvaloniaBase.Visual
    {
        bounds = (control != null) ? control.Bounds : default;
        return control;
    }

    public static T? ClipToBounds<T>(T? control, out bool clipToBounds) where T : AvaloniaBase.Visual
    {
        clipToBounds = (control != null) ? control.ClipToBounds : default;
        return control;
    }

    public static T? SetClipToBounds<T>(T? control, bool clipToBounds = default) where T : AvaloniaBase.Visual
    {
        
        if (control != null) control.ClipToBounds = clipToBounds;
        return control;
    }

    public static T? Clip<T>(T? control, out AvaloniaMedia.Geometry? clip) where T : AvaloniaBase.Visual
    {
        clip = (control != null) ? control.Clip : default;
        return control;
    }

    public static T? SetClip<T>(T? control, AvaloniaMedia.Geometry? clip) where T : AvaloniaBase.Visual
    {

        if (control != null) control.Clip = clip;
        return control;
    }

    public static T? IsEffectivelyVisible<T>(T? control, out bool isEffectivelyVisible) where T : AvaloniaBase.Visual
    {
        isEffectivelyVisible = (control != null) ? control.IsEffectivelyVisible : default;
        return control;
    }

    public static T? IsVisible<T>(T? control, out bool isVisible) where T : AvaloniaBase.Visual
    {
        isVisible = (control != null) ? control.IsVisible : default;
        return control;
    }

    public static T? SetIsVisible<T>(T? control, bool isVisible = default) where T : AvaloniaBase.Visual
    {

        if (control != null) control.IsVisible = isVisible;
        return control;
    }

    public static T? Opacity<T>(T? control, out double opacity) where T : AvaloniaBase.Visual
    {
        opacity = (control != null) ? control.Opacity : default;
        return control;
    }

    public static T? SetOpacity<T>(T? control, double opacity = default) where T : AvaloniaBase.Visual
    {

        if (control != null) control.Opacity = opacity;
        return control;
    }

    public static T? OpacityMask<T>(T? control, out AvaloniaMedia.IBrush? opacityMask) where T : AvaloniaBase.Visual
    {
        opacityMask = (control != null) ? control.OpacityMask : null;
        return control;
    }

    public static T? SetOpacityMask<T>(T? control, AvaloniaMedia.IBrush? opacityMask) where T : AvaloniaBase.Visual
    {

        if (control != null) control.OpacityMask = opacityMask;
        return control;
    }

    public static T? Effect<T>(T? control, out AvaloniaMedia.IEffect? effect) where T : AvaloniaBase.Visual
    {
        effect = (control != null) ? control.Effect : null;
        return control;
    }

    public static T? SetEffect<T>(T? control, AvaloniaMedia.IEffect? effect) where T : AvaloniaBase.Visual
    {

        if (control != null) control.Effect = effect;
        return control;
    }

    public static T? HasMirrorTransform<T>(T? control, out bool hasMirrorTrasform) where T : AvaloniaBase.Visual
    {
        hasMirrorTrasform = (control != null) ? control.HasMirrorTransform : default;
        return control;
    }

    public static T? RenderTransform<T>(T? control, out AvaloniaMedia.ITransform? renderTransform) where T : AvaloniaBase.Visual
    {
        renderTransform = (control != null) ? control.RenderTransform : null;
        return control;
    }

    public static T? SetRenderTransform<T>(T? control, AvaloniaMedia.ITransform? renderTransform) where T : AvaloniaBase.Visual
    {

        if (control != null) control.RenderTransform = renderTransform;
        return control;
    }

    public static T? RenderTransformOrigin<T>(T? control, out AvaloniaBase.RelativePoint renderTransformOrigin) where T : AvaloniaBase.Visual
    {
        renderTransformOrigin = (control != null) ? control.RenderTransformOrigin : default;
        return control;
    }

    public static T? SetRenderTransformOrigin<T>(T? control, AvaloniaBase.RelativePoint renderTransformOrigin = default) where T : AvaloniaBase.Visual
    {

        if (control != null) control.RenderTransformOrigin = renderTransformOrigin;
        return control;
    }

    public static T? FlowDirection<T>(T? control, out AvaloniaMedia.FlowDirection flowDirection) where T : AvaloniaBase.Visual
    {
        flowDirection = (control != null) ? control.FlowDirection : default;
        return control;
    }

    public static T? SetFlowDirection<T>(T? control, AvaloniaMedia.FlowDirection flowDirection = default) where T : AvaloniaBase.Visual
    {

        if (control != null) control.FlowDirection = flowDirection;
        return control;
    }

    public static T? ZIndex<T>(T? control, out int zIndex) where T : AvaloniaBase.Visual
    {
        zIndex = (control != null) ? control.ZIndex : default;
        return control;
    }

    public static T? SetZIndex<T>(T? control, int zIndex = default) where T : AvaloniaBase.Visual
    {

        if (control != null) control.ZIndex = zIndex;
        return control;
    }
}
