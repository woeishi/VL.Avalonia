using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using VL.Avalonia.CodeGen.AttributeHandlers;
using VL.Avalonia.CodeGen.ClassHandlers;

namespace VL.Avalonia.CodeGen;

// https://github.com/dotnet/roslyn/issues/63780

[Generator(LanguageNames.CSharp)]
public class AvaloniaSourceGenerator : IIncrementalGenerator
{
    /// <summary>
    /// Attribute handlers should be registered here
    /// </summary>
    private static readonly List<IAttributeHandler> AttributeHandlers = new()
    {
        new OutputAttributeHandler(),
        new StyleAttributeHandler(),
        new ContentAttributeHandler(),
        new ChildrenAttributeHandler(),
        new OptionalAttributeHandler(),
        new IsEnabledHandler(),
    };

    /// <summary>
    /// Class handlers should be registered here
    /// </summary>
    private static readonly List<IClassHandler> ClassHandlers = new()
    {
        new ProcessNodeHandler(),
    };

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
#if DEBUG
        if (!Debugger.IsAttached)
        {
            // Debugger.Launch();
        }
#endif 
        var attributesSyntaxProvider = context.SyntaxProvider.CreateSyntaxProvider(
                predicate: static (node, _) => node is ClassDeclarationSyntax syntax,
                transform: static (ctx, _) =>
                {
                    var classDecl = (ClassDeclarationSyntax)ctx.Node;
                    var semanticModel = ctx.SemanticModel;
                    var classSymbol = semanticModel.GetDeclaredSymbol(classDecl);

                    // We are going to execute generator against each class in target package
                    return (classDecl, semanticModel, classSymbol);
                });

        context.RegisterSourceOutput(attributesSyntaxProvider, (context, result) => Execute(context, result.classDecl, result.semanticModel, result.classSymbol));
    }

    private void Execute(SourceProductionContext context, ClassDeclarationSyntax classDecl, SemanticModel semanticModel, ISymbol classSymbol)
    {

        var generatedMethods = new List<string>();

        foreach (var member in classDecl.Members.OfType<FieldDeclarationSyntax>())
        {
            foreach (var variable in member.Declaration.Variables)
            {
                var fieldSymbol = semanticModel.GetDeclaredSymbol(variable) as IFieldSymbol;
                if (fieldSymbol == null)
                    continue;

                foreach (var attr in fieldSymbol.GetAttributes())
                {
                    foreach (var handler in AttributeHandlers)
                    {
                        if (handler.CanHandle(attr))
                        {
                            var generatedMethod = handler.GenerateMethod(attr, fieldSymbol, variable.Identifier.Text);

                            if (generatedMethod != null)
                            {
                                generatedMethods.Add(generatedMethod);
                            }
                        }
                    }
                }

            }
        }

        if (generatedMethods.Count == 0)
            return;

        foreach (var handler in ClassHandlers)
        {
            if (handler.CanHandle(classDecl))
            {
                var generatedClass = handler.GenerateClass(classDecl, classSymbol, generatedMethods);

                if (generatedClass != null)
                {
                    context.AddSource($"{classSymbol.Name}.g.cs", SourceText.From(generatedClass, System.Text.Encoding.UTF8));
                }
            }
        }
    }
}

