namespace VL.Avalonia.Attributes;

/// <summary>
/// New attempt on general purpose property attribute handler
/// </summary>
/// <typeparam name="T"></typeparam>
[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
class ImplementProperty : Attribute
{
    public string PropertyPath { get; }
    public int Order { get; set; }

    public Model.PinVisibility PinVisibility { get; set; }

    public ImplementProperty(string propertyPath)
    {
        PropertyPath = propertyPath;
    }
}

/// <summary>
/// Implement property with cast
/// </summary>
/// <typeparam name="TProperty"></typeparam>
/// <typeparam name="TValue"></typeparam>
class ImplementProperty<TProperty, TValue> : ImplementProperty
{
    public ImplementProperty(string propertyPath) : base(propertyPath) { }
}
