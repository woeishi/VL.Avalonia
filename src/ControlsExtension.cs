using Avalonia;
using Avalonia.Controls;
using Avalonia.Styling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VL.Avalonia
{
    public static class ControlsExtension
    {
        public static Window AddStyle(this Window window, IStyle item)
        {
            window.Styles.Add(item);
            return window;
        }

        public static Application AddStyle(this Application application, IStyle item)
        {
            application.Styles.Add(item);
            return application;
        }
    }
}
