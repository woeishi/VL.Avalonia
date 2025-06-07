namespace VL.Avalonia.Attributes;

/// <summary>
/// Used in CodeGen, implements children as Spread `object`
/// </summary>
[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
internal sealed class ImplementChildren : Attribute
{
    public bool IsPinGroup { get; set; }
    public ImplementChildren()
    {
    }
}
