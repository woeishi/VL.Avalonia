namespace VL.Avalonia.Attributes
{
    /// <summary>
    /// Used in CodeGen to auto Output
    /// </summary>
    /// <typeparam name="T">Property Owner Class</typeparam>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    internal sealed class ImplementOutput<T> : Attribute
    {
    }
}
