using Avalonia.Styling;
using System.Reflection;

namespace VL.Avalonia.Helpers;

/// <summary>
/// This class uses reflection to parse internal Avalonia XAML Markup
/// Mostly done by Claude Sonnet 4 so i do not expect it not going to
/// break.
/// <remarks>
/// To parse types effectively, we need typeResolver, and it should know 
/// available types in advance, making it hard to use with let's say additional
/// packages.
/// </remarks>
/// Used for <c>ParseSelector()</c>
/// </summary>
public static class SelectorHelper
{
    private static readonly object _initLock = new object();
    private static bool _reflectionInitialized = false;

    private static Type _parserType;
    private static ConstructorInfo _parserCtor;
    private static MethodInfo _parseMethod;

    // Holds the user/app-registered resolver, if any
    private static Func<string, string, Type> _registeredTypeResolver;

    // Default resolver (fallback)
    private static readonly Func<string, string, Type> _defaultTypeResolver = (ns, name) =>
    {
        // Try common Avalonia namespaces
        var qualifiedNames = new[]
        {
        $"Avalonia.Controls.{name}, Avalonia.Controls",
        $"Avalonia.Controls.Presenters.{name}, Avalonia.Controls"
    };

        foreach (var qn in qualifiedNames)
        {
            var t = Type.GetType(qn);
            if (t != null)
                return t;
        }

        // As fallback, scan loaded assemblies for the type name
        var found = AppDomain.CurrentDomain.GetAssemblies()
            .Select(a => a.GetType($"Avalonia.Controls.{name}")
                      ?? a.GetType($"Avalonia.Controls.Presenters.{name}"))
            .FirstOrDefault(t => t != null);

        if (found != null)
            return found;

        throw new InvalidOperationException($"Type {name} not found in Avalonia.Controls or Avalonia.Controls.Presenters.");
    };

    // Parser instance (created with the active resolver)
    private static object _parserInstance;
    private static Func<string, string, Type> _activeResolver; // Tracks which resolver the parser is using

    private static void EnsureReflectionInitialized()
    {
        if (_reflectionInitialized) return;
        lock (_initLock)
        {
            if (_reflectionInitialized) return;

            var markupAssembly = AppDomain.CurrentDomain
                .GetAssemblies()
                .First(a => a.GetName().Name == "Avalonia.Markup");

            _parserType = markupAssembly.GetType("Avalonia.Markup.Parsers.SelectorParser");
            _parserCtor = _parserType.GetConstructor(new[] { typeof(Func<string, string, Type>) });
            _parseMethod = _parserType.GetMethod("Parse", new[] { typeof(string) });

            _reflectionInitialized = true;
        }
    }

    /// <summary>
    /// Registers a global type resolver for selector parsing.
    /// Call this once at application startup if you need a custom resolver!
    /// </summary>
    public static void RegisterTypeResolver(Func<string, string, Type> resolver)
    {
        lock (_initLock)
        {
            _registeredTypeResolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
            _parserInstance = null; // Force parser re-init on next use
        }
    }

    // Gets the current resolver (registered or default)
    private static Func<string, string, Type> GetCurrentResolver(Func<string, string, Type> overrideResolver)
        => overrideResolver ?? _registeredTypeResolver ?? _defaultTypeResolver;

    // Gets or creates the parser instance for the current resolver
    private static object GetParserInstance(Func<string, string, Type> resolver)
    {
        EnsureReflectionInitialized();

        // Only recreate the parser if the resolver changed
        if (_parserInstance == null || !ReferenceEquals(resolver, _activeResolver))
        {
            lock (_initLock)
            {
                if (_parserInstance == null || !ReferenceEquals(resolver, _activeResolver))
                {
                    _parserInstance = _parserCtor.Invoke(new object[] { resolver });
                    _activeResolver = resolver;
                }
            }
        }
        return _parserInstance;
    }

    /// <summary>
    /// Parses a selector string into an Avalonia Selector instance.
    /// </summary>
    /// <param name="selectorString">Selector string, e.g. "TextBlock.h1"</param>
    /// <param name="typeResolver">Optional per-call resolver; otherwise uses registered or default resolver.</param>
    /// <returns>Avalonia.Styling.Selector instance</returns>
    public static Selector? ParseSelector(string? selectorString, Func<string, string, Type> typeResolver = null)
    {
        if (selectorString == null)
        {
            return null;
        }

        var resolver = GetCurrentResolver(typeResolver);
        var parser = GetParserInstance(resolver);

        var result = _parseMethod.Invoke(parser, new object[] { selectorString });

        if (result is Selector)
        {
            return (Selector)result;
        }

        return null;
    }
}
