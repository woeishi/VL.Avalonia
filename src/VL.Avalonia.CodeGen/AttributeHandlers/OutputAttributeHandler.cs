using Microsoft.CodeAnalysis;
using System.Linq;

namespace VL.Avalonia.CodeGen.AttributeHandlers
{
    public class OutputAttributeHandler : IAttributeHandler
    {
        public bool CanHandle(AttributeData attr)
             => attr.AttributeClass?.Name.StartsWith("ImplementOutput") == true;

        public string? GenerateMethod(AttributeData attr, IFieldSymbol fieldSymbol, string fieldName)
        {
            // This is T!
            var tArg = attr.AttributeClass.TypeArguments.FirstOrDefault()
                .ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);

            return $@"
        public {tArg} Output => {fieldName};
";
        }
    }
}
