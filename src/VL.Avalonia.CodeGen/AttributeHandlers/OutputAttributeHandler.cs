using Microsoft.CodeAnalysis;

namespace VL.Avalonia.CodeGen.AttributeHandlers
{
    public class OutputAttributeHandler : IAttributeHandler
    {
        public bool CanHandle(AttributeData attr)
             => attr.AttributeClass?.Name.StartsWith("ImplementOutput") == true;

        public string? GenerateMethod(AttributeData attr, IFieldSymbol fieldSymbol, string fieldName)
        {
            var fieldType = fieldSymbol.Type.Name;

            var template =
$@"
    public {fieldType} Output => {fieldName};
";
            return template;
        }
    }
}
