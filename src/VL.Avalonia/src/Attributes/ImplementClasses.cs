namespace VL.Avalonia.Attributes
{
    /// <summary>
    /// Used in CodeGen, implements Avalonia Classes 
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public sealed class ImplementClasses : Attribute
    {
    }
}
