namespace VL.Avalonia.Attributes
{
    /// <summary>
    /// Used in CodeGen, implements content as Optional `object`
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    internal sealed class ImplementContent : Attribute
    {

    }
}
