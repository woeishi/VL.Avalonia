namespace VL.Avalonia.Attributes
{
    /// <summary>
    /// Used in CodeGem to implement IsEnabled
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    internal sealed class ImplementIsEnabled<T> : Attribute
    {
    }
}
