using System;
using System.Drawing;

namespace TagsCloudVisualization
{
    class Spiral
    {
        private readonly Point SpiralCenter;
        private int CurrentIteration;
        private const double StartRadius = 0.005;
        private const double StartAngle = 20;

        public Spiral(Point spiralCenter)
        {
            SpiralCenter = spiralCenter;
            CurrentIteration = 1;
        }

        public Point GenerateNextPoint()
        {
            var radius = StartRadius * CurrentIteration;
            var angle = StartAngle * CurrentIteration;
            var x = (int)(radius * Math.Cos(angle));
            var y = (int)(radius * Math.Sin(angle));
            CurrentIteration++;
            return new Point(SpiralCenter.X + x, SpiralCenter.Y + y);
        }
    }
}
