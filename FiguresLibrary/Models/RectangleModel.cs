using UiLibrary.Static;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using UiLibrary.Resources;
using Newtonsoft.Json;

namespace UiLibrary.Models
{
    [Serializable]
    public class RectangleModel : AbstractFigure
    {
        public RectangleModel()
        {
            CreateRectangleShape();
        }

        private void CreateRectangleShape()
        {
            Rectangle rectangle = new Rectangle()
            {
                Height = Height,
                Width = Width
            };

            _canvasElement = rectangle;

            ApplySize();
            ApplyColor(ColorRgbValues);
        }

        private void CreateRectangularShape(double height, double width, (byte R, byte G, byte B) colorRgb)
        {
            SolidColorBrush brush = new SolidColorBrush
            {
                Color = Color.FromRgb(colorRgb.R, colorRgb.G, colorRgb.B)
            };

            Rectangle rectangle = new Rectangle()
            {
                Height = height,
                Width = width,

                Fill = brush
            };

            _canvasElement = rectangle;
        }

        private double _width;
        public double Width
        {
            get
            {
                if (_width == 0)
                {
                    _width = RandomValuesProvider.GetRandomSize();
                }

                return _width;
            }
            set
            {
                _width = value;

                ApplySize();
            }
        }

        private double _height;
        public double Height
        {
            get
            {
                if (_width == 0)
                {
                    _height = RandomValuesProvider.GetRandomSize();
                }

                return _height;
            }
            set
            {
                _height = value;

                ApplySize();
            }
        }

        protected override void ApplyColor((byte R, byte G, byte B) colorRgbValues)
        {
            Rectangle rectangle = (Rectangle)_canvasElement;

            SolidColorBrush brush = new SolidColorBrush()
            {
                Color = System.Windows.Media.Color.FromRgb(ColorRgbValues.R, ColorRgbValues.G, ColorRgbValues.B)
            };

            rectangle.Fill = brush;

            _canvasElement = rectangle;
        }

        private void ApplySize()
        {
            Rectangle rectangle = (Rectangle)_canvasElement;

            rectangle.Width = Width;
            rectangle.Height = Height;

            _canvasElement = rectangle;
        }

        [JsonIgnore]
        public override UIElement CanvasElement
        {
            get
            {
                if (_canvasElement != null)
                {
                    return _canvasElement;
                }

                CreateRectangularShape(Height, Width, ColorRgbValues);

                return _canvasElement;
            }
        }

        private string _displayName;
        public override string DisplayName
        {
            get
            {
                if (string.IsNullOrEmpty(_displayName))
                {
                    _displayName = GlobalStrings.RectangleDisplayName;
                }
                return _displayName;
            }
            set
            {
                _displayName = value;
            }
        }

        public override double MaxHeight
        {
            get
            {
                return Height;
            }
        }

        public override double MaxWidth
        {
            get
            {
                return Width;
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
    }
}
