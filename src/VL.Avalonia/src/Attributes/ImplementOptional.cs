namespace VL.Avalonia.Attributes;

/// <summary>
/// Used in CodeGen to auto implement
/// </summary>
/// <typeparam name="T"></typeparam>
[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
internal sealed class ImplementOptional<T> : Attribute
{
    public string PropertyPath { get; }
    public Type? ConverterType { get; }
    public ImplementOptional(string propertyPath)
    {
        PropertyPath = propertyPath;
    }

    public ImplementOptional(string propertyPath, Type converterType)
    {
        PropertyPath = propertyPath;
        ConverterType = converterType;
    }
}
