using FiguresLibrary.Static;
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
   public class RectangleModel : AbstractFigure
    {
        private readonly string figureName = "Rectangle";
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

            Rectangle rectangle = new()
            {
                Width = width,
                Height = height,
                Fill = mySolidColorBrush,
            };

            _canvasElement = rectangle;
        }

        public override string DisplayName 
        { 
            get 
            { 
                return figureName; 
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
