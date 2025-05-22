using Microsoft.CodeAnalysis;
using System.Linq;

namespace VL.Avalonia.CodeGen.AttributeHandlers
{
    public class ChannelAttributeHandler : IAttributeHandler
    {




        public bool CanHandle(AttributeData attr)
          => attr.AttributeClass?.Name.StartsWith("ImplementChannel") == true;

        public string? GenerateMethod(AttributeData attr, IFieldSymbol fieldSymbol, string fieldName)
        {
            var tArg = attr.AttributeClass.TypeArguments.FirstOrDefault()
          .ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);

            var fieldType = fieldSymbol.Type as INamedTypeSymbol;
            var typeArg = fieldType?.TypeArguments.FirstOrDefault()?.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat) ?? "object";
            var propertyPath = attr.ConstructorArguments.FirstOrDefault().Value?.ToString() ?? "<UnknownProperty>";

            var paramBase = fieldName.TrimStart('_');
            var paramName = paramBase.Length > 0 ? char.ToLower(paramBase[0]) + paramBase.Substring(1) : fieldName;

            var methodBase = (paramBase.Length > 0 ? char.ToUpper(paramBase[0]) + paramBase.Substring(1) : fieldName);
            var methodName = "Set" + methodBase;

            var template = $@"
        public void {methodName}(IChannel<{typeArg}>? {paramName})
        {{
            if ({fieldName} != {paramName})
            {{
                {fieldName} = {paramName};
                {fieldName}?.Subscribe((x) =>
                {{
                    Output.SetValue({tArg}.{propertyPath}, {fieldName}?.Value);
                }});

                Output.SetValue({tArg}.{propertyPath}, {fieldName}?.Value);
            }}
        }}
        [Fragment(IsHidden = true)]
        public IChannel<{typeArg}>? {methodBase} => {fieldName};
";

            return template;
        }


    }
}

