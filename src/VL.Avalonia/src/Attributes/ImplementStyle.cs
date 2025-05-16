namespace VL.Avalonia.Attributes
{
    /// <summary>
    /// Used in CodeGen, implements IAvaloniaStyle
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    internal sealed class ImplementStyle : Attribute
    {
    }
}
