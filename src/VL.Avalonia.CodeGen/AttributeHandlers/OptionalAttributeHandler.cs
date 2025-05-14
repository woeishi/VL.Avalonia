using Microsoft.CodeAnalysis;
using System.Linq;

namespace VL.Avalonia.CodeGen.AttributeHandlers;


public class OptionalAttributeHandler : IAttributeHandler
{
    public bool CanHandle(AttributeData attr)
        => attr.AttributeClass?.Name.StartsWith("ImplementOptional") == true;

    public string? GenerateMethod(AttributeData attr, IFieldSymbol fieldSymbol, string fieldName)
    {
        // This is T!
        var tArg = attr.AttributeClass.TypeArguments.FirstOrDefault()
            .ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);

        var fieldType = fieldSymbol.Type as INamedTypeSymbol;
        var typeArg = fieldType?.TypeArguments.FirstOrDefault()?.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat) ?? "object";
        var propertyPath = attr.ConstructorArguments.FirstOrDefault().Value?.ToString() ?? "<UnknownProperty>";

        var paramBase = fieldName.TrimStart('_');
        var paramName = paramBase.Length > 0 ? char.ToLower(paramBase[0]) + paramBase.Substring(1) : fieldName;

        var methodName = "Set" + (paramBase.Length > 0 ? char.ToUpper(paramBase[0]) + paramBase.Substring(1) : fieldName);

        return $@"
        public void {methodName}(Optional<{typeArg}> {paramName})
        {{
            if ({paramName} != {fieldName})
            {{
                {fieldName} = {paramName};
                if ({fieldName}.HasValue)
                {{
                    Output.SetValue({tArg}.{propertyPath}, {fieldName}.Value);
                }}
            }}
        }}
";
    }
}

