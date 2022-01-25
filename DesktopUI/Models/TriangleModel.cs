using UiLibrary.Static;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using DesktopUI.Resources;

namespace DesktopUI.Models
{
    [Serializable]
    public class TriangleModel : AbstractFigure
    {
        private double maxHeight;
        private double maxWidth;

        public TriangleModel()
        {
            CreateTriangleShape();
        }

        private void CreateTriangleShape()
        {
            Polygon triangle = new();
            _canvasElement = triangle;

            ApplyTrianglePoints();
            ApplyColor(ColorRgbValues);
        }

        private void ApplyTrianglePoints()
        {
            PointCollection pointCollection = new();
            pointCollection.Add(TrianglePoints.pointA);
            pointCollection.Add(TrianglePoints.pointB);
            pointCollection.Add(TrianglePoints.pointC);

            Polygon triangle = (Polygon)_canvasElement;
            triangle.Points = pointCollection;

            SetTriangleMaxWidth(pointCollection);
            SetTriangleMaxHeight(pointCollection);
        }

        private void CreateTriangleShape((Point pointA, Point pointB, Point pointC) trianglePoints)
        {
            Polygon triangle = new();
            _canvasElement = triangle;

            TrianglePoints = trianglePoints;

            ApplyTrianglePoints();

            ApplyColor(ColorRgbValues);
        }

        private (Point pointA, Point pointB, Point pointC) _trianglePoints;
        public (Point pointA, Point pointB, Point pointC) TrianglePoints
        {
            get
            {
                if (_trianglePoints.pointA.X == 0 && _trianglePoints.pointA.Y == 0
                    && _trianglePoints.pointB.X == 0 && _trianglePoints.pointB.Y == 0
                   && _trianglePoints.pointC.X == 0 && _trianglePoints.pointC.Y == 0)
                {
                    PointCollection pointCollection = RandomValuesProvider.GetRandomPointsForTriangle();
                    _trianglePoints.pointA = pointCollection[0];
                    _trianglePoints.pointB = pointCollection[1];
                    _trianglePoints.pointC = pointCollection[2];
                }

                return _trianglePoints;
            }
            set
            {
                _trianglePoints.pointA = value.pointA;
                _trianglePoints.pointB = value.pointB;
                _trianglePoints.pointC = value.pointC;

                ApplyTrianglePoints();
            }
        }

        protected override void ApplyColor((byte R, byte G, byte B) colorRgbValues)
        {
            Polygon triangle = (Polygon)_canvasElement;

            SolidColorBrush brush = new()
            {
                Color = System.Windows.Media.Color.FromRgb(ColorRgbValues.R, ColorRgbValues.G, ColorRgbValues.B)
            };

            triangle.Fill = brush;

            _canvasElement = triangle;
        }

        public override UIElement CanvasElement
        {
            get
            {
                if (_canvasElement != null)
                {
                    return _canvasElement;
                }

                CreateTriangleShape(TrianglePoints);

                return _canvasElement;
            }
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
            set
            {
                _velocityX = value;
            }
        }

        public override double VelocityY
        {
            get
            {
                return _velocityY;
            }
            set
            {
                _velocityY = value;
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

        private string _displayName;
        public override string DisplayName
        {
            get
            {
                if (string.IsNullOrEmpty(_displayName))
                {
                    _displayName = GlobalStrings.TriangleDisplayName;
                }
                return _displayName;
            }
            set
            {
                _displayName = value;
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
