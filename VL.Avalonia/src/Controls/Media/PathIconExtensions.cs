using Avalonia.Controls;
using Avalonia.Media;

namespace VL.Avalonia.Controls.Media
{
    public static class PathIconExtensions
    {
        /// <inheritdoc cref="PathIcon.Data"/>
        public static void SetData(this PathIcon input, Geometry geometry)
        {
            if (input is PathIcon pathIcon)
                pathIcon.Data = geometry;
        }

        /// <inheritdoc cref="PathIcon.Data"/>
        public static Geometry? Data(this PathIcon input) => input?.Data;
    }
}

