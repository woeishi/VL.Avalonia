using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Linq;

// TEMPLATE
/*
    protected Optional<IBrush> _background;
    [Fragment(Order = -10)]
    public void SetBackground([Pin(Visibility = Model.PinVisibility.Optional)] Optional<IBrush> background)
    {
        if (_background != background)
        {
            _background = background;

            if (background.HasValue)
            {
                _output.SetValue(Border.BackgroundProperty, background.Value);
            }
            else
            {
                _output.ClearValue(Border.BackgroundProperty);
            }
        }
    }
*/

namespace VL.Avalonia.CodeGen.AttributeHandlers
{
    public sealed class PropertyAttributeHandler : IAttributeHandler
    {
        private static readonly Dictionary<string, string> PinVisibilities = new Dictionary<string, string>()
        {
            { "0", "Model.PinVisibility.Visible" },
            { "1", "Model.PinVisibility.Optional" },
            { "2", "Model.PinVisibility.Hidden" }

        };

        public bool CanHandle(AttributeData attr) =>
            attr.AttributeClass?.Name.StartsWith("ImplementProperty") == true;


        public string? GenerateMethod(AttributeData attr, IFieldSymbol fieldSymbol, string fieldName)
        {
            var t1 = attr.AttributeClass?.TypeArguments.ElementAtOrDefault(0)?.ToDisplayString();
            var t2 = attr.AttributeClass?.TypeArguments.ElementAtOrDefault(1)?.ToDisplayString();

            var typeCast = t1 == null ? "" : $"({t1})";

            var order = int.Parse(attr.NamedArguments.Where(x => x.Key == "Order").FirstOrDefault().Value.Value?.ToString() ?? "0");
            var pinVisibility = PinVisibilities[attr.NamedArguments.Where(x => x.Key == "PinVisibility").FirstOrDefault().Value.Value?.ToString() ?? "0"];

            var fieldType = fieldSymbol.Type as INamedTypeSymbol;
            var typeArg = fieldType?.TypeArguments.FirstOrDefault()?.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat) ?? "object";

            var propertyPath = attr.ConstructorArguments.ElementAt(0).Value?.ToString();

            var paramBase = fieldName.TrimStart('_');

            var paramName = paramBase.Length > 0 ? char.ToLower(paramBase[0]) + paramBase.Substring(1) : fieldName;
            var methodName = "Set" + (paramBase.Length > 0 ? char.ToUpper(paramBase[0]) + paramBase.Substring(1) : fieldName);

            var template =
$@"
    [Fragment(Order = {order})]
    public void {methodName}([Pin(Visibility = {pinVisibility})] Optional<{typeArg}> {paramBase})
    {{
        if ({fieldName} != {paramBase})
        {{
            {fieldName} = {paramBase};

            if ({paramBase}.HasValue)
            {{
                _output.SetValue({propertyPath}, {typeCast} {paramBase}.Value);
            }}
            else 
            {{
                _output.ClearValue({propertyPath});
            }}
        }}
    }}
";

            return template;
        }
    }
}
