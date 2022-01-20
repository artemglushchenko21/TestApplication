using UiLibrary.Static;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DesktopUI.Models
{

   public class TriangleModel : AbstractFigure
    {
        private readonly string figureName = "Triangle";
        private double maxHeight;
        private double maxWidth;

        public TriangleModel()
        {
            CreateTriangleShape();
        }

        private void CreateTriangleShape()
        {
            PointCollection pointCollection = RandomValuesProvider.GetRandomPointsForTriangle();

            SetTriangleMaxWidth(pointCollection);
            SetTriangleMaxHeight(pointCollection);

            Polygon triangle = new();
            triangle.Points = pointCollection;

            SolidColorBrush mySolidColorBrush = new()
            {
                Color = RandomValuesProvider.GetRandomColor()
            };

            triangle.Fill = mySolidColorBrush;

            _canvasElement = triangle;
        }

        public override double MaxHeight
        {
            get
            {
                return maxHeight;
            }
        }

        public override double MaxWidth
        {
            get
            {
                return maxWidth;
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

        private void SetTriangleMaxHeight(PointCollection pointCollection)
        {
            double yMin = pointCollection.Min(point => point.Y);
            double yMax = pointCollection.Max(point => point.Y);

            maxHeight = yMax - yMin;
        }

        private void SetTriangleMaxWidth(PointCollection pointCollection)
        {
          double xMin = pointCollection.Min(point => point.X);
          double xMax = pointCollection.Max(point => point.X);

          maxWidth = xMax - xMin;
        }
    }
}
