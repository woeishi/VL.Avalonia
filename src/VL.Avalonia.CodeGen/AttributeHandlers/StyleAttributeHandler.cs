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
    [Fragment(Order = -1)]
    public Optional<IAvaloniaStyle> Style
    {{
        private get => _style;
        set
        {{
            if (_style != value)
            {{
                _style = value;

                if (value.HasValue)
                {{
                    value.Value.ApplyStyle(_output);
                }}

                _output.ApplyStyling();
            }}
        }}
    }}
";

            return template;
        }
    }
}
