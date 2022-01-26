using UiLibrary.Static;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Drawing;
using System.Xml.Serialization;
using System.Windows.Media;

namespace UiLibrary.Models
{
    [XmlInclude(typeof(CircleModel))]
    [XmlInclude(typeof(RectangleModel))]
    [XmlInclude(typeof(TriangleModel))]
    [Serializable]
    public abstract class AbstractFigure
    {
        private protected double _velocityX = RandomValuesProvider.GetRandomVelocity();
        private protected double _velocityY = RandomValuesProvider.GetRandomVelocity();
        private protected PointModel _currentPosition;
        protected abstract void ApplyColor((byte R, byte G, byte B) colorRgbValues);

        protected (byte R, byte G, byte B) _colorRgbValues;
        public virtual (byte R, byte G, byte B) ColorRgbValues
        {
            get
            {
                if (_colorRgbValues.R == 0 && _colorRgbValues.G == 0 && _colorRgbValues.B == 0)
                {
                    var color = RandomValuesProvider.GetRandomColor();

                    _colorRgbValues = (color.R, color.G, color.B);
                }
                return _colorRgbValues;
            }
            set
            {
                _colorRgbValues.R = value.R;
                _colorRgbValues.G = value.G;
                _colorRgbValues.B = value.B;

                ApplyColor(_colorRgbValues);
            }
        }

        [NonSerialized]
        private protected UIElement _canvasElement;

        public virtual UIElement CanvasElement
        {
            get
            {
                return _canvasElement;
            }
        }

        public abstract string DisplayName { get; set; }

        public abstract double MaxHeight { get; }

        public abstract double MaxWidth { get; }

        public abstract double VelocityX { get; set; }

        public abstract double VelocityY { get; set; }

        public bool IsStoped { get; set; }

        public abstract PointModel CurrentPosition { get; set; }

        public virtual void Move(PointModel maxPoint)
        {
            if (IsStoped)
            {
                return;
            }

            if (_currentPosition.X < 0)
            {
                _velocityX *= -1;
            }

            if (_currentPosition.X > maxPoint.X - MaxWidth)
            {
                _velocityX *= -1;
            }

            if (_currentPosition.Y < 0)
            {
                _velocityY *= -1;
            }

            if (_currentPosition.Y > maxPoint.Y - MaxHeight)
            {
                _velocityY *= -1;
            }

            _currentPosition.X += _velocityX;
            _currentPosition.Y += _velocityY;
        }
    }
}
