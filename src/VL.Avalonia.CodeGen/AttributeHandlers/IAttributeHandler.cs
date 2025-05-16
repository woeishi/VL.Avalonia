using Microsoft.CodeAnalysis;

namespace VL.Avalonia.CodeGen.AttributeHandlers;

public interface IAttributeHandler
{
    /// <summary>
    /// Returns true if this handler can process the given attribute.
    /// </summary>
    bool CanHandle(AttributeData attr);

    /// <summary>
    /// Generates source code for the field with the given attribute.
    /// Returns null or empty if no code should be generated.
    /// </summary>
    string? GenerateMethod(AttributeData attr, IFieldSymbol fieldSymbol, string fieldName);
}
