using Avalonia.Collections;
using Avalonia.Controls;
using Visual = Avalonia.Visual;

namespace VL.Avalonia
{
    //https://github.com/AvaloniaUI/avalonia-dotnet-templates/blob/master/templates/csharp/app/Program.cs
    public class VLWindow : Window
    {
        public VLWindow():base()
        {
            InitializeIfNeeded();
        }
    }
}
