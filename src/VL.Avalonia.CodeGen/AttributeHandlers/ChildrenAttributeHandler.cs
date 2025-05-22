using Microsoft.CodeAnalysis;
using System.Linq;

namespace VL.Avalonia.CodeGen.AttributeHandlers
{
    public class ChildrenAttributeHandler : IAttributeHandler
    {
        public bool CanHandle(AttributeData attr)
         => attr.AttributeClass?.Name.StartsWith("ImplementChildren") == true;

        public string? GenerateMethod(AttributeData attr, IFieldSymbol fieldSymbol, string fieldName)
        {
            var argDec = "Spread<Control>? children";

            var isPinGroup = attr.NamedArguments.Where(x => x.Key == "IsPinGroup").FirstOrDefault().Value.Value as bool? ?? false;

            var args = isPinGroup ? $"[Pin(PinGroupKind = Model.PinGroupKind.Collection, PinGroupDefaultCount = 1)] {argDec}" : $"{argDec}";
            var modifier = isPinGroup ? "override" : "virtual";


            var template =
$@"
    [Fragment(Order = -5)]
    public {modifier} void SetChildren({args})
    {{
        if (_children != children)
        {{
            _children = children;
            _output.Children.Clear();
            foreach (var child in _children)
            {{
                if (child is Control control)
                {{
                    _output.Children.Add(control);
                }}
            }}

            _output.ApplyStyling();
        }}
    }}
";

            return template;
        }
    }
}
