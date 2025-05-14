using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace VL.Avalonia.CodeGen.ClassHandlers;

public interface IClassHandler
{
    bool CanHandle(ClassDeclarationSyntax classDecl);

    string? GenerateClass(ClassDeclarationSyntax classDecl, ISymbol classSymbol, IEnumerable<string> generatedMethods);
}
