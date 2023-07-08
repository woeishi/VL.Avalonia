using Avalonia;
using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VL.Avalonia
{
    public static class VLAppBuilder
    {
        public static AppBuilder BuildVLApp()
        => AppBuilder.Configure<VLApplication>()
        .UsePlatformDetect()
        .LogToTrace();
    }
}
