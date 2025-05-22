using Microsoft.CodeAnalysis;

namespace VL.Avalonia.CodeGen.AttributeHandlers
{
    public class ContentAttributeHandler : IAttributeHandler
    {
        public bool CanHandle(AttributeData attr)
         => attr.AttributeClass?.Name.StartsWith("ImplementContent") == true;

        public string? GenerateMethod(AttributeData attr, IFieldSymbol fieldSymbol, string fieldName)
        {
            var template =
$@"
    [Fragment(Order = -5)]
    public void SetContent(Optional<object?> content)
    {{
        if (_content != content)
        {{
            _content = content;
            _output.Content = _content.HasValue ? _content.Value : null;

        }}
    }}
";

            return template;
        }
    }
}
