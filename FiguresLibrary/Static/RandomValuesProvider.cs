using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace FiguresLibrary.Static
{
    public static class RandomValuesProvider
    {
        private static readonly double maxFigureSize = 100;
        private static readonly double moveIncrementStep = 3;

        private static readonly int lowerRgbForPastelShades = 60;
        private static readonly int upperRgbForPastelShades = 200;

        public static Color GetRandomColor()
        {
            Random random = new Random();

            byte r = (byte)random.Next(lowerRgbForPastelShades, upperRgbForPastelShades);
            byte g = (byte)random.Next(lowerRgbForPastelShades, upperRgbForPastelShades);
            byte b = (byte)random.Next(lowerRgbForPastelShades, upperRgbForPastelShades);

            return Color.FromRgb(r, g, b);
        }

        public static double GetRandomVelocity()
        {
            Random random = new Random();
            return random.NextDouble() * moveIncrementStep;
        }

        public static double GetRandomSize()
        {
            Random random = new Random();
            return random.NextDouble() * maxFigureSize;
        }

        public static PointCollection GetRandomPointsForTriangle()
        {
            PointCollection pointCollection = new PointCollection
            {
                new Point(0, 0),
                new Point(GetRandomSize(), GetRandomSize()),
                new Point(GetRandomSize(), GetRandomSize())
            };

            return pointCollection;
        }
    }
}
