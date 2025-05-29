using Avalonia.Styling;
using VL.Avalonia.Helpers;
using VL.Core.Import;
using static VL.Avalonia.Styles;

namespace VL.Avalonia;

// https://docs.avaloniaui.net/docs/reference/styles/style-selector-syntax

/// <summary>
/// <b>This node is EXPERIMENTAL</b>
/// </summary>
[ProcessNode(Name = "StyleSelector (String)")]
public class SelectorWrapperReflection : SelectorWrapper
{
    private string? _selectorString;
    public void SetSelectorString(string? selectorString)
    {
        if (_selectorString != selectorString)
        {
            _selectorString = selectorString;
            SetSelector(SelectorHelper.ParseSelector(selectorString));
        }
    }
    public SelectorWrapperReflection() : base(null)
    {
    }
}

[ProcessNode(Name = "StyleSelector")]
public class SelectorWrapper
{
    protected IAvaloniaStyle _output;
    public IAvaloniaStyle Output => _output;

    protected IAvaloniaStyle? _input;
    public void SetStyle(IAvaloniaStyle input)
    {
        if (_input != input)
        {
            _input = input;

            UpdateOutput();
        }
    }

    public SelectorWrapper(Selector? selector)
    {
        _selector = selector;
    }

    protected Selector? _selector;

    [Fragment(IsHidden = true)]
    public void SetSelector(Selector? selector)
    {
        if (_selector != selector)
        {
            _selector = selector;

            UpdateOutput();
        }
    }

    private IAvaloniaStyle? _selectorStyle;
    public void SetSelectorStyle(IAvaloniaStyle? selectorStyle)
    {
        if (_selectorStyle != selectorStyle)
        {
            _selectorStyle = selectorStyle;

            UpdateOutput();
        }
    }

    protected void UpdateOutput()
    {
        _output = new StyleSelector(_input, _selector, _selectorStyle);
    }
}
