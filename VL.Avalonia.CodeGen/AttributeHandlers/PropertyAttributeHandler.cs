using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using Microsoft.CodeAnalysis;

namespace VL.Avalonia.CodeGen.AttributeHandlers
{
    public sealed class PropertyAttributeHandler : IAttributeHandler
    {
        private static readonly Dictionary<string, string> PinVisibilities = new Dictionary<
            string,
            string
        >()
        {
            { "0", "Model.PinVisibility.Visible" },
            { "1", "Model.PinVisibility.Optional" },
            { "2", "Model.PinVisibility.Hidden" },
        };

        public bool CanHandle(AttributeData attr) =>
            attr.AttributeClass?.Name.StartsWith("ImplementProperty") == true;

        public string? GenerateMethod(
            AttributeData attr,
            IFieldSymbol fieldSymbol,
            string fieldName
        )
        {
            var tValue = attr
                .AttributeClass?.TypeArguments.ElementAtOrDefault(0)
                ?.ToDisplayString();
            var tProperty = attr
                .AttributeClass?.TypeArguments.ElementAtOrDefault(1)
                ?.ToDisplayString();

            var typeCast = tProperty == null ? "" : $"({tProperty})";

            // A converter is required when the field is built on an open generic type parameter,
            // because a static cast is not available from a type parameter.
            var converterName = attr
                .NamedArguments.FirstOrDefault(x => x.Key == "Converter")
                .Value.Value?.ToString();

            var converterReturnsNullable = false;
            if (!string.IsNullOrEmpty(converterName))
            {
                IMethodSymbol? converterMethod = null;
                var declaringType = fieldSymbol.ContainingType;
                while (declaringType != null && converterMethod == null)
                {
                    converterMethod = declaringType
                        .GetMembers(converterName!)
                        .OfType<IMethodSymbol>()
                        .FirstOrDefault(m => m.Parameters.Length == 1);
                    declaringType = declaringType.BaseType;
                }

                converterReturnsNullable =
                    converterMethod?.ReturnType is INamedTypeSymbol converterReturnType
                    && converterReturnType.OriginalDefinition.SpecialType
                        == SpecialType.System_Nullable_T;
            }

            var order = int.Parse(
                attr.NamedArguments.FirstOrDefault(x => x.Key == "Order").Value.Value?.ToString()
                ?? "0"
            );

            var pinVisibility = PinVisibilities[
                attr.NamedArguments.FirstOrDefault(x => x.Key == "PinVisibility")
                    .Value.Value?.ToString()
                ?? "0"
            ];

            // 1. Resolve names first so we can use paramBase in the XML documentation
            var paramBase = fieldName.TrimStart('_');
            var paramName =
                paramBase.Length > 0
                    ? char.ToLower(paramBase[0]) + paramBase.Substring(1)
                    : fieldName;
            var methodName =
                "Set"
                + (
                    paramBase.Length > 0
                        ? char.ToUpper(paramBase[0]) + paramBase.Substring(1)
                        : fieldName
                );

            // 2. Parse existing XML documentation on the field
            var xmlDoc = fieldSymbol.GetDocumentationCommentXml();
            var paramDocText = "";
            bool isInheritDoc = false;

            if (!string.IsNullOrEmpty(xmlDoc))
            {
                var doc = new XmlDocument();
                doc.LoadXml(xmlDoc);

                var memberNode = doc.SelectSingleNode("/member");
                if (memberNode != null)
                {
                    var existingParam = memberNode.SelectSingleNode("param");
                    var inheritNode = memberNode.SelectSingleNode("inheritdoc");

                    if (existingParam != null)
                    {
                        paramDocText = existingParam.InnerXml.Trim();
                    }
                    else if (inheritNode != null)
                    {
                        isInheritDoc = true;
                    }
                    else
                    {
                        var summaryNode = memberNode.SelectSingleNode("summary");
                        if (summaryNode != null)
                        {
                            paramDocText = summaryNode.InnerXml.Trim();
                        }
                    }
                }
            }

            // 3. Get generic type argument for Optional<T>
            var fieldType = fieldSymbol.Type as INamedTypeSymbol;
            var typeArg =
                fieldType
                    ?.TypeArguments.FirstOrDefault()
                    ?.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat) ?? "object";

            // 4. Resolve PropertyPath based on which constructor the user used
            string propertyPath = "";
            if (attr.ConstructorArguments.Length == 1)
            {
                propertyPath = attr.ConstructorArguments[0].Value?.ToString() ?? "";
            }
            else if (attr.ConstructorArguments.Length == 2)
            {
                var ownerTypeSymbol = attr.ConstructorArguments[0].Value as INamedTypeSymbol;
                var propertyValueName = attr.ConstructorArguments[1].Value?.ToString();

                var typeString = ownerTypeSymbol?.ToDisplayString(
                    SymbolDisplayFormat.FullyQualifiedFormat
                );
                propertyPath = $"{typeString}.{propertyValueName}";

                // 4b. AUTO-FETCH AVALONIA DOCS if inheritdoc is used or docs are empty
                if (
                    (isInheritDoc || string.IsNullOrEmpty(paramDocText))
                    && ownerTypeSymbol != null
                    && !string.IsNullOrEmpty(propertyValueName)
                )
                {
                    // Strip "Property" from "HotKeyProperty" to find the "HotKey" property symbol
                    string targetPropertyName = propertyValueName!;
                    if (targetPropertyName.EndsWith("Property"))
                    {
                        targetPropertyName = targetPropertyName.Substring(
                            0,
                            targetPropertyName.Length - 8
                        );
                    }

                    // Traverse inheritance hierarchy to find the CLR property symbol
                    var currentType = ownerTypeSymbol;
                    IPropertySymbol? targetProperty = null;
                    while (currentType != null && targetProperty == null)
                    {
                        targetProperty = currentType
                            .GetMembers(targetPropertyName)
                            .OfType<IPropertySymbol>()
                            .FirstOrDefault();
                        currentType = currentType.BaseType;
                    }

                    // If we found the property, extract its <summary>!
                    if (targetProperty != null)
                    {
                        var targetXml = targetProperty.GetDocumentationCommentXml();
                        if (!string.IsNullOrEmpty(targetXml))
                        {
                            var targetDoc = new XmlDocument();
                            targetDoc.LoadXml(targetXml);
                            var targetSummary = targetDoc.SelectSingleNode("/member/summary");
                            if (targetSummary != null)
                            {
                                paramDocText = targetSummary.InnerXml.Trim();
                            }
                        }
                    }
                }
            }

            string finalParamDoc = "";
            if (!string.IsNullOrWhiteSpace(paramDocText))
            {
                paramDocText = Regex.Replace(
                    paramDocText,
                    @"<see\s+cref=""[A-Z]:([^""]+)""\s*/>",
                    match => match.Groups[1].Value.Split('.').Last()
                );
                paramDocText = Regex.Replace(paramDocText, @"<[^>]+>", "");
                paramDocText = Regex.Replace(paramDocText, @"[\r\n\t]+", " ");

                // Use standard \r\n and no leading whitespace for the first slash
                finalParamDoc =
                    $"/// <summary>Sets the {paramBase} property.</summary>\r\n        /// <param name=\"{paramBase}\">{paramDocText}</param>";
            }

            // 6. Build the assignment body
            var valueExpression = string.IsNullOrEmpty(converterName)
                ? $"{typeCast} {paramBase}.Value"
                : $"{converterName}({paramBase}.Value)";

            var assignment = converterReturnsNullable
                ? $@"var __converted = {paramBase}.HasValue ? {valueExpression} : null;

            if (__converted.HasValue)
            {{
                _output.SetValue({propertyPath}, __converted.Value);
            }}
            else 
            {{
                _output.ClearValue({propertyPath});
            }}"
                : $@"if ({paramBase}.HasValue)
            {{
                _output.SetValue({propertyPath}, {valueExpression});
            }}
            else 
            {{
                _output.ClearValue({propertyPath});
            }}";

            // 7. Generate the final template
            var template =
                $@"
        {finalParamDoc}
        [Fragment(Order = {order})]
        public void {methodName}([Pin(Visibility = {pinVisibility})] Optional<{typeArg}> {paramBase})
        {{
            if ({fieldName} == {paramBase})
                return;

            {fieldName} = {paramBase};

            {assignment}
        }}";

            return template;
        }
    }
}
