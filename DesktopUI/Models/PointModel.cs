using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopUI.Models
{
    [Serializable]
    public struct PointModel
    {
        public PointModel(double posX, double posY)
        {
            X = posX;
            Y = posY;
        }

        public double X { get; set; }
        public double Y { get; set; }

        public static PointModel GetRandomPoint(double maxX, double maxY)
        {
            Random random = new();

            double posX = random.NextDouble() * maxX;
            double posY = random.NextDouble() * maxY;

            return new PointModel(posX, posY);
        }
    }
}
