using Avalonia;
using Avalonia.Styling;
using static VL.Avalonia.Styles;

namespace VL.Avalonia.Helpers;

/// <summary>
/// Provides helper methods for working with property registries in Avalonia.
/// </summary>
/// <remarks>This class contains static methods to assist in retrieving registered properties associated with a
/// specific <see cref="StyledElement"/> and property name.</remarks>
public static class PropertyRegistryHelper
{
    /// <summary>
    /// Attempts to retrieve a registered Avalonia property by name for the specified styled element.
    /// </summary>
    /// <remarks>This method searches the registered properties of the specified styled element's type for a property
    /// with the given name. If no matching property is found, the method returns <see langword="false"/> and sets <paramref
    /// name="property"/> to <see langword="null"/>.</remarks>
    /// <param name="owner">The styled element whose registered properties are being queried. Cannot be null.</param>
    /// <param name="propertyName">The name of the property to retrieve. Cannot be null or empty.</param>
    /// <param name="property">When the method returns, contains the matching <see cref="AvaloniaProperty"/> if found; otherwise, <see
    /// langword="null"/>.</param>
    /// <returns><see langword="true"/> if a property with the specified name is found; otherwise, <see langword="false"/>.</returns>
    public static bool TryGetProperty(StyledElement owner, string propertyName, out AvaloniaProperty? property)
    {
        property = AvaloniaPropertyRegistry.Instance.GetRegistered(owner.GetType()).FirstOrDefault(p => p.Name == propertyName);
        return property != null;
    }
}

public static class StyleExtensions
{
    /// <summary>
    /// Attempts to add a <see cref="Setter"/> to the specified <see cref="Style"/> for the given property name and value.
    /// </summary>
    /// <remarks>If the specified property name does not exist in the <paramref name="owner"/>, no <see
    /// cref="Setter"/> is added.</remarks>
    /// <param name="style">The <see cref="Style"/> to which the <see cref="Setter"/> will be added.</param>
    /// <param name="owner">The <see cref="StyledElement"/> that owns the property.</param>
    /// <param name="propertyName">The name of the property for which the <see cref="Setter"/> is being added.</param>
    /// <param name="value">The value to set for the specified property. Can be <see langword="null"/>.</param>
    /// <returns>The original <see cref="Style"/> instance, with the <see cref="Setter"/> added if the property was found.</returns>
    public static Style TryAddSetter(this Style style, StyledElement owner, string propertyName, object? value)
    {
        if (PropertyRegistryHelper.TryGetProperty(owner, propertyName, out var property))
        {
            style.Add(new Setter(property, value));
        }

        return style;
    }
}

public static class StyledElementExtensions
{
    public static bool TryParseClasses(string? input, out IEnumerable<string>? classes)
    {
        classes = input?.Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        return classes != null && classes.Any();
    }

    /// <summary>
    /// Attempts to parse and apply a set of CSS class names to the specified <see cref="StyledElement"/>.
    /// </summary>
    /// <remarks>If <paramref name="classes"/> is null or cannot be parsed into valid class names, no changes
    /// are made to the <paramref name="element"/>. If parsing succeeds, the existing classes in <paramref
    /// name="element"/> are cleared and replaced with the parsed class names.</remarks>
    /// <param name="element">The <see cref="StyledElement"/> to which the classes will be applied. Cannot be null.</param>
    /// <param name="classes">A string containing the CSS class names, separated by spaces. Can be null or empty.</param>
    public static void TryUpdateClasses(this StyledElement element, string? classes)
    {
        element.Classes.Clear();
        if (TryParseClasses(classes, out var parsedClasses))
        {
            foreach (var cls in parsedClasses)
            {
                element.Classes.Add(cls);
            }
        }
    }

    /// <summary>
    /// Attempts to update the styles of the specified <see cref="StyledElement"/> using the provided style.
    /// </summary>
    /// <remarks>If the <paramref name="style"/> is null, all existing styles on the <paramref
    /// name="element"/> are cleared. Otherwise, the method builds a new style using the provided <paramref
    /// name="style"/> and adds it to the element's styles.</remarks>
    /// <param name="element">The <see cref="StyledElement"/> whose styles are to be updated. Cannot be null.</param>
    /// <param name="style">The style to apply to the <paramref name="element"/>. If null, the styles will be cleared.</param>
    public static void TryUpdateStyles(this StyledElement element, IAvaloniaStyle? style)
    {
        element.Styles.Clear();

        var s = style?.BuildStyle(element, new Style());
        if (s != null)
        {
            element.Styles.Add(s);
        }
    }
}

