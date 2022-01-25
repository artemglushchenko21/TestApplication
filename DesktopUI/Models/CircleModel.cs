using DesktopUI.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using UiLibrary.Static;

namespace DesktopUI.Models
{
    [Serializable]
    public class CircleModel : AbstractFigure
    {
        public CircleModel()
        {
            CreateCircleShape();
        }

        private void CreateCircleShape()
        {
            Ellipse ellipse = new();

            _canvasElement = ellipse;

            ApllyRadius();
            ApplyColor(ColorRgbValues);
        }

        private double _radius;
        public double Radius 
        {
            get
            {
                if(_radius == 0)
                {
                    _radius = RandomValuesProvider.GetRandomSize();
                }
                return _radius;
            }
            set
            {
                _radius = value;

                ApllyRadius();
            }
        } 

        private void ApllyRadius()
        {
            Ellipse ellipse = (Ellipse)_canvasElement;

            ellipse.Width = Radius;
            ellipse.Height = ellipse.Width;

            _canvasElement = ellipse;
        }

        private void CreateCircularShape(double radius, (byte R, byte G, byte B) colorRgb)
        {
            Ellipse ellipse = new();

            SolidColorBrush mySolidColorBrush = new()
            {
                Color = System.Windows.Media.Color.FromRgb(colorRgb.R, colorRgb.G, colorRgb.B)
            };

            ellipse.Fill = mySolidColorBrush;


            ellipse.Width = radius;
            ellipse.Height = ellipse.Width;

            _canvasElement = ellipse;
        }

        protected override void ApplyColor((byte R, byte G, byte B) colorRgbValues)
        {
            Ellipse ellipse = (Ellipse)_canvasElement;

            SolidColorBrush brush = new()
            {
                Color = System.Windows.Media.Color.FromRgb(ColorRgbValues.R, ColorRgbValues.G, ColorRgbValues.B)
            };

            ellipse.Fill = brush;

            _canvasElement = ellipse;
        }

        public override UIElement CanvasElement
        {
            get
            {
                if (_canvasElement != null)
                {
                    return _canvasElement;
                }

                CreateCircularShape(Radius, ColorRgbValues);

                return _canvasElement;
            }
        }

        public override double MaxHeight
        {
            get
            {
                return Radius;
            }
        }

        public override double MaxWidth
        {
            get
            {
                return Radius;
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
                    _displayName = GlobalStrings.CircleDisplayName;              
                }
                return _displayName;
            }
            set
            {
                _displayName = value;
            }
        }
    }
}
