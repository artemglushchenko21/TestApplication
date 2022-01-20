using UiLibrary.Static;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DesktopUI.Models
{
    public abstract class AbstractFigure
    {
        private protected double _velocityX = RandomValuesProvider.GetRandomVelocity();
        private protected double _velocityY = RandomValuesProvider.GetRandomVelocity();
        private protected PointModel _currentPosition;
        private protected UIElement _canvasElement;

        public virtual UIElement CanvasElement
        {
            get 
            { 
                return _canvasElement; 
            }
        }

        public abstract string DisplayName { get; }

        public abstract double MaxHeight { get; }

        public abstract double MaxWidth { get; }

        public abstract double VelocityX { get; }

        public abstract double VelocityY { get; }

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
