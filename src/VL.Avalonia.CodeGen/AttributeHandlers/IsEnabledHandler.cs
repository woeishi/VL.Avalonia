using Microsoft.CodeAnalysis;
using System.Linq;

namespace VL.Avalonia.CodeGen.AttributeHandlers
{
    public class IsEnabledHandler : IAttributeHandler
    {
        public bool CanHandle(AttributeData attr)
            => attr.AttributeClass?.Name.StartsWith("ImplementIsEnabled") == true;
        public string? GenerateMethod(AttributeData attr, IFieldSymbol fieldSymbol, string fieldName)
        {
            var attrClassArg = attr.AttributeClass.TypeArguments.FirstOrDefault().ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);

            var fieldType = fieldSymbol.Type.Name;

            var template =
$@"
    [Fragment(Order = 10)]
    public void SetEnabled(Optional<bool> isEnabled)
    {{
        if (_isEnabled != isEnabled)
        {{
            _isEnabled = isEnabled;
            if (isEnabled.HasValue)
            {{
                _output.SetValue({attrClassArg}.IsEnabledProperty, isEnabled.Value);
            }}
        }}
    }}
";
            return template;
        }
    }
}

