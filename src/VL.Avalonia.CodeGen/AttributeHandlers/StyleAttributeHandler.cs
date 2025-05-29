using Microsoft.CodeAnalysis;

namespace VL.Avalonia.CodeGen.AttributeHandlers
{
    public class StyleAttributeHandler : IAttributeHandler
    {
        public bool CanHandle(AttributeData attr)
         => attr.AttributeClass?.Name.StartsWith("ImplementStyle") == true;

        public string? GenerateMethod(AttributeData attr, IFieldSymbol fieldSymbol, string fieldName)
        {
            var template =
$@"
    [Fragment(Order = -3)]
    public void SetStyle(Optional<IAvaloniaStyle> style)
    {{
        if (_style != style)
        {{
            _style = style;
            _output.TryUpdateStyles(style.Value);
        }}
    }}
";
            return template;
        }
    }
}
