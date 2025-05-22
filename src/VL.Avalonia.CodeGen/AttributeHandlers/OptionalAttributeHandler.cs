using Microsoft.CodeAnalysis;
using System.Linq;

namespace VL.Avalonia.CodeGen.AttributeHandlers;


public class OptionalAttributeHandler : IAttributeHandler
{
    public bool CanHandle(AttributeData attr)
        => attr.AttributeClass?.Name.StartsWith("ImplementOptional") == true;

    public string? GenerateMethod(AttributeData attr, IFieldSymbol fieldSymbol, string fieldName)
    {
        var typeT = attr.AttributeClass.TypeArguments.FirstOrDefault()
            .ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);


        var fieldType = fieldSymbol.Type as INamedTypeSymbol;
        var typeArg = fieldType?.TypeArguments.FirstOrDefault()?.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat) ?? "object";

        var propertyPath = attr.ConstructorArguments.FirstOrDefault().Value?.ToString() ?? "<UnknownProperty>";

        var converterPath = attr.ConstructorArguments.ElementAtOrDefault(1).Value?.ToString();

        var paramBase = fieldName.TrimStart('_');
        var paramName = paramBase.Length > 0 ? char.ToLower(paramBase[0]) + paramBase.Substring(1) : fieldName;

        var methodName = "Set" + (paramBase.Length > 0 ? char.ToUpper(paramBase[0]) + paramBase.Substring(1) : fieldName);

        var withConverter = converterPath != null ? $"{converterPath}.ConvertToProperty({fieldName}.Value)" : $"{fieldName}.Value";

        var template =
$@"
    public void {methodName}(Optional<{typeArg}> {paramName})
    {{
        if ({paramName} != {fieldName})
        {{
            {fieldName} = {paramName};
            if ({fieldName}.HasValue)
            {{
                _output.SetValue({typeT}.{propertyPath}, {withConverter});
            }}

             _output.UpdateLayout();
        }}
    }}
";
        return template;
    }
}

