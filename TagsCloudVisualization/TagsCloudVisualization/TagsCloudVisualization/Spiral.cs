using System;
using System.Drawing;

namespace TagsCloudVisualization
{
    class Spiral
    {
        private readonly Point _spiralCenter;
        private int _currentIteration;
        private const double StartRadius = 0.01;
        private const double StartAngle = 50;

        public Spiral(Point spiralCenter)
        {
            _spiralCenter = spiralCenter;
            _currentIteration = 1;
        }

        public Point GenerateNextPoint()
        {
            var radius = StartRadius * _currentIteration;
            var angle = StartAngle * _currentIteration;
            var x = (int)(radius * Math.Cos(angle));
            var y = (int)(radius * Math.Sin(angle));
            _currentIteration++;
            return new Point(_spiralCenter.X + x, _spiralCenter.Y + y);
        }
    }
}
