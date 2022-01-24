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
    public class RectangleModel : AbstractFigure
    {
        private readonly double width = RandomValuesProvider.GetRandomSize();
        private readonly double height = RandomValuesProvider.GetRandomSize();

        public RectangleModel()
        {
            CreateRectangleShape();
        }

        private void CreateRectangleShape()
        {
            SolidColorBrush mySolidColorBrush = new();
            mySolidColorBrush.Color = RandomValuesProvider.GetRandomColor();

            colorRgbValues = (mySolidColorBrush.Color.R, mySolidColorBrush.Color.G, mySolidColorBrush.Color.B);

            Rectangle rectangle = new()
            {
                Height = height,
                Width = width,
                
                Fill = mySolidColorBrush
            };

            _canvasElement = rectangle;
        }

        private void CreateRectangularShape(double height, double width, (byte R, byte G, byte B) colorRgb)
        {
            SolidColorBrush mySolidColorBrush = new();
            mySolidColorBrush.Color = System.Windows.Media.Color.FromRgb(colorRgb.R, colorRgb.G, colorRgb.B);

            Rectangle rectangle = new()
            {
                Height = height,
                Width = width,

                Fill = mySolidColorBrush
            };

            _canvasElement = rectangle;
        }

        public override UIElement CanvasElement
        {
            get
            {
                if(_canvasElement != null)
                {
                    return _canvasElement;
                }

                CreateRectangularShape(height, width, colorRgbValues);
              //  CreateRectangleShape();

                return _canvasElement;
            }
        }

        public override string DisplayName 
        { 
            get 
            { 
                return GlobalStrings.RectangleDisplayName; 
            } 
        }

        public override double MaxHeight
        {
            get
            {
                return height;
            }
        }

        public override double MaxWidth
        {
            get
            {
                return width;
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
    }
}
