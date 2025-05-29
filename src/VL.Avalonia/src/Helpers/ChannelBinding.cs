using System.ComponentModel;
using VL.Lib.Reactive;

namespace VL.Avalonia.Helpers
{
    public class ChannelBinding<T> : INotifyPropertyChanged
    {
        private T _value;
        public T Value
        {
            get => _value;
            set
            {
                SetField(ref _value, value, nameof(Value));
                _channel?.OnNext(value);
            }
        }

        private IChannel<T> _channel = ChannelHelpers.CreateChannelOfType<T>();

        public ChannelBinding() => _channel.Subscribe(value => SetField(ref _value, value, nameof(Value)));

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        protected bool SetField<T>(ref T field, T value, string propertyName)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
