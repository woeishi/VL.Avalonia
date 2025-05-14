using Microsoft.CodeAnalysis;

namespace VL.Avalonia.CodeGen.AttributeHandlers
{
    public class ChannelAttributeHandler : IAttributeHandler
    {
        const string template = @"
        public void SetValueChannel(IChannel<double>? valueChannel)
        {
            if (_valueChannel != valueChannel)
            {
                _valueChannel = valueChannel;
                _valueChannel?.Subscribe((x) =>
                {
                    _output.SetValue(RangeBase.ValueProperty, _valueChannel?.Value ?? 0);
                });

                _output.SetValue(RangeBase.ValueProperty, _valueChannel?.Value ?? 0);
            }
        }
        [Fragment(IsHidden = true)]
        public IChannel<double>? ValueChannel => _valueChannel;
";



        public bool CanHandle(AttributeData attr)
          => attr.AttributeClass?.Name.StartsWith("ImplementChannel") == true;

        public string? GenerateMethod(AttributeData attr, IFieldSymbol fieldSymbol, string fieldName)
        {
            throw new System.NotImplementedException();
        }
    }
}

/*
public void SetValueChannel(IChannel<double>? valueChannel)
{
    if (_valueChannel != valueChannel)
    {
        _valueChannel = valueChannel;
        _valueChannel?.Subscribe((x) =>
        {
            _output.SetValue(RangeBase.ValueProperty, _valueChannel?.Value ?? 0);
        });

        _output.SetValue(RangeBase.ValueProperty, _valueChannel?.Value ?? 0);
    }
}
[Fragment(IsHidden = true)]
public IChannel<double>? ValueChannel => _valueChannel;
*/