using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using FiguresLibrary.Static;

namespace DesktopUI.Models
{
    public class CircleModel : AbstractFigure
    {
        private readonly string figureName = "Circle";
        private readonly double radius = RandomValuesProvider.GetRandomSize();

        public CircleModel()
        {
            CreateCircleShape();
        }

        private void CreateCircleShape()
        {
            Ellipse ellipse = new();

            SolidColorBrush mySolidColorBrush = new()
            {
                Color = RandomValuesProvider.GetRandomColor()
            };

            ellipse.Fill = mySolidColorBrush;

            ellipse.Width = radius;
            ellipse.Height = radius;

            _canvasElement = ellipse;
        }

        public override double MaxHeight
        {
            get
            {
                return radius;
            }
        }

        public override double MaxWidth
        {
            get
            {
                return radius;
            }
        }

        public override double VelocityX
        {
            get
            {
                return _velocityX;
            }
        }

        public override double VelocityY
        {
            get
            {
                return _velocityY;
            }
        }

        public override PointModel CurrentPosition
        {
            get
            {
                return _currentPosition;
            }

            set
            {
                _currentPosition = value;
            }
        }

        public override string DisplayName
        {
            get
            {
                return figureName;
            }
        }
    }
}
