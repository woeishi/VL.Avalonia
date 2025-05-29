using Microsoft.CodeAnalysis;

namespace VL.Avalonia.CodeGen.AttributeHandlers
{
    public class ClassesAttributeHandler : IAttributeHandler
    {
        public bool CanHandle(AttributeData attr)
         => attr.AttributeClass?.Name.StartsWith("ImplementClasses") == true;

        public string? GenerateMethod(AttributeData attr, IFieldSymbol fieldSymbol, string fieldName)
        {
            var template =
$@"
    [Fragment(Order = -2)]
    public void SetClasses([Pin(Visibility = Model.PinVisibility.Optional)] Optional<string> classes)
    {{
         if (_classes != classes)
        {{
            _classes = classes;
            _output.TryUpdateClasses(classes.Value);
        }}
    }}
";
            return template;
        }
    }
}
