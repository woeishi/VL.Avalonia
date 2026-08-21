namespace VL.Avalonia.Attributes
{
    /// <summary>
    /// New attempt on general purpose property attribute handler
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class ImplementProperty : Attribute
    {
        [Obsolete]
        public string PropertyPath { get; }
        public Type? OwnerType { get; }
        public string? PropertyName { get; }

        public int Order { get; set; }
        public Model.PinVisibility PinVisibility { get; set; }

        /// <summary>
        /// Name of a method on the declaring type used to convert the pin value into the value
        /// the Avalonia property expects, for example <c>Converter = nameof(ToProperty)</c>.
        /// <br/>
        /// Required whenever the field type is built on an open generic type parameter, since a
        /// static cast is not available there. When the method returns a nullable value type and
        /// the property is not nullable, a <c>null</c> result clears the property.
        /// </summary>
        public string? Converter { get; set; }

        [Obsolete(
            "Use ImplementProperty(string propertyPath) or ImplementProperty(Type ownerType, string propertyName) instead."
        )]
        public ImplementProperty(string propertyPath)
        {
            PropertyPath = propertyPath;
        }

        public ImplementProperty(Type ownerType, string propertyName)
        {
            OwnerType = ownerType;
            PropertyName = propertyName;
        }
    }

    /// <summary>
    /// Implement property with cast
    /// </summary>
    /// <typeparam name="TProperty"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class ImplementProperty<TValue, TProperty> : ImplementProperty
    {
        [Obsolete(
            "Use ImplementProperty(string propertyPath) or ImplementProperty(Type ownerType, string propertyName) instead."
        )]
        public ImplementProperty(string propertyPath)
            : base(propertyPath) { }

        public ImplementProperty(Type ownerType, string propertyName)
            : base(ownerType, propertyName) { }
    }
}
